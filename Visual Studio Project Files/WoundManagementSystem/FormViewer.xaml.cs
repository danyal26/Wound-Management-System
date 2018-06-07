using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Syncfusion.PdfViewer;
using System.Reflection;
using Syncfusion.Pdf.Parsing;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WoundManagementSystem
{
    public sealed partial class FormViewer : Page
    {
        private string fileName = "";

        public FormViewer()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            fileName = (string)e.Parameter;

            Assembly assembly = typeof(FormViewer).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream($"WoundManagementSystem.Assets.Forms.{fileName}");

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            //Loads the PDF document.
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(buffer);
            PDFViewer.LoadDocument(loadedDocument);

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
