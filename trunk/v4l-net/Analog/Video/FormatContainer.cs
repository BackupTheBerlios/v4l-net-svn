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

using Video4Linux.Analog.Kernel;

namespace Video4Linux.Analog.Video
{
	/// <summary>
	/// Provides a container for all video capture and output formats.
	/// </summary>
	public class FormatContainer
	{
		#region Private Fields
		
		private Analog.Adapter adapter;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		internal FormatContainer(Analog.Adapter adapter)
		{
			this.adapter = adapter;
		}
		
		#endregion Constructors and Destructors
		
		#region Private Methods
		
		private void checkForCapability(Analog.AdapterCapability cap)
		{
			if (!adapter.Capabilities.Contains(cap))
				throw new Exception("Unsupported capability: " + cap.ToString());
		}
		
		#endregion Private Methods
		
		#region Public Properties
		
		/// <summary>
		/// Gets the current video capture format.
		/// </summary>
		/// <value>The video capture format.</value>
		public Analog.Video.VideoFormat VideoCapture
		{
			get
			{
				checkForCapability(Analog.AdapterCapability.VideoCapture);
				return Analog.Video.VideoFormat.Get(adapter, v4l2_buf_type.VideoCapture);
			}
			set
			{
				checkForCapability(Analog.AdapterCapability.VideoCapture);
				Analog.Video.VideoFormat.Set(adapter, v4l2_buf_type.VideoCapture, value);
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
