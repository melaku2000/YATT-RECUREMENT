using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yatt.MessageBroker.Interfaces;
using Yatt.MessageBroker.Settings;

namespace Yatt.MessageBroker
{
    public static class Extensions
    {
        public static IServiceCollection AddRabbitMqExtension(this IServiceCollection services)
        {
            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();

                RabbitMqSettings? rabbitSetting = configuration!.GetSection(nameof(RabbitMqSettings))
                    .Get<RabbitMqSettings>();

                var factory = new ConnectionFactory
                {
                    HostName = rabbitSetting.HostName
            ,
                };
                //UserName = rabbitSetting.UserName,Password = rabbitSetting.Password,VirtualHost = rabbitSetting.VertualHost

                var connection = factory.CreateConnection();
                using IModel channel = connection.CreateModel();

                return channel;
            });

           return services;
        }
        //public static IServiceCollection AddMessageProducer<T>(this IServiceCollection services, string discoveryName)
        //   where T : class
        //{
        //    services.AddSingleton<IMessageProducer<T>>(serviceProvider =>
        //    {
        //        var channel = serviceProvider.GetService<IModel>();
        //        channel.QueueDeclare(discoveryName);
        //        return new MessageProducer<T>(channel);
        //    });

        //    return services;
        //}
    }
}
