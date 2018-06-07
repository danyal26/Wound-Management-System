using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace WoundManagementSystem
{
    public class VisitPhotoInformation
    {
        public StorageFile file { get; set; }
        public string fileName { get; set; }
        public BitmapImage image { get; set; }

        public VisitPhotoInformation(StorageFile file, string fileName, BitmapImage image)
        {
            this.file = file;
            this.fileName = fileName;
            this.image = image;
        }
    }
}
