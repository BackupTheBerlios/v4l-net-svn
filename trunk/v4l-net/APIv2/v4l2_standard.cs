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
	public struct v4l2_standard
	{
		public uint index;
		public v4l2_std_id id;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=24)]
		public byte[] name;
		public v4l2_fract frameperiod;
		public uint framelines;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		public uint[] reserved;
	}
}
