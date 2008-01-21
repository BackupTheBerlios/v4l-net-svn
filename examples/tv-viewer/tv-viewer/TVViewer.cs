using SdlDotNet.Core;
using SdlDotNet.Input;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Tao.Sdl;

using Video4Linux.Analog;
using Video4Linux.Analog.Kernel;
using Video4Linux.Analog.Video;

namespace tvviewer
{
	public class TVViewer
	{
		private static IntPtr screen, yuvOverlay, pixels;
		private static Sdl.SDL_Rect rect;
		private static Adapter adapter;
		private static Thread thread;
		
		public TVViewer()
		{
			// initialize sdl stuff
			screen = Sdl.SDL_SetVideoMode(720, 576, 32, Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF);
			yuvOverlay = Sdl.SDL_CreateYUVOverlay(720, 576, Sdl.SDL_YUY2_OVERLAY, screen);
			rect = new Sdl.SDL_Rect(0, 0, 720, 576);
			pixels = Marshal.ReadIntPtr(Marshal.ReadIntPtr(yuvOverlay, 20));
			
			// initialize adapter
			adapter = new Adapter("/dev/video0");
			adapter.SetControlValue(Control.Mute, 0);
			
			// set desired capture method
			adapter.CaptureMethod = CaptureMethod.ReadWrite;
			
			// set video format
			VideoCaptureFormat format = new VideoCaptureFormat(720, 576);
			format.PixelFormat = v4l2_pix_format_id.YUYV;
			format.Field = v4l2_field.Interlaced;
			adapter.SetFormat(format);
			
			adapter.Input = adapter.Inputs[adapter.Inputs.IndexOf("Name", "Television")];
			adapter.Standard = adapter.Standards[adapter.Standards.IndexOf("Name", "PAL")];
			
			Events.Quit += quit;
			Events.KeyboardDown += keyDown;
		}
		
		public void Run()
		{
			adapter.Input.Tuner.Frequency = (uint)(217.25 * 16);
			
			adapter.StartStreaming();
			thread = new Thread(readBuffer);
			thread.Start();
			
			Events.Run();
		}
		
		private void readBuffer()
		{
			byte[] buffer = new byte[720*576*2];
			
			while (true)
				if (adapter.VideoStream.Read(buffer, 0, 720*576*2) > 0)
					drawBuffer(buffer);
		}
		
		private void drawBuffer(byte[] buffer)
		{
			if (Sdl.SDL_MUSTLOCK(screen) == 1)
				Sdl.SDL_LockSurface(screen);
			
			Sdl.SDL_LockYUVOverlay(yuvOverlay);
			
			// copy the image to the overlay
			Marshal.Copy(buffer, 0, pixels, 720*576*2);
			
			if (Sdl.SDL_MUSTLOCK(screen) == 1)
				Sdl.SDL_UnlockSurface(screen);
			
			Sdl.SDL_UnlockYUVOverlay(yuvOverlay);
			Sdl.SDL_DisplayYUVOverlay(yuvOverlay, ref rect);
		}
		
		private void quit(object sender, QuitEventArgs e)
		{
			thread.Abort();
			
			adapter.StopStreaming();
			adapter.SetControlValue(Control.Mute, 1);
			
			Events.QuitApplication();
		}
		
		private void keyDown(object sender, KeyboardEventArgs e)
		{
			switch (e.Key)
			{
			case Key.Escape:
			case Key.Q:
				quit(null, null);
				break;
			case Key.One:
				adapter.Input.Tuner.Frequency = (uint)(217.25 * 16); // RTL
				break;
			case Key.Two:
				adapter.Input.Tuner.Frequency = (uint)(294.25 * 16); // Pro7
				break;
			case Key.Three:
				adapter.Input.Tuner.Frequency = (uint)(303.25 * 16); // Super RTL
				break;
			}
		}
	}
}
