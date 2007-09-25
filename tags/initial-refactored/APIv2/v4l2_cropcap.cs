using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_cropcap
	{
		public v4l2_buf_type type;
		public v4l2_rect bounds;
		public v4l2_rect defrect;
		public v4l2_fract pixelaspect;
	}
}