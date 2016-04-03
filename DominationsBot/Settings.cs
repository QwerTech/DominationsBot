using System;
using System.IO;

namespace DominationsBot
{
    public class Settings
    {
        private readonly string _basePath = AppDomain.CurrentDomain.BaseDirectory;

        private readonly Lazy<string> _logPath;

        public Settings()
        {
            _logPath = new Lazy<string>(() =>
            {
                var logsPath = Path.Combine(_basePath, "logs");
                if (!Directory.Exists(logsPath))
                    Directory.CreateDirectory(logsPath);
                return logsPath;
            });
        }

        public string LogsPath => _logPath.Value;
    }
}