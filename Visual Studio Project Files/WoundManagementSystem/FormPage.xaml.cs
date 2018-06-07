using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Pdf;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WoundManagementSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FormPage : Page
    {

        public FormPage()
        {
            this.InitializeComponent();
        }

        private void BtnToggleNav_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Dashboard));
        }

        private void ShowPDF_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "";

            Button clickedButton = (Button)sender;

            switch (clickedButton.Name)
            {
                case "BtnEntry":
                    fileName = "EntryForm.pdf";
                    break;
                case "BtnConsentEnglish":
                    fileName = "ConsentFormEnglish.pdf";
                    break;
                case "BtnConsentFrench":
                    fileName = "ConsentFormFrancais.pdf";
                    break;
                case "BtnWound":
                    fileName = "WoundManagementForm.pdf";
                    break;
                case "BtnGPBurn":
                    fileName = "GPFormBurn.pdf";
                    break;
                case "BtnGPUlcer":
                    fileName = "GPFormChronicUlcer.pdf";
                    break;
                case "BtnGPDiabetic":
                    fileName = "GPFormDiabeticFoot.pdf";
                    break;
                case "BtnSpecialistBurn":
                    fileName = "SpecialistFormBurn.pdf";
                    break;
                case "BtnSpecialistUlcer":
                    fileName = "SpecialistFormChronicUlcer.pdf";
                    break;
                case "BtnSpecialistDiabetic":
                    fileName = "SpecialistFormDiabeticFoot.pdf";
                    break;
                default:
                    break;
            }

            Frame.Navigate(typeof(FormViewer), fileName);
        }

        // NAVIGATION BUTTONS
        private void NavPatients_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PatientPage));
        }

        private void NavStaff_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StaffPage));
        }

        private void NavStock_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Stock));
        }

        private void NavPriceLists_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PriceLists));
        }

        private void NavServices_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ServicesPage));
        }

        private void NavContacts_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContactsPage));
        }

        private void NavFinances_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Finances));
        }

        private void NavForms_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FormPage));
        }

        private void NavLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
