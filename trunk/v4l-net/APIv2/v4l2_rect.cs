using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_rect
	{
		public int left;
		public int top;
		public int width;
		public int height;
	}
}
