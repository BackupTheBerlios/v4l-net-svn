using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_memory : uint
	{
		MemoryMapping = 1, // The buffer is used for memory mapping I/O.
		UserPointer   = 2, // The buffer is used for user pointer I/O.
		MemoryOverlay = 3
	}
}
