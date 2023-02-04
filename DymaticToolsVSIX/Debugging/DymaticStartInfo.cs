using EnvDTE;
using Mono.Debugging.Soft;
using Mono.Debugging.VisualStudio;

namespace DymaticToolsVSIX.Debugging
{
    public enum DymaticSessionType
    {
        PlayInEditor = 0,
        AttachDymaticEditorDebugger
    }

    internal class DymaticStartInfo : StartInfo
    {
        public readonly DymaticSessionType SessionType;

        public DymaticStartInfo(SoftDebuggerStartArgs args, DebuggingOptions options, Project startupProject, DymaticSessionType sessionType)
            : base(args, options, startupProject)
        {
            SessionType = sessionType;
        }
    }
}
