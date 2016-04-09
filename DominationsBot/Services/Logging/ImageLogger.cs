using System;
using System.Drawing;
using System.IO;

namespace DominationsBot.Services.Logging
{
    public class ImageLogger
    {
        private readonly ISettings _settings;

        public ImageLogger(ISettings settings)
        {
            _settings = settings;
        }

        public void Log(Bitmap bitmap, string imageName = "image")
        {
            bitmap.Save(Path.Combine(_settings.LogsPath, $"{DateTime.Now:yyyy-M-dd--HH-mm-ss}_{imageName}.png"));
        }
    }
}