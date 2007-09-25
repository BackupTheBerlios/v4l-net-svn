using System;

namespace Video4Linux.APIv2
{
	public enum v4l2_input_status : uint
	{
		// General
		NoPower              = 0x00000001, // Attached device is off.
		NoSignal             = 0x00000002,
		NoColor              = 0x00000004, // The hardware supports color decoding, but does not detect color modulation in the signal.
			
		// Analog Video
		NoHorizontalSyncLock = 0x00000100, // No horizontal sync lock.
		ColorKill            = 0x00000200, // A color killer circuit automatically disables color decoding when it detects no color modulation. When this flag is set the color killer is enabled and has shut off color decoding.

		// Digital Video
		NoSyncLock           = 0x00010000, // No synchronization lock.
		NoEqualizerLock      = 0x00020000, // No equalizer lock.
		NoCarrier            = 0x00040000, // Carrier recovery failed.
		
		// VCR and Set-Top Box
		Macrovision          = 0x01000000, // Macrovision is an analog copy prevention system mangling the video signal to confuse video recorders. When this flag is set Macrovision has been detected.
		NoAccess             = 0x02000000, // Conditional access denied.
		VTR                  = 0x04000000 // VTR time constant. [?]
	}
}
