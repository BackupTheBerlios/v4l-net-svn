#region LICENSE
/* 
 * Copyright (C) 2007 Tim Taubert (twenty-three@users.berlios.de)
 * 
 * This file is part of video4linux-net.
 *
 * Video4linux-net is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * Video4linux-net is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */
#endregion LICENSE

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