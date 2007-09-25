using System;
using System.Text;

namespace Video4Linux
{
	public class V4LAudioInput
	{
		private V4LDevice device;
		private APIv2.v4l2_audio input;
		
		public V4LAudioInput(V4LDevice device, APIv2.v4l2_audio input)
		{
			this.device = device;
			this.input = input;
		}
		
		/**************************************************/
		
		public APIv2.v4l2_audio ToStruct()
		{
			return input;
		}
		
		/**************************************************/
		
		public uint Index
		{
			get { return input.index; }
		}
		
		public string Name
		{
			get { return Encoding.ASCII.GetString(input.name); }
		}
		
		public uint Capabilities
		{
			get { return input.capability; }
		}
		
		public uint Mode
		{
			get { return input.mode; }
		}
	}
}