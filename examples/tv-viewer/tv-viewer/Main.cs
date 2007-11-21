using SdlDotNet.Core;
//using SdlDotNet.Graphics;
using SdlDotNet.Input;
using System;
using System.Runtime.InteropServices;
using Tao.Sdl;
using Video4Linux;

namespace SdlTest
{
	class MainClass
	{
		private static IntPtr screen, yuvOverlay;
		private static Sdl.SDL_Rect rect;
		private static V4LDevice device;
		
		public static void Main(string[] args)
		{
			screen = Sdl.SDL_SetVideoMode(360, 288, 32, Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | Sdl.SDL_RESIZABLE);
			yuvOverlay = Sdl.SDL_CreateYUVOverlay(720, 576, Sdl.SDL_YUY2_OVERLAY, screen);
			rect = new Sdl.SDL_Rect(0, 0, 360, 288);
			
			device = new V4LDevice("/dev/video0");
			
			device.Format.VideoCapture.SetDimensions(720, 576);
			device.Format.VideoCapture.PixelFormat = Video4Linux.APIv2.v4l2_pix_format_id.YUYV;
			device.Format.VideoCapture.Field = Video4Linux.APIv2.v4l2_field.Interlaced;
			
			device.Input = device.Inputs[device.Inputs.IndexOf("Name", "Television")];
			device.Standard = device.Standards[device.Standards.IndexOf("Name", "PAL")];
			
			// set to RTL initially
			tune((uint)(217.25 * 16));
			
			if (!device.Capabilities.Contains(V4LDeviceCapability.Streaming))
				throw new Exception("device is not able to do streaming!");
			
			device.BufferFilled += bufferFilled;
			device.StartStreaming();
			
			Events.Quit += quit;
			Events.KeyboardDown += keyDown;
			//Events.VideoResize += videoResize;
			Events.Run();
		}
		
		private static void quit(object sender, QuitEventArgs e)
		{
			device.StopStreaming();
			Events.QuitApplication();
		}
		
		private static void keyDown(object sender, KeyboardEventArgs e)
		{
			if (e.Key == Key.Escape || e.Key == Key.Q)
				quit(null, null);
			else if (e.Key == Key.One)
				tune((uint)(217.25 * 16)); // RTL
			else if (e.Key == Key.Two)
				tune((uint)(294.25 * 16)); // Pro7
			else if (e.Key == Key.Three)
				tune((uint)(303.25 * 16)); // Super RTL
		}
		
		/*private static void videoResize(object sender, VideoResizeEventArgs e)
		{
			System.Console.WriteLine(e.Width + "x" + e.Height);
		}*/
		
		private static void tune(uint frequency)
		{
			device.Input.Tuner.Frequency = frequency;
		}
		
		private static unsafe void bufferFilled(V4LDevice device, V4LBuffer buffer)
		{
			if (Sdl.SDL_MUSTLOCK(screen) == 1)
				if (Sdl.SDL_LockSurface(screen) < 0)
					throw new Exception("SDL_LockSurface");
			
			if (Sdl.SDL_LockYUVOverlay(yuvOverlay) < 0)
				throw new Exception("SDL_LockYUVOverlay");
			
			IntPtr pixels = Marshal.ReadIntPtr(yuvOverlay, 20);
			memcpy(Marshal.ReadIntPtr(pixels).ToPointer(), buffer.Start.ToPointer(), buffer.Length);
			
			if (Sdl.SDL_MUSTLOCK(screen) == 1)
				Sdl.SDL_UnlockSurface(screen);
			
			Sdl.SDL_UnlockYUVOverlay(yuvOverlay);
			Sdl.SDL_DisplayYUVOverlay(yuvOverlay, ref rect);
		}
		
		[DllImport("libc")]
		private static unsafe extern void
			memcpy(void* target, void* source, ulong size);
	}
}