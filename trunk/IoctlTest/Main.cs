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
			
			/*v4l2_std_id std_id = V4L2_STD_PAL_BG;
			if (ioctl(fd, VIDIOC_S_STD, &std_id) < 0)
				printf("err: cant set vid std\n");*/
		}
	}
}