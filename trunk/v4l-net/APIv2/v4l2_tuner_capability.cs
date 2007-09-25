using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_tuner_capability
	{
		Low   = 0x0001, // tuning frequencies are expressed in units of 62.5 Hz, otherwise in units of 62.5 kHz.
		Norm  = 0x0002, // Multi-standard tuner; the video standard can or must be switched, only v4l2_tuner_type.AnalogTV
		Lang1 = 0x0040, // Primary Language, only v4l2_tuner_type.AnalogTV
		Lang2 = 0x0020  // Secondary Language, only v4l2_tuner_type.AnalogTV
	}
}
