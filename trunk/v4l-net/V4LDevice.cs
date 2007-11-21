#region LICENSE
/* 
 * Copyright (C) 2007 Tim Taubert (twenty-three@users.berlios.de)
 * 
 * This file is part of Video4Linux.Net.
 *
 * Video4Linux.Net is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * Video4Linux.Net is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */
#endregion LICENSE

using Mono.Unix.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Video4Linux.APIv2;

namespace Video4Linux
{
	/// <summary>
	/// Represents a Video4Linux hardware device.
	/// </summary>
	public class V4LDevice
	{
		#region Private Fields
		
		private int deviceHandle;
		private V4LIOControl ioControl;
		
		private v4l2_capability device;
		private uint bufferCount = 4;
		private V4LFormatContainer formatContainer;
		
		private ManagedList<V4LAudioInput> audioInputs;
		private ManagedList<V4LAudioOutput> audioOutputs;
		private List<V4LBuffer> buffers = new List<V4LBuffer>();
		private List<V4LDeviceCapability> capabilities;
		private List<V4LFormat> formats;
		private ManagedList<V4LInput> inputs;
		private ManagedList<V4LOutput> outputs;
		private ManagedList<V4LStandard> standards;
		private ManagedList<V4LTuner> tuners;
		
		private Thread streamingThread;
		
		#endregion Private Fields
		
		public delegate void BufferFilledEventHandler(V4LDevice sender, V4LBuffer buffer);
		/// <summary>
		/// Gets fired when a buffer was filled by the driver (when a frame was captured).
		/// </summary>
		public event BufferFilledEventHandler BufferFilled;
		
		#region Constructors and Destructors
		
		/// <summary>
		/// Creates a Video4Linux device.
		/// </summary>
		/// <param name="path">Path to the device.</param>
		public V4LDevice(string path)
		{
			deviceHandle = Syscall.open(path, OpenFlags.O_RDWR);
			ioControl = new V4LIOControl(deviceHandle);
			fetchDevice();
		}
		
		/// <summary>
		/// Destroys a Video4Linux device.
		/// </summary>
		~V4LDevice()
		{
			Syscall.close(deviceHandle);
		}
		
		#endregion Constructors and Destructors
		
		#region Private Methods
		
		/// <summary>
		/// Collects all available audio inputs from the device.
		/// </summary>
		private void fetchAudioInputs()
		{
			audioInputs = new ManagedList<V4LAudioInput>();
			v4l2_audio cur = new v4l2_audio();
			
			cur.index = 0;
			while (ioControl.EnumerateAudioInputs(ref cur) == 0)
			{
				audioInputs.Add(new V4LAudioInput(this, cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Collects all available audio outputs from the device.
		/// </summary>
		private void fetchAudioOutputs()
		{
			audioOutputs = new ManagedList<V4LAudioOutput>();
			v4l2_audioout cur = new v4l2_audioout();
			
			cur.index = 0;
			while (ioControl.EnumerateAudioOutputs(ref cur) == 0)
			{
				audioOutputs.Add(new V4LAudioOutput(cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Queries the device for its capabilites.
		/// </summary>
		private void fetchDevice()
		{
			device = new v4l2_capability();
			if (ioControl.QueryDeviceCapabilities(ref device) < 0)
				throw new Exception("VIDIOC_QUERYCAP");
		}
		
		/// <summary>
		/// Collects all available image formats from the device.
		/// </summary>
		private void fetchFormats()
		{
			formats = new List<V4LFormat>();
			v4l2_fmtdesc cur = new v4l2_fmtdesc();
			
			cur.index = 0;
			while (ioControl.EnumerateFormats(ref cur) == 0)
			{
				formats.Add(new V4LFormat(cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Collects all available video inputs from the device.
		/// </summary>
		private void fetchInputs()
		{
			inputs = new ManagedList<V4LInput>();
			v4l2_input cur = new v4l2_input();
			
			cur.index = 0;
			while (ioControl.EnumerateInputs(ref cur) == 0)
			{
				inputs.Add(new V4LInput(this, cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Collects all available video outputs from the device.
		/// </summary>
		private void fetchOutputs()
		{
			outputs = new ManagedList<V4LOutput>();
			v4l2_output cur = new v4l2_output();
			
			cur.index = 0;
			while (ioControl.EnumerateOutputs(ref cur) == 0)
			{
				outputs.Add(new V4LOutput(cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Collects all available TV standards from the device.
		/// </summary>
		private void fetchStandards()
		{
			standards = new ManagedList<V4LStandard>();
			v4l2_standard cur = new v4l2_standard();
			
			cur.index = 0;
			while (ioControl.EnumerateStandards(ref cur) == 0)
			{
				standards.Add(new V4LStandard(cur));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Collects all available tuners from the device.
		/// </summary>
		private void fetchTuners()
		{
			tuners = new ManagedList<V4LTuner>();
			v4l2_tuner cur = new v4l2_tuner();
			
			cur.index = 0;
			while (ioControl.GetTuner(ref cur) == 0)
			{
				tuners.Add(new V4LTuner(this, cur.index, cur.type));
				cur.index++;
			}
		}
		
		/// <summary>
		/// Enqueues all requested buffers.
		/// </summary>
		private void enqueueAllBuffers()
		{
			foreach (V4LBuffer buf in buffers)
				buf.Enqueue();
		}
		
		/// <summary>
		/// Tries to dequeue a buffer and fires the 'BufferFilled' event if the buffer was filled by the driver.
		/// </summary>
		private void captureFromBuffers()
		{
			v4l2_buffer buf = new v4l2_buffer();
			buf.type = Buffers[0].Type;
			buf.memory = Buffers[0].Memory;
			
			while (ioControl.DequeueBuffer(ref buf) == 0)
			{
				V4LBuffer dbuf = Buffers[(int)buf.index];
				
				// invoke the event
				if (BufferFilled != null)
					BufferFilled(this, dbuf);
				
				// re-enqueue the buffer
				dbuf.Enqueue();
			}
		}
		
		/// <summary>
		/// Requests a given number of buffers for mmap data transfer.
		/// </summary>
		private void requestBuffers()
		{
			v4l2_requestbuffers req = new v4l2_requestbuffers();
			req.count = bufferCount;
			req.type = v4l2_buf_type.VideoCapture;
			req.memory = v4l2_memory.MemoryMapping;
			if (ioControl.RequestBuffers(ref req) < 0)
				throw new Exception("VIDIOC_REQBUFS");
				
			if (req.count < bufferCount)
				throw new Exception("VIDIOC_REQBUFS [count]");
			
			fetchBuffers(req);
		}
		
		/// <summary>
		/// Queries the device for information about each requested buffer.
		/// </summary>
		/// <param name="req">Struct with information about the request buffers.</param>
		private void fetchBuffers(v4l2_requestbuffers req)
		{
			for (uint i=0; i<req.count; i++)
			{
				v4l2_buffer buffer = new v4l2_buffer();
				buffer.index = i;
				buffer.type = req.type;
				buffer.memory = req.memory;
				if (ioControl.QueryBuffer(ref buffer) < 0)
					throw new Exception("VIDIOC_QUERYBUF");
				
				buffers.Add(new V4LBuffer(this, buffer));
			}
		}
		
		private void fetchCapabilities()
		{
			capabilities = new List<V4LDeviceCapability>();
			
			foreach (object val in Enum.GetValues(typeof(V4LDeviceCapability)))
				if ((device.capabilities & (uint)val) != 0)
					capabilities.Add((V4LDeviceCapability)val);
		}
		
		/// <summary>
		/// Gets a V4LStandard out of the list of all supported standards.
		/// </summary>
		/// <param name="std">Id of the standard.</param>
		private V4LStandard getStandardById(v4l2_std_id std)
		{
			foreach (V4LStandard standard in Standards)
				if (standard.Id == std)
					return standard;
			
			throw new Exception("VIDIOC_G_STD [std not in list]");
		}
		
		#endregion Private Methods
		
		#region Public Methods
		
		/// <summary>
		/// Starts the streaming I/O.
		/// </summary>
		public void StartStreaming()
		{
			// request the streaming buffers if necessary
			if (buffers == null || buffers.Count != bufferCount)
				requestBuffers();
			
			// make sure that all buffers are in the incoming queue
			enqueueAllBuffers();
			
			v4l2_buf_type type = v4l2_buf_type.VideoCapture;
			if (ioControl.StreamingOn(ref type) < 0)
				throw new Exception("VIDIOC_STREAMON");
			
			streamingThread = new Thread(new ThreadStart(captureFromBuffers));
			streamingThread.Priority = ThreadPriority.Lowest;
			streamingThread.Start();
		}
		
		/// <summary>
		/// Stops the streaming I/O.
		/// </summary>
		public void StopStreaming()
		{
			// abort checking for captured frames
			streamingThread.Abort();
			streamingThread = null;
			
			// destroy the buffers
			buffers = new List<V4LBuffer>();
			
			v4l2_buf_type type = v4l2_buf_type.VideoCapture;
			if (ioControl.StreamingOff(ref type) < 0)
				throw new Exception("VIDIOC_STREAMOFF");
		}
		
		#endregion Public Methods
		
		#region Internal Properties
		
		/// <summary>
		/// Gets the file handle for the v4l device.
		/// </summary>
		/// <value>The file handle.</value>
		internal int DeviceHandle
		{
			get { return deviceHandle; }
		}
		
		internal V4LIOControl IoControl
		{
			get { return ioControl; }
		}
		
		#endregion Internal Properties
		
		#region Public Properties
		
		/// <summary>
		/// Gets the device's name.
		/// </summary>
		/// <value>The device's name.</value>
		public string Name
		{
			get { return device.card; }
		}
		
		/// <summary>
		/// Gets the device driver's name.
		/// </summary>
		/// <value>The driver's name.</value>
		public string Driver
		{
			get { return device.driver; }
		}
		
		/// <summary>
		/// Gets information about the bus the device is attached to.
		/// </summary>
		/// <value>The bus info string.</value>
		public string BusInfo
		{
			get { return device.bus_info; }
		}
		
		/// <summary>
		/// Gets information about the device's capabilities.
		/// </summary>
		/// <value>The capability bitmap.</value>
		public ReadOnlyCollection<V4LDeviceCapability> Capabilities
		{
			get
			{
				if (capabilities == null)
					fetchCapabilities();
				
				return capabilities.AsReadOnly();
			}
		}
		
		/// <summary>
		/// Gets the device driver's version.
		/// </summary>
		/// <value>The version string.</value>
		public uint Version
		{
			get { return device.version; }
		}
		
		/// <summary>
		/// Gets or sets the current audio input.
		/// </summary>
		/// <value>The audio input.</value>
		public V4LAudioInput AudioInput
		{
			get
			{
				v4l2_audio input = new v4l2_audio();
				if (ioControl.GetAudioInput(ref input) < 0)
					throw new Exception("VIDIOC_G_AUDIO");
				
				return AudioInputs[(int)input.index];
			}
			set
			{
				v4l2_audio input = value.ToStruct();
				if (ioControl.SetAudioInput(ref input) < 0)
					throw new Exception("VIDIOC_S_AUDIO");
			}
		}
		
		/// <summary>
		/// Gets or sets the current audio output.
		/// </summary>
		/// <value>The audio output.</value>
		public V4LAudioOutput AudioOutput
		{
			get
			{
				v4l2_audioout output = new v4l2_audioout();
				if (ioControl.GetAudioOutput(ref output) < 0)
					throw new Exception("VIDIOC_G_AUDOUT");
				
				return AudioOutputs[(int)output.index];
			}
			set
			{
				v4l2_audioout output = value.ToStruct();
				if (ioControl.SetAudioOutput(ref output) < 0)
					throw new Exception("VIDIOC_S_AUDOUT");
			}
		}
		
		/// <summary>
		/// Gets or sets the number of buffers to use for streaming I/O with mmap.
		/// </summary>
		/// <value>The number of buffers to use.</value>
		public uint BufferCount
		{
			get { return bufferCount; }
			// TODO: must be immutable while capturing
			set { bufferCount = value; }
		}
		
		/// <summary>
		/// Gets a container holding all video capture and output formats.
		/// </summary>
		/// <value>The format container.</value>
		public V4LFormatContainer Format
		{
			get
			{
				if (formatContainer == null)
					formatContainer = new V4LFormatContainer(this);
				
				return formatContainer;
			}
		}
		
		/// <summary>
		/// Gets or sets the current video input.
		/// </summary>
		/// <value>The video input.</value>
		public V4LInput Input
		{
			get
			{
				int idx = 0;
				if (ioControl.GetInput(ref idx) < 0)
					throw new Exception("VIDIOC_G_INPUT");
				
				return Inputs[idx];
			}
			set
			{
				int idx = (int)value.Index;
				if (ioControl.SetInput(ref idx) < 0)
					throw new Exception("VIDIOC_S_INPUT");
			}
		}
		
		/// <summary>
		/// Gets or sets the current video output.
		/// </summary>
		/// <value>The video output.</value>
		public V4LOutput Output
		{
			get
			{
				int idx = 0;
				if (ioControl.GetOutput(ref idx) < 0)
					throw new Exception("VIDIOC_G_OUTPUT");
				
				return Outputs[idx];
			}
			set
			{
				int idx = (int)value.Index;
				if (ioControl.SetOutput(ref idx) < 0)
					throw new Exception("VIDIOC_S_OUTPUT");
			}
		}
		
		/// <summary>
		/// Gets or sets the current TV standard.
		/// </summary>
		/// <value>The TV standard.</value>
		public V4LStandard Standard
		{
			get
			{
				v4l2_std_id std = 0;
				if (ioControl.GetStandard(ref std) < 0)
					throw new Exception("VIDIOC_G_STD");
				
				return getStandardById(std);
			}
			set
			{
				v4l2_std_id std = value.Id;
				if (ioControl.SetStandard(ref std) < 0)
					throw new Exception("VIDIOC_S_STD");
			}
		}
		
		/// <summary>
		/// Gets all available audio inputs.
		/// </summary>
		/// <value>A list of audio inputs.</value>
		public ManagedList<V4LAudioInput> AudioInputs
		{
			get
			{
				if (audioInputs == null)
					fetchAudioInputs();
				
				return audioInputs;
			}
		}
		
		/// <summary>
		/// Gets all available audio outputs.
		/// </summary>
		/// <value>A list of audio outputs.</value>
		public ManagedList<V4LAudioOutput> AudioOutputs
		{
			get
			{
				if (audioOutputs == null)
					fetchAudioOutputs();
				
				return audioOutputs;
			}
		}
		
		/// <summary>
		/// Gets the requested buffers for streaming I/O.
		/// </summary>
		/// <value>A list of buffers.</value>
		public List<V4LBuffer> Buffers
		{
			get { return buffers; }
		}
		
		/// <summary>
		/// Gets all available image formats.
		/// </summary>
		/// <value>A list of image formats.</value>
		public List<V4LFormat> Formats
		{
			get
			{
				if (formats == null)
					fetchFormats();
				
				return formats;
			}
		}
		
		/// <summary>
		/// Gets all available video inputs.
		/// </summary>
		/// <value>A list of video inputs.</value>
		public ManagedList<V4LInput> Inputs
		{
			get
			{
				if (inputs == null)
					fetchInputs();
				
				return inputs;
			}
		}
		
		/// <summary>
		/// Gets all available video outputs.
		/// </summary>
		/// <value>A list of video outputs.</value>
		public ManagedList<V4LOutput> Outputs
		{
			get
			{
				if (outputs == null)
					fetchOutputs();
				
				return outputs;
			}
		}
		
		/// <summary>
		/// Gets all avaible TV standards.
		/// </summary>
		/// <value>A list of standards.</value>
		public ManagedList<V4LStandard> Standards
		{
			get
			{
				if (standards == null)
					fetchStandards();
				
				return standards;
			}
		}
		
		/// <summary>
		/// Gets all available tuners.
		/// </summary>
		/// <value>A list of tuners.</value>
		public ManagedList<V4LTuner> Tuners
		{
			get
			{
				if (tuners == null)
					fetchTuners();
				
				return tuners;
			}
		}
		
		#endregion Public Properties
	}
}