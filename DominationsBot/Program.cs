using System;
using System.Text;
using DominationsBot.DI;
using DominationsBot.Services.System.WorkerProcess;
using NLog;
using NLog.Config;
using NLog.Targets;
using Topshelf;

namespace DominationsBot
{
    public class Program
    {
        private static void Main()
        {
            HostFactory.Run(x => //1
            {
                x.Service<DominationsBot>();

                x.RunAsLocalSystem(); //6
                x.UseNLog();
                x.SetDescription("DominationsBot"); //7
                x.SetDisplayName("DominationsBot"); //8
                x.SetServiceName("DominationsBot"); //9
            });
        }
    }

    internal class DominationsBot : ServiceControl
    {
        public bool Start(HostControl hostControl)
        {
            var consoleTarget = new ColoredConsoleTarget
            {
                Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message}"
            };

            var fileTarget = new FileTarget
            {
                Layout = consoleTarget.Layout,
                FileName = "logs/bot.log",
                Encoding = Encoding.UTF8
            };

            var consoleRule = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            var fileRule = new LoggingRule("*", LogLevel.Trace, fileTarget);

            var config = new LoggingConfiguration();
            config.AddTarget("console", consoleTarget);
            config.AddTarget("file", fileTarget);
            config.LoggingRules.Add(consoleRule);
            config.LoggingRules.Add(fileRule);

            LogManager.Configuration = config;

            var taskSheduler = IoC.Container.GetInstance<TaskSheduler>();
            try
            {
                taskSheduler.DoWork();
            }
            catch (Exception e)
            {
                LogManager.GetCurrentClassLogger().Error(e);
                throw;
            }
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            IoC.Container.Dispose();
            return true;
        }
    }
}