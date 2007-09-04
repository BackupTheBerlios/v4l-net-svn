using System.Runtime.InteropServices;

namespace Video4Linux.APIv2
{
	/*
	 * Holds information about the device's capabilities.
	 */
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
	
	/*
	 * Holds information about a device's input.
	 */
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
		public struct fmt
		{
			public v4l2_pix_format pix;           // V4L2_BUF_TYPE_VIDEO_CAPTURE
			public v4l2_window win;               // V4L2_BUF_TYPE_VIDEO_OVERLAY
			public v4l2_vbi_format vbi;           // V4L2_BUF_TYPE_VBI_CAPTURE
			public v4l2_sliced_vbi_format sliced; // V4L2_BUF_TYPE_SLICED_VBI_CAPTURE
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=200)]
			public byte[] raw;
		}
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct v4l2_pix_format
	{
		public uint width;
		public uint height;
		public uint pixelformat;
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
		public v4l2_clip clips; // v4l2_clip*
		public uint clipcount;
		public uint bitmap; // void*
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
		public v4l2_clip next; // v4l2_clip*
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
}