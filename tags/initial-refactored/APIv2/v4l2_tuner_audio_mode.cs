using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_tuner_audio_mode
	{
		Mono        = 0,
		Stereo      = 1,
		Lang1       = 3,
		Lang2       = 2,
		Lang1_Lang2 = 4
	}
}
