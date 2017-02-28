using System;
using Delegate.Tera.Common.game;

namespace Delegate.Tera.Network.Messaging
{
    public class MessageSplitter
    {
        public event Action<Message> MessageReceived;
        private readonly BlockSplitter _clientSplitter = new BlockSplitter();
        private readonly BlockSplitter _serverSplitter = new BlockSplitter();
        private DateTime _time;

        public MessageSplitter()
        {
            _clientSplitter.BlockFinished += ClientBlockFinished;
            _serverSplitter.BlockFinished += ServerBlockFinished;
        }

        private void ClientBlockFinished(byte[] block)
        {
            OnMessageReceived(new Message(_time, MessageDirection.ClientToServer, new ArraySegment<byte>(block)));
        }

        private void ServerBlockFinished(byte[] block)
        {
            OnMessageReceived(new Message(_time, MessageDirection.ServerToClient, new ArraySegment<byte>(block)));
        }

        protected void OnMessageReceived(Message message)
        {
            Action<Message> handler = MessageReceived;
            handler?.Invoke(message);
        }

        public void ClientToServer(DateTime time, byte[] data)
        {
            _time = time;
            _clientSplitter.Data(data);
            _clientSplitter.PopAllBlocks();
        }

        public void ServerToClient(DateTime time, byte[] data)
        {
            _time = time;
            _serverSplitter.Data(data);
            _serverSplitter.PopAllBlocks();
        }
    }
}
