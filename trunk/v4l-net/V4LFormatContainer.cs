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
	public class V4LFormatContainer
	{
		#region Private Fields
		
		private V4LDevice device;
		
		private V4LVideoFormat videoCapture, videoOutput;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		internal V4LFormatContainer(V4LDevice device)
		{
			this.device = device;
		}
		
		#endregion Constructors and Destructors
		
		#region Public Properties
		
		public V4LVideoFormat VideoCapture
		{
			get
			{
				if (videoCapture == null)
					videoCapture = new V4LVideoFormat(device, v4l2_buf_type.VideoCapture);
				
				return videoCapture;
			}
		}
		
		public V4LVideoFormat VideoOutput
		{
			get
			{
				if (videoOutput == null)
					videoOutput = new V4LVideoFormat(device, v4l2_buf_type.VideoOutput);
				
				return videoOutput;
			}
		}
		
		/*
		 * public V4LOverlayFormat OverlayCapture
		 * public V4LOverlayFormat OverlayOutput
		 * 
		 * public V4LVBIFormat VBICapture
		 * public V4LVBIFormat VBIOutput
		 * 
		 * public V4LSlicedVBIFormat SlicedVBICapture
		 * public V4LSlicedVBIFormat SlicedVBIOutput
		 */
		
		#endregion Public Properties
	}
}
