using System;
using System.IO;
using System.Linq;
using DominationsBot.Storage;
using NLog.Internal;

namespace DominationsBot
{
    public interface ISettings
    {
        string SymbolsPath { get; }
        string LogsPath { get; }
    }

    public class Settings : ISettings
    {
        public static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;

        private readonly Lazy<string> _logPath;
        

        public string SymbolsPath => Path.Combine(BasePath, "Resources/Symbols");

        public string LogsPath => _logPath.Value;
        public Settings(StorageContext gcrDbContext)
        {
            _logPath = new Lazy<string>(() =>
            {
                var logsPath = Path.Combine(BasePath, "logs");
                if (!Directory.Exists(logsPath))
                    Directory.CreateDirectory(logsPath);
                return logsPath;
            });
            var database = gcrDbContext.Database;
            //IsDebugDatabase = new Lazy<bool>(() => database.SqlQuery<int>("SELECT 1 FROM sys.fn_listextendedproperty( default,  default, default, default, default, default, default) WHERE name='Debug'").Any()).Value;
        }

        public bool IsDebugDatabase { get; } = true;
        public bool IsDebugCode
        {
            get
            {
#if (DEBUG)
                return true;
#else
                return false;
#endif
            }
        }
        public bool IsAnyDebug => IsDebugDatabase || IsDebugCode;
        
    }
}