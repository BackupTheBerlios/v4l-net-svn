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
		private v4l2_buf_type type;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		internal V4LVideoFormat(V4LDevice device, v4l2_buf_type type)
		{
			this.device = device;
			this.type = type;
		}
		
		#endregion Constructors and Destructors
		
		#region Public Methods
		
		public void SetDimensions(uint width, uint height)
		{
			v4l2_format fmt = new v4l2_format();
			fmt.type = type;
			device.IoControl.GetFormat(ref fmt);
			
			fmt.fmt.pix.width = width;
			fmt.fmt.pix.height = height;
			device.IoControl.SetFormat(ref fmt);
		}
		
		#endregion Public Methods
		
		#region Public Properties
		
		/*
		 * public uint width [get]
		 * public uint height [get]
		 * public uint byteperline
		 * public uint sizeimage [get]
		 * public v4l2_colorspace colorspace [get]
		 * public v4l2_field field [get]
		 * public uint pixelformat
		 */
		
		#endregion Public Properties
	}
}
