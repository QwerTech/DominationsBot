using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DominationsBot.Extensions;

namespace DominationsBot.UI.MainView
{
    public class MainViewModel : ViewModelBase
    {
        private ImageSource _imageSource;

        public MainViewModel()
        {
            
        }

        public ImageSource RenderImage
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged("RenderImage");
            }
        }

        public void SetBitmap(Bitmap bitmap)
        {
            RenderImage = bitmap.ToWpfBitmap();
        }
    }
}