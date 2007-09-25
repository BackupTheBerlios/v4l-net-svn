using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_buf_type : uint
	{
		// v4l2_pix_format
		VideoCapture       = 1,
		VideoOutput        = 2,
		// v4l2_window
		VideoOverlay       = 3,
		VideoOutputOverlay = 8, // TODO: ensure that v4l2_window is filled!
		// v4l2_vbi_format
		VBICapture         = 4,
		VBIOutput          = 5,
		// v4l2_sliced_vbi_format
		SlicedVBICapture   = 6,
		SlicedVBIOutput    = 7,
		// byte[] raw
		Private            = 0x80
	}
}
