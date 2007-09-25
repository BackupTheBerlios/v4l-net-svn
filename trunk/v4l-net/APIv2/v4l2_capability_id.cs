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

namespace Video4Linux.APIv2
{
	public enum v4l2_capability_id : uint
	{
		VideoCapture       = 0x00000001,
		VideoOutput        = 0x00000002,
		VideoOverlay       = 0x00000004,
		VBICapture         = 0x00000010,
		VBIOutput          = 0x00000020,
		SlicedVBICapture   = 0x00000040,
		SlicedVBIOutput    = 0x00000080,
		CaptureRDS         = 0x00000100,
		VideoOutputOverlay = 0x00000200,
		Tuner              = 0x00010000,
		Audio              = 0x00020000,
		Radio              = 0x00040000,
		ReadWrite          = 0x01000000,
		AsyncIO            = 0x02000000,
		Streaming          = 0x04000000
	}
}
