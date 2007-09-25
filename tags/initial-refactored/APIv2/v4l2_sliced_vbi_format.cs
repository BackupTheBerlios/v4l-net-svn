using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_sliced_vbi_format
	{
		public uint service_set;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=48)]
		public ushort[] service_lines;
		public uint io_size;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public uint[] reserved;
	}
}
