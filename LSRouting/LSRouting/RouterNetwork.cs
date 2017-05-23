using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSRouting
{
    class RouterNetwork
    {
        private int[][] links;
        private IDictionary<string, int> map;
        private LinkedList<int> freeCells;
        private int freeIndex;

        private int MAX_ROUTERS_AMOUNT;

        public RouterNetwork(int maxRouters)
        {
            MAX_ROUTERS_AMOUNT = maxRouters;
            freeIndex = 0;
            links = new int[maxRouters][];
            for (int i = 0; i < maxRouters; i++)
            {
                links[i] = new int[maxRouters];
            }

            map = new Dictionary<string, int>();
            freeCells = new LinkedList<int>();

            for (int i = 0; i < maxRouters; i++)
            {
                for (int j = 0; j < maxRouters; j++)
                {
                    links[i][j] = 0;
                }
            }
        }

        public virtual int Size
        {
            get
            {
                return map.Count;
            }
        }

        public string[] getLinks()
        {
            string[] router = new string[map.Count];
            int i = 0;
            foreach (string @var in map.Keys)
            {
                router[i] = @var;
                i++;
            }
            return router;
        }

        public virtual bool addLink(string router)
        {
            int index;
            if (map.ContainsKey(router))
            {
                return false;
            }
            if (freeCells.Count == 0)
            {
                index = freeIndex;
                map[router] = index;
                freeIndex++;
            }
            else
            {
                index = freeCells.First.Value;
                freeCells.RemoveFirst();
                map[router] = index;
            }
            return true;
        }

        public virtual bool removeEdge(string router)
        {
            int index;
            if (map.ContainsKey(router))
            {
                map.TryGetValue(router, out index);
                map.Remove(router);
                freeCells.AddLast(index);
                for (int i = 0; i < links[index].Length; i++)
                {
                    links[index][i] = 0;
                    links[i][index] = 0;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool setLink(string source, string dest, int weight)
        {
            if (map.ContainsKey(source) && map.ContainsKey(dest))
            {
                links[map[source]][map[dest]] = weight;
                links[map[dest]][map[source]] = weight;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool removeLink(string source, string dest)
        {
            if (map.ContainsKey(source) && map.ContainsKey(dest))
            {
                links[map[source]][map[dest]] = 0;
                links[map[dest]][map[source]] = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual int getWeight(string source, string dest)
        {
            if (map.ContainsKey(source) && map.ContainsKey(dest))
            {
                return links[map[source]][map[dest]];
            }
            else
            {
                return -1;
            }
        }

        public virtual string[] getNeighbors(string router)
        {
            int[] numbers;
            string[] neighbors;
            if (map.ContainsKey(router))
            {
                int count = 0;
                int index;
                map.TryGetValue(router, out index);
                for (int i = 0; i < links.Length; i++)
                {
                    if (links[index][i] > 0)
                    {
                        count++;
                    }
                }
                numbers = new int[count];
                neighbors = new string[count];
                count = 0;
                for (int i = 0; i < links.Length; i++)
                {
                    if (links[index][i] > 0)
                    {
                        numbers[count++] = i;
                    }
                }
                count = 0;
                foreach (string id in map.Keys)
                {
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if (numbers[i] == map[id])
                        {
                            neighbors[count++] = id;
                            break;
                        }
                    }
                }
                return neighbors;
            }
            else
            {
                return null;
            }
        }

        public void printNetwork()
        {
            Console.Write("    |");
            for (int i = 0; i < MAX_ROUTERS_AMOUNT; i++)
            {
                Console.Write("{0,-4}|", i);
            }
            Console.WriteLine();
            for (int i = 0; i < MAX_ROUTERS_AMOUNT; i++)
            {
                Console.Write("{0,4}|", i);
                for (int j = 0; j < MAX_ROUTERS_AMOUNT; j++)
                {
                    Console.Write("{0,4}|", links[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}

