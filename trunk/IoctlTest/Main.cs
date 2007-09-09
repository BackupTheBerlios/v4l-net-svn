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
			
			foreach (V4LTuner tuner in dev.Tuners)
				System.Console.WriteLine(tuner.Name);
			System.Console.WriteLine();
			
			System.Console.WriteLine("Current Input: " + dev.Input.Name);
			dev.Standard = (ulong)Video4Linux.APIv2.v4l2_std_id.Composite_PAL_BG;
			System.Console.WriteLine("Current Standard: " + dev.Standard);
			
			// set the format
			Video4Linux.APIv2.v4l2_format fmt = new Video4Linux.APIv2.v4l2_format();
			fmt.fmt.pix.width = 720;
			fmt.fmt.pix.height = 576;
			fmt.fmt.pix.pixelformat = Video4Linux.APIv2.v4l2_pix_format_id.YUYV;
			fmt.fmt.pix.field = Video4Linux.APIv2.v4l2_field.Interlaced;
			dev.Format[Video4Linux.APIv2.v4l2_buf_type.VideoCapture] = fmt;
			
			// get the current format
			fmt = dev.Format[Video4Linux.APIv2.v4l2_buf_type.VideoCapture];
			System.Console.WriteLine(fmt.type);
			System.Console.WriteLine(fmt.fmt.pix.pixelformat);
			System.Console.WriteLine(fmt.fmt.pix.field);
			System.Console.WriteLine(fmt.fmt.pix.width + "x" + fmt.fmt.pix.height);
			System.Console.WriteLine();
			
			// get the current frequency
			Video4Linux.APIv2.v4l2_frequency freq = dev.Tuners[0].Frequency;
			// set the current frequency
			freq.frequency = 217250 / 1000 * 16;
			dev.Tuners[0].Frequency = freq;
			System.Console.WriteLine("Frequency: " + dev.Tuners[0].Frequency.frequency);
			System.Console.WriteLine();
			
			// request one buffer
			Video4Linux.APIv2.v4l2_requestbuffers req = new Video4Linux.APIv2.v4l2_requestbuffers();
			req.count = 1;
			req.type = Video4Linux.APIv2.v4l2_buf_type.VideoCapture;
			req.memory = Video4Linux.APIv2.v4l2_memory.MemoryMapping;
			int res = dev.IOControl
				(Video4Linux.APIv2.v4l2_operation_id.RequestBuffers,
				 ref req);
			if (res < 0)
				throw new Exception("Could not request buffers.");
			else
				System.Console.WriteLine("Requested a buffer.");
			
			/*/ query buffer info
			struct v4l2_buffer buffer;
			memset(&buffer, 0, sizeof(buffer));
			buffer.type = req.type;
			buffer.memory = V4L2_MEMORY_MMAP;
			buffer.index = 0;
			if (ioctl(fd, VIDIOC_QUERYBUF, &buffer) < 0)
				printf("err: cant query buf\n");*/
		}
	}
}