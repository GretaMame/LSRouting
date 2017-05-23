using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSRouting
{
    class LSPacket
    {
        //identification of the sender of the LSP
        public string Router { private set; get; }
        //sequence number of the LSP
        public int Seq { private set; get; }
        //links advertised in the LSP
        public RouterNetwork Links { private set; get; }
        private static int counter = 0;

        public LSPacket(int seq, string router, RouterNetwork links)
        {
            this.Router = router;
            this.Seq = seq;
            this.Links = links;
        }

        public static int GetCounter()
        {
            return counter++;
        }
    }
}
