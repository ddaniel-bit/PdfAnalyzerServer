using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PdfAnalyzerServer
{
    class Program
    {
        private static readonly string[] Responses = { "a", "b", "c", "d" };
        private static readonly Random Random = new Random();

        static void Main(string[] args)
        {
            // Beállítások a szerverhez
            int port = 8080;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, port);

            server.Start();
            Console.WriteLine("Server started...");

            while (true)
            {
                // Kapcsolódó kliens elfogadása
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                // Üzenet fogadása a kliensből
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedTitle = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received title: {receivedTitle}");

                // Véletlenszerű válasz kiválasztása
                string randomResponse = Responses[Random.Next(Responses.Length)];
                byte[] responseBytes = Encoding.ASCII.GetBytes(randomResponse);

                // Válasz küldése vissza a kliensnek
                stream.Write(responseBytes, 0, responseBytes.Length);
                Console.WriteLine($"Sent response: {randomResponse}");

                // Kapcsolat lezárása
                client.Close();
            }
        }
    }
}
