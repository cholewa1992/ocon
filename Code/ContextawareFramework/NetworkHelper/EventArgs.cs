using System;
using System.IO;
using System.Net;

namespace NetworkHelper
{
    public class EntityEventArgs : EventArgs
    {
        public string Message { set; get; }
        internal EntityEventArgs(string msg)
        {
            Message = msg;
        }
    }

    public class IncommingSituationEventArgs : EventArgs
    {
        public Stream Stream { set; get; }
        internal IncommingSituationEventArgs(Stream stream)
        {
            Stream = stream;
        }
    }

    public class HandshakeEventArgs : EventArgs
    {
        public IPEndPoint Ipep { set; get; }
        public Guid Guid { set; get; }

        internal HandshakeEventArgs(IPEndPoint ipep, Guid guid)
        {
            Ipep = ipep;
            Guid = guid;
        }
    }

    /// <summary>
    /// Custom EventArgs to use for notification about new Widgets
    /// </summary>
    public class ContextFilterEventArgs : EventArgs
    {
        public Peer Peer { get; set; }

        internal ContextFilterEventArgs(IPEndPoint ipep)
        {
            Peer = new Peer { IpEndPoint = ipep };
        }
    }
}