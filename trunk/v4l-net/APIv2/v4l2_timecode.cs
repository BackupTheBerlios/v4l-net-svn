using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_timecode
	{
		public uint type;
		public uint flags;
		public byte frames;
		public byte seconds;
		public byte minutes;
		public byte hours;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public byte[] userbits;
	}
}
