using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace DymaticToolsVSIX
{
	public class DymaticToolsGeneralOptions : DialogPage
	{

		[Category("Debugging")]
		[DisplayName("Connection Port")]
		[Description("The port that DymaticEditor is expected to use for the debugger. This value should match the Debugger Port value in the DymaticEditor project settings")]
		public int ConnectionPort { get; set; } = 2550;

		[Category("Debugging")]
		[DisplayName("Maximum Connection Attempts")]
		[Description("Determines how many connection attempts Dymatic Tools can make if it fails to attach to DymaticEditor (0 means inifite attempts. Default: 1)")]
		public int MaxConnectionAttempts { get; set; } = 1;

	}
}
