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

using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_input_status : uint
	{
		// General
		NoPower              = 0x00000001, // Attached device is off.
		NoSignal             = 0x00000002,
		NoColor              = 0x00000004, // The hardware supports color decoding, but does not detect color modulation in the signal.
			
		// Analog Video
		NoHorizontalSyncLock = 0x00000100, // No horizontal sync lock.
		ColorKill            = 0x00000200, // A color killer circuit automatically disables color decoding when it detects no color modulation. When this flag is set the color killer is enabled and has shut off color decoding.

		// Digital Video
		NoSyncLock           = 0x00010000, // No synchronization lock.
		NoEqualizerLock      = 0x00020000, // No equalizer lock.
		NoCarrier            = 0x00040000, // Carrier recovery failed.
		
		// VCR and Set-Top Box
		Macrovision          = 0x01000000, // Macrovision is an analog copy prevention system mangling the video signal to confuse video recorders. When this flag is set Macrovision has been detected.
		NoAccess             = 0x02000000, // Conditional access denied.
		VTR                  = 0x04000000 // VTR time constant. [?]
	}
}
