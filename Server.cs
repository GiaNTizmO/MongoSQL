using MongoSQL.Packets;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Server
{
    private TcpListener listener;

    public Server(string ip, int port)
    {
        listener = new TcpListener(IPAddress.Parse(ip), port);
        listener.Start();
        Listen();
    }

    public void Listen()
    {
        while (listener.Server.IsBound)
        {
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Awaiting connection...");
            var tcpClient = listener.AcceptTcpClient();
            Console.WriteLine("\n[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Getted new connection! CLIENT: " + tcpClient.Client.RemoteEndPoint.ToString());
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Sending Greeting Packet...");
            MongoSQL.Packets.Greeting.SendGreeting(tcpClient.GetStream());
            ThreadPool.QueueUserWorkItem(state => HandleDeivce(tcpClient));
        }
    }

    public void HandleDeivce(TcpClient tcpClient)
    {
        try
        {
            var binaryWriter = new BinaryWriter(tcpClient.GetStream());
            var binaryReader = new BinaryReader(tcpClient.GetStream());
            Thread.Sleep(1);
            MongoSQL.Packets.PacketRouter.WritePackets(tcpClient.GetStream(), Response.showDatabases());
            /*
            var binmessage = binaryReader.ReadBytes(999);
                Console.WriteLine("Received Bytes: ", binmessage + "\n");

            var message = streamReader.ReadLine();
        Console.WriteLine("Received Stream: ", message + "\n");

        string response = "done";
        */
            //Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "{1}: Sent: {0}", response, Thread.CurrentThread.ManagedThreadId);
        }
        catch (Exception e)
        {
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Error: " + e.Message);
        }
    }
}