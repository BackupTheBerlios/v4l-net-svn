using System;
using System.Text;

namespace Video4Linux
{
	public class V4LTuner
	{
		/*public int AFCValue;
		public bool UseAFC;*/
		
		private V4LDevice device;
		private APIv2.v4l2_tuner tuner;
		
		public V4LTuner(V4LDevice device, APIv2.v4l2_tuner tuner)
		{
			this.device = device;
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