using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_fmtdesc
	{
		public uint index;
		public v4l2_buf_type type;
		public uint flags;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public byte[] description;
		public v4l2_pix_format_id pixelformat;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public uint[] reserved;
	}
}