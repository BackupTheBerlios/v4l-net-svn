using System;

namespace Video4Linux
{
	public class V4LTuner
	{
		private V4LDevice device;
		private APIv2.v4l2_tuner tuner;
		
		public V4LTuner(V4LDevice device, APIv2.v4l2_tuner tuner)
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
	}
}
