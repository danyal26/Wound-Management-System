using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WoundManagementSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewAssessment : Page
    {
        PatientVisit thisVisit = null;

        public ViewAssessment()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            thisVisit = (PatientVisit)e.Parameter;

            changeComponentVisibility();

            getDetails();
        }

        private void changeComponentVisibility()
        {

            switch (thisVisit.type)
            {
                case "Burn":
                    Row6.Visibility = Visibility.Collapsed;
                    Row7.Visibility = Visibility.Collapsed;
                    Row8.Visibility = Visibility.Collapsed;
                    Row9.Visibility = Visibility.Collapsed;
                    Row18.Visibility = Visibility.Collapsed;
                    Row19.Visibility = Visibility.Collapsed;
                    Row20.Visibility = Visibility.Collapsed;
                    Row21.Visibility = Visibility.Collapsed;
                    Row22.Visibility = Visibility.Collapsed;
                    break;
                case "Chronic Ulcer":
                    Row2.Visibility = Visibility.Collapsed;
                    Row6.Visibility = Visibility.Collapsed;
                    Row7.Visibility = Visibility.Collapsed;
                    Row9.Visibility = Visibility.Collapsed;
                    Row17.Visibility = Visibility.Collapsed;
                    Row18.Visibility = Visibility.Collapsed;
                    Row19.Visibility = Visibility.Collapsed;
                    Row21.Visibility = Visibility.Collapsed;
                    break;
                case "Diabetic Foot":
                    Row2.Visibility = Visibility.Collapsed;
                    Row17.Visibility = Visibility.Collapsed;
                    break;

                default:
                    break;
            }
        }

        private void getDetails()
        {

            switch (thisVisit.type)
            {
                case "Burn":
                    getBurnsVisit();
                    break;
                case "Chronic Ulcer":
                    getUlcerVisit();
                    break;
                case "Diabetic Foot":
                    getDiabeticFootVisit();
                    break;
                default:
                    break;
            }
            
        }

        private void getBurnsVisit()
        {

            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"SELECT * FROM burns_visit WHERE PatientBurnsVisitID = '{thisVisit.id}'";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                TxtPurpose.Text = reader["PurposeOfVisit"].ToString(); 

                TxtDescription.Text = reader["Description"].ToString();
                TxtManner.Text = reader["Manner"].ToString();

                TxtDiabetes.Text = reader["DiabetesMellitus"].ToString();
                TxtHypertension.Text = reader["Hypertension"].ToString();
                TxtIHD.Text = reader["IHD"].ToString();
                TxtAsthma.Text = reader["BronchialAsthma"].ToString();
                TxtThyroid.Text = reader["Thyroid"].ToString();
                TxtCVA.Text = reader["CVA"].ToString();
                TxtDVT.Text = reader["DVT"].ToString();
                TxtAllergy.Text = reader["Allergy"].ToString();
                TxtSmoking.Text = reader["SmokingAlcoholIVDA"].ToString();
                TxtPallor.Text = reader["Pallor"].ToString();
                TxtJaundice.Text = reader["Jaundice"].ToString();
                TxtClubbing.Text = reader["Clubbing"].ToString();
                TxtPulses.Text = reader["PeripheralPulses"].ToString();
                TxtCardiovascular.Text = reader["CardiovascularSystem"].ToString();
                TxtRespiratory.Text = reader["RespiratorySystem"].ToString();
                TxtAbdominal.Text = reader["AbdominalExamination"].ToString();
                TxtNeurological.Text = reader["NeurologicalExamination"].ToString();
                TxtBP.Text = reader["BP"].ToString();
                TxtPulse.Text = reader["Pulse"].ToString();
                TxtTemp.Text = reader["Temperature"].ToString();
                TxtRBS.Text = reader["RBS"].ToString();
                TxtSite.Text = reader["Site"].ToString();
                TxtSize.Text = reader["Size"].ToString();
                TxtDepth.Text = reader["Depth"].ToString();
                TxtWoundExudate.Text = reader["WoundExudate"].ToString();

                TxtCircumferential.Text = reader["Circumferential"].ToString();
                TxtCirculation.Text = reader["PeripheralCirculation"].ToString();
            }
        }

        private void getUlcerVisit()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"SELECT * FROM chronic_ulcer_visit WHERE PatientChronicUlcerVisitID = '{thisVisit.id}'";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {


                TxtPurpose.Text = reader["PurposeOfVisit"].ToString();
                TxtDiabetes.Text = reader["DiabetesMellitus"].ToString();
                TxtHypertension.Text = reader["Hypertension"].ToString();
                TxtIHD.Text = reader["IHD"].ToString();
                TxtAsthma.Text = reader["BronchialAsthma"].ToString();
                TxtThyroid.Text = reader["Thyroid"].ToString();
                TxtCVA.Text = reader["CVA"].ToString();
                TxtDVT.Text = reader["DVT"].ToString();
                TxtAllergy.Text = reader["Allergy"].ToString();
                TxtSmoking.Text = reader["SmokingAlcoholIVDA"].ToString();
                TxtPallor.Text = reader["Pallor"].ToString();
                TxtJaundice.Text = reader["Jaundice"].ToString();
                TxtClubbing.Text = reader["Clubbing"].ToString();
                TxtPulses.Text = reader["PeripheralPulses"].ToString();
                TxtCardiovascular.Text = reader["CardiovascularSystem"].ToString();
                TxtRespiratory.Text = reader["RespiratorySystem"].ToString();
                TxtAbdominal.Text = reader["AbdominalExamination"].ToString();
                TxtNeurological.Text = reader["NeurologicalExamination"].ToString();
                TxtBP.Text = reader["BP"].ToString();
                TxtPulse.Text = reader["Pulse"].ToString();
                TxtTemp.Text = reader["Temperature"].ToString();
                TxtRBS.Text = reader["RBS"].ToString();
                TxtSite.Text = reader["Site"].ToString();
                TxtSize.Text = reader["Size"].ToString();
                TxtDepth.Text = reader["Depth"].ToString();
                TxtWoundExudate.Text = reader["WoundExudate"].ToString();

                TxtDurationUlcer.Text = reader["DurationOfNonHealingUlcer"].ToString();
                TxtTreatment.Text = reader["TreatmentSurgeryPerformed"].ToString();
                TxtUndermining.Text = reader["ExtentOfUndermining"].ToString();
                TxtWoundSurface.Text = reader["WoundSurface"].ToString();
                TxtPeriwound.Text = reader["StatusOfPeriwoundTissue"].ToString();

            }
        }
        private void getDiabeticFootVisit()
        {
            MySqlConnectionStringBuilder builder = DBConnection.Connect();

            MySqlConnection conn = new MySqlConnection(builder.ToString());
            String qry = $"SELECT * FROM diabetic_foot_visit WHERE PatientDiabeticFootVisitID = '{thisVisit.id}'";
            MySqlCommand cmd = new MySqlCommand(qry, conn);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {


                TxtPurpose.Text = reader["PurposeOfVisit"].ToString();
                TxtDiabetes.Text = reader["DiabetesMellitus"].ToString();
                TxtHypertension.Text = reader["Hypertension"].ToString();
                TxtIHD.Text = reader["IHD"].ToString();
                TxtAsthma.Text = reader["BronchialAsthma"].ToString();
                TxtThyroid.Text = reader["Thyroid"].ToString();
                TxtCVA.Text = reader["CVA"].ToString();
                TxtDVT.Text = reader["DVT"].ToString();
                TxtAllergy.Text = reader["Allergy"].ToString();
                TxtSmoking.Text = reader["SmokingAlcoholIVDA"].ToString();
                TxtPallor.Text = reader["Pallor"].ToString();
                TxtJaundice.Text = reader["Jaundice"].ToString();
                TxtClubbing.Text = reader["Clubbing"].ToString();
                TxtPulses.Text = reader["PeripheralPulses"].ToString();
                TxtCardiovascular.Text = reader["CardiovascularSystem"].ToString();
                TxtRespiratory.Text = reader["RespiratorySystem"].ToString();
                TxtAbdominal.Text = reader["AbdominalExamination"].ToString();
                TxtNeurological.Text = reader["NeurologicalExamination"].ToString();
                TxtBP.Text = reader["BP"].ToString();
                TxtPulse.Text = reader["Pulse"].ToString();
                TxtTemp.Text = reader["Temperature"].ToString();
                TxtRBS.Text = reader["RBS"].ToString();
                TxtSite.Text = reader["Site"].ToString();
                TxtSize.Text = reader["Size"].ToString();
                TxtDepth.Text = reader["Depth"].ToString();
                TxtWoundExudate.Text = reader["WoundExudate"].ToString();

                TxtDurationUlcer.Text = reader["DurationOfNonHealingUlcer"].ToString();
                TxtTreatment.Text = reader["TreatmentSurgeryPerformed"].ToString();

                TxtClaudication.Text = reader["IntermittenClaudication"].ToString();
                TxtPainAtRest.Text = reader["IschaemicPainAtRest"].ToString();
                TxtHypesthesia.Text = reader["Hypesthesia"].ToString();
                TxtHyperesthesia.Text = reader["Hyperesthesia"].ToString();
                TxtParaesthesia.Text = reader["Paraesthesia"].ToString();
                TxtDysesthesia.Text = reader["Dysesthesia"].ToString();
                TxtRadPain.Text = reader["RadicularPain"].ToString();
                TxtAnhydrosis.Text = reader["Anhydrosis"].ToString();
                TxtCallus.Text = reader["HypertrophicCallus"].ToString();
                TxtBrittleNail.Text = reader["BrittleNail"].ToString();
                TxtHammerToe.Text = reader["HammerToe"].ToString();
                TxtFissures.Text = reader["Fissures"].ToString();
                TxtPulses.Text = reader["PeripheralPulses"].ToString();
                TxtLossHairGrowth.Text = reader["LossOfPedalHairGrowth"].ToString();
                TxtPallorFoot.Text = reader["PallorOfFoot"].ToString();
                TxtCyanosis.Text = reader["CyanosisOfToes"].ToString();

                TxtUndermining.Text = reader["ExtentOfUndermining"].ToString();
                TxtWoundSurface.Text = reader["WoundSurface"].ToString();
                TxtPeriwound.Text = reader["StatusOfPeriwoundTissue"].ToString();
            }
        }

        private void BtnCancelAssessment_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
