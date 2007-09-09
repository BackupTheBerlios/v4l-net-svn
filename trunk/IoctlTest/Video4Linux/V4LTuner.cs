using System;
using Video4Linux.APIv2;

namespace Video4Linux
{
	public class V4LTuner
	{
		private V4LDevice device;
		private v4l2_tuner tuner;
		
		public V4LTuner(V4LDevice device, v4l2_tuner tuner)
		{
			this.device = device;
			this.tuner = tuner;
		}
		
		/***************************************************/
		/* Public Getters and Setters                      */
		/***************************************************/
		
		public uint Index
		{
			get { return tuner.index; }
		}
		
		public string Name
		{
			get { return System.Text.Encoding.ASCII.GetString(tuner.name); }
		}
		
		public v4l2_frequency Frequency
		{
			get
			{
				v4l2_frequency freq = new v4l2_frequency();
				freq.tuner = tuner.index;
				
				// TODO: zero out the reserved field
				if (this.device.IOControl(v4l2_operation_id.GetFrequency, ref freq) < 0)
					throw new Exception("Could not get the current frequency.");
				
				return freq;
			}
			set
			{
				value.tuner = tuner.index;
				if (this.device.IOControl(v4l2_operation_id.SetFrequency, ref value) < 0)
					throw new Exception("Could not set the current frequency.");
			}
		}
	}
}
