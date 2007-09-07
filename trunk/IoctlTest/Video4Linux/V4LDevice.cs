using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Mono.Unix.Native;

/* Syscall.mmap(); */

namespace Video4Linux
{
	public class V4LDevice
	{
		[DllImport("libc")]
		private static extern int
			ioctl(int device,
			      APIv2.v4l2_operation_id request,
			      ref APIv2.v4l2_capability cap);
		[DllImport("libc")]
		private static extern int
			ioctl(int device,
			      APIv2.v4l2_operation_id request,
			      ref APIv2.v4l2_input input);
		[DllImport("libc")]
		private static extern int
			ioctl(int device,
			      APIv2.v4l2_operation_id request,
			      ref int misc);
		[DllImport("libc")]
		private static extern int
			ioctl(int device,
			      APIv2.v4l2_operation_id request,
			      ref ulong misc);
		[DllImport("libc")]
		private static extern int
			ioctl(int device,
			      APIv2.v4l2_operation_id request,
			      ref APIv2.v4l2_format fmt);
		[DllImport("libc")]
		private static extern int
			ioctl(int device,
			      APIv2.v4l2_operation_id request,
			      ref APIv2.v4l2_tuner tuner);
		
		/***************************************************/
		
		public int IOControl(APIv2.v4l2_operation_id request, ref APIv2.v4l2_format fmt)
		{
			return ioctl(deviceHandle, request, ref fmt);
		}
		
		/***************************************************/
		
		private int deviceHandle;
		private APIv2.v4l2_capability? _capabilities;
		
		private List<V4LInput> inputs;
		private List<V4LTuner> tuners;
		
		private V4LFormatMap formatMap;
		
		public V4LDevice()
			: this("/dev/video0")
		{}
		
		public V4LDevice(string devicePath)
		{
			deviceHandle = Syscall.open(devicePath, OpenFlags.O_RDONLY | OpenFlags.O_NONBLOCK);
		}
		
		~V4LDevice()
		{
			Syscall.close(deviceHandle);
		}
		
		private void fetchCapabilities()
		{
			APIv2.v4l2_capability tempCap =
				new APIv2.v4l2_capability();
			int res = ioctl(deviceHandle,
			      APIv2.v4l2_operation_id.QueryCapabilities,
			      ref tempCap);
			if (res < 0)
				throw new Exception("Could not query the device's capabilities.");
			_capabilities = tempCap;
		}
		
		private void fetchInputs()
		{
			inputs = new List<V4LInput>();
			APIv2.v4l2_input cur = new APIv2.v4l2_input();
			
			cur.index = 0;
			while (ioctl(deviceHandle, APIv2.v4l2_operation_id.EnumerateInputs, ref cur) == 0)
			{
				inputs.Add(new V4LInput(this, cur));
				cur.index++;
			}
		}
		
		private void fetchTuners()
		{
			tuners = new List<V4LTuner>();
			APIv2.v4l2_tuner cur = new APIv2.v4l2_tuner();
			
			// TODO: zero out the tuner field
			
			cur.index = 0;
			while (ioctl(deviceHandle, APIv2.v4l2_operation_id.GetTuner, ref cur) == 0)
			{
				tuners.Add(new V4LTuner(this, cur));
				cur.index++;
			}
		}
		
		/***************************************************/
		/* Private Getters and Setters                     */
		/***************************************************/
		
		private APIv2.v4l2_capability capabilities
		{
			get
			{
				if (!_capabilities.HasValue)
					fetchCapabilities();
				
				return (APIv2.v4l2_capability)_capabilities;
			}
		}
		
		/***************************************************/
		/* Public Getters and Setters                      */
		/***************************************************/
		
		public string Name
		{
			get { return System.Text.Encoding.ASCII.GetString(capabilities.card); }
		}
		
		public string Driver
		{
			get { return System.Text.Encoding.ASCII.GetString(capabilities.driver); }
		}
		
		public string BusInfo
		{
			get { return System.Text.Encoding.ASCII.GetString(capabilities.bus_info); }
		}
		
		public uint Version
		{
			get { return capabilities.version; }
		}
		
		public uint Capabilities
		{
			get { return capabilities.capabilities; }
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
		
		public List<V4LTuner> Tuners
		{
			get
			{
				if (tuners == null)
					fetchTuners();
				
				return tuners;
			}
		}
				
		public V4LInput Input
		{
			get
			{
				int currentInput = 0;
				if (ioctl(
				          deviceHandle,
				          APIv2.v4l2_operation_id.GetInput,
				          ref currentInput) < 0)
					throw new Exception("Could not get the current video input.");
				
				return Inputs[currentInput];
			}
			set
			{
				int idx = (int)value.Index;
				ioctl(deviceHandle, APIv2.v4l2_operation_id.SetInput, ref idx);
			}
		}
		
		public ulong Standard
		{
			get
			{
				ulong std = 0;
				if (ioctl(deviceHandle, APIv2.v4l2_operation_id.GetStandard, ref std) < 0)
					throw new Exception("Could not get the current standard.");
				return std;
			}
			set
			{
				if (ioctl(deviceHandle, APIv2.v4l2_operation_id.SetStandard, ref value) < 0)
					throw new Exception("Could not set the given standard.");
			}
		}
		
		public V4LFormatMap Format
		{
			get
			{
				if (formatMap == null)
					formatMap = new V4LFormatMap(this);
				
				return formatMap;
			}
		}
	}
}
