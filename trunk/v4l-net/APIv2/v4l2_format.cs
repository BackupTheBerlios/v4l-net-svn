using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_format
	{
		public v4l2_buf_type type;
		public fmt_union fmt;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct fmt_union
	{
		public v4l2_pix_format pix;
	}
	
	/*[StructLayout(LayoutKind.Explicit)]
	public struct fmt_union
	{
		[FieldOffset(0)]
		public v4l2_pix_format pix;           // V4L2_BUF_TYPE_VIDEO_CAPTURE
		[FieldOffset(0)]
		public v4l2_window win;               // V4L2_BUF_TYPE_VIDEO_OVERLAY
		[FieldOffset(0)]
		public v4l2_vbi_format vbi;           // V4L2_BUF_TYPE_VBI_CAPTURE
		[FieldOffset(0)]
		public v4l2_sliced_vbi_format sliced; // V4L2_BUF_TYPE_SLICED_VBI_CAPTURE
		// HACK: extensibility removed :(
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=200)]
		public byte[] raw;
	}*/
}
