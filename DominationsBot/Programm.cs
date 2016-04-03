using System;
using DominationsBot.DI;
using DominationsBot.Services.GameProcess.WorkerProcess;
using NLog;
using NLog.Config;
using NLog.Targets;
using Topshelf;

namespace DominationsBot
{
    public class Programm
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
            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration 
            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);


            consoleTarget.Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message}";

            // Step 4. Define rules
            var rule1 = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.LoggingRules.Add(rule1);

            // Step 5. Activate the configuration
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