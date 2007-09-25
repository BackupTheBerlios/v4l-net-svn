using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_tuner
	{
		public uint index;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public byte[] name;
		public v4l2_tuner_type type;
		public uint capability;
		public uint rangelow;
		public uint rangehigh;
		public uint rxsubchans;
		public uint audmode;
		public uint signal;
		public int afc;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public uint[] reserved;
	}
}
