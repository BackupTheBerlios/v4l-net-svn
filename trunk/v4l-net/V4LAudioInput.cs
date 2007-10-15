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
using Video4Linux.APIv2;

namespace Video4Linux
{
	/// <summary>
	/// Represents an audio input device.
	/// </summary>
	public class V4LAudioInput
	{
		#region Private Fields
		
		private v4l2_audio input;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		internal V4LAudioInput(v4l2_audio input)
		{
			this.input = input;
		}
		
		#endregion Constructors and Destructors
		
		internal v4l2_audio ToStruct()
		{
			return input;
		}
		
		#region Public Properties
		
		public uint Index
		{
			get { return input.index; }
		}
		
		public string Name
		{
			get { return input.name; }
		}
		
		public uint Capabilities
		{
			get { return input.capability; }
		}
		
		public uint Mode
		{
			get { return input.mode; }
		}
		
		#endregion Public Properties
	}
}