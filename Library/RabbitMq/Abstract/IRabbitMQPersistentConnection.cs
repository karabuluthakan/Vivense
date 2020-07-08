using System;
using RabbitMQ.Client;

namespace Library.RabbitMq.Abstract
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}