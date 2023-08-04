using AJCHospitalConsol.DAL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    // L'Hospital observe les patients : observateur , hérite de l'interface IObserver
    // L'Hospital est unique : un singleton en mode thread safe et utilise Lazy

    internal class Hospital : IObserver
    {
        //la classe Hopital utilise le modèle Singleton avec un verrouillage
        //explicite pour garantir un accès thread-safe à la liste de patients.
        //Les méthodes pour ajouter un patient et obtenir la liste de patients
        //sont protégées par des verrouillages pour assurer la sécurité
        //dans un environnement multithread.

        private static readonly Lazy<Hospital> lazyInstanceHospital = 
            new Lazy<Hospital>(() => new Hospital());
        private static readonly object lockObject = new object();

        // attributs
        // Propriété statique pour accéder à l'instance unique de l'hôpital
        public static Hospital InstanceHospital => lazyInstanceHospital.Value;
        private string _nameHospital;
        private QueuePatients _queuePatients;
        //private List<Doctors> _doctorList;

        // Les patients sont censés être connu dans les systèmes de
        // l’hôpital donc extrait de la base de données DAO Patients
        // la v ue est censé me forunir un patient a ajouter
        // pour ensuite prétendre être rajouté à la file d’attente

        public Secretary OneSecretary { get; }

        //public void AddPatientInQueue(Patient patient)
        //{
        //    _queuePatients.AddPatient(patient);
        //}
        public void DisplayNextPatient()
        {
            Patient nextPatient = _queuePatients.NextPatient();
            // Affiche les informations du prochain patient
            nextPatient.ToString();
        }
        public void DisplayQueuePatients()
        {
            // Utilisation de la méthode ToString de la classe Patient
            _queuePatients.ToString();
        }

        // constructeurs
        public Hospital() {}
        public Hospital(string nameHospital)
        {
            this._nameHospital = nameHospital;
        }
        public void Update(ISubject subject)
        {
            throw new NotImplementedException();
        }

        public void Update(ISubject subject)
        {
            throw new NotImplementedException();
        }
    }
}
