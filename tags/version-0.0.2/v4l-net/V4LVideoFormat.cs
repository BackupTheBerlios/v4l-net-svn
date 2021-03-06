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
	/// Represents a video capture and output format.
	/// </summary>
	public class V4LVideoFormat
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
		internal V4LVideoFormat(V4LDevice device, v4l2_buf_type type)
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
		
		/// <summary>
		/// Sets the image dimensions.
		/// </summary>
		/// <param name="width">Image width in pixels.</param>
		/// <param name="height">Image height in pixels.</param>
		public void SetDimensions(uint width, uint height)
		{
			getFormat();
			format.fmt.pix.width = width;
			format.fmt.pix.height = height;
			setFormat();
		}
		
		#endregion Public Methods
		
		#region Public Properties
		
		/// <summary>
		/// Gets the image width in pixels.
		/// </summary>
		public uint Width
		{
			get
			{
				getFormat();
				return format.fmt.pix.width;
			}
		}
		
		/// <summary>
		/// Gets the image height in pixels.
		/// </summary>
		/// <value>The image height.</value>
		public uint Height
		{
			get
			{
				getFormat();
				return format.fmt.pix.height;
			}
		}
		
		/// <summary>
		/// Gets the size of a line in bytes.
		/// </summary>
		/// <value>The bytes per line.</value>
		public uint BytesPerLine
		{
			get
			{
				getFormat();
				return format.fmt.pix.bytesperline;
			}
			set
			{
				getFormat();
				format.fmt.pix.bytesperline = value;
				setFormat();
			}
		}
		
		public v4l2_field Field
		{
			get
			{
				getFormat();
				return format.fmt.pix.field;
			}
			set
			{
				getFormat();
				format.fmt.pix.field = value;
				device.IoControl.SetFormat(ref format);
			}
		}
		
		/// <summary>
		/// Gets or sets the image's pixel format.
		/// </summary>
		/// <value>The pixel format.</value>
		public v4l2_pix_format_id PixelFormat
		{
			get
			{
				getFormat();
				return format.fmt.pix.pixelformat;
			}
			set
			{
				getFormat();
				format.fmt.pix.pixelformat = value;
				setFormat();
			}
		}
		
		/*
		 * public uint sizeimage [get]
		 * public v4l2_colorspace colorspace [get]
		 */
		
		#endregion Public Properties
	}
}
