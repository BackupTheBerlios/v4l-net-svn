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
	public enum v4l2_tuner_capability
	{
		Low   = 0x0001, // tuning frequencies are expressed in units of 62.5 Hz, otherwise in units of 62.5 kHz.
		Norm  = 0x0002, // Multi-standard tuner; the video standard can or must be switched, only v4l2_tuner_type.AnalogTV
		Lang1 = 0x0040, // Primary Language, only v4l2_tuner_type.AnalogTV
		Lang2 = 0x0020  // Secondary Language, only v4l2_tuner_type.AnalogTV
	}
}
