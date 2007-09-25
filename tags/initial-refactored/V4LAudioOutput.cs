using System;
using System.Text;

namespace Video4Linux
{
	public class V4LAudioOutput
	{
		private V4LDevice device;
		private APIv2.v4l2_audioout output;
		
		public V4LAudioOutput(V4LDevice device, APIv2.v4l2_audioout output)
		{
			this.device = device;
			this.output = output;
		}
		
		/**************************************************/
		
		public APIv2.v4l2_audioout ToStruct()
		{
			return output;
		}
		
		/**************************************************/
		
		public uint Index
		{
			get { return output.index; }
		}
		
		public string Name
		{
			get { return Encoding.ASCII.GetString(output.name); }
		}
		
		public uint Capabilities
		{
			get { return output.capability; }
		}
		
		public uint Mode
		{
			get { return output.mode; }
		}
	}
}