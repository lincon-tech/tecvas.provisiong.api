using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using Techrunch.TecVas.Fullfillment.Common.Services;
using Microsoft.Extensions.Configuration;

namespace Techrunch.TecVas.FulFillment.EtranzactConsumer.Services
{
    public interface IConsumerService
    {
        Task ReadMessgaes();
    }

    public class ConsumerService : IConsumerService, IDisposable
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly IConfiguration _config;
        private string _queueName;
        private string _exchangeName;

        public ConsumerService(IRabbitMqService rabbitMqService, IConfiguration configuration)
        {
            _config = configuration;
            _queueName = _config.GetSection("AmqpExchange:Queue:Name").Value;
            _exchangeName = _config.GetSection("AmqpExchange:Queue:Name").Value;
            _connection = rabbitMqService.CreateChannel();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: true, autoDelete: false);
            _channel.QueueBind(_queueName, _exchangeName, string.Empty);
        }
        //const string _queueName = "mtn.queue";
        public async Task ReadMessgaes()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                ProcessMessage(body);
                //var text = System.Text.Encoding.UTF8.GetString(body);
                //Console.WriteLine(text);
                await Task.CompletedTask;
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(_queueName, false, consumer);
            await Task.CompletedTask;
        }
        
        private static void ProcessMessage(byte[] body)
        {
            var text = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine(text);
        }
        public void Dispose()
        {
            if (_channel.IsOpen)
                _channel.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
