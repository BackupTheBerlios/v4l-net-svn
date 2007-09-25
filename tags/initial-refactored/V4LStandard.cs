using System;
using System.Text;

namespace Video4Linux
{
	public class V4LStandard
	{
		private V4LDevice device;
		private APIv2.v4l2_standard standard;
		
		public V4LStandard(V4LDevice device, APIv2.v4l2_standard standard)
		{
			this.device = device;
			this.standard = standard;
		}
		
		/**************************************************/
		
		/**************************************************/
		
		public uint Index
		{
			get { return standard.index; }
		}
		
		
		public APIv2.v4l2_std_id Id
		{
			get { return standard.id; }
		}
		
		public string Name
		{
			get { return Encoding.ASCII.GetString(standard.name); }
		}
		
		public uint FrameLines
		{
			get { return standard.framelines; }
		}
	}
}