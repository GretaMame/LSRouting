using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSRouting
{
    class Router
    {
        public string Name { private set; get; }
        private List<Router> neighbors;
        public RouterNetwork network { private set; get; }
        private List<int> packets;
        public Dictionary<string, string> connections { private set; get; }


        public Router(string name)
        {
            this.Name = name;
            neighbors = new List<Router>();
            network = new RouterNetwork(20);
            network.addLink(Name);
            packets = new List<int>();
            connections = new Dictionary<string, string>();
            connections.Add(Name, Name);

        }

        public void RemoveRouter(Router router)
        {
            neighbors.Remove(router);
            network.removeEdge(router.Name);
            ReceiveLSP(new LSPacket(LSPacket.GetCounter(), Name, network));
        }

        public void RemoveLink(Router router)
        {
            neighbors.Remove(router);
            network.removeLink(Name, router.Name);
            ReceiveLSP(new LSPacket(LSPacket.GetCounter(), Name, network));
        }
        public void AddNeighbor(Router router)
        {
            neighbors.Add(router);
        }

        public Router[] GetNeighbors()
        {
            return neighbors.ToArray();
        }

        private LSPacket GeneratePacket(Router router)
        {
            RouterNetwork secondNetwork = router.network;

            foreach (string edge in secondNetwork.getLinks())
            {
                network.addLink(edge);
                foreach (string neighbor in secondNetwork.getNeighbors(edge))
                {
                    network.addLink(neighbor);
                    network.setLink(edge, neighbor, secondNetwork.getWeight(edge, neighbor));
                }
            }

            return new LSPacket(LSPacket.GetCounter(), Name, network);
        }

        public void SendLSP(LSPacket packet)
        {
            foreach (Router router in neighbors)
            {
                router.ReceiveLSP(packet);
            }
        }

        public void ReceiveLSP(LSPacket packet)
        {
            if (!packets.Contains(packet.Seq))
            {
                packets.Add(packet.Seq);
                network = packet.Links;
                connections = Dijkstra.dijkstra(network, Name);
                SendLSP(packet);
            }
        }

        public void SendMessage(string destination, string content)
        {
            printNeighbors();
            Console.WriteLine("The message is at router {0}", Name);
            Thread.Sleep(5000);
            if (destination.Equals(Name))
            {
                Console.WriteLine("The message received destination router!");
            }
            else
            {
                string sendTo;
                connections.TryGetValue(destination, out sendTo);
                foreach (Router router in neighbors)
                {
                    if (sendTo != null && sendTo.Equals(router.Name))
                    {
                        router.SendMessage(destination, content);
                        break;
                    }
                }
            }
        }

        public void AddLink(Router neighbor, int cost)
        {
            neighbors.Add(neighbor);
            neighbor.AddNeighbor(this);
            network.addLink(neighbor.Name);
            network.setLink(Name, neighbor.Name, cost);
            ReceiveLSP(GeneratePacket(neighbor));
        }

        public void printNeighbors()
        {
            foreach (Router r in neighbors)
                Debug.WriteLine(r.Name);
            
        }

    }
}
