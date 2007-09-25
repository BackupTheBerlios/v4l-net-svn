#region LICENSE
/* 
 * Copyright (C) 2007 Tim Taubert (twenty-three@users.berlios.de)
 * 
 * This file is part of video4linux-net.
 *
 * Video4linux-net is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * Video4linux-net is distributed in the hope that it will be useful,
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
	public class V4LOutput
	{
		private APIv2.v4l2_output output;
		
		public V4LOutput(APIv2.v4l2_output output)
		{
			this.output = output;
		}
		
		/**************************************************/
		
		/**************************************************/
		
		public uint Index
		{
			get { return output.index; }
		}
		
		public string Name
		{
			get { return Encoding.ASCII.GetString(output.name); }
		}
		
		public uint Type
		{
			get { return output.type; }
		}
		
		public uint Status
		{
			get { return output.status; }
		}
		
		public ulong SupportedStandards
		{
			get { return output.std; }
		}
	}
}