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
	/// Represents a rectangle.
	/// </summary>
	public struct V4LRectangle
	{
		#region Public Fields
		
		/// <summary>
		/// The left position of the top-left corner in pixels.
		/// </summary>
		public int Left;
		/// <summary>
		/// The top position of the top-left corner in pixels.
		/// </summary>
		public int Top;
		/// <summary>
		/// The height in pixels.
		/// </summary>
		public int Height;
		/// <summary>
		/// The width in pixels.
		/// </summary>
		public int Width;
		
		#endregion Public Fields
		
		#region Constructors and Destructors
		
		/// <summary>
		/// Creates a new rectangle.
		/// </summary>
		/// <param name="rect">The struct holding the rectangle information.</param>
		internal V4LRectangle(v4l2_rect rect)
		{
			Left = rect.left;
			Top = rect.top;
			Height = rect.height;
			Width = rect.width;
		}
		
		#endregion Constructors and Destructors
		
		#region Internal Methods
		
		/// <summary>
		/// Converts a rectangle back to a v4l2_rect.
		/// </summary>
		internal v4l2_rect ToStruct()
		{
			return new v4l2_rect(Left, Top, Height, Width);
		}
		
		#endregion Internal Methods
	}
}
