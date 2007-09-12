using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Video4Linux
{
	struct pixel
	{
		public byte y;
		public byte u;
		public byte v;
		
		public pixel(byte y, byte u, byte v)
		{
			this.y = y;
			this.u = u;
			this.v = v;
		}
	}
	
	public class YUYVFrame
	{
		//private static convert table
		
		private List<pixel> pixels;
		private int width, height;
		
		public YUYVFrame(IntPtr ptr, int width, int height)
		{
			pixels = new List<Video4Linux.pixel>();
			this.width = width;
			this.height = height;
			
			for (int i=0; i<width*height/2; i++)
			{
				byte
					u = Marshal.ReadByte(ptr, i*4+1),
					v = Marshal.ReadByte(ptr, i*4+3);
				
				pixels.Add(new pixel(Marshal.ReadByte(ptr, i*4), u, v));
				pixels.Add(new pixel(Marshal.ReadByte(ptr, i*4+2), u, v));
			}
		}
		
		// TODO: don't use BM.SetPixel
		public Bitmap toBitmap()
		{
			Bitmap bitmap = new Bitmap(width, height);
			
			int x=0, y=0;
			foreach (pixel pix in pixels)
			{
				bitmap.SetPixel(x++, y, convertYuyvToRgb(pix));
				
				// swap the line if we reached the end
				if (x == width)
				{
					x = 0;
					y++;
				}
			}
			
			return bitmap;
		}
		
		// converts 4:4:4 YUV to RGB888
		private Color convertYuyvToRgb(pixel pix)
		{
			/*/ fourcc method -- works!
            int
				r = (int)(pix.y + 1.370705 * (pix.v-128)),
				g = (int)(pix.y - 0.698001 * (pix.v-128) - 0.337633 * (pix.u-128)),
				b = (int)(pix.y + 1.732446 * (pix.u-128));*/
			
			// MS's conversion! -- seems to work!
			int
				c = pix.y - 16,
				d = pix.u - 128,
				e = pix.v - 128;
			
			int
				r = (int)((298 * c           + 409 * e + 128) >> 8),
				g = (int)((298 * c - 100 * d - 208 * e + 128) >> 8),
				b = (int)((298 * c + 516 * d           + 128) >> 8);
			
			// clip the values to range [0,255]
			clip(ref r);
			clip(ref g);
			clip(ref b);
			
			return Color.FromArgb(r, g, b);
		}
		
		private void clip(ref int val)
		{
			if (val < 0)
				val = 0;
			else if (val > 255)
				val = 255;
		}
	}
}
