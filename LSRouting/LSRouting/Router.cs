using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSRouting
{
    class Router
    {
        public string Name { private set; get; }
        private List<Router> neighbours;



        public Router(string name)
        {
            this.Name = name;
            neighbours = new List<Router>();

        }

        public void AddNeighbour(Router router)
        {
            neighbours.Add(router);
        }


        //pasalinus perskaiciuot reikia paketus, issiust lsp nauja
        public void RemoveNeighbour(Router toRemove)
        {
            neighbours.Remove(toRemove);


        }

        public void SendLSP()
        {

        }

        public void ReceiveLSP()
        {

        }

        public void SendMessage()
        {

        }

    }
}
