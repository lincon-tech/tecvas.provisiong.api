using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Techrunch.TecVas.Fullfillment.Common.Models;

namespace Techrunch.TecVas.Fullfillment.Common.Services
{
    public interface IRabbitMqService
    {
        IConnection CreateChannel();
    }

    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                UserName = _configuration.Username,
                Password = _configuration.Password,
                HostName = _configuration.HostName,
                Port = _configuration.Port,
                VirtualHost = _configuration.VirtualHost,
                AutomaticRecoveryEnabled = true
            };
            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
        }
    }
}
