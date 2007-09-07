using System;

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
		
		public void SetAsCurrent()
		{
			device.Input = this;
		}
		
		/***************************************************/
		/* Public Getters and Setters                      */
		/***************************************************/
		
		public uint Index
		{
			get { return input.index; }
		}
		
		public string Name
		{
			get { return System.Text.Encoding.ASCII.GetString(input.name); }
		}
		
		public uint Type
		{
			get { return input.type; }
		}
		
		public uint AudioSet
		{
			get { return input.audioset; }
		}
		
		public uint Tuner
		{
			// TODO: return this.device.Tuners[input.tuner]!
			get { return input.tuner; }
		}
		
		public ulong SupportedStandards
		{
			get { return input.std; }
		}
		
		public uint Status
		{
			get { return input.status; }
		}
	}
}
