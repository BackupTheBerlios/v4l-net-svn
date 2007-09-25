using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_operation : int
	{
		// HACK: check for _IOR/_IOWR
		QueryCapabilities     = -2140645888, // VIDIOC_QUERYCAP
		GetStandard           = -2146937321, // VIDIOC_G_STD
		SetStandard           =  1074288152, // VIDIOC_S_STD
		GetInput              = -2147199450, // VIDIOC_G_INPUT
		SetInput              = -1073457625, // VIDIOC_S_INPUT
		GetOutput             = -2147199442, // VIDIOC_G_OUTPUT
		SetOutput             = -1073457617, // VIDIOC_S_OUTPUT
		GetAudioInput         = -2144053727, // VIDIOC_G_AUDIO
		SetAudioInput         =  1077171746, // VIDIOC_S_AUDIO
		GetAudioOutput        = -2144053711, // VIDIOC_G_AUDOUT
		SetAudioOutput        =  1077171762, // VIDIOC_S_AUDOUT
		GetFormat             = -1060350460, // VIDIOC_G_FMT
		SetFormat             = -1060350459, // VIDIOC_S_FMT
		GetTuner              = -1068214755, // VIDIOC_G_TUNER
		SetTuner              =  1079268894, // VIDIOC_S_TUNER
		GetFrequency          = -1070836168, // VIDIOC_G_FREQUENCY
		SetFrequency          =  1076647481, // VIDIOC_S_FREQUENCY
		GetFramebuffer        = -2144578038, // VIDIOC_G_FBUF
		SetFramebuffer        =  1076647435, // VIDIOC_S_FBUF
		RequestBuffers        = -1072409080, // VIDIOC_REQBUFS
		QueryBuffer           = -1069263351, // VIDIOC_QUERYBUF
		StreamingOn           =  1074026002, // VIDIOC_STREAMON
		StreamingOff          =  1074026003, // VIDIOC_STREAMOFF
		EnqueueBuffer         = -1069263345, // VIDIOC_QBUF
		DequeueBuffer         = -1069263343, // VIDIOC_DQBUF
		EnumerateInputs       = -1068739046, // VIDIOC_ENUMINPUT
		EnumerateOutputs      = -1069001168, // VIDIOC_ENUMOUTPUT
		EnumerateAudioInputs  = -1070311871, // VIDIOC_ENUMAUDIO
		EnumerateAudioOutputs = -1070311870, // VIDIOC_ENUMAUDOUT
		EnumerateStandards    = -1069525479, // VIDIOC_ENUMSTD
		EnumerateFormats      = -1069525502  // VIDIOC_ENUM_FMT
	}
}
