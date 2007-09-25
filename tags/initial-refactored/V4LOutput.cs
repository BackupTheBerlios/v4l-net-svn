using System;
using System.Text;

namespace Video4Linux
{
	public class V4LOutput
	{
		private V4LDevice device;
		private APIv2.v4l2_output output;
		
		public V4LOutput(V4LDevice device, APIv2.v4l2_output output)
		{
			this.device = device;
			this.output = output;
		}
		
		/**************************************************/
		
		/**************************************************/
		
		public uint Index
		{
			get { return output.index; }
		}
		
		public string Name
		{
			get { return Encoding.ASCII.GetString(output.name); }
		}
		
		public uint Type
		{
			get { return output.type; }
		}
		
		public uint Status
		{
			get { return output.status; }
		}
		
		public ulong SupportedStandards
		{
			get { return output.std; }
		}
	}
}