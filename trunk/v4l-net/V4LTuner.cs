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

namespace Video4Linux
{
	public class V4LTuner
	{
		/*public int AFCValue;
		public bool UseAFC;*/
		
		private APIv2.v4l2_tuner tuner;
		
		public V4LTuner(APIv2.v4l2_tuner tuner)
		{
			this.tuner = tuner;
		}
		
		/**************************************************/
		
		/**************************************************/
		
		public uint Index
		{
			get { return tuner.index; }
		}
		
		public string Name
		{
			get { return Encoding.ASCII.GetString(tuner.name); }
		}
		
		public APIv2.v4l2_tuner_type Type
		{
			get { return tuner.type; }
		}
		
		public uint Capabilities
		{
			get { return tuner.capability; }
		}
		
		/*public uint Frequency
		{
		}*/
		
		public uint LowestFrequency
		{
			get { return tuner.rangelow; }
		}
		
		public uint HighestFrequency
		{
			get { return tuner.rangehigh; }
		}
		
		public uint Signal
		{
			get { return tuner.signal; }
		}
	}
}