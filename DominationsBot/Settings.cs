using System;
using System.IO;

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

        public Settings()
        {
            _logPath = new Lazy<string>(() =>
            {
                var logsPath = Path.Combine(BasePath, "logs");
                if (!Directory.Exists(logsPath))
                    Directory.CreateDirectory(logsPath);
                return logsPath;
            });
        }

        public string SymbolsPath => Path.Combine(BasePath, "Resources/Symbols");

        public string LogsPath => _logPath.Value;
    }
}