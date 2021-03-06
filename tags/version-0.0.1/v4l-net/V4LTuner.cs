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
using System.Text;
using Video4Linux.APIv2;

namespace Video4Linux
{
	/// <summary>
	/// Represents a tuner.
	/// </summary>
	public class V4LTuner
	{
		#region Private Fields
		
		private V4LDevice device;
		private v4l2_tuner tuner;
		
		#endregion Private Fields
		
		#region Constructors and Destructors
		
        /// <summary>
        /// Creates a tuner.
        /// </summary>
		/// <param name="device">The parental Video4Linux device.</param>
		/// <param name="tuner">The struct holding the tuner information.</param>
		internal V4LTuner(V4LDevice device, v4l2_tuner tuner)
		{
			this.device = device;
			this.tuner = tuner;
		}
		
		#endregion Constructors and Destructors
		
		#region Public Properties
		
        /// <summary>
        /// Gets the tuner's index in the list of all tuners.
        /// </summary>
		/// <value>The index of the tuner.</value>
		public uint Index
		{
			get { return tuner.index; }
		}
		
        /// <summary>
        /// Gets the tuner's name.
        /// </summary>
		/// <value>The name of the tuner.</value>
		public string Name
		{
			get { return tuner.name; }
		}
		
        /// <summary>
        /// Gets the tuner's type.
        /// </summary>
		/// <value>The type of the tuner.</value>
		public APIv2.v4l2_tuner_type Type
		{
			get { return tuner.type; }
		}
		
		public uint Capabilities
		{
			get { return tuner.capability; }
		}
		
        /// <summary>
        /// Gets or sets the tuner's current frequency.
        /// </summary>
		/// <value>The frequency.</value>
		public uint Frequency
		{
			get
			{
				v4l2_frequency freq = new v4l2_frequency();
				freq.tuner = tuner.index;
				freq.type = tuner.type;
				if (device.IoControl.GetFrequency(ref freq) < 0)
					throw new Exception("VIDIOC_G_FREQUENCY");
				
				return freq.frequency;
			}
			// TODO: if AFC is on, then negotiate the right frequency
			set 
			{
				v4l2_frequency freq = new v4l2_frequency();
				freq.tuner = tuner.index;
				freq.type = tuner.type;
				freq.frequency = value;
				if (device.IoControl.SetFrequency(ref freq) < 0)
					throw new Exception("VIDIOC_S_FREQUENCY");
			}
		}
		
        /// <summary>
        /// Gets the lowest possible tuner frequency.
        /// </summary>
		/// <value>The frequency.</value>
		public uint LowestFrequency
		{
			get { return tuner.rangelow; }
		}
		
        /// <summary>
        /// Gets the highest possible tuner frequency.
        /// </summary>
		/// <value>The frequency.</value>
		public uint HighestFrequency
		{
			get { return tuner.rangehigh; }
		}
		
        /// <summary>
        /// Gets the tuner's signal quality.
        /// </summary>
		/// <value>The signal quality.</value>
		public uint Signal
		{
			get { return tuner.signal; }
		}
		
		#endregion Public Properties
	}
}