#region LICENSE
/* 
 * Copyright (C) 2007 Tim Taubert (twenty-three@users.berlios.de)
 * 
 * This file is part of video4linux-net.
 *
 * Video4linux-net is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * Video4linux-net is distributed in the hope that it will be useful,
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
using System.Text;
using System.Threading;

namespace Video4Linux
{
    /// <summary>
    /// Represent a Video4Linux hardware device.
    /// </summary>
	public class V4LDevice
	{
		#region Private Fields
		
		private int deviceHandle;
		private V4LIOControl ioControl;
		
		private APIv2.v4l2_capability? _deviceCapabilities;
		private uint bufferCount = 4;
		
		private List<V4LAudioInput> audioInputs;
		private List<V4LAudioOutput> audioOutputs;
		private List<V4LBuffer> buffers = new List<V4LBuffer>();
		private List<V4LFormat> formats;
		private List<V4LInput> inputs;
		private List<V4LOutput> outputs;
		private List<V4LStandard> standards;
		private List<V4LTuner> tuners;
		
		#endregion Private Fields
		
		public delegate void BufferFilledEventHandler(V4LDevice sender, V4LBuffer buffer);
		public event BufferFilledEventHandler BufferFilled;

		#region Constructors and Destructors
		
        /// <summary>
        /// Creates a Video4Linux device.
        /// </summary>
        /// <param name="width">Width of surface</param>
        /// <param name="height">Height of surface</param>
		public V4LDevice()
			: this("/dev/video0")
		{}
		
        /// <summary>
        /// Creates a Video4Linux device.
        /// </summary>
        /// <param name="path">Path to the device</param>
		public V4LDevice(string path)
		{
			deviceHandle = Syscall.open(path, OpenFlags.O_RDWR);
			ioControl = new V4LIOControl(deviceHandle);
		}
		
        /// <summary>
        /// Destroys a Video4Linux device.
        /// </summary>
		~V4LDevice()
		{
			Syscall.close(deviceHandle);
		}
	
		#endregion Constructors and Destructors
		
		private void fetchAudioInputs()
		{
			audioInputs = new List<V4LAudioInput>();
			APIv2.v4l2_audio cur = new APIv2.v4l2_audio();
			
			cur.index = 0;
			while (ioControl.EnumerateAudioInputs(ref cur) == 0)
			{
				audioInputs.Add(new V4LAudioInput(cur));
				cur.index++;
			}
		}
		
		private void fetchAudioOutputs()
		{
			audioOutputs = new List<V4LAudioOutput>();
			APIv2.v4l2_audioout cur = new APIv2.v4l2_audioout();
			
			cur.index = 0;
			while (ioControl.EnumerateAudioOutputs(ref cur) == 0)
			{
				audioOutputs.Add(new V4LAudioOutput(cur));
				cur.index++;
			}
		}
		
		private void fetchCapabilites()
		{
			APIv2.v4l2_capability tmpCap = new APIv2.v4l2_capability();
			if (ioControl.QueryDeviceCapabilities(ref tmpCap) < 0)
				throw new Exception("VIDIOC_QUERYCAP");
			
			_deviceCapabilities = tmpCap;
		}
		
		private void fetchFormats()
		{
			formats = new List<V4LFormat>();
			APIv2.v4l2_fmtdesc cur = new APIv2.v4l2_fmtdesc();
			
			cur.index = 0;
			while (ioControl.EnumerateFormats(ref cur) == 0)
			{
				formats.Add(new V4LFormat(cur));
				cur.index++;
			}
		}
		
		private void fetchInputs()
		{
			inputs = new List<V4LInput>();
			APIv2.v4l2_input cur = new APIv2.v4l2_input();
			
			cur.index = 0;
			while (ioControl.EnumerateInputs(ref cur) == 0)
			{
				inputs.Add(new V4LInput(this, cur));
				cur.index++;
			}
		}
		
		private void fetchOutputs()
		{
			outputs = new List<V4LOutput>();
			APIv2.v4l2_output cur = new APIv2.v4l2_output();
			
			cur.index = 0;
			while (ioControl.EnumerateOutputs(ref cur) == 0)
			{
				outputs.Add(new V4LOutput(cur));
				cur.index++;
			}
		}
		
		private void fetchStandards()
		{
			standards = new List<V4LStandard>();
			APIv2.v4l2_standard cur = new APIv2.v4l2_standard();
			
			cur.index = 0;
			while (ioControl.EnumerateStandards(ref cur) == 0)
			{
				standards.Add(new V4LStandard(cur));
				cur.index++;
			}
		}
		
		private void fetchTuners()
		{
			tuners = new List<V4LTuner>();
			APIv2.v4l2_tuner cur = new APIv2.v4l2_tuner();
			
			cur.index = 0;
			while (ioControl.GetTuner(ref cur) == 0)
			{
				tuners.Add(new V4LTuner(cur));
				cur.index++;
			}
		}
		
		private void enqueueAllBuffers()
		{
			foreach (V4LBuffer buf in buffers)
				buf.Enqueue();
		}
		
		private void threadTest()
		{
			APIv2.v4l2_buffer buf = new APIv2.v4l2_buffer();
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
		
		private void requestBuffers()
		{
			APIv2.v4l2_requestbuffers req = new Video4Linux.APIv2.v4l2_requestbuffers();
			req.count = bufferCount;
			req.type = APIv2.v4l2_buf_type.VideoCapture;
			req.memory = APIv2.v4l2_memory.MemoryMapping;
			if (ioControl.RequestBuffers(ref req) < 0)
				throw new Exception("VIDIOC_REQBUFS");
				
			if (req.count < bufferCount)
				throw new Exception("VIDIOC_REQBUFS [not enough buffers]");
			
			fetchBuffers(req);
		}
		
		private void fetchBuffers(APIv2.v4l2_requestbuffers req)
		{
			for (uint i=0; i<req.count; i++)
			{
				APIv2.v4l2_buffer buffer = new APIv2.v4l2_buffer();
				buffer.index = i;
				buffer.type = req.type;
				buffer.memory = req.memory;
				if (ioControl.QueryBuffer(ref buffer) < 0)
					throw new Exception("VIDIOC_QUERYBUF");
				
				buffers.Add(new V4LBuffer(this, buffer));
			}
		}
		
		/**************************************************/

		public void StartStreaming()
		{
			// request the streaming buffers
			if (buffers == null || buffers.Count != bufferCount)
				requestBuffers();
			
			// make sure that all buffers are in the incoming queue
			enqueueAllBuffers();
			
			APIv2.v4l2_buf_type type = APIv2.v4l2_buf_type.VideoCapture;
			if (ioControl.StreamingOn(ref type) < 0)
				throw new Exception("VIDIOC_STREAMON");
			
			Thread t = new Thread(new ThreadStart(threadTest));
			t.Priority = ThreadPriority.Lowest;
			t.Start();
		}

		public void StopStreaming()
		{
			APIv2.v4l2_buf_type type = APIv2.v4l2_buf_type.VideoCapture;
			if (ioControl.StreamingOff(ref type) < 0)
				throw new Exception("VIDIOC_STREAMOFF");
		}
		
		/**************************************************/
		
		private APIv2.v4l2_capability deviceCapabilities
		{
			get
			{
				if (!_deviceCapabilities.HasValue)
					fetchCapabilites();
				
				return (APIv2.v4l2_capability)_deviceCapabilities;
			}
		}
		
		/**************************************************/
		
		// TODO: remove
		public int DeviceHandle
		{
			get { return deviceHandle; }
		}
		
		public string Name
		{
			get { return Encoding.ASCII.GetString(deviceCapabilities.card); }
		}
		
		public string Driver
		{
			get { return Encoding.ASCII.GetString(deviceCapabilities.driver); }
		}
		
		public string BusInfo
		{
			get { return Encoding.ASCII.GetString(deviceCapabilities.bus_info); }
		}
		
		public uint Version
		{
			get { return deviceCapabilities.version; }
		}
		
		public V4LAudioInput AudioInput
		{
			get
			{
				APIv2.v4l2_audio input = new APIv2.v4l2_audio();
				if (ioControl.GetAudioInput(ref input) < 0)
					throw new Exception("VIDIOC_G_AUDIO");
				
				return AudioInputs[(int)input.index];
			}
			set
			{
				APIv2.v4l2_audio input = value.ToStruct();
				if (ioControl.SetAudioInput(ref input) < 0)
					throw new Exception("VIDIOC_S_AUDIO");
			}
		}
		
		public V4LAudioOutput AudioOutput
		{
			get
			{
				APIv2.v4l2_audioout output = new APIv2.v4l2_audioout();
				if (ioControl.GetAudioOutput(ref output) < 0)
					throw new Exception("VIDIOC_G_AUDOUT");
				
				return AudioOutputs[(int)output.index];
			}
			set
			{
				APIv2.v4l2_audioout output = value.ToStruct();
				if (ioControl.SetAudioOutput(ref output) < 0)
					throw new Exception("VIDIOC_S_AUDOUT");
			}
		}
		
		public uint BufferCount
		{
			get { return bufferCount; }
			// TODO: must be immutable while capturing
			set { bufferCount = value; }
		}
		
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
		
		public APIv2.v4l2_std_id Standard
		{
			get
			{
				APIv2.v4l2_std_id std = 0;
				if (ioControl.GetStandard(ref std) < 0)
					throw new Exception("VIDIOC_G_STD");
				
				return std;
			}
			set
			{
				if (ioControl.GetStandard(ref value) < 0)
					throw new Exception("VIDIOC_S_STD");
			}
		}
		
		public List<V4LAudioInput> AudioInputs
		{
			get
			{
				if (audioInputs == null)
					fetchAudioInputs();
				
				return audioInputs;
			}
		}
		
		public List<V4LAudioOutput> AudioOutputs
		{
			get
			{
				if (audioOutputs == null)
					fetchAudioOutputs();
				
				return audioOutputs;
			}
		}
		
		public List<V4LBuffer> Buffers
		{
			get { return buffers; }
		}
		
		public List<V4LFormat> Formats
		{
			get
			{
				if (formats == null)
					fetchFormats();
				
				return formats;
			}
		}
		
		public List<V4LInput> Inputs
		{
			get
			{
				if (inputs == null)
					fetchInputs();
				
				return inputs;
			}
		}
		
		public List<V4LOutput> Outputs
		{
			get
			{
				if (outputs == null)
					fetchOutputs();
				
				return outputs;
			}
		}
		
		
		public List<V4LStandard> Standards
		{
			get
			{
				if (standards == null)
					fetchStandards();
				
				return standards;
			}
		}
		
		public List<V4LTuner> Tuners
		{
			get
			{
				if (tuners == null)
					fetchTuners();
				
				return tuners;
			}
		}
		
		public V4LIOControl IoControl
		{
			get { return ioControl; }
		}
	}
}