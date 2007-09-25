using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_requestbuffers
	{
		public uint count;
		public v4l2_buf_type type;
		public v4l2_memory memory;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public uint[] reserverd;
	}
}
