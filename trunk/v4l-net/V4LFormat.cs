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
	public class V4LFormat
	{
		private APIv2.v4l2_fmtdesc fmtdesc;
		
		public V4LFormat(APIv2.v4l2_fmtdesc fmtdesc)
		{
			this.fmtdesc = fmtdesc;
		}
		
		/**************************************************/
		
		/**************************************************/
		
		public uint Index
		{
			get { return fmtdesc.index; }
		}
		
		public APIv2.v4l2_buf_type Type
		{
			get { return fmtdesc.type; }
		}
		
		public uint Flags
		{
			get { return fmtdesc.flags; }
		}
		
		public string Description
		{
			get { return Encoding.ASCII.GetString(fmtdesc.description); }
		}
		
		public APIv2.v4l2_pix_format_id pixelformat
		{
			get { return fmtdesc.pixelformat; }
		}
	}
}
