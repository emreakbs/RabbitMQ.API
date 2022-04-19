using RabbitMQ.Consumer.Abstract;
using RabbitMQ.Manager;
using RabbitMQ.Manager.Abstract;
using RabbitMQ.Producer.Abstarct;
using RabbitMQ.Utils.Helper;
using RabbitMQ.Utils.Helper.Abstract;

namespace RabbitMQ.API
{
    public static class ConfigureServiceExtension
    {
        public static void AddScoped(this IServiceCollection services)
        {
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ILog, Log>();
            services.AddScoped<IConsumer, Consumer.Consumer>();
            services.AddScoped<IProducer, Producer.Producer>();
            services.AddScoped<IConnectionHelper, ConnectionHelper>();
        }
    }
}
