using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSRouting
{
    class Message
    {
        private Router router;
        private string destination;
        private string content;

        public Message(Router router, string dest, string content)
        {
            this.router = router;
            destination = dest;
            this.content = content;
        }

        public void run()
        {
            router.SendMessage(destination, content);
        }
    }
}
