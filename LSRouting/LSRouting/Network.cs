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
        private Dictionary<string, int> routers;
        int freeIndex;
        private int MAX_ROUTERS_AMOUNT;

        public Network(int maxRouters)
        {
            MAX_ROUTERS_AMOUNT = maxRouters;
            routers = new Dictionary<string, int>();
            freeIndex = 0;
        }

        public bool AddRouter(string name)
        {
            if (routers.ContainsKey(name))
            {
                return false;
            }
            routers.Add(name, freeIndex++);
            return true;
        }

        //reikia ir linkus istrint
        public bool RemoveRouter(string name)
        {
            if (routers.ContainsKey(name))
            {
                //istrinam linkus
                for (int i = 0; i<MAX_ROUTERS_AMOUNT; i++)
                {
                    links[routers[name]][i] = 0;
                    links[i][routers[name]] = 0;
                }
                //istrinam routeri
                routers.Remove(name);
                return true;
            }
            else  return false;
        }

        public bool AddLink(string router1, string router2, int cost)
        {
            if (routers.ContainsKey(router1) && routers.ContainsKey(router2))
            {
                links[routers[router1]][routers[router2]] = cost;
                links[routers[router2]][routers[router1]] = cost;
                return true;
            }
            else return false;
        }

        public bool RemoveLink(string router1, string router2)
        {
            if (routers.ContainsKey(router1) && routers.ContainsKey(router2))
            {
                links[routers[router1]][routers[router2]] = 0;
                links[routers[router2]][routers[router1]] = 0;
                return true;
            }
            else return false;
        }



    }
}
