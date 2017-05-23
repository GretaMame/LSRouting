using System;
using System.Collections.Generic;
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
        public List<Router> Neighbors { private set; get; }
        public RouterNetwork network { private set; get; }
        private List<int> packets;
        public Dictionary<string, string> connections { private set; get; }


        public Router(string name)
        {
            this.Name = name;
            Neighbors = new List<Router>();
            network = new RouterNetwork(20);
            network.addLink(Name);
            packets = new List<int>();
            connections = new Dictionary<string, string>();
            connections.Add(Name, Name);

        }

        public void RemoveRouter(Router router)
        {
            Neighbors.Remove(router);
            network.removeEdge(router.Name);
            ReceiveLSP(new LSPacket(LSPacket.GetCounter(), Name, network));
        }

        public void RemoveLink(Router router)
        {
            Neighbors.Remove(router);
            network.removeLink(Name, router.Name);
            ReceiveLSP(new LSPacket(LSPacket.GetCounter(), Name, network));
        }
        public void AddNeighbour(Router router)
        {
            Neighbors.Add(router);
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
            foreach (Router router in Neighbors)
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
            Console.WriteLine("The message is at router {0}", Name);
            Thread.Sleep(3000);
            if (destination.Equals(Name))
            {
                Console.WriteLine("The message received destination router!");
            }
            else
            {
                string sendTo;
                connections.TryGetValue(destination, out sendTo);
                foreach (Router router in Neighbors)
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
            Neighbors.Add(neighbor);
            neighbor.AddNeighbour(this);
            network.addLink(neighbor.Name);
            network.setLink(Name, neighbor.Name, cost);
            ReceiveLSP(GeneratePacket(neighbor));
        }

    }
}
