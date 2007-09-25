using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_audio
	{
		public uint index;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public byte[] name;
		public uint capability;
		public uint mode;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public uint[] reserved;
	}
}