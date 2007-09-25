using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_tuner_audio_reception
	{
		Mono   = 0x0001,
		Stereo = 0x0002,
		Lang1  = 0x0008, // Primary Language, only v4l2_tuner_type.AnalogTV
		Lang2  = 0x0004  // Secondary Language, only v4l2_tuner_type.AnalogTV
	}
}
