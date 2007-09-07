namespace Video4Linux.APIv2
{
	/*
	 * Specifies a V4L2 operation code.
	 */
	public enum v4l2_operation_id : int
	{
		// TODO: check for _IOR/_IOWR
		QueryCapabilities = -2140645888, // VIDIOC_QUERYCAP
		GetStandard       = -2146937321, // VIDIOC_G_STD
		SetStandard       =  1074288152, // VIDIOC_S_STD
		GetInput          = -2147199450, // VIDIOC_G_INPUT
		SetInput          = -1073457625, // VIDIOC_S_INPUT
		GetFormat         = -1060350460, // VIDIOC_G_FMT
		SetFormat         = -1060350459, // VIDIOC_S_FMT
		SetFrequency      =  1076647481, // VIDIOC_S_FREQUENCY
		RequireBuffers    = -1072409080, // VIDIOC_REQBUFS
		QueryBuffer       = -1069263351, // VIDIOC_QUERYBUF
		StreamingOn       =  1074026002, // VIDIOC_STREAMON
		StreamingOff      =  1074026003, // VIDIOC_STREAMOFF
		EnqueueBuffer     = -1069263345, // VIDIOC_QBUF
		DequeueBuffer     = -1069263343, // VIDIOC_DQBUF
		EnumerateInputs   = -1068739046  // VIDIOC_ENUMINPUT
	}
	
	/*
	 * Specifies a V4L2 device capability.
	 */
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
	
	/*
	 * Specifies a V4L2 video standard.
	 */
	public enum v4l2_std_id : ulong
	{
		Unknown     = 0,
		PAL_B       = 0x00000001,
		PAL_B1      = 0x00000002,
		PAL_G       = 0x00000004,
		PAL_H       = 0x00000008,
		PAL_I       = 0x00000010,
		PAL_D       = 0x00000020,
		PAL_D1      = 0x00000040,
		PAL_K       = 0x00000080,
		PAL_M       = 0x00000100,
		PAL_N       = 0x00000200,
		PAL_Nc      = 0x00000400,
		PAL_60      = 0x00000800,
		NTSC_M      = 0x00001000,
		NTSC_M_JP   = 0x00002000,
		NTSC_443    = 0x00004000,
		NTSC_M_KR   = 0x00008000,
		SECAM_B     = 0x00010000,
		SECAM_D     = 0x00020000,
		SECAM_G     = 0x00040000,
		SECAM_H     = 0x00080000,
		SECAM_K     = 0x00100000,
		SECAM_K1    = 0x00200000,
		SECAM_L     = 0x00400000,
		SECAM_LC    = 0x00800000,
		ATSC_8_VSB  = 0x01000000,
		ATSC_16_VSB = 0x02000000,
		
		/*
		 * Composite video standards.
		 */
		Composite_PAL_BG   = PAL_B | PAL_B1 | PAL_G,
		Composite_PAL_B    = PAL_B | PAL_B1 | SECAM_B,
		Composite_PAL_GH   = PAL_G | PAL_H  | SECAM_G | SECAM_H,
		Composite_PAL_DK   = PAL_D | PAL_D1 | PAL_K,
		Composite_PAL      = Composite_PAL_BG | Composite_PAL_DK | PAL_H | PAL_I,
		Composite_NTSC     = NTSC_M | NTSC_M_JP | NTSC_M_KR,
		Composite_MN       = PAL_M | PAL_N | PAL_Nc | Composite_NTSC,
		Composite_SECAM_DK = SECAM_D | SECAM_K | SECAM_K1,
		Composite_DK       = Composite_PAL_DK | Composite_SECAM_DK,
		Composite_SECAM    =
			SECAM_B | SECAM_G | SECAM_H | Composite_SECAM_DK | SECAM_L | SECAM_LC,
		Composite_525_60   = PAL_M | PAL_60 | Composite_NTSC | NTSC_443,
		Composite_625_50   = Composite_PAL | PAL_N | PAL_Nc | Composite_SECAM,
		Composite_All      = Composite_525_60 | Composite_625_50
	}
	
	/*
	 * Specifies a V4L2 input type.
	 */
	public enum v4l2_input_id : uint
	{
		Tuner  = 1, // This input uses a tuner (RF demodulator).
		Camera = 2 // Analog baseband input, for example CVBS / Composite Video, S-Video, RGB.
	}
	
	/*
	 * Specfies a V4L2 input status.
	 */
	public enum v4l2_input_status_id : uint
	{
		// General
		NoPower              = 0x00000001, // Attached device is off.
		NoSignal             = 0x00000002,
		NoColor              = 0x00000004, // The hardware supports color decoding, but does not detect color modulation in the signal.
			
		// Analog Video
		NoHorizontalSyncLock = 0x00000100, // No horizontal sync lock.
		ColorKill            = 0x00000200, // A color killer circuit automatically disables color decoding when it detects no color modulation. When this flag is set the color killer is enabled and has shut off color decoding.

		// Digital Video
		NoSyncLock           = 0x00010000, // No synchronization lock.
		NoEqualizerLock      = 0x00020000, // No equalizer lock.
		NoCarrier            = 0x00040000, // Carrier recovery failed.
		
		// VCR and Set-Top Box
		Macrovision          = 0x01000000, // Macrovision is an analog copy prevention system mangling the video signal to confuse video recorders. When this flag is set Macrovision has been detected.
		NoAccess             = 0x02000000, // Conditional access denied.
		VTR                  = 0x04000000 // VTR time constant. [?]
	}
	
	public enum v4l2_buf_type : byte
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
	
	public enum v4l2_field : byte
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
	
	// image format - fourcc
	public enum v4l2_pix_format_id : uint
	{
		// RGB formats
		RGB332  =  826427218, // RGB1
		RGB444  =  875836498, // R444
		RGB555  = 1329743698, // RGBO
		RGB565  = 1346520914, // RGBP
		RGB555X = 1363298130, // RGBQ
		RGB565X = 1380075346, // RGBR
		BGR24   =  861030210, // BGR3
		RGB24   =  859981650, // RGB3
		BGR32   =  877807426, // BGR4
		RGB32   =  876758866, // RGB4
		
		// YUV formats
		GREY    = 1497715271, // GREY
		YUYV    = 1448695129, // YUYV
		UYVY    = 1498831189, // UYVY
		Y41P    = 1345401945, // Y41P
		YVU420  =  842094169, // YV12
		YUV420  =  842093913, // YU12
		YVU410  =  961893977, // YVU9
		YUV410  =  961959257, // YUV9
		YUV422P = 1345466932, // 422P
		NV12    =  842094158, // NV12
		NV21    =  825382478, // NV21
		
		// Reserved
		DV       = 1685288548, // dvsd
		ET61X251 =  892483141, // E625
		HI240    =  875710792, // HI24
		HM12     =  842091848, // HM12
		MJPEG    = 1196444237, // MJPG
		PWC1     =  826496848, // PWC1
		PWC2     =  843274064, // PWC2
		SN9C10X  =  808532307, // S910
		WNVA     = 1096175191, // WNVA
		YYUV     = 1448434009  // YYUV
	}
	
	class keks
	{
		public static uint bla(string fourcc)
		{
			return ((uint)fourcc[3]) << 24 |
				((uint)fourcc[2]) << 16 |
				((uint)fourcc[1]) << 8 |
				((uint)fourcc[0]);
		}
	}
}