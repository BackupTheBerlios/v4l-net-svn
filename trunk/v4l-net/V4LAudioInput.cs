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
using System.Collections.Generic;
using Video4Linux.APIv2;

namespace Video4Linux
{
	/// <summary>
	/// Represents an audio input device.
	/// </summary>
	public class V4LAudioInput
	{
		#region Private Fields
		
		private V4LDevice device;
		private v4l2_audio input;
		private List<V4LAudioInputCapability> capabilities;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
		internal V4LAudioInput(V4LDevice device, v4l2_audio input)
		{
			this.device = device;
			this.input = input;
		}
		
		#endregion Constructors and Destructors
		
		#region Private Methods
		
		private void getInput()
		{
			if (device.IoControl.GetAudioInput(ref input) < 0)
				throw new Exception("VIDIOC_G_AUDIO");
		}
		
		private void setInput()
		{
			if (device.IoControl.SetAudioInput(ref input) < 0)
				throw new Exception("VIDIOC_S_AUDIO");
		}
		
		private void fetchCapabilities()
		{
			capabilities = new List<V4LAudioInputCapability>();
			
			foreach (object val in Enum.GetValues(typeof(V4LAudioInputCapability)))
				if ((input.capabilities & (uint)val) != 0)
					capabilities.Add((V4LAudioInputCapability)val);
		}
		
		#endregion Private Methods
		
		#region Internal Methods
		
		internal v4l2_audio ToStruct()
		{
			return input;
		}
		
		#endregion Internal Methods
		
		#region Public Properties
		
		/// <summary>
		/// Gets the name of the audio input.
		/// </summary>
		/// <value>The name of the audio input.</value>
		public string Name
		{
			get { return input.name; }
		}
		
		/// <summary>
		/// Gets the audio input's capabilities.
		/// </summary>
		/// <value>A list of capabilities.</value>
		public List<V4LAudioInputCapability> Capabilities
		{
			get
			{
				if (capabilities == null)
					fetchCapabilities();
				
				return capabilities;
			}
		}
		
		/// <summary>
		/// Gets or sets the current Automatic Volume Level mode status.
		/// </summary>
		/// <value></value>
		public bool UsesAVLMode
		{
			get
			{
				getInput();
				return input.mode == 1;
			}
			set
			{
				input.mode = value ? (uint)1 : (uint)0;
				setInput();
			}
		}
		
		#endregion Public Properties
		
		#region Internal Properties
		
		/// <summary>
		/// Gets the index of this audio input in the list of all available audio inputs.
		/// </summary>
		/// <value>The index of this audio input.</value>
		internal uint Index
		{
			get { return input.index; }
		}
		
		#endregion Internal Properties
	}
}