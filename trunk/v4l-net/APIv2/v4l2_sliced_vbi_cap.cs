using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_sliced_vbi_cap
	{
		public ushort service_set;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=48)]
		public ushort[] service_lines;
		public v4l2_buf_type type;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
		public uint[] reserved;
	}
}