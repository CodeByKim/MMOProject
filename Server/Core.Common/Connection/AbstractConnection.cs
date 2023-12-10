using System;
using System.Data.Common;
using System.Net;
using System.Net.Sockets;

using Google.Protobuf;

namespace Core.Common
{
    public abstract class AbstractConnection
    {
        public string ID { get; private set; }

        protected Socket _socket;

        private RingBuffer _receiveBuffer;
        private bool _isSending;
        private object _sendLock;

        private List<ArraySegment<byte>> _reservedSendList;

        public AbstractConnection()
        {
        }

        public void Send(short packetId, IMessage packet)
        {
            var header = new PacketHeader(packet, packetId);
            var buffer = PacketUtil.CreateBuffer(header, packet);

            lock (_sendLock)
            {
                _reservedSendList.Add(buffer);

                if (_isSending)
                    return;

                TrySend();
            }
        }

        public void Initialize(Socket socket, int receiveBufferSize)
        {
            _socket = socket;
            _receiveBuffer = new RingBuffer(receiveBufferSize);

            ID = Guid.NewGuid().ToString();

            _sendLock = new object();
            _reservedSendList = new List<ArraySegment<byte>>();

            _isSending = false;
        }

        public void Release()
        {
            //Logger.Info($"Release Connection: {ID}");
        }

        public async void ReceiveAsync()
        {
            var writableSegments = _receiveBuffer.GetWritable();

            try
            {
                var byteTransfer = await _socket.ReceiveAsync(writableSegments, SocketFlags.None);
                if (byteTransfer == 0)
                {
                    ForceDisconnect(DisconnectReason.RemoteClosing);
                    return;
                }

                ProcessReceive(byteTransfer);

                ReceiveAsync();

            }
            catch (Exception e)
            {
                ForceDisconnect(DisconnectReason.RemoteClosing);
            }
        }

        internal void ProcessReceive(int byteTransfer)
        {
            _receiveBuffer.FinishWrite(byteTransfer);

            if (byteTransfer == 0 || _receiveBuffer.UseSize == 0)
                return;

            while (_receiveBuffer.UseSize > 0)
            {
                PacketHeader header;
                if (!TryGetHeader(out header))
                    return;

                if (_receiveBuffer.UseSize < header.PayloadSize)
                    return;

                var packetSize = PacketHeader.HeaderSize + header.PayloadSize;
                var packetBuffer = _receiveBuffer.Peek(packetSize);
                if (packetBuffer.Array is null)
                {
                    ForceDisconnect(DisconnectReason.InvalidConnection);
                    return;
                }

                var payload = new ArraySegment<byte>(packetBuffer.Array,
                                                     packetBuffer.Offset + PacketHeader.HeaderSize,
                                                     header.PayloadSize);
                var packet = ParsePacket(header, payload);
                OnDispatchPacket(header.PacketId, packet);

                _receiveBuffer.FinishRead(packetSize);
            }
        }

        internal void ForceDisconnect(DisconnectReason reason)
        {
            //if (_socket is null)
            //    Logger.Warnning("socket is null...");
            //else
                _socket.Close();

            OnDisconnected(this, reason);
        }

        private void TrySend()
        {
            List<ArraySegment<byte>> sendList;
            sendList = _reservedSendList;

            _reservedSendList = new List<ArraySegment<byte>>();

            SendAsync(sendList);
        }

        private async void SendAsync(List<ArraySegment<byte>> sendList)
        {
            try
            {
                _isSending = true;
                await _socket.SendAsync(sendList, SocketFlags.None);
            }
            catch (Exception e)
            {
                ForceDisconnect(DisconnectReason.RemoteClosing);
            }

            lock (_sendLock)
            {
                //완료 처리
                _isSending = false;

                if (_reservedSendList.Count > 0)
                    TrySend();
            }
        }

        private bool TryGetHeader(out PacketHeader header)
        {
            header = new PacketHeader();

            if (_receiveBuffer.UseSize < PacketHeader.HeaderSize)
                return false;

            header.CopyTo(_receiveBuffer);
            return true;
        }

        private IMessage ParsePacket(PacketHeader header, ArraySegment<byte> payload)
        {
            var packet = OnResolvePacket(header.PacketId);
            if (packet is null)
            {
                //Logger.Error($"Not Found Packet Handler, PacketId: {packetId}");
                return null;
            }

            packet.MergeFrom(payload);
            return packet;
        }

        protected abstract IMessage OnResolvePacket(short packetId);

        protected abstract void OnDispatchPacket(short packetId, IMessage packet);

        protected abstract void OnDisconnected(AbstractConnection conn, DisconnectReason reason);
    }
}