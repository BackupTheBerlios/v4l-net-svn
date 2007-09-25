using Mono.Unix.Native;
using System;
using System.Runtime.InteropServices;

namespace Video4Linux
{
	public class V4LBuffer
	{
		private const int HEIGHT = 576;
		private const int WIDTH = 720;
		
		private V4LDevice device;
		private APIv2.v4l2_buffer buffer;
		private IntPtr start;
		
		public V4LBuffer(V4LDevice device, APIv2.v4l2_buffer buffer)
		{
			this.device = device;
			this.buffer = buffer;
			
			mapMemory();
		}
		
		/**************************************************/
		
		private void mapMemory()
		{
			start = Syscall.mmap
				(IntPtr.Zero,
				 buffer.length,
				 MmapProts.PROT_READ | MmapProts.PROT_WRITE,
				 MmapFlags.MAP_SHARED,
				 device.DeviceHandle,
				 buffer.m.offset);
			if (start == Syscall.MAP_FAILED)
				throw new Exception("Memory mapping failed.");
		}
		
		/**************************************************/

		public void Enqueue()
		{
			if (device.IoControl.EnqueueBuffer(ref buffer) < 0)
				throw new Exception("VIDIOC_QBUF");
		}

		public void Dequeue()
		{
			if (device.IoControl.DequeueBuffer(ref buffer) < 0)
				throw new Exception("VIDIOC_DQBUF");
		}
		
		/**************************************************/
		
		public uint Index
		{
			get { return buffer.index; }
		}
		
		public uint BytesUsed
		{
			get { return buffer.bytesused; }
		}
		
		public uint Offset
		{
			get { return buffer.m.offset; }
		}
		
		public uint Length
		{
			get { return buffer.length; }
		}
		
		public uint Sequence
		{
			get { return buffer.sequence; }
		}
		
		public APIv2.v4l2_buf_type Type
		{
			get { return buffer.type; }
		}
		
		public APIv2.v4l2_memory Memory
		{
			get { return buffer.memory; }
		}
		
		public IntPtr Start
		{
			get { return start; }
		}
		
		//public V4LField Field;
	}
}