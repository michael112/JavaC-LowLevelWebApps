// Most based on: https://stackoverflow.com/questions/2068367/using-httpwebrequest-to-send-html-to-a-browser
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcp_port_listener
{
    class Program
    {
        public static void Main()
        {
            try
            {
                int port = 8080;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                TcpListener server = new TcpListener(localAddr, port);
                server.Start();
                while( true )
                {
                    Console.WriteLine("Waiting for a connection (listening on port " + port + ")...");
                    TcpClient client = server.AcceptTcpClient();
                    using( NetworkStream stream = client.GetStream() )
                    using( StreamReader streamReader = new StreamReader(stream) )
                    {
                        List<byte> requestContent = new List<byte>();
                        string requestLine;
                        while( ( requestLine = streamReader.ReadLine() ) != "" )
                        {
                            requestContent.AddRange(System.Text.Encoding.ASCII.GetBytes(requestLine + "\n"));
                        }
                        Console.WriteLine("<request>\n" + Encoding.Default.GetString(requestContent.ToArray()) + "</request>");
                        using( StreamWriter streamWriter = new StreamWriter(stream) )
                        {
                            string responseContent = "";
                            responseContent += "HTTP/1.1 200 OK" + "\n";
                            responseContent += "Content-Type: application/json" + "\n";
                            responseContent += "\n";
                            responseContent += "{" + "\n" + "\"imie\": \"Michał\"," + "\n" + "\"nazwisko\": \"Choromański\"," + "\n" + "\"wydzial\": \"Elektryczny\"," + "\n" + "\"kierunek\": \"Informatyka stosowana\"," + "\n" + "\"poziom\": \"magisterskie\"" + "\n" + "}";
                            responseContent += "\n";
                            streamWriter.Write(responseContent);
                            Console.Write("<response>\n" + responseContent + "\n</response>\n");
                        }
                    }
                    client.Close();
                }
            }
            catch( SocketException e )
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.Read();
        }
    }
}
