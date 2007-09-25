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
