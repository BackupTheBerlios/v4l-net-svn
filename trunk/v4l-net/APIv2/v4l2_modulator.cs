using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_modulator
	{
		public uint index;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public byte[] name;
		public uint capability;
		public uint rangelow;
		public uint rangehigh;
		public uint txsubchans;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public uint[] reserved;
	}
}
