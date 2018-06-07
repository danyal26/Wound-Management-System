using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem.ObjectClasses
{
    public class ChronicUlcerVisit : VisitAssessment
    {
        public string DurationOfUlcer { get; set; }
        public string Treatment { get; set; }
        public string ExtentOfUndermining { get; set; }
        public string WoundSurface { get; set; }
        public string PeriwoundTissue { get; set; }

        public ChronicUlcerVisit(string DurationOfUlcer, string Treatment, string VisitID, string PatientVisitID, string Purpose, 
            string DiabetesMellitus, string Hypertension, string IHD, string Asthma, 
            string Thyroid, string CVA, string DVT, string Allergy, string Smoking, string Pallor, 
            string Jaundice, string Clubbing, string Pulses, string Cardiovascular, 
            string Respiratory, string Abdominal, string Neurological, string BP, string Pulse, 
            string Temperature, string RBS, string Site, string Size, string Depth, string ExtentOfUndermining,
            string WoundSurface, string PeriwoundTissue, string WoundExudate)
            : base(VisitID, PatientVisitID, Purpose, DiabetesMellitus, Hypertension, 
                  IHD, Asthma, Thyroid, CVA, DVT, Allergy, Smoking, Pallor, Jaundice, 
                  Clubbing, Pulses, Cardiovascular, Respiratory, Abdominal, Neurological, 
                  BP, Pulse, Temperature, RBS, Site, Size, Depth, WoundExudate)
        {
            this.DurationOfUlcer = DurationOfUlcer;
            this.Treatment = Treatment;
            this.ExtentOfUndermining = ExtentOfUndermining;
            this.WoundSurface = WoundSurface;
            this.PeriwoundTissue = PeriwoundTissue;
        }
    }
}
