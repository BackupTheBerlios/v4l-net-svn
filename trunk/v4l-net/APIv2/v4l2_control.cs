using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_control
	{
		public uint id;
		public int value;
	}
}
