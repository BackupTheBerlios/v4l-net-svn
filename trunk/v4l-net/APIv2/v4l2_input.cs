using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_input
	{
		public uint index;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public byte[] name;
		public uint type;
		public uint audioset;
		public uint tuner;
		public ulong std;
		public uint status;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public uint[] reserved;
	}
}
