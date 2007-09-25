using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_clip
	{
		public v4l2_rect c;
		public unsafe v4l2_clip *next;
	}
}
