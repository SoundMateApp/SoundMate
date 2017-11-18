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

        static UdpClient listener;
        static bool receive = true;

        public static void Stop()
        {
            receive = false;
            listener.Client.Close();
        }       
        
        public static void Start(ref string input)
        {

            listener = new UdpClient(DEFAULT_IP);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, DEFAULT_IP);
            
            while (receive) {
                Console.WriteLine("Started Listening");
                byte[] inputBytes = listener.Receive(ref groupEP);
                input = Encoding.ASCII.GetString(inputBytes);
            }
        }

        public static void Send()
        {
            UdpClient sender = new UdpClient();
            sender.Connect("127.0.0.1", 11000);
            Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");
            sender.Send(sendBytes, sendBytes.Length);
        }
    }
}
