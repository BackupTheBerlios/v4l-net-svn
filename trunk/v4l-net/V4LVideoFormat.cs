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
	public class V4LVideoFormat
	{
		#region Private Fields
		
		private V4LDevice device;
		private v4l2_format format;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		internal V4LVideoFormat(V4LDevice device, v4l2_buf_type type)
		{
			this.device = device;
			
			format = new v4l2_format();
			format.type = type;
			getFormat();
		}
		
		#endregion Constructors and Destructors
		
		#region Private Methods
		
		private void getFormat()
		{
			if (device.IoControl.GetFormat(ref format) < 0)
				throw new Exception("VIDIOC_G_FMT");
		}
		
		private void setFormat()
		{
			if (device.IoControl.SetFormat(ref format) < 0)
				throw new Exception("VIDIOC_S_FMT");
		}
		
		#endregion Private Methods
		
		#region Public Methods
		
		public void SetDimensions(uint width, uint height)
		{
			getFormat();
			format.fmt.pix.width = width;
			format.fmt.pix.height = height;
			setFormat();
		}
		
		#endregion Public Methods
		
		#region Public Properties
		
		public uint Width
		{
			get
			{
				getFormat();
				return format.fmt.pix.width;
			}
		}
		
		public uint Height
		{
			get
			{
				getFormat();
				return format.fmt.pix.height;
			}
		}
		
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
		 * public uint pixelformat [get??]
		 */
		
		#endregion Public Properties
	}
}
