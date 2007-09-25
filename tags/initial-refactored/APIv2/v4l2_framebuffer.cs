using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_framebuffer
	{
		public uint capability;
		public uint flags;
		public unsafe void *baseaddr;
		public v4l2_pix_format fmt;
	}
}
