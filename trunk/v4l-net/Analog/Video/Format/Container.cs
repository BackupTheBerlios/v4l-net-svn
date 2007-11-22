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

namespace Video4Linux.Analog.Video.Format
{
	/// <summary>
	/// Provides a container for all video capture and output formats.
	/// </summary>
	public class Container
	{
		#region Private Fields
		
		private Analog.Adapter.Adapter adapter;
		private Analog.Video.Format.Video videoCapture, videoOutput;
		private Analog.Video.Format.Overlay overlayCapture, overlayOutput;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		internal Container(Analog.Adapter.Adapter adapter)
		{
			this.adapter = adapter;
		}
		
		#endregion Constructors and Destructors
		
		#region Public Properties
		
		/// <summary>
		/// Gets the current video capture format.
		/// </summary>
		/// <value>The video capture format.</value>
		public Analog.Video.Format.Video VideoCapture
		{
			get
			{
				if (!adapter.Capabilities.Contains(Analog.Adapter.Capability.VideoCapture))
					throw new Exception("video capture not supported");
				
				if (videoCapture == null)
					videoCapture = new Analog.Video.Format.Video(adapter, v4l2_buf_type.VideoCapture);
				
				return videoCapture;
			}
		}
		
		/// <summary>
		/// Gets the current video output format.
		/// </summary>
		/// <value>The video output format.</value>
		public Analog.Video.Format.Video VideoOutput
		{
			get
			{
				if (!adapter.Capabilities.Contains(Analog.Adapter.Capability.VideoOutput))
					throw new Exception("video output not supported");
				
				if (videoOutput == null)
					videoOutput = new Analog.Video.Format.Video(adapter, v4l2_buf_type.VideoOutput);
				
				return videoOutput;
			}
		}
		
		/// <summary>
		/// Gets the current overlay capture format.
		/// </summary>
		/// <value>The overlay capture format.</value>
		public Analog.Video.Format.Overlay OverlayCapture
		{
			get
			{
				if (!adapter.Capabilities.Contains(Analog.Adapter.Capability.VideoOverlay))
					throw new Exception("overlay capture not supported");
				
				if (overlayCapture == null)
					overlayCapture = new Analog.Video.Format.Overlay(adapter, v4l2_buf_type.VideoOverlay);
				
				return overlayCapture;
			}
		}
		
		/// <summary>
		/// Gets the current overlay output format.
		/// </summary>
		/// <value>The overlay output format.</value>
		public Analog.Video.Format.Overlay OverlayOutput
		{
			get
			{
				if (!adapter.Capabilities.Contains(Analog.Adapter.Capability.VideoOutputOverlay))
					throw new Exception("overlay output not supported");
				
				if (overlayOutput == null)
					overlayOutput = new Analog.Video.Format.Overlay(adapter, v4l2_buf_type.VideoOutputOverlay);
				
				return overlayOutput;
			}
		}
		
		/*
		 * public V4LVBIFormat VBICapture
		 * public V4LVBIFormat VBIOutput
		 * 
		 * public V4LSlicedVBIFormat SlicedVBICapture
		 * public V4LSlicedVBIFormat SlicedVBIOutput
		 */
		
		#endregion Public Properties
	}
}
