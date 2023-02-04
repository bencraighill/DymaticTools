using System;
using System.ComponentModel.Design;
using System.IO;
using Mono.Debugging.Client;
using Mono.Debugging.Soft;
using Mono.Debugger.Soft;

namespace DymaticToolsVSIX.Debugging
{
	internal class DymaticDebuggerSession : SoftDebuggerSession
	{
		private bool m_IsAttached;
		private MenuCommand m_AttachToDymaticEditorMenuItem;

		/*internal DymaticDebuggerSession(MenuCommand attachToDymaticEditorMenuItem)
		{
			m_AttachToDymaticEditorMenuItem = attachToDymaticEditorMenuItem;
		}*/

		protected override void OnRun(DebuggerStartInfo startInfo)
		{
			var dymaticStartInfo = startInfo as DymaticStartInfo;

			switch (dymaticStartInfo.SessionType)
			{
			case DymaticSessionType.PlayInEditor:
				break;
			case DymaticSessionType.AttachDymaticEditorDebugger:
			{
				m_IsAttached = true;
				base.OnRun(dymaticStartInfo);
				break;
			}
			default:
				throw new ArgumentOutOfRangeException(dymaticStartInfo.SessionType.ToString());
			}
		}

		protected override void OnConnectionError(Exception ex)
		{
			// The session was manually terminated
			if (HasExited)
			{
				base.OnConnectionError(ex);
				return;
			}

			if (ex is VMDisconnectedException || ex is IOException)
			{
				HasExited = true;
				base.OnConnectionError(ex);
				return;
			}

			string message = "An error occured when trying to attach to DymaticEditor. Please make sure that DymaticEditor is running and that it's up-to-date.";
			message += Environment.NewLine;
			message += string.Format("Message: {0}", ex != null ? ex.Message : "No error message provided.");

			if (ex != null)
			{
				message += Environment.NewLine;
				message += string.Format("Source: {0}", ex.Source);
				message += Environment.NewLine;
				message += string.Format("Stack Trace: {0}", ex.StackTrace);

				if (ex.InnerException != null)
				{
					message += Environment.NewLine;
					message += string.Format("Inner Exception: {0}", ex.InnerException.ToString());
				}
			}
			
			_ = DymaticToolsPackage.Instance.ShowErrorMessageBoxAsync("Connection Error", message);
			base.OnConnectionError(ex);
		}

		protected override void OnExit()
		{
			if (m_IsAttached)
			{
				m_IsAttached = false;
				base.OnDetach();
			}
			else
			{
				base.OnExit();
			}
		}
	}
}
