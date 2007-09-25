using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_frequency
	{
		public uint tuner;
		public v4l2_tuner_type type;
		public uint frequency;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		public uint[] reserved;
	}
}
