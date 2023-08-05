using AJCHospitalConsol.DAL;
using AJCHospitalConsol.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AJCHospitalConsol.Logic
{
    internal class LogicTest
    {
        //static void Main(string[] args)
        //{
        //    TestHospitalManagement();
        //}
        //static void TestHospitalManagement()
        //{
        //    // Création de l'hôpital
        //    Hospital myHospital;

        //    // Création de patients
        //    Patient_T patient1 = new Patient_T("1255", "316HH", "toto", "titi",  21, "3rue du Gral de Gaulle");
        //    Patient_T patient2 = new Patient_T(1255, "316HH", "coco", "cici", 54466675, 12, "7 rue du Mrchal lecler");

        //    //// Création de médecins
        //    //Medecin medecin1 = new Medecin("Dr. Dupont", 1);
        //    //Medecin medecin2 = new Medecin("Dr. Martin", 2);

        //    //// Ajout des patients à l'hôpital par la secrétaire
        //    Secretary.AddPatientInQueue(patient1);
        //    Secretary.AddPatientInQueue(patient2);
            //Hospital.AddMedecin(medecin1);
            //Hospital.AddMedecin(medecin2);

            //// Création de rendez-vous
            //Visite rdv1 = new Visite(DateTime.Now.AddDays(1), patient1, medecin1);
            //Visite rdv2 = new Visite(DateTime.Now.AddDays(2), patient2, medecin2);

            //// Prise de rendez-vous
            //Hospital.AddVisite(rdv1);
            //Hospital.AddVisite(rdv2);

            //// Affichage des patients, médecins et rendez-vous
            //Hospital.DisplayPatients();
            //Hospital.DisplayMedecins();
            //Hospital.DisplayVisites();
        }
    }
