using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_standard
	{
		public uint index;
		public v4l2_std_id id;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=24)]
		public byte[] name;
		public v4l2_fract frameperiod;
		public uint framelines;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public uint[] reserved;
	}
}
