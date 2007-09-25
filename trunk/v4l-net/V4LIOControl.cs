#region LICENSE
/* 
 * Copyright (C) 2007 Tim Taubert (twenty-three@users.berlios.de)
 * 
 * This file is part of video4linux-net.
 *
 * Video4linux-net is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * Video4linux-net is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */
#endregion LICENSE

using System;
using System.Runtime.InteropServices;

namespace Video4Linux
{
	public class V4LIOControl
	{
		private int deviceHandle;

		/**************************************************/

		public V4LIOControl(int deviceHandle)
		{
			this.deviceHandle = deviceHandle;
		}

		public int QueryDeviceCapabilities(ref APIv2.v4l2_capability cap)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.QueryCapabilities, ref cap);
		}

		public int QueryBuffer(ref APIv2.v4l2_buffer buffer)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.QueryBuffer, ref buffer);
		}

		public int RequestBuffers(ref APIv2.v4l2_requestbuffers req)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.RequestBuffers, ref req);
		}

		public int StreamingOn(ref APIv2.v4l2_buf_type type)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.StreamingOn, ref type);
		}

		public int StreamingOff(ref APIv2.v4l2_buf_type type)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.StreamingOff, ref type);
		}

		public int EnqueueBuffer(ref APIv2.v4l2_buffer buffer)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.EnqueueBuffer, ref buffer);
		}

		public int DequeueBuffer(ref APIv2.v4l2_buffer buffer)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.DequeueBuffer, ref buffer);
		}

		public int GetFrequency(ref APIv2.v4l2_frequency freq)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.GetFrequency, ref freq);
		}

		public int SetFrequency(ref APIv2.v4l2_frequency freq)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.SetFrequency, ref freq);
		}

		public int EnumerateInputs(ref APIv2.v4l2_input input)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.EnumerateInputs, ref input);
		}

		public int EnumerateOutputs(ref APIv2.v4l2_output output)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.EnumerateOutputs, ref output);
		}

		public int EnumerateStandards(ref APIv2.v4l2_standard std)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.EnumerateStandards, ref std);
		}

		public int GetStandard(ref APIv2.v4l2_std_id std)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.GetStandard, ref std);
		}

		public int SetStandard(ref APIv2.v4l2_std_id std)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.SetStandard, ref std);
		}

		public int GetInput(ref int input)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.GetInput, ref input);
		}

		public int SetInput(ref int input)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.SetInput, ref input);
		}
		
		public int GetTuner(ref APIv2.v4l2_tuner tuner)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.GetTuner, ref tuner);
		}
		
		public int SetTuner(ref APIv2.v4l2_tuner tuner)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.SetTuner, ref tuner);
		}

		public int GetFormat(ref APIv2.v4l2_format fmt)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.GetFormat, ref fmt);
		}

		public int SetFormat(ref APIv2.v4l2_format fmt)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.SetFormat, ref fmt);
		}

		public int GetOutput(ref int output)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.GetOutput, ref output);
		}

		public int SetOutput(ref int output)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.SetOutput, ref output);
		}

		public int QueryInputStandard(ref APIv2.v4l2_std_id std)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int EnumerateFormats(ref APIv2.v4l2_fmtdesc fmt)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.EnumerateFormats, ref fmt);
		}

		public int QuerySlicedVBICapabilities(ref APIv2.v4l2_sliced_vbi_cap cap)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int GetControl(ref APIv2.v4l2_control control)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int SetControl(ref APIv2.v4l2_control control)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int GetAccessPriority(ref APIv2.v4l2_priority priority)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int SetAccessPriority(ref APIv2.v4l2_priority priority)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int WriteDebugInformation()
		{
			return -1; // FIXME: Unimplemented;
		}

		public int GetModulator(ref APIv2.v4l2_modulator mod)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int SetModulator(ref APIv2.v4l2_modulator mod)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int GetCroppingRect(ref APIv2.v4l2_crop crop)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int SetCroppingRect(ref APIv2.v4l2_crop crop)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int GetAudioInput(ref APIv2.v4l2_audio input)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.GetAudioInput, ref input);
		}

		public int SetAudioInput(ref APIv2.v4l2_audio input)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.SetAudioInput, ref input);
		}

		public int GetAudioOutput(ref APIv2.v4l2_audioout output)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.GetAudioOutput, ref output);
		}

		public int SetAudioOutput(ref APIv2.v4l2_audioout output)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.SetAudioOutput, ref output);
		}

		public int EnumerateAudioInputs(ref APIv2.v4l2_audio input)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.EnumerateAudioInputs, ref input);
		}

		public int EnumerateAudioOutputs(ref APIv2.v4l2_audioout output)
		{
			return ioctl(deviceHandle, APIv2.v4l2_operation.EnumerateAudioOutputs, ref output);
		}

		public int QueryCroppingCapabilities(ref APIv2.v4l2_cropcap cap)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int GetFrameBufferOverlay(ref APIv2.v4l2_framebuffer fbov)
		{
			return -1; // FIXME: Unimplemented;
		}

		public int SetFrameBufferOverlay(ref APIv2.v4l2_framebuffer fbov)
		{
			return -1; // FIXME: Unimplemented;
		}
		
		/**************************************************/
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_capability argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_audio argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_audioout argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_buffer argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_buf_type argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_fmtdesc argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_format argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_frequency argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_input argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_output argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_requestbuffers argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_standard argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_tuner argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref APIv2.v4l2_std_id argp);
		
		[DllImport("libc", SetLastError=true)]
		private static extern int
			ioctl(int device, APIv2.v4l2_operation request, ref int argp);
	}
}