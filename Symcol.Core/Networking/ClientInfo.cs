using System;

namespace Symcol.Core.Networking
{
    /// <summary>
    /// Just a client signature
    /// </summary>
    [Serializable]
    public class ClientInfo
    {
        public string IP;

        public int Port;

        public int Ping;

        public int ConncetionTryCount;

        public double LastConnectionTime;
    }
}
