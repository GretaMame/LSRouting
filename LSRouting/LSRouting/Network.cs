using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSRouting
{
    //the graph of the whole network
    class Network
    {
        private int[][] links;
        private Dictionary<int, string> routers;

        public Network()
        {
            routers = new Dictionary<int, string>();
        }

        public bool AddRouter(string name)
        {
            return true;
        }

        public bool RemoveRouter(string name)
        {
            return true;
        }

        public bool AddLink(string router1, string router2, int cost)
        {
            return true;
        }

        public bool RemoveLink(string router1, string router2)
        {
            return true;
        }



    }
}
