using Microsoft.Extensions.Configuration;
using RabbitMQ.Utils.Helper.Abstract;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Utils.Helper
{
    public class Log : ILog
    {
        private static Serilog.Core.Logger _logInformation;
        private static Serilog.Core.Logger _logError;
        private readonly IConfiguration _configuration;
        public Log(IConfiguration configuration)
        {
            _configuration = configuration;
            _logError = NewMethod("error").CreateLogger();
            _logInformation = NewMethod("information").CreateLogger();
        }
        public void Error(Exception ex, string messageTemplate)
        {
            _logError.Error(ex, messageTemplate);
        }
        public void Info(string messageTemplate)
        {
            _logInformation.Information(messageTemplate);
        }
        private LoggerConfiguration NewMethod(string logType)
        {
            return new LoggerConfiguration()
                           .MinimumLevel.Verbose()
                           .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri($"http://{_configuration.GetSection("Elasticsearch:HostName").Value}:{_configuration.GetSection("Elasticsearch:Port").Value}"))
                           {
                               AutoRegisterTemplate = true,
                               AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                               ModifyConnectionSettings = x => x.BasicAuthentication(_configuration.GetSection("Elasticsearch:UserName").Value, _configuration.GetSection("Elasticsearch:Password").Value),
                               IndexFormat = $"rabbitmq-{logType}-log-{{0:yyyy.MM.dd}}",
                               TemplateName = "serilog-events-template",
                           });
        }
    }
}
