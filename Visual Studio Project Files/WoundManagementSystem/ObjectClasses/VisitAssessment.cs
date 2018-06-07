using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem.ObjectClasses
{
    public class VisitAssessment
    {
        public string VisitID { get; set; }
        public string PatientVisitID { get; set; }
        public string Purpose { get; set; }
        public string DiabetesMellitus { get; set; }
        public string Hypertension { get; set; }
        public string IHD { get; set; }
        public string Asthma { get; set; }
        public string Thyroid { get; set; }
        public string CVA { get; set; }
        public string DVT { get; set; }
        public string Allergy { get; set; }
        public string Smoking { get; set; }
        public string Pallor { get; set; }
        public string Jaundice { get; set; }
        public string Clubbing { get; set; }
        public string Pulses { get; set; }
        public string Cardiovascular { get; set; }
        public string Respiratory { get; set; }
        public string Abdominal { get; set; }
        public string Neurological { get; set; }
        public string BP { get; set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public string RBS { get; set; }
        public string Site { get; set; }
        public string Size { get; set; }
        public string Depth { get; set; }
        public string Exudate { get; set; }

        public VisitAssessment(string VisitID, string PatientVisitID, string Purpose, string DiabetesMellitus, 
            string Hypertension, string IHD, string Asthma, string Thyroid, string CVA, 
            string DVT, string Allergy, string Smoking, string Pallor, string Jaundice, 
            string Clubbing, string Pulses, string Cardiovascular, string Respiratory, 
            string Abdominal, string Neurological, string BP, string Pulse, 
            string Temperature, string RBS, string Site, string Size, string Depth, string Exudate)
        {
            this.VisitID = VisitID;
            this.PatientVisitID = PatientVisitID;
            this.Purpose = Purpose;
            this.DiabetesMellitus = DiabetesMellitus;
            this.Hypertension = Hypertension;
            this.IHD = IHD;
            this.Asthma = Asthma;
            this.Thyroid = Thyroid;
            this.CVA = CVA;
            this.DVT = DVT;
            this.Allergy = Allergy;
            this.Smoking = Smoking;
            this.Pallor = Pallor;
            this.Jaundice = Jaundice;
            this.Clubbing = Clubbing;
            this.Pulses = Pulses;
            this.Cardiovascular = Cardiovascular;
            this.Respiratory = Respiratory;
            this.Abdominal = Abdominal;
            this.Neurological = Neurological;
            this.BP = BP;
            this.Pulse = Pulse;
            this.Temperature = Temperature;
            this.RBS = RBS;
            this.Site = Site;
            this.Size = Size;
            this.Depth = Depth;
            this.Exudate = Exudate;
        }
        
    }
}
