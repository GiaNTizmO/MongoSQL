
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public static class StreamHelpers
{
    public static byte[] ReadAllBytes(this BinaryReader reader)
    {
        // Pre .Net version 4.0
        const int bufferSize = 4096;
        using (var ms = new MemoryStream())
        {
            byte[] buffer = new byte[bufferSize];
            int count;
            while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                ms.Write(buffer, 0, count);
            return ms.ToArray();
        }
    }
}
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
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Ожидание подключений...");
            var tcpClient = listener.AcceptTcpClient();

            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Полученно входящее соединение!");
            ThreadPool.QueueUserWorkItem(state => HandleDeivce(tcpClient));
        }
    }
    public void HandleDeivce(TcpClient tcpClient)
    {
        try
        {
        var streamReader = new StreamReader(tcpClient.GetStream());
        var streamWriter = new StreamWriter(tcpClient.GetStream());
        var binaryWriter = new BinaryWriter(tcpClient.GetStream());
        var binaryReader = new BinaryReader(tcpClient.GetStream());
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Sending Greeting Packet...");
            binaryWriter.Write(new byte[]
                 {
                     // Offset 0x00000000 to 0x00000109
	0x6A, 0x00, 0x00, 0x00, 0x0A, 0x35, 0x2E, 0x35, 0x2E, 0x35, 0x2D, 0x31,
    0x30, 0x2E, 0x33, 0x2E, 0x32, 0x35, 0x2D, 0x4D, 0x6F, 0x6E, 0x67, 0x6F,
    0x53, 0x51, 0x4C, 0x2D, 0x41, 0x6C, 0x70, 0x68, 0x61, 0x2D, 0x30, 0x2E,
    0x31, 0x32, 0x2E, 0x33, 0x34, 0x2E, 0x31, 0x00, 0x92, 0x00, 0x00, 0x00,
    0x7E, 0x77, 0x5C, 0x45, 0x25, 0x28, 0x36, 0x79, 0x00, 0xFE, 0xF7, 0x2D,
    0x02, 0x00, 0xBF, 0x81, 0x15, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x07,
    0x00, 0x00, 0x00, 0x7E, 0x2E, 0x68, 0x42, 0x50, 0x4D, 0x71, 0x6F, 0x5C,
    0x5F, 0x49, 0x30, 0x00, 0x6D, 0x79, 0x73, 0x71, 0x6C, 0x5F, 0x6E, 0x61,
    0x74, 0x69, 0x76, 0x65, 0x5F, 0x70, 0x61, 0x73, 0x73, 0x77, 0x6F, 0x72,
    0x64, 0x00
                 });
            binaryWriter.Flush();
            Thread.Sleep(1);
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Sending Okay Auth Packet...");
            binaryWriter.Write(new byte[]
                 {
                 0x07, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x01, 0x00
                 });
            binaryWriter.Flush();
            Thread.Sleep(500);
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Sending Version Comment Packet...");
            binaryWriter.Write(new byte[]
                 {
	// Offset 0x00000000 to 0x00000101
	0x01, 0x00, 0x00, 0x01, 0x01, 0x27, 0x00, 0x00, 0x02, 0x03, 0x64, 0x65,
    0x66, 0x00, 0x00, 0x00, 0x11, 0x40, 0x40, 0x76, 0x65, 0x72, 0x73, 0x69,
    0x6F, 0x6E, 0x5F, 0x63, 0x6F, 0x6D, 0x6D, 0x65, 0x6E, 0x74, 0x00, 0x0C,
    0x24, 0x00, 0x1F, 0x00, 0x00, 0x00, 0xFD, 0x00, 0x00, 0x27, 0x00, 0x00,
    0x05, 0x00, 0x00, 0x03, 0xFE, 0x00, 0x00, 0x02, 0x00, 0x20, 0x00, 0x00,
    0x04, 0x1F, 0x67, 0x69, 0x74, 0x68, 0x75, 0x62, 0x2E, 0x63, 0x6F, 0x6D,
    0x2F, 0x47, 0x69, 0x61, 0x4E, 0x54, 0x69, 0x7A, 0x6D, 0x4F, 0x2F, 0x4D,
    0x6F, 0x6E, 0x67, 0x6F, 0x53, 0x51, 0x4C, 0x2F, 0x20, 0x05, 0x00, 0x00,
    0x05, 0xFE, 0x00, 0x00, 0x02, 0x00
                 });
            binaryWriter.Flush();
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
