using HostingAdapter.Hosting.Contracts;

namespace DebuggerProtocolAdapter.Hosting.Contracts
{
    public class TerminateEvent
    {
        public static readonly
           EventType<TerminateParams> Type =
           EventType<TerminateParams>.Create("terminated");
    }

    public class TerminateParams
    {
        /// <summary>
        /// A debug adapter may set 'restart' to true to request that the front end restarts the session.
        /// </summary>
        public bool Restart
        {
            get;set;
        }

    }

    
}
