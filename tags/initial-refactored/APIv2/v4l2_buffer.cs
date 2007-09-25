using System;
using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_buffer
	{
		public uint index;
		public v4l2_buf_type type;
		public uint bytesused;
		public uint flags;
		public v4l2_field field;
		
		[StructLayout(LayoutKind.Sequential)]
		public struct timeval
		{
			public uint tv_sec;
			public uint tv_usec;
		}
		public timeval timestamp;
		
		public v4l2_timecode timecode;
		public uint sequence;
		public v4l2_memory memory;
		
		[StructLayout(LayoutKind.Explicit)]
		public struct m_union
		{
			[FieldOffset(0)]
			public uint offset;
			[FieldOffset(0)]
			public uint userptr;
		}
		public m_union m;
		
		public uint length;
		public uint input;
		public uint reserved;
	}
}
