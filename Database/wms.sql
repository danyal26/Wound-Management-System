-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.6.34-log - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL Version:             9.4.0.5174
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for woundmanagementsystem
CREATE DATABASE IF NOT EXISTS `woundmanagementsystem` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `woundmanagementsystem`;

-- Dumping structure for table woundmanagementsystem.appointments
CREATE TABLE IF NOT EXISTS `appointments` (
  `AppointmentID` varchar(10) NOT NULL,
  `Date` varchar(50) NOT NULL,
  `Time` varchar(50) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Surname` varchar(45) NOT NULL,
  `Contact` varchar(45) NOT NULL,
  `Type` varchar(500) NOT NULL,
  `Notes` varchar(500) NOT NULL,
  PRIMARY KEY (`AppointmentID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.appointments: ~21 rows (approximately)
/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
INSERT INTO `appointments` (`AppointmentID`, `Date`, `Time`, `Name`, `Surname`, `Contact`, `Type`, `Notes`) VALUES
	('APT002', '15-09-2017', '08:47', 'Megan', 'Megs', '07445678988', 'Burn', ''),
	('APT004', '15-09-2017', '11:27', 'James', 'Kevinson', '0739382493', 'Chronic Ulcer', ''),
	('APT005', '16-09-2017', '07:29', 'James', 'Kevinson', '0739382493', 'Diabetic Foot', ''),
	('APT006', '11-09-2017', '08:32', 'd', 'd', '4', 'Diabetic Foot', ''),
	('APT008', '30-09-2017', '07:35', 'James', 'Kevinson', '0739382493', 'Burn', ''),
	('APT009', '07-10-2017', '19:03', 'd', 'd', 'd', '', ''),
	('APT010', '08-10-2017', '13:51', 'Megan', 'Megs', '324324', 'Chronic Ulcer', ''),
	('APT011', '09-10-2017', '20:19', 'James', 'Kevinson', '0739382492', 'Chronic Ulcer', ''),
	('APT012', '10-10-2017', '20:47', 'James', 'Kevinson', '0739382492', 'Chronic Ulcer', ''),
	('APT013', '10-10-2017', '16:53', 'Megan', 'Megs', '0737445678', 'Chronic Ulcer', ''),
	('APT014', '11-10-2017', '09:32', 'Pat', 'Harpur', '07432144567', 'Burn', ''),
	('APT015', '15-10-2017', '19:07', 'James', 'Kevinson', '0739382492', 'Burn', ''),
	('APT016', '15-10-2017', '19:11', 'Megan', 'Megs', '0737445678', 'Burn', ''),
	('APT017', '15-10-2017', '19:22', 'Pat', 'Harpur', '07432144567', 'Burn', ''),
	('APT018', '22-10-2017', '08:51', 'Scruffy', 'Pupper', '0627377777', 'Burn', ''),
	('APT019', '24-10-2017', '16:29', 'Pat', 'Harpur', '07432144567', 'Burn', ''),
	('APT020', '25-10-2017', '08:47', 'Pat', 'Harpur', '07432144567', 'Burn', ''),
	('APT021', '30-10-2017', '10:35', 'Pat', 'Harpur', '07432144567', 'Burn', ''),
	('APT022', '03-11-2017', '09:14', 'Mohamed', 'Khan', '08181839447', 'Burn', ''),
	('APT023', '01-11-2017', '09:15', 'Megan', 'Megs', '0737445678', 'Burn', ''),
	('APT024', '01-11-2017', '09:38', 'James', 'Kevinson', '0739382492', 'Burn', ''),
	('APT025', '06-11-2017', '08:04', 'James', 'Kevinson', '0739382492', 'Burn', '');
/*!40000 ALTER TABLE `appointments` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.burns_visit
CREATE TABLE IF NOT EXISTS `burns_visit` (
  `VisitID` varchar(10) NOT NULL,
  `PurposeOfVisit` varchar(100) NOT NULL,
  `Description` varchar(500) NOT NULL,
  `Manner` varchar(50) NOT NULL,
  `DiabetesMellitus` varchar(100) NOT NULL,
  `Hypertension` varchar(100) NOT NULL,
  `IHD` varchar(100) NOT NULL,
  `BronchialAsthma` varchar(100) NOT NULL,
  `Thyroid` varchar(100) NOT NULL,
  `CVA` varchar(100) NOT NULL,
  `DVT` varchar(100) NOT NULL,
  `Allergy` varchar(100) NOT NULL,
  `SmokingAlcoholIVDA` varchar(100) NOT NULL,
  `Pallor` varchar(100) NOT NULL,
  `Jaundice` varchar(100) NOT NULL,
  `Clubbing` varchar(100) NOT NULL,
  `PeripheralPulses` varchar(100) NOT NULL,
  `CardiovascularSystem` varchar(100) NOT NULL,
  `RespiratorySystem` varchar(100) NOT NULL,
  `AbdominalExamination` varchar(100) NOT NULL,
  `NeurologicalExamination` varchar(100) NOT NULL,
  `BP` varchar(100) NOT NULL,
  `Pulse` varchar(100) NOT NULL,
  `Temperature` varchar(100) NOT NULL,
  `RBS` varchar(100) NOT NULL,
  `Site` varchar(100) NOT NULL,
  `Size` varchar(100) NOT NULL,
  `Depth` varchar(100) NOT NULL,
  `WoundExudate` varchar(100) NOT NULL,
  `Circumferential` varchar(100) NOT NULL,
  `PeripheralCirculation` varchar(100) NOT NULL,
  `PatientBurnsVisitID` varchar(10) NOT NULL,
  PRIMARY KEY (`VisitID`),
  KEY `PatientBurnsVisitID_idx` (`PatientBurnsVisitID`),
  CONSTRAINT `PatientBurnsVisitID` FOREIGN KEY (`PatientBurnsVisitID`) REFERENCES `patient_visits` (`PatientVisitID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.burns_visit: ~1 rows (approximately)
/*!40000 ALTER TABLE `burns_visit` DISABLE KEYS */;
INSERT INTO `burns_visit` (`VisitID`, `PurposeOfVisit`, `Description`, `Manner`, `DiabetesMellitus`, `Hypertension`, `IHD`, `BronchialAsthma`, `Thyroid`, `CVA`, `DVT`, `Allergy`, `SmokingAlcoholIVDA`, `Pallor`, `Jaundice`, `Clubbing`, `PeripheralPulses`, `CardiovascularSystem`, `RespiratorySystem`, `AbdominalExamination`, `NeurologicalExamination`, `BP`, `Pulse`, `Temperature`, `RBS`, `Site`, `Size`, `Depth`, `WoundExudate`, `Circumferential`, `PeripheralCirculation`, `PatientBurnsVisitID`) VALUES
	('BV001', 'Lorem ipsum dolor sit amet, malorum', 'Lorem ipsum dolor sit amet, malorum', 'Lorem ipsum dolor sit amet, malorum', 'Yes. Lorem ipsum dolor sit amet, malorum um dolor sit amet, malorum', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'Yes. Lorem ipsum dolor sit amet, malorum', '12. ', '13. Lorem ipsum dolor sit amet, malorum', '14. Lorem ipsum dolor sit amet, malorum', '15. ', 'Lorem ipsum dolor sit amet, malorum', 'Lorem ipsum dolor sit amet, malorum', 'Lorem ipsum dolor sit amet, malorum', 'Lorem ipsum dolor sit amet, malorum', 'Lorem ipsum dolor sit amet, malorum', 'Lorem ipsum dolor sit amet, malorum', 'PV003'),
	('BV002', 'Lorem ipsum dolor sit a', 'Lorem ipsum dolor sit a', 'Lorem ipsum dolor sit a', 'No. ', 'Yes. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'Yes. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'Yes. Lorem ipsum dolor sit a', 'No. ', 'No. ', 'No. ', '12. ', '13. ', '14. ', '123. ', 'sad', 'asd', 'sad', 'asd', 'sad', 'sad', 'PV011');
/*!40000 ALTER TABLE `burns_visit` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.chronic_ulcer_visit
CREATE TABLE IF NOT EXISTS `chronic_ulcer_visit` (
  `VisitID` varchar(10) NOT NULL,
  `PurposeOfVisit` varchar(500) NOT NULL,
  `DurationOfNonHealingUlcer` varchar(100) NOT NULL,
  `TreatmentSurgeryPerformed` varchar(100) NOT NULL,
  `DiabetesMellitus` varchar(100) NOT NULL,
  `Hypertension` varchar(100) NOT NULL,
  `IHD` varchar(100) NOT NULL,
  `BronchialAsthma` varchar(100) NOT NULL,
  `Thyroid` varchar(100) NOT NULL,
  `CVA` varchar(100) NOT NULL,
  `DVT` varchar(100) NOT NULL,
  `Allergy` varchar(100) NOT NULL,
  `SmokingAlcoholIVDA` varchar(100) NOT NULL,
  `Pallor` varchar(100) NOT NULL,
  `Jaundice` varchar(100) NOT NULL,
  `Clubbing` varchar(100) NOT NULL,
  `PeripheralPulses` varchar(100) NOT NULL,
  `CardiovascularSystem` varchar(100) NOT NULL,
  `RespiratorySystem` varchar(100) NOT NULL,
  `AbdominalExamination` varchar(100) NOT NULL,
  `NeurologicalExamination` varchar(100) NOT NULL,
  `BP` varchar(100) NOT NULL,
  `Pulse` varchar(100) NOT NULL,
  `Temperature` varchar(100) NOT NULL,
  `RBS` varchar(100) NOT NULL,
  `Site` varchar(100) NOT NULL,
  `Size` varchar(100) NOT NULL,
  `Depth` varchar(100) NOT NULL,
  `ExtentOfUndermining` varchar(100) NOT NULL,
  `WoundSurface` varchar(100) NOT NULL,
  `WoundExudate` varchar(100) NOT NULL,
  `StatusOfPeriwoundTissue` varchar(100) NOT NULL,
  `PatientChronicUlcerVisitID` varchar(10) NOT NULL,
  PRIMARY KEY (`VisitID`),
  KEY `PatientChronicUlcerVisitID_idx` (`PatientChronicUlcerVisitID`),
  CONSTRAINT `PatientChronicUlcerVisitID` FOREIGN KEY (`PatientChronicUlcerVisitID`) REFERENCES `patient_visits` (`PatientVisitID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.chronic_ulcer_visit: ~2 rows (approximately)
/*!40000 ALTER TABLE `chronic_ulcer_visit` DISABLE KEYS */;
/*!40000 ALTER TABLE `chronic_ulcer_visit` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.contacts
CREATE TABLE IF NOT EXISTS `contacts` (
  `ContactID` varchar(10) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Phone` varchar(13) NOT NULL,
  `Email` varchar(45) NOT NULL,
  PRIMARY KEY (`ContactID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.contacts: ~3 rows (approximately)
/*!40000 ALTER TABLE `contacts` DISABLE KEYS */;
INSERT INTO `contacts` (`ContactID`, `Name`, `Phone`, `Email`) VALUES
	('CONT001', 'Bandage supplier', '0753648597', 'bsupplier@mail.com'),
	('CONT002', 'Ointment Supplier', '0742537485', 'osupplier@mail.com');
/*!40000 ALTER TABLE `contacts` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.diabetic_foot_visit
CREATE TABLE IF NOT EXISTS `diabetic_foot_visit` (
  `VisitID` varchar(10) NOT NULL,
  `PurposeOfVisit` varchar(500) NOT NULL,
  `DurationOfNonHealingUlcer` varchar(100) NOT NULL,
  `TreatmentSurgeryPerformed` varchar(100) NOT NULL,
  `IntermittenClaudication` varchar(100) NOT NULL,
  `IschaemicPainAtRest` varchar(100) NOT NULL,
  `Hypesthesia` varchar(100) NOT NULL,
  `Hyperesthesia` varchar(100) NOT NULL,
  `Paraesthesia` varchar(100) NOT NULL,
  `Dysesthesia` varchar(100) NOT NULL,
  `RadicularPain` varchar(100) NOT NULL,
  `Anhydrosis` varchar(100) NOT NULL,
  `DiabetesMellitus` varchar(100) NOT NULL,
  `Hypertension` varchar(100) NOT NULL,
  `IHD` varchar(100) NOT NULL,
  `BronchialAsthma` varchar(100) NOT NULL,
  `Thyroid` varchar(100) NOT NULL,
  `CVA` varchar(100) NOT NULL,
  `DVT` varchar(100) NOT NULL,
  `Allergy` varchar(100) NOT NULL,
  `SmokingAlcoholIVDA` varchar(100) NOT NULL,
  `Pallor` varchar(100) NOT NULL,
  `Jaundice` varchar(100) NOT NULL,
  `Clubbing` varchar(100) NOT NULL,
  `PeripheralPulses` varchar(100) NOT NULL,
  `CardiovascularSystem` varchar(100) NOT NULL,
  `RespiratorySystem` varchar(100) NOT NULL,
  `AbdominalExamination` varchar(100) NOT NULL,
  `NeurologicalExamination` varchar(100) NOT NULL,
  `BP` varchar(100) NOT NULL,
  `Pulse` varchar(100) NOT NULL,
  `Temperature` varchar(100) NOT NULL,
  `RBS` varchar(100) NOT NULL,
  `Site` varchar(100) NOT NULL,
  `Size` varchar(100) NOT NULL,
  `Depth` varchar(100) NOT NULL,
  `ExtentOfUndermining` varchar(100) NOT NULL,
  `WoundSurface` varchar(100) NOT NULL,
  `WoundExudate` varchar(100) NOT NULL,
  `StatusOfPeriwoundTissue` varchar(100) NOT NULL,
  `HypertrophicCallus` varchar(100) NOT NULL,
  `BrittleNail` varchar(100) NOT NULL,
  `HammerToe` varchar(100) NOT NULL,
  `Fissures` varchar(100) NOT NULL,
  `LossOfPedalHairGrowth` varchar(100) NOT NULL,
  `CyanosisOfToes` varchar(100) NOT NULL,
  `PallorOfFoot` varchar(100) NOT NULL,
  `PatientDiabeticFootVisitID` varchar(10) NOT NULL,
  PRIMARY KEY (`VisitID`),
  KEY `PatientDiabeticFootVisitID_idx` (`PatientDiabeticFootVisitID`),
  CONSTRAINT `PatientDiabeticFootVisitID` FOREIGN KEY (`PatientDiabeticFootVisitID`) REFERENCES `patient_visits` (`PatientVisitID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.diabetic_foot_visit: ~1 rows (approximately)
/*!40000 ALTER TABLE `diabetic_foot_visit` DISABLE KEYS */;
INSERT INTO `diabetic_foot_visit` (`VisitID`, `PurposeOfVisit`, `DurationOfNonHealingUlcer`, `TreatmentSurgeryPerformed`, `IntermittenClaudication`, `IschaemicPainAtRest`, `Hypesthesia`, `Hyperesthesia`, `Paraesthesia`, `Dysesthesia`, `RadicularPain`, `Anhydrosis`, `DiabetesMellitus`, `Hypertension`, `IHD`, `BronchialAsthma`, `Thyroid`, `CVA`, `DVT`, `Allergy`, `SmokingAlcoholIVDA`, `Pallor`, `Jaundice`, `Clubbing`, `PeripheralPulses`, `CardiovascularSystem`, `RespiratorySystem`, `AbdominalExamination`, `NeurologicalExamination`, `BP`, `Pulse`, `Temperature`, `RBS`, `Site`, `Size`, `Depth`, `ExtentOfUndermining`, `WoundSurface`, `WoundExudate`, `StatusOfPeriwoundTissue`, `HypertrophicCallus`, `BrittleNail`, `HammerToe`, `Fissures`, `LossOfPedalHairGrowth`, `CyanosisOfToes`, `PallorOfFoot`, `PatientDiabeticFootVisitID`) VALUES
	('DFV001', 'pur', 'Lorem ipsum dolor sit amet, at summo', 'Yes. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'Yes. Lorem ipsum dolor sit amet, at summo', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', 'No. ', '12. ', '13. ', '14. ', '15. ', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'Lorem ipsum dolor sit amet, at summo', 'PV004');
/*!40000 ALTER TABLE `diabetic_foot_visit` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.expense
CREATE TABLE IF NOT EXISTS `expense` (
  `ExpenseID` varchar(10) NOT NULL,
  `ExpenseType` varchar(500) NOT NULL,
  `Amount` varchar(50) NOT NULL,
  `Notes` varchar(500) NOT NULL,
  PRIMARY KEY (`ExpenseID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.expense: ~2 rows (approximately)
/*!40000 ALTER TABLE `expense` DISABLE KEYS */;
INSERT INTO `expense` (`ExpenseID`, `ExpenseType`, `Amount`, `Notes`) VALUES
	('EX001', 'Purchase', '1000', 'Note 1'),
	('EX002', 'Purchase', '1200', 'Note 2');
/*!40000 ALTER TABLE `expense` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.income
CREATE TABLE IF NOT EXISTS `income` (
  `IncomeID` varchar(10) NOT NULL,
  `IncomeType` varchar(45) NOT NULL,
  `Amount` varchar(50) NOT NULL,
  `Notes` varchar(500) NOT NULL,
  PRIMARY KEY (`IncomeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.income: ~14 rows (approximately)
/*!40000 ALTER TABLE `income` DISABLE KEYS */;
INSERT INTO `income` (`IncomeID`, `IncomeType`, `Amount`, `Notes`) VALUES
	('INC001', 'Visit', '210', 'note 1'),
	('INC002', 'Visit', '210', 'a'),
	('INC003', 'Visit', '550', 'note 3'),
	('INC004', 'Visit', '110', ''),
	('INC005', 'Visit', '210', ''),
	('INC006', 'Visit', '200', ''),
	('INC007', 'Visit', '300', ''),
	('INC008', 'Visit', '200', ''),
	('INC009', 'Visit', '150', ''),
	('INC010', 'Visit', '200', ''),
	('INC011', 'Visit', '250', ''),
	('INC012', 'Visit', '300', ''),
	('INC013', 'Visit', '250', ''),
	('INC014', 'Visit', '300', ''),
	('INC015', 'Visit', '300', ''),
	('INC016', 'Visit', '200', ''),
	('INC017', 'Visit', '150', ''),
	('INC018', 'Visit', '150', ''),
	('INC019', 'Visit', '150', ''),
	('INC020', 'Visit', '200', ''),
	('INC021', 'Visit', '50', ''),
	('INC022', 'Visit', '50', '');
/*!40000 ALTER TABLE `income` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.medication_buying_list
CREATE TABLE IF NOT EXISTS `medication_buying_list` (
  `MedicationID` varchar(10) NOT NULL,
  `MedicationName` varchar(100) NOT NULL,
  `Price` varchar(50) NOT NULL,
  PRIMARY KEY (`MedicationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.medication_buying_list: ~2 rows (approximately)
/*!40000 ALTER TABLE `medication_buying_list` DISABLE KEYS */;
INSERT INTO `medication_buying_list` (`MedicationID`, `MedicationName`, `Price`) VALUES
	('MEDBL001', 'Bandages', '40'),
	('MEDBL002', 'Cream', '50');
/*!40000 ALTER TABLE `medication_buying_list` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.medication_selling_list
CREATE TABLE IF NOT EXISTS `medication_selling_list` (
  `MedicationID` varchar(10) NOT NULL,
  `MedicationName` varchar(100) NOT NULL,
  `Price` varchar(50) NOT NULL,
  PRIMARY KEY (`MedicationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.medication_selling_list: ~2 rows (approximately)
/*!40000 ALTER TABLE `medication_selling_list` DISABLE KEYS */;
INSERT INTO `medication_selling_list` (`MedicationID`, `MedicationName`, `Price`) VALUES
	('MEDSL001', 'Ointment', '90'),
	('MEDSL002', 'Panado', '100');
/*!40000 ALTER TABLE `medication_selling_list` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.patients
CREATE TABLE IF NOT EXISTS `patients` (
  `PatientID` varchar(10) NOT NULL,
  `Title` varchar(45) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Surname` varchar(45) NOT NULL,
  `DateOfBirth` varchar(50) NOT NULL,
  `Sex` enum('Male','Female') NOT NULL,
  `Address` varchar(45) NOT NULL,
  `Contact` varchar(13) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Occupation` varchar(45) NOT NULL,
  PRIMARY KEY (`PatientID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.patients: ~3 rows (approximately)
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` (`PatientID`, `Title`, `Name`, `Surname`, `DateOfBirth`, `Sex`, `Address`, `Contact`, `Email`, `Occupation`) VALUES
	('P001', 'Mr', 'James', 'Kevinson', '17-06-1993', 'Male', 'Rondies', '0739382492', 'james@mail.com', 'Farmer'),
	('P002', 'Mrs', 'Megan', 'Megs', '06-10-1992', 'Female', 'Claremnont', '0737445678', 'meg@mail.com', 'Student'),
	('P003', 'Mrs', 'Pat', 'Harpur', '08-07-1987', 'Female', 'Mauritius', '07432144567', 'pat@mail.com', 'Lecturer');
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.patient_appointments
CREATE TABLE IF NOT EXISTS `patient_appointments` (
  `PatientAppointmentID` varchar(10) NOT NULL,
  `AppointmentID` varchar(10) NOT NULL,
  `PatientID` varchar(10) NOT NULL,
  PRIMARY KEY (`PatientAppointmentID`),
  KEY `AppointmentID_idx` (`AppointmentID`),
  KEY `PatientID_idx` (`PatientID`),
  CONSTRAINT `AppointmentID` FOREIGN KEY (`AppointmentID`) REFERENCES `appointments` (`AppointmentID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `PatientID` FOREIGN KEY (`PatientID`) REFERENCES `patients` (`PatientID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.patient_appointments: ~18 rows (approximately)
/*!40000 ALTER TABLE `patient_appointments` DISABLE KEYS */;
INSERT INTO `patient_appointments` (`PatientAppointmentID`, `AppointmentID`, `PatientID`) VALUES
	('PA002', 'APT002', 'P002'),
	('PA003', 'APT004', 'P001'),
	('PA004', 'APT005', 'P001'),
	('PA006', 'APT008', 'P001'),
	('PA007', 'APT010', 'P002'),
	('PA008', 'APT011', 'P001'),
	('PA009', 'APT012', 'P001'),
	('PA010', 'APT013', 'P002'),
	('PA011', 'APT014', 'P003'),
	('PA012', 'APT015', 'P001'),
	('PA013', 'APT016', 'P002'),
	('PA014', 'APT017', 'P003'),
	('PA015', 'APT019', 'P003'),
	('PA016', 'APT020', 'P003'),
	('PA017', 'APT021', 'P003'),
	('PA018', 'APT023', 'P002'),
	('PA019', 'APT024', 'P001'),
	('PA020', 'APT025', 'P001');
/*!40000 ALTER TABLE `patient_appointments` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.patient_archives
CREATE TABLE IF NOT EXISTS `patient_archives` (
  `PatientID` varchar(10) NOT NULL,
  `Title` varchar(45) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Surname` varchar(45) NOT NULL,
  `DateOfBirth` varchar(50) NOT NULL,
  `Sex` enum('Male','Female') NOT NULL,
  `Address` varchar(45) NOT NULL,
  `Contact` varchar(13) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Occupation` varchar(45) NOT NULL,
  PRIMARY KEY (`PatientID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

-- Dumping data for table woundmanagementsystem.patient_archives: ~0 rows (approximately)
/*!40000 ALTER TABLE `patient_archives` DISABLE KEYS */;
INSERT INTO `patient_archives` (`PatientID`, `Title`, `Name`, `Surname`, `DateOfBirth`, `Sex`, `Address`, `Contact`, `Email`, `Occupation`) VALUES
	('P004', 'Mr', 'Tim', 'Jones', '15-06-1974', 'Male', 'Mauritius', '0747283888', 'tim@mail.com', 'Doctor'),
	('P005', 'Ms', 'sdaf', 'sdf', '15-11-2017', 'Male', 'dsf', '324', 'a@d.c', 'efrewr');
/*!40000 ALTER TABLE `patient_archives` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.patient_visits
CREATE TABLE IF NOT EXISTS `patient_visits` (
  `PatientVisitID` varchar(10) NOT NULL,
  `PatientID` varchar(10) NOT NULL,
  `Notes` varchar(500) NOT NULL,
  `Date` varchar(12) NOT NULL,
  `Time` varchar(12) NOT NULL,
  `IncomeID` varchar(45) NOT NULL,
  `AfterOpening` varchar(45) NOT NULL,
  `ActionsPerformed` varchar(45) NOT NULL,
  `BeforeClosing` varchar(45) NOT NULL,
  `Type` varchar(50) NOT NULL,
  PRIMARY KEY (`PatientVisitID`),
  KEY `IncomeID_idx` (`IncomeID`),
  KEY `FK_patient_visits_patients` (`PatientID`),
  CONSTRAINT `FK_patient_visits_patients` FOREIGN KEY (`PatientID`) REFERENCES `patients` (`PatientID`),
  CONSTRAINT `IncomeID` FOREIGN KEY (`IncomeID`) REFERENCES `income` (`IncomeID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.patient_visits: ~10 rows (approximately)
/*!40000 ALTER TABLE `patient_visits` DISABLE KEYS */;
INSERT INTO `patient_visits` (`PatientVisitID`, `PatientID`, `Notes`, `Date`, `Time`, `IncomeID`, `AfterOpening`, `ActionsPerformed`, `BeforeClosing`, `Type`) VALUES
	('PV001', 'P001', 'notes', '11-09-2017', '20:14', 'INC001', 'after', 'actions', 'before', 'Chronic Ulcer'),
	('PV002', 'P001', 'd', '15-09-2017', '11:27', 'INC002', 'a', 'b', 'c', 'Chronic Ulcer'),
	('PV003', 'P002', 'Lorem ipsum dolor sit amet, malorum', '15-09-2017', '08:47', 'INC004', 'Lorem ipsum dolor sit amet, malorum', 'Lorem ipsum dolor sit amet, malorum', 'Lorem ipsum dolor sit amet, malorum', 'Burn'),
	('PV004', 'P001', 'a', '16-09-2017', '07:29', 'INC005', 'a', 'a', 'a', 'Diabetic Foot'),
	('PV005', 'P002', 'hh', '08-10-2017', '13:51', 'INC006', 'dd', 'ff', 'gg', 'Chronic Ulcer'),
	('PV006', 'P001', '', '09-10-2017', '20:19', 'INC007', 'ee', 'ee', 'ee', 'Chronic Ulcer'),
	('PV007', 'P001', 'asd', '10-10-2017', '20:47', 'INC008', 'asd', 'asd', 'asd', 'Chronic Ulcer'),
	('PV008', 'P003', '', '11-10-2017', '09:32', 'INC011', 'Sample', 'Sample', 'Sample', 'Burn'),
	('PV009', 'P002', '', '10-10-2017', '16:53', 'INC012', 'asd', 'sad', 'sad', 'Chronic Ulcer'),
	('PV010', 'P002', '', '15-10-2017', '19:11', 'INC016', 'h', 'h', 'h', 'Burn'),
	('PV011', 'P003', '', '15-10-2017', '19:22', 'INC018', 'Lorem ipsum dolor sit amet, erat debet timeam', 'Lorem ipsum dolor sit amet, erat debet timeam', 'Lorem ipsum dolor sit amet, erat debet timeam', 'Burn'),
	('PV012', 'P001', '', '15-10-2017', '19:07', 'INC019', 'd', 'd', 'd', 'Burn'),
	('PV013', 'P003', 's', '24-10-2017', '16:29', 'INC020', 'f', 'd', 's', 'Burn'),
	('PV014', 'P001', '', '01-11-2017', '09:38', 'INC021', 'g', 'f', 'b', 'Burn'),
	('PV015', 'P001', '', '06-11-2017', '08:04', 'INC022', 's', 's', 's', 'Burn');
/*!40000 ALTER TABLE `patient_visits` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.patient_visit_medication
CREATE TABLE IF NOT EXISTS `patient_visit_medication` (
  `ID` varchar(10) NOT NULL,
  `PatientVisitID` varchar(45) DEFAULT NULL,
  `MedicationID` varchar(45) DEFAULT NULL,
  `Quantity` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `PatientVisitMedicationID_idx` (`PatientVisitID`),
  KEY `MedicationID_idx` (`MedicationID`),
  CONSTRAINT `MedicationID` FOREIGN KEY (`MedicationID`) REFERENCES `stock` (`MedicationID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `PatientVisitMedicationID` FOREIGN KEY (`PatientVisitID`) REFERENCES `patient_visits` (`PatientVisitID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.patient_visit_medication: ~10 rows (approximately)
/*!40000 ALTER TABLE `patient_visit_medication` DISABLE KEYS */;
INSERT INTO `patient_visit_medication` (`ID`, `PatientVisitID`, `MedicationID`, `Quantity`) VALUES
	('PVM001', 'PV001', 'MD001', '1'),
	('PVM002', 'PV002', 'MD001', '1'),
	('PVM003', 'PV003', 'MD002', '1'),
	('PVM004', 'PV004', 'MD001', '1'),
	('PVM005', 'PV005', 'MD002', '1'),
	('PVM006', 'PV006', 'MD001', '1'),
	('PVM007', 'PV007', 'MD002', '1'),
	('PVM008', 'PV008', 'MD001', '1'),
	('PVM009', 'PV009', 'MD001', '1'),
	('PVM010', 'PV010', 'MD002', '1'),
	('PVM011', 'PV011', 'MD002', '1'),
	('PVM012', 'PV012', 'MD002', '1'),
	('PVM013', 'PV013', 'MD002', '1'),
	('PVM014', 'PV014', 'MD002', '1'),
	('PVM015', 'PV015', 'MD002', '1');
/*!40000 ALTER TABLE `patient_visit_medication` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.patient_visit_services
CREATE TABLE IF NOT EXISTS `patient_visit_services` (
  `ID` varchar(10) NOT NULL,
  `PatientVisitID` varchar(10) NOT NULL,
  `ServiceID` varchar(10) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `PatientVisitServicesID_idx` (`PatientVisitID`),
  KEY `FK_patient_visit_services_services` (`ServiceID`),
  CONSTRAINT `FK_patient_visit_services_services` FOREIGN KEY (`ServiceID`) REFERENCES `services` (`ServiceID`),
  CONSTRAINT `PatientVisitServicesID` FOREIGN KEY (`PatientVisitID`) REFERENCES `patient_visits` (`PatientVisitID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.patient_visit_services: ~10 rows (approximately)
/*!40000 ALTER TABLE `patient_visit_services` DISABLE KEYS */;
INSERT INTO `patient_visit_services` (`ID`, `PatientVisitID`, `ServiceID`) VALUES
	('PVS001', 'PV001', 'S001'),
	('PVS002', 'PV002', 'S002'),
	('PVS003', 'PV003', 'S001'),
	('PVS004', 'PV004', 'S002'),
	('PVS005', 'PV005', 'S002'),
	('PVS006', 'PV006', 'S002'),
	('PVS007', 'PV007', 'S002'),
	('PVS008', 'PV008', 'S001'),
	('PVS009', 'PV009', 'S002'),
	('PVS010', 'PV010', 'S002'),
	('PVS011', 'PV011', 'S001'),
	('PVS012', 'PV012', 'S001'),
	('PVS013', 'PV013', 'S002');
/*!40000 ALTER TABLE `patient_visit_services` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.services
CREATE TABLE IF NOT EXISTS `services` (
  `ServiceID` varchar(10) NOT NULL,
  `ServiceType` varchar(100) NOT NULL,
  `ServiceName` varchar(100) NOT NULL,
  `Price` varchar(10) NOT NULL,
  PRIMARY KEY (`ServiceID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.services: ~2 rows (approximately)
/*!40000 ALTER TABLE `services` DISABLE KEYS */;
INSERT INTO `services` (`ServiceID`, `ServiceType`, `ServiceName`, `Price`) VALUES
	('S001', 'Stitching', 'Stitches', '100'),
	('S002', 'Clean up', 'Clean up', '150');
/*!40000 ALTER TABLE `services` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.staff
CREATE TABLE IF NOT EXISTS `staff` (
  `StaffID` varchar(10) NOT NULL,
  `Status` varchar(45) NOT NULL,
  `Title` varchar(45) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Surname` varchar(45) NOT NULL,
  `DateOfBirth` varchar(45) NOT NULL,
  `Sex` enum('Male','Female') NOT NULL,
  `Address` varchar(45) NOT NULL,
  `TelNo` varchar(13) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Rate` varchar(45) NOT NULL,
  `Occurrence` varchar(45) NOT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `Rights` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`StaffID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.staff: ~3 rows (approximately)
/*!40000 ALTER TABLE `staff` DISABLE KEYS */;
INSERT INTO `staff` (`StaffID`, `Status`, `Title`, `Name`, `Surname`, `DateOfBirth`, `Sex`, `Address`, `TelNo`, `Email`, `Rate`, `Occurrence`, `Password`, `Rights`) VALUES
	('ST001', 'Wound Manager', 'Mr', 'Nil', 'Emrith', '10-10-1988', 'Male', 'Mauritius', '0747293746', 'emrith@mail.com', '100', 'per hour', 'pass', 'Administrative'),
	('ST002', 'Receptionist', 'Ms', 'Shesh', 'Emrith', '25-08-1970', 'Female', 'Mauritius', '07853654724', 'shesh@mail.com', '150', 'per hour', 'pass', 'Normal');
/*!40000 ALTER TABLE `staff` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.staff_schedule
CREATE TABLE IF NOT EXISTS `staff_schedule` (
  `ShiftID` varchar(10) NOT NULL,
  `Date` varchar(12) NOT NULL,
  `StartTime` varchar(12) NOT NULL,
  `EndTime` varchar(12) NOT NULL,
  `StaffID` varchar(10) NOT NULL,
  PRIMARY KEY (`ShiftID`),
  KEY `StaffScheduleID_idx` (`StaffID`),
  CONSTRAINT `StaffScheduleID` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`StaffID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.staff_schedule: ~7 rows (approximately)
/*!40000 ALTER TABLE `staff_schedule` DISABLE KEYS */;
INSERT INTO `staff_schedule` (`ShiftID`, `Date`, `StartTime`, `EndTime`, `StaffID`) VALUES
	('STFS001', '18-10-2017', '09:00', '16:00', 'ST001'),
	('STFS003', '19-10-2017', '08:00:00', '17:00:00', 'ST001'),
	('STFS004', '18-10-2017', '08:00:00', '16:00:00', 'ST002'),
	('STFS005', '17-10-2017', '08:00:00', '16:00:00', 'ST001'),
	('STFS006', '19-10-2017', '08:00:00', '16:00:00', 'ST002'),
	('STFS007', '22-10-2017', '08:00', '16:00', 'ST002'),
	('STFS008', '30-10-2017', '08:00:00', '16:00:00', 'ST002');
/*!40000 ALTER TABLE `staff_schedule` ENABLE KEYS */;

-- Dumping structure for table woundmanagementsystem.stock
CREATE TABLE IF NOT EXISTS `stock` (
  `MedicationID` varchar(10) NOT NULL,
  `MedicationName` varchar(500) NOT NULL,
  `QuantityAvailable` varchar(50) NOT NULL,
  `Price` varchar(50) NOT NULL,
  PRIMARY KEY (`MedicationID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table woundmanagementsystem.stock: ~3 rows (approximately)
/*!40000 ALTER TABLE `stock` DISABLE KEYS */;
INSERT INTO `stock` (`MedicationID`, `MedicationName`, `QuantityAvailable`, `Price`) VALUES
	('MD001', 'Cream', '0', '150'),
	('MD002', 'Bandage', '9', '50');
/*!40000 ALTER TABLE `stock` ENABLE KEYS */;

-- Dumping structure for trigger woundmanagementsystem.archive_patient
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `archive_patient` BEFORE DELETE ON `patients` FOR EACH ROW BEGIN
INSERT INTO patient_archives VALUES(OLD.PatientID, OLD.Title, OLD.Name, OLD.Surname, OLD.DateOfBirth, OLD.Sex,
OLD.Address, OLD.Contact, OLD.Email, OLD.Occupation);
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- Dumping structure for trigger woundmanagementsystem.delete_visit
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `delete_visit` AFTER INSERT ON `patient_archives` FOR EACH ROW BEGIN
DELETE FROM patient_visits WHERE patient_visits.PatientID = NEW.PatientID;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- Dumping structure for trigger woundmanagementsystem.delete_visit_med
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `delete_visit_med` BEFORE DELETE ON `patient_visits` FOR EACH ROW BEGIN
DELETE FROM patient_visit_medication WHERE patient_visit_medication.PatientVisitID = OLD.PatientVisitID;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- Dumping structure for trigger woundmanagementsystem.delete_visit_serv
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `delete_visit_serv` AFTER DELETE ON `patient_visit_medication` FOR EACH ROW BEGIN
DELETE FROM patient_visit_services WHERE patient_visit_services.PatientVisitID = OLD.PatientVisitID;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
