using System;
using System.Collections.Generic;

namespace Video4Linux
{
	public class TestMain
	{
		public static void Main(string[] args)
		{
			V4LDevice dev = new V4LDevice();
			System.Console.WriteLine("Device:");
			System.Console.WriteLine("\t" + dev.Name);
			System.Console.WriteLine("\t" + dev.Driver);
			System.Console.WriteLine("\t" + dev.BusInfo);
			System.Console.WriteLine("\t" + dev.Version);
			System.Console.WriteLine();
			
			System.Console.WriteLine("Inputs:");
			foreach (V4LInput input in dev.Inputs)
				System.Console.WriteLine("\t" + input.Name);
			System.Console.WriteLine();
			
			System.Console.WriteLine("Outputs:");
			foreach (V4LOutput output in dev.Outputs)
				System.Console.WriteLine("\t" + output.Name);
			System.Console.WriteLine();
			
			System.Console.WriteLine("Tuners:");
			foreach (V4LTuner tuner in dev.Tuners)
				System.Console.WriteLine("\t" + tuner.Name);
			System.Console.WriteLine();
			
			System.Console.WriteLine("Audio Inputs:");
			foreach (V4LAudioInput input in dev.AudioInputs)
				System.Console.WriteLine("\t" + input.Name);
			System.Console.WriteLine();
			
			System.Console.WriteLine("Audio Outputs:");
			foreach (V4LAudioOutput output in dev.AudioOutputs)
				System.Console.WriteLine("\t" + output.Name);
			System.Console.WriteLine();
			
			System.Console.WriteLine("Standards:");
			foreach (V4LStandard standard in dev.Standards)
				System.Console.WriteLine("\t" + standard.Name);
			System.Console.WriteLine();
			
			System.Console.WriteLine("Standard:\n\t" + dev.Standard);
			System.Console.WriteLine();
			
			System.Console.WriteLine("Image Formats:");
			foreach (V4LFormat format in dev.Formats)
				System.Console.WriteLine("\t" + format.Description + " (" + format.Type + ")");
			System.Console.WriteLine();
			
			dev.RequestBuffers(5);
			System.Console.WriteLine("Buffers:");
			foreach (V4LBuffer buffer in dev.Buffers)
			{
				System.Console.WriteLine("\t" + buffer.Index + "(" + buffer.Offset + ")");
				buffer.Enqueue();
			}
			System.Console.WriteLine();
			
			dev.BufferFilled += OnBufferFilled;
			dev.StartStreaming();
			
			System.Threading.Thread.Sleep(2000);
			
			dev.StopStreaming();
		}
		
		private static void OnBufferFilled(V4LDevice sender, V4LBuffer buffer)
		{
			System.Console.WriteLine("Filled: " + buffer.Index);
		}
	}
}
