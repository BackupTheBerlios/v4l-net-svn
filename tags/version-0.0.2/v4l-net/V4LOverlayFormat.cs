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
	/// Represents a video overlay capture and output format.
	/// </summary>
	public class V4LOverlayFormat
	{
		#region Private Fields
		
		private V4LDevice device;
		private v4l2_format format;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
        /// <summary>
        /// Creates a video capture/output format.
        /// </summary>
        /// <param name="device">The parental Video4Linux device.</param>
		/// <param name="type">The buffer type the format belongs to.</param>
		internal V4LOverlayFormat(V4LDevice device, v4l2_buf_type type)
		{
			this.device = device;
			
			format = new v4l2_format();
			format.type = type;
		}
		
		#endregion Constructors and Destructors
		
		#region Private Methods
		
		/// <summary>
		/// Gets the current v4l2_format settings.
		/// </summary>
		private void getFormat()
		{
			if (device.IoControl.GetFormat(ref format) < 0)
				throw new Exception("VIDIOC_G_FMT");
		}
		
		/// <summary>
		/// Sets the current v4l2_format settings.
		/// </summary>
		private void setFormat()
		{
			if (device.IoControl.SetFormat(ref format) < 0)
				throw new Exception("VIDIOC_S_FMT");
		}
		
		#endregion Private Methods
		
		#region Public Methods
		
		#endregion Public Methods
		
		#region Public Properties
		
		/// <summary>
		/// Gets or sets the clipping rectangle.
		/// </summary>
		/// <value>The rectangle to be clipped.</value>
		public V4LRectangle Window
		{
			get
			{
				getFormat();
				return new V4LRectangle(format.fmt.win.w);
			}
			set
			{
				getFormat();
				format.fmt.win.w = value.ToStruct();
				setFormat();
			}
		}
		
		/*
		 * field
		 * chromakey
		 * clips
		 * clipcount
		 * bitmap
		 * global_alpha
		 */
		
		#endregion Public Properties
	}
}