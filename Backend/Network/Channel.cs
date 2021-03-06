﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Runtime.Serialization.Formatters.Binary;
using Common;

namespace Backend.Network
{
    // State object for reading client data asynchronously
    public class Channel : IChannel
    {
        private BlockingCollection<CompleteEvent> m_queue;

        public BlockingCollection<CompleteEvent> CompleteQueue
        {
            get { return m_queue; }
            set { m_queue = value; }
        }
        // Client  socket.

        public Socket Socket { get { return m_socket; } set { m_socket = value; } }

        private int message;

        private int unreadSize;

        private int bodySize;

        private bool headerFin = false;

        // Size of receive buffer.
        private const int BufferSize = 1024;
        // Receive buffer.
        private byte[] buffer = new byte[BufferSize];

        private BinaryFormatter formatter = new BinaryFormatter();

        private MemoryStream m_recvStream = new MemoryStream();

        private MemoryStream m_sendStream = new MemoryStream();

        private Dictionary<Command, MessageDelegate> onMessageRecv;

        private List<ChannelDelegate> m_onClose;

        private const int MsgHeaderSize = 4;

        private Object m_player;

        private Socket m_socket;

        public Channel(Socket socket)
        {
            m_socket = socket;
        }

        public void SetContent(Object content)
        {
            m_player = content;
        }

        public Object GetContent()
        {
            return m_player;
        }

        public void Send(Message msg)
        {
            Channel.Send(this, msg);
        }

        public void Close()
        {
            if (m_onClose == null)
            {
                return;
            }
            if (m_socket == null)
            {
                return;
            }

            foreach (ChannelDelegate @d in m_onClose)
            {
                CompleteEvent e = new CompleteEvent();
                e.@delegate = @d;
                e.channel = this;
                e.message = null;
                m_queue.Add(e);
            }
        }
        public void BeginRecv()
        {
            m_socket.BeginReceive(buffer, 0, MsgHeaderSize, 0,
                new AsyncCallback(ReadCallback), this);
        }

        public void RegisterOnMessageRecv(Dictionary<Command, MessageDelegate> delegateMap)
        {
            this.onMessageRecv = delegateMap;
        }

        public void RegisterOnClose(List<ChannelDelegate> onClose)
        {
            this.m_onClose = onClose;
        }

        public void SetQueue(BlockingCollection<CompleteEvent> queue)
        {
            m_queue = queue;
        }

        static private void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            Channel channel = (Channel)ar.AsyncState;
            Socket handler = channel.m_socket;
            try
            {
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead <= 0)
                {
                    channel.Close();
                    return;
                }
                if (channel.headerFin)
                {
                    channel.m_recvStream.Write(channel.buffer, 0, bytesRead);
                    if (bytesRead < channel.unreadSize)
                    {
                        channel.unreadSize -= bytesRead;
                        int size = Math.Min(channel.unreadSize, Channel.BufferSize);
                        handler.BeginReceive(channel.buffer, 0, size, 0,
                            new AsyncCallback(ReadCallback), channel);
                    }
                    else if (bytesRead == channel.unreadSize)
                    {
                        channel.m_recvStream.Seek(0, SeekOrigin.Begin);
                        Message message = (Message)channel.formatter.Deserialize(channel.m_recvStream);

                        MessageDelegate @delegate;
                        bool exists = channel.onMessageRecv.TryGetValue(message.command, out @delegate);
                        if (exists)
                        {
                            //@delegate.Invoke(channel, message);
                            CompleteEvent e = new CompleteEvent();
                            e.@delegate = @delegate;
                            e.channel = channel;
                            e.message = message;
                            channel.m_queue.Add(e);
                        }
                        else
                        {
                            // error, may be unregistered COMMAND
                        }

                        channel.unreadSize = MsgHeaderSize;
                        channel.headerFin = false;
                        handler.BeginReceive(channel.buffer, 0, channel.unreadSize, 0,
                            new AsyncCallback(ReadCallback), channel);
                    }
                }
                else
                {
                    channel.message = BitConverter.ToUInt16(channel.buffer, 0); ;
                    channel.unreadSize = channel.bodySize = BitConverter.ToUInt16(channel.buffer, 2) - MsgHeaderSize;
                    channel.headerFin = true;
                    channel.m_recvStream.Seek(0, SeekOrigin.Begin);
                    channel.m_recvStream.SetLength(0);
                    int size = Math.Min(channel.unreadSize, Channel.BufferSize);
                    handler.BeginReceive(channel.buffer, 0, size, 0,
                              new AsyncCallback(ReadCallback), channel);
                }
            }
            catch (SystemException)
            {
                channel.Close();
            }
        }

        private static void Send(Channel channel, Message msg)
        {
            try
            {
                MemoryStream stream = new MemoryStream();

                stream.Seek(Message.MsgHeaderSize, SeekOrigin.Begin);
                channel.formatter.Serialize(stream, msg);
                UInt16 length = (UInt16)stream.Length;
                stream.Seek(0, SeekOrigin.Begin);
                stream.Write(BitConverter.GetBytes((UInt16)msg.command), 0, 2);
                stream.Write(BitConverter.GetBytes(length), 0, 2);
                stream.Seek(0, SeekOrigin.Begin);
                // Begin sending the data to the remote device.
                channel.m_socket.BeginSend(stream.GetBuffer(), 0, (int)stream.Length, 0,
                    new AsyncCallback(SendCallback), channel);
            }
            catch (SystemException)
            {
                channel.Close();
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            Channel channel = (Channel)ar.AsyncState;
            Socket handler = channel.m_socket;
            try
            {
                // Retrieve the socket from the state object.


                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                if (bytesSent < 0)
                {
                    channel.Close();
                }
                //Console.WriteLine("Sent {0} bytes.", bytesSent);
            }
            catch (SocketException)
            {
                channel.Close();
            }
        }
    }
}
