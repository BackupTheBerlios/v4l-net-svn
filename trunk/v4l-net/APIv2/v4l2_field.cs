using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_field : uint
	{
		Any                 = 0,
		None                = 1,
		Top                 = 2,
		Bottom              = 3,
		Interlaced          = 4,
		SequentialTopBottom = 5,
		SequentialBottomTop = 6,
		Alternate           = 7,
		InterlacedTopBottom = 8,
		InterlacedBottomTop = 9
	}
}
