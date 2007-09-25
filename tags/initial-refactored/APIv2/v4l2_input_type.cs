using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_input_type : uint
	{
		Tuner  = 1, // This input uses a tuner (RF demodulator).
		Camera = 2 // Analog baseband input, for example CVBS / Composite Video, S-Video, RGB.
	}
}
