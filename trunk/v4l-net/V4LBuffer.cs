#region LICENSE
/* 
 * Copyright (C) 2007 Tim Taubert (twenty-three@users.berlios.de)
 * 
 * This file is part of Video4Linux.Net.
 *
 * Video4Linux.Net is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * Video4Linux.Net is distributed in the hope that it will be useful,
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
using Video4Linux.APIv2;

namespace Video4Linux
{
    /// <summary>
	/// Represents a Video4Linux buffer used with streaming I/O and mmap
	/// and stores (info about) the captured frames.
    /// </summary>
	public class V4LBuffer
	{
		#region Private Fields
		
		private V4LDevice device;
		private v4l2_buffer buffer;
		private IntPtr start;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
        /// <summary>
        /// Creates a buffer to be used with Video4Linux streaming I/O.
        /// </summary>
		/// <param name="device">The parental Video4Linux device.</param>
		/// <param name="buffer">The struct holding the buffer information.</param>
		internal V4LBuffer(V4LDevice device, v4l2_buffer buffer)
		{
			this.device = device;
			this.buffer = buffer;
			
			mapMemory();
		}
		
        /// <summary>
        /// Extends the destruction of a Video4Linux buffer.
        /// </summary>
		~V4LBuffer()
		{
			unmapMemory();
		}
		
		#endregion Constructors and Destructors
		
		#region Private Methods
		
		/// <summary>
		/// Maps the memory belonging to the buffer.
		/// </summary>
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
		
		private void unmapMemory()
		{
			Mono.Unix.Native.Syscall.munmap(start, buffer.length);
		}
		
		#endregion Private Methods
		
		#region Public Methods
		
		/// <summary>
		/// Puts the buffer into the driver's incoming queue.
		/// </summary>
		internal void Enqueue()
		{
			if (device.IoControl.EnqueueBuffer(ref buffer) < 0)
				throw new Exception("VIDIOC_QBUF");
		}
		
		/// <summary>
		/// Removes the buffer from the driver's outgoing queue.
		/// </summary>
		internal void Dequeue()
		{
			if (device.IoControl.DequeueBuffer(ref buffer) < 0)
				throw new Exception("VIDIOC_DQBUF");
		}
		
		#endregion Public Methods
		
		#region Internal Properties
		
		internal v4l2_memory Memory
		{
			get { return buffer.memory; }
		}
		
		#endregion Internal Properties
		
		#region Public Properties
		
		/// <summary>
		/// Gets the number of bytes that are currently used.
		/// </summary>
		/// <value>The number of bytes currently in use.</value>
		public uint BytesUsed
		{
			get { return buffer.bytesused; }
		}
		
		public uint Offset
		{
			get { return buffer.m.offset; }
		}
		
		/// <summary>
		/// Gets the length of the buffer in bytes.
		/// </summary>
		/// <value>The length of the buffer in bytes.</value>
		public uint Length
		{
			get { return buffer.length; }
		}
		
		public uint Sequence
		{
			get { return buffer.sequence; }
		}
		
		public IntPtr Start
		{
			get { return start; }
		}
		
		#endregion Public Properties
		
		#region Internal Properties
		
		internal uint Index
		{
			get { return buffer.index; }
		}
		
		internal v4l2_buf_type Type
		{
			get { return buffer.type; }
		}
		
		#endregion Internal Properties
	}
}