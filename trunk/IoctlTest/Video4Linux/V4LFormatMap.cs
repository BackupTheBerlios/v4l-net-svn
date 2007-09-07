using System;
using System.Collections.Generic;
using Video4Linux.APIv2;

namespace Video4Linux
{
	public class V4LFormatMap
	{
		private V4LDevice device;
		
		public V4LFormatMap(V4LDevice device)
		{
			this.device = device;
		}
		
		public v4l2_format this[v4l2_buf_type bufType]
		{
			get { return fetchFormat(bufType); }
			set
			{
				value.type = bufType;
				setFormat(value);
			}
		}
		
		private v4l2_format fetchFormat(v4l2_buf_type bufType)
		{
			v4l2_format fmt = new v4l2_format();
			fmt.type = bufType;
			
			// HACK: bad implementation of ioctl
			if (this.device.IOControl(v4l2_operation_id.GetFormat, ref fmt) < 0)
				throw new Exception("Could not get the current format.");
			
			// TODO: fetch the real format
			return fmt;
		}
		
		private void setFormat(v4l2_format fmt)
		{
			if (this.device.IOControl(v4l2_operation_id.SetFormat, ref fmt) < 0)
				throw new Exception("Could not set the given format.");
		}
	}
}
