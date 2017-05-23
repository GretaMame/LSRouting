using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSRouting
{
    //the graph of the whole network
    class Network
    {
        private List<Router> routers;

        public Network()
        {
            routers = new List<Router>();
        }

        public bool AddRouter(string name)
        {
            routers.Add(new Router(name));
            return true;
        }

        public bool RemoveRouter(string name)
        {
            foreach(Router router in routers)
            {
                if (router.Name.Equals(name))
                {
                    foreach(Router r in router.Neighbors)
                    {
                        r.RemoveRouter(router);
                    }
                    routers.Remove(router);
                    return true;
                }
            }
            return false;
        }

        public bool AddLink(string router1, string router2, int cost)
        {
            foreach(Router r1 in routers)
            {
                if (r1.Equals(router1))
                {
                    foreach(Router r2 in routers)
                    {
                        if (r2.Equals(router2))
                        {
                            r1.AddLink(r2, cost);
                        }
                    }
                }
            }
            return false;
        }

        public Dictionary<string, string> GetList(string source)
        {
            foreach (Router router in routers)
            {
                if (source.Equals(router.Name))
                {
                    return router.connections;
                }
            }
            return null;
        }

        public bool RemoveLink(string router1, string router2)
        {
            foreach (Router r1 in routers)
            {
                if (router1.Equals(r1.Name))
                {
                    foreach (Router r2 in routers)
                    {
                        if (router2.Equals(r2.Name))
                        {
                            r1.RemoveLink(r2);
                            r2.RemoveLink(r1);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool SendMessage(string sender, string receiver, string message)
        {
            foreach (Router router in routers)
            {
                if (sender.Equals(router.Name))
                {
                    Message toSend = new Message(router, receiver, message);
                    Thread send = new Thread(toSend.run);
                    send.Start();
                    return true;
                }
            }
            return false;
        }
    }
}
