using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_priority : uint
	{
		Unset       = 0,
		Background  = 1,
		Interactive = 2,
		Record      = 3,
		Default     = Interactive
	}
}
