using System;
using System.Text;

namespace Video4Linux
{
	public class V4LInput
	{
		private V4LDevice device;
		private APIv2.v4l2_input input;
		
		public V4LInput(V4LDevice device, APIv2.v4l2_input input)
		{
			this.device = device;
			this.input = input;
		}
		
		/**************************************************/
		
		/**************************************************/
		
		public uint Index
		{
			get { return input.index; }
		}
		
		public string Name
		{
			get { return Encoding.ASCII.GetString(input.name); }
		}
		
		public uint Type
		{
			get { return input.type; }
		}
		
		public V4LTuner tuner
		{
			get { return device.Tuners[(int)input.tuner]; }
		}
		
		public uint Status
		{
			get { return input.status; }
		}
		
		public ulong SupportedStandards
		{
			get { return input.std; }
		}
	}
}