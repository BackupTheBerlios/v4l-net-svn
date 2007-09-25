using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_window
	{
		public v4l2_rect w;
		public v4l2_field field;
		public uint chromakey;
		public unsafe v4l2_clip *clips;
		public uint clipcount;
		public unsafe void *bitmap;
	}
}
