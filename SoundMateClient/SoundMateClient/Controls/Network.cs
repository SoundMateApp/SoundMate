using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SoundMateClient.Controls
{
    class Network
    {
        const int DEFAULT_IP = 11000;

        UdpClient listener;
        bool receive = true;

        public void Stop()
        {
            receive = false;
            listener.Client.Close();
        }       
        
        public void Start(ref string input)
        {

            listener = new UdpClient(DEFAULT_IP);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, DEFAULT_IP);
            
            while (receive) {
                Console.WriteLine("Started Listening");
                byte[] inputBytes = listener.Receive(ref groupEP);
                input = Encoding.ASCII.GetString(inputBytes);
                Console.WriteLine(input);
            }
        }
    }
}
