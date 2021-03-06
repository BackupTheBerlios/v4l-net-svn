using System;

namespace Video4Linux.APIv2
{
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
}
