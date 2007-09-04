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
}