using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem.ObjectClasses
{
    public class BurnsVisit : VisitAssessment
    {
        public string Description { get; set; }
        public string Manner { get; set; }
        public string Circumferential { get; set; }
        public string PeripheralCirculation { get; set; }

        public BurnsVisit(string Description, string Manner, string Circumferential, 
            string PeripheralCirculation, string VisitID, string PatientVisitID, string Purpose, 
            string DiabetesMellitus, string Hypertension, string IHD, string Asthma, 
            string Thyroid, string CVA, string DVT, string Allergy, string Smoking, string Pallor, 
            string Jaundice, string Clubbing, string Pulses, string Cardiovascular, string Respiratory, 
            string Abdominal, string Neurological, string BP, string Pulse, string Temperature, 
            string RBS, string Site, string Size, string Depth, string Exudate)
            : base(VisitID, PatientVisitID, Purpose, DiabetesMellitus, 
                  Hypertension, IHD, Asthma, Thyroid, CVA, DVT, Allergy, Smoking, 
                  Pallor, Jaundice, Clubbing, Pulses, Cardiovascular, Respiratory, 
                  Abdominal, Neurological, BP, Pulse, Temperature, RBS, Site, Size, Depth, Exudate)
        {
            this.Description = Description;
            this.Manner = Manner;
            this.Circumferential = Circumferential;
            this.PeripheralCirculation = PeripheralCirculation;
        }
    }
}
