using System;
using Video4Linux;

/*Mono.Unix.Native.Stdlib.GetLastError();
Mono.Unix.UnixMarshal.GetErrorDescription(errno);*/

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
			
			// get the current format
			Video4Linux.APIv2.v4l2_format fmt = dev.Format
				[Video4Linux.APIv2.v4l2_buf_type.VideoCapture];
			System.Console.WriteLine(fmt.type);
			System.Console.WriteLine(fmt.fmt.pix.pixelformat);
			System.Console.WriteLine(fmt.fmt.pix.field);
			System.Console.WriteLine(fmt.fmt.pix.width + "x" + fmt.fmt.pix.height);
			System.Console.WriteLine();
			
			// set the format
			fmt.fmt.pix.width = 720;
			fmt.fmt.pix.height = 576;
			fmt.fmt.pix.pixelformat = Video4Linux.APIv2.v4l2_pix_format_id.YUYV;
			fmt.fmt.pix.field = Video4Linux.APIv2.v4l2_field.Interlaced;
			dev.Format[Video4Linux.APIv2.v4l2_buf_type.VideoCapture] = fmt;
			
			// get the current frequency
			Video4Linux.APIv2.v4l2_frequency freq = dev.Tuners[0].Frequency;
			// set the current frequency
			// RTL: 217250
			// Pro7: 294250
			// Super RTL: 303250
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
			System.Console.WriteLine("Buffer Count: " + req.count);
			
			// query the buffer
			Video4Linux.APIv2.v4l2_buffer buf = new Video4Linux.APIv2.v4l2_buffer();
			buf.type = req.type;
			buf.memory = req.memory;
			buf.index = 0;
			res = dev.IOControl
				(Video4Linux.APIv2.v4l2_operation_id.QueryBuffer,
				 ref buf);
			if (res < 0)
				throw new Exception("Could not query the buffer.");
			else
				System.Console.WriteLine("Queried the buffer.");
			System.Console.WriteLine();
			
			// map the data
			IntPtr start = Mono.Unix.Native.Syscall.mmap
				(IntPtr.Zero,
				 buf.length,
				 Mono.Unix.Native.MmapProts.PROT_READ | Mono.Unix.Native.MmapProts.PROT_WRITE,
				 Mono.Unix.Native.MmapFlags.MAP_SHARED,
				 dev.DeviceHandle,
				 buf.m.offset);
			
			if (start == Mono.Unix.Native.Syscall.MAP_FAILED)
				throw new Exception("Memory mapping failed.");
			
			// enqueue the buffer
			res = dev.IOControl
				(Video4Linux.APIv2.v4l2_operation_id.EnqueueBuffer, ref buf);
			if (res < 0)
				throw new Exception("Could not enqueue the buffer.");
			
			// start streaming
			// return EINVAL if no buffers are enqueued!
			res = dev.IOControl
				(Video4Linux.APIv2.v4l2_operation_id.StreamingOn, ref buf.type);
			if (res < 0)
				throw new Exception("Could not start streaming.");
			
			// wait 'til data is received
			while (buf.bytesused == 0)
				dev.IOControl
					(Video4Linux.APIv2.v4l2_operation_id.QueryBuffer, ref buf);
			
			System.Console.WriteLine();
			System.Console.Write("Field: " + buf.field);
			
			// read the yuyv bitmap
			System.Drawing.Bitmap bm = ReadYUYVFrame(start, 720, 576);
			bm.Save("/home/tim/test.bmp");
			
			// dequeue the buffer
			res = dev.IOControl(Video4Linux.APIv2.v4l2_operation_id.DequeueBuffer, ref buf);
			if (res < 0)
				throw new Exception("Could not dequeue the buffer.");
			
			// re-enqueue the buffer
			res = dev.IOControl
				(Video4Linux.APIv2.v4l2_operation_id.EnqueueBuffer, ref buf);
			if (res < 0)
				throw new Exception("Could not enqueue the buffer.");
			
			// wait 'til data is received
			while (buf.bytesused == 0)
				dev.IOControl
					(Video4Linux.APIv2.v4l2_operation_id.QueryBuffer, ref buf);
			
			System.Console.WriteLine();
			System.Console.Write("Field: " + buf.field);
			
			// read the yuyv bitmap
			System.Drawing.Bitmap bm2 = ReadYUYVFrame(start, 720, 576);
			bm2.Save("/home/tim/test2.bmp");
			
			// dequeue the buffer
			res = dev.IOControl(Video4Linux.APIv2.v4l2_operation_id.DequeueBuffer, ref buf);
			if (res < 0)
				throw new Exception("Could not dequeue the buffer.");
			
			// re-enqueue the buffer
			res = dev.IOControl
				(Video4Linux.APIv2.v4l2_operation_id.EnqueueBuffer, ref buf);
			if (res < 0)
				throw new Exception("Could not enqueue the buffer.");
			
			// wait 'til data is received
			while (buf.bytesused == 0)
				dev.IOControl
					(Video4Linux.APIv2.v4l2_operation_id.QueryBuffer, ref buf);
			
			System.Console.WriteLine();
			System.Console.Write("Field: " + buf.field);
			
			// read the yuyv bitmap
			System.Drawing.Bitmap bm3 = ReadYUYVFrame(start, 720, 576);
			bm3.Save("/home/tim/test3.bmp");
			
			// stop streaming
			res = dev.IOControl
				(Video4Linux.APIv2.v4l2_operation_id.StreamingOff, ref buf.type);
			if (res < 0)
				throw new Exception("Could not stop streaming.");
		}
		
		private static System.Drawing.Bitmap ReadYUYVFrame(IntPtr ptr, int width, int height)
		{
			System.Drawing.Bitmap bm = new System.Drawing.Bitmap(width, height);
			
			int
				x = 0,
				y = 0;
			for (int i=0; i<width*height/2; i++)
			{
				byte
					y1 = System.Runtime.InteropServices.Marshal.ReadByte(ptr, i*4),
					u = System.Runtime.InteropServices.Marshal.ReadByte(ptr, i*4+1),
					y2 = System.Runtime.InteropServices.Marshal.ReadByte(ptr, i*4+2),
					v = System.Runtime.InteropServices.Marshal.ReadByte(ptr, i*4+3);
				
				bm.SetPixel(x++, y, YUYVtoRGB(y1, u, v));
				bm.SetPixel(x++, y+1, YUYVtoRGB(y2, u, v));
				
				if (x == 720)
				{
					x = 0;
					y++;
				}
			}
			
			return bm;
		}
		
		private static System.Drawing.Color YUYVtoRGB(byte y, byte u, byte v)
		{
			int
				r = (int)(y + 1.370705 * (v-128)),
				g = (int)(y - 0.698001 * (v-128) - 0.337633 * (u-128)),
				b = (int)(y + 1.732446 * (u-128));
			
			if (r < 0)
				r = 0;
			else if (r > 255)
				r = 255;
			
			if (g < 0)
				g = 0;
			else if (g > 255)
				g = 255;
			
			if (b < 0)
				b = 0;
			else if (b > 255)
				b = 255;
			
			r = r * 220 / 256;
			g = g * 220 / 256;
			b = b * 220 / 256;
			
			return System.Drawing.Color.FromArgb(r, g, b);
		}
	}
}