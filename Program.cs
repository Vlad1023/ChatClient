using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Input adress");
                string adress = Console.ReadLine();
                Console.WriteLine("Input port");
                int port = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Input name");
                string name = Console.ReadLine();
                var client = new TcpClient(adress, port);
                Console.WriteLine("Tcp client started");
                byte[] writeData = System.Text.Encoding.ASCII.GetBytes(name);
                client.GetStream().Write(writeData, 0, writeData.Length);
                while (true)
                {
                    if (client.Available > 0)
                    {
                        var responceData = new byte[1000];
                        int bytesRead = client.GetStream().Read(responceData, 0, responceData.Length);
                        var responceMessage = System.Text.Encoding.ASCII.GetString(responceData, 0, bytesRead);
                        Console.WriteLine(responceMessage);
                    }
                    if (Console.KeyAvailable == true)
                    {
                        string message = Console.ReadLine();
                        message += String.Format("\n{0}", name);
                        byte[] writeMessage = System.Text.Encoding.ASCII.GetBytes(message);
                        client.GetStream().Write(writeMessage, 0, writeMessage.Length);
                    }       
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error - {0}",e.Message);
            }
            Console.ReadLine();
        }
    }
}
