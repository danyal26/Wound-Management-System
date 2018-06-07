using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoundManagementSystem.ObjectClasses
{
    public class DiabeticFootVisit : VisitAssessment
    {
        public string DurationOfUlcer { get; set; }
        public string Treatment { get; set; }
        public string Claudication { get; set; }
        public string PainAtRest { get; set; }
        public string Hypesthesia { get; set; }
        public string Hyperesthesia { get; set; }
        public string Paraesthesia { get; set; }
        public string Dysesthesia { get; set; }
        public string RadicularPain { get; set; }
        public string Anhydrosis { get; set; }
        public string ExtentOfUndermining { get; set; }
        public string WoundSurface { get; set; }
        public string PeriwoundTissue { get; set; }
        public string Callus { get; set; }
        public string BrittleNail { get; set; }
        public string HammerToe { get; set; }
        public string Fissures { get; set; }
        public string HairGrowth { get; set; }
        public string CyanosisOfToes { get; set; }
        public string PallorOfFoot { get; set; }


        public DiabeticFootVisit(string DurationOfUlcer, string Treatment, string Claudication,
            string PainAtRest, string Hypesthesia, string Hyperesthesia, string Paraesthesia,
            string Dysesthesia, string RadicularPain, string Anhydrosis, string ExtentOfUndermining,
            string WoundSurface, string PeriwoundTissue, string Callus, string BrittleNail,
            string HammerToe, string Fissures, string HairGrowth, string CyanosisOfToes,
            string PallorOfFoot, string VisitID, string PatientVisitID, string Purpose, 
            string DiabetesMellitus, string Hypertension, string IHD, string Asthma, 
            string Thyroid, string CVA, string DVT, string Allergy, string Smoking, string Pallor, 
            string Jaundice, string Clubbing, string Pulses, string Cardiovascular, string Respiratory, 
            string Abdominal, string Neurological, string BP, string Pulse, string Temperature, 
            string RBS, string Site, string Size, string Depth, string WoundExudate)
            : base(VisitID, PatientVisitID, Purpose, DiabetesMellitus, Hypertension, IHD,
                  Asthma, Thyroid, CVA, DVT, Allergy, Smoking, Pallor, Jaundice, Clubbing, 
                  Pulses, Cardiovascular, Respiratory, Abdominal, Neurological, BP, Pulse, 
                  Temperature, RBS, Site, Size, Depth, WoundExudate)
        {
            this.DurationOfUlcer = DurationOfUlcer;
            this.Treatment = Treatment;
            this.Claudication = Claudication;
            this.PainAtRest = PainAtRest;
            this.Hypesthesia = Hypesthesia;
            this.Hyperesthesia = Hyperesthesia;
            this.Paraesthesia = Paraesthesia;
            this.Dysesthesia = Dysesthesia;
            this.RadicularPain = RadicularPain;
            this.Anhydrosis = Anhydrosis;
            this.ExtentOfUndermining = ExtentOfUndermining;
            this.WoundSurface = WoundSurface;
            this.PeriwoundTissue = PeriwoundTissue;
            this.Callus = Callus;
            this.BrittleNail = BrittleNail;
            this.HammerToe = HammerToe;
            this.Fissures = Fissures;
            this.HairGrowth = HairGrowth;
            this.CyanosisOfToes = CyanosisOfToes;
            this.PallorOfFoot = PallorOfFoot;
        }
        

    }
}
