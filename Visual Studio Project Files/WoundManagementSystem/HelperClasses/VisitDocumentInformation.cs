using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WoundManagementSystem
{

    public class VisitDocumentInformation
    {
        public StorageFile file { get; set; }
        public string fileName { get; set; }

        public VisitDocumentInformation(StorageFile file, string fileName)
        {
            this.file = file;
            this.fileName = fileName;
        }
    }
}
