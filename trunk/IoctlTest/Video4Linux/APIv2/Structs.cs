using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_capability
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
		public byte[] driver;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public byte[] card;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public byte[] bus_info;
		public uint version;
		public uint capabilities;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public uint[] reserved;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_input
	{
		public uint index;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public byte[] name;
		public uint type;
		public uint audioset;
		public uint tuner;
		public ulong std;
		public uint status;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public uint[] reserved;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_format
	{
		public v4l2_buf_type type;
		public fmt_union fmt;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct fmt_union
	{
		public v4l2_pix_format pix;
	}
	
	/*[StructLayout(LayoutKind.Explicit)]
	public struct fmt_union
	{
		[FieldOffset(0)]
		public v4l2_pix_format pix;           // V4L2_BUF_TYPE_VIDEO_CAPTURE
		[FieldOffset(0)]
		public v4l2_window win;               // V4L2_BUF_TYPE_VIDEO_OVERLAY
		[FieldOffset(0)]
		public v4l2_vbi_format vbi;           // V4L2_BUF_TYPE_VBI_CAPTURE
		[FieldOffset(0)]
		public v4l2_sliced_vbi_format sliced; // V4L2_BUF_TYPE_SLICED_VBI_CAPTURE
		// HACK: extensibility removed :(
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=200)]
		public byte[] raw;
	}*/
	
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_pix_format
	{
		public uint width;
		public uint height;
		public v4l2_pix_format_id pixelformat;
		public v4l2_field field;
		public uint bytesperline;
		public uint sizeimage;
		public v4l2_colorspace colorspace;
		public uint priv;
	}
	
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
	
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_rect
	{
		public int left;
		public int top;
		public int width;
		public int height;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_clip
	{
		public v4l2_rect c;
		public unsafe v4l2_clip *next;
	}
	
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
	
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_sliced_vbi_format
	{
		public uint service_set;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=48)]
		public ushort[] service_lines;
		public uint io_size;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
		public uint[] reserved;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_frequency
	{
		public uint tuner;
		public v4l2_tuner_type type;
		public uint frequency;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		public uint[] reserved;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_tuner
	{
		public uint index;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
		public byte[] name;
		public v4l2_tuner_type type;
		public uint capability;
		public uint rangelow;
		public uint rangehigh;
		public uint rxsubchans;
		public uint audmode;
		public uint signal;
		public int afc;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public uint[] reserved;
	}
	
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