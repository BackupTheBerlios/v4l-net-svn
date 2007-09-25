using System;
using System.Text;

namespace Video4Linux
{
	public class V4LFormat
	{
		private V4LDevice device;
		private APIv2.v4l2_fmtdesc fmtdesc;
		
		public V4LFormat(V4LDevice device, APIv2.v4l2_fmtdesc fmtdesc)
		{
			this.device = device;
			this.fmtdesc = fmtdesc;
		}
		
		/**************************************************/
		
		/**************************************************/
		
		public uint Index
		{
			get { return fmtdesc.index; }
		}
		
		public APIv2.v4l2_buf_type Type
		{
			get { return fmtdesc.type; }
		}
		
		public uint Flags
		{
			get { return fmtdesc.flags; }
		}
		
		public string Description
		{
			get { return Encoding.ASCII.GetString(fmtdesc.description); }
		}
		
		public APIv2.v4l2_pix_format_id pixelformat
		{
			get { return fmtdesc.pixelformat; }
		}
	}
}
