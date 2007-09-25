using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_vbi_format
	{
		public uint sampling_rate;
		public uint offset;
		public uint samples_per_line;
		public uint sample_format;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public uint[] start;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public uint[] count;
	}
}
