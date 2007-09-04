using System;
using Video4Linux;

namespace IoctlTest
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			V4LDevice dev = new V4LDevice("/dev/video0");
			
			System.Console.WriteLine(dev.Name);
			System.Console.WriteLine(dev.Driver);
			System.Console.WriteLine(dev.BusInfo);
			System.Console.WriteLine(dev.Version);
			System.Console.WriteLine();
			
			foreach (V4LInput input in dev.Inputs)
				System.Console.WriteLine(input.Name);
			System.Console.WriteLine();
			
			System.Console.WriteLine("Current Input: " + dev.Input.Name);
			dev.Standard = (ulong)Video4Linux.APIv2.v4l2_std_id.Composite_PAL_BG;
			System.Console.WriteLine("Current Standard: " + dev.Standard);
			
			/*/ set image format	
			struct v4l2_format imgformat;
			imgformat.type = V4L2_BUF_TYPE_VIDEO_CAPTURE;
			memset( &(imgformat.fmt.pix), 0, sizeof( struct v4l2_pix_format ) );
			imgformat.fmt.pix.width = 720;
			imgformat.fmt.pix.height = 576;
			imgformat.fmt.pix.pixelformat = V4L2_PIX_FMT_YUYV;
			imgformat.fmt.pix.field = V4L2_FIELD_INTERLACED;
			if (ioctl(fd, VIDIOC_S_FMT, &imgformat) < 0)
				printf("err: cant set img fmt\n");*/
		}
	}
}