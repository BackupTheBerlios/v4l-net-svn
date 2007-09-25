using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_colorspace : byte
	{
		SMPTE170M     = 1,
		SMPTE240M     = 2,
		REC709        = 3,
		BT878         = 4,
		SYSTEM_M_470  = 5,
		SYSTEM_BG_470 = 6,
		JPEG          = 7,
		SRGB          = 8
	}
}
