using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem
{
    class PdfReport : INotifyPropertyChanged
    {
        private Stream docStream;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Stream object to be bound to the ItemsSource of the PDF Viewer
        /// </summary>
        public Stream DocumentStream
        {
            get
            {
                return docStream;
            }
            set
            {
                docStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }

        public PdfReport()
        {
            //Loads the stream from the embedded resource.
            Assembly assembly = typeof(FormViewer).GetTypeInfo().Assembly;
            docStream = assembly.GetManifestResourceStream("WoundManagementSystem.Assets.EntryForm.pdf");
        }

        public PdfReport(String name)
        {
            //Loads the stream from the embedded resource.
            Assembly assembly = typeof(FormViewer).GetTypeInfo().Assembly;
            docStream = assembly.GetManifestResourceStream("WoundManagementSystem.Assets.EntryForm.pdf");
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
