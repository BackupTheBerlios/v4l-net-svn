using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_capability_id : uint
	{
		VideoCapture       = 0x00000001,
		VideoOutput        = 0x00000002,
		VideoOverlay       = 0x00000004,
		VBICapture         = 0x00000010,
		VBIOutput          = 0x00000020,
		SlicedVBICapture   = 0x00000040,
		SlicedVBIOutput    = 0x00000080,
		CaptureRDS         = 0x00000100,
		VideoOutputOverlay = 0x00000200,
		Tuner              = 0x00010000,
		Audio              = 0x00020000,
		Radio              = 0x00040000,
		ReadWrite          = 0x01000000,
		AsyncIO            = 0x02000000,
		Streaming          = 0x04000000
	}
}
