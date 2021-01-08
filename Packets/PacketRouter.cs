using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace MongoSQL.Packets
{
    public static class PacketRouter
    {
        private static byte[] packetbuff = null;

        public static void ReadPackets(NetworkStream netstream) // TODO: Test
        {
            var binaryReader = new BinaryReader(netstream);
            packetbuff = binaryReader.ReadBytes(int.MaxValue);
            Console.WriteLine(packetbuff);
        }

        public static void WritePackets(NetworkStream netstream, byte[] packet)
        {
            var binaryWriter = new BinaryWriter(netstream);
            binaryWriter.Write(packet);
            binaryWriter.Flush();
            Console.WriteLine("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] " + "Sended packet");
        }
    }
}