using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_pix_format
	{
		public uint width;
		public uint height;
		public v4l2_pix_format_id pixelformat;
		public v4l2_field field;
		public uint bytesperline;
		public uint sizeimage;
		public v4l2_colorspace colorspace;
		public uint priv;
	}
}
