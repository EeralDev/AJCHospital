using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    internal class QueuePatients : ISubject
    {
        // La classe QueuePatients utilise une Queue<Patient> pour stocker les éléments de la file d'attente
        // Cette classe est observée par l'hopital, la secrétaire et les médecins.

        //Queue<Patient> est une classe du namespace System.Collections.Generic qui représente une 
        //file d'attente de Patients. Une file d'attente suit le principe du 
        //"premier entré, premier sorti" (First-In-First-Out ou FIFO)

        private Queue<Patient> _queuePatient;

        //--------------properties-------------
        public Queue<Patient> QueuePatient
        {
            get { return _queuePatient; }
            set { _queuePatient = value; }
        }
        //-----------constructeurs--------------

        public QueuePatients() 
        { 
            QueuePatient = new Queue<Patient>();
        }

        // méthodes 
        public void AddPatientInQueue(Patient patient)
        {
            QueuePatient.Enqueue(patient);
        }
        //ConsultNextPatient Retire l'élément en tête de la file d'attente
        //Il est important de noter que si la file d'attente est 
        //vide et que vous appelez la méthode Dequeue(), 
        // une exception de type InvalidOperationException sera levée
        //par conséquent, il est recommandé de vérifier la taille de la file 
        //d'attente avant de retirer un élément en utilisant la propriété Count 
        //ou en vérifiant si la file d'attente est vide à l'aide de la méthode Any() ou Count == 0.
        // les méthodes sont déclarées virtuelles pour les classes filles dans la classe de base et peut 
        // être redéfinie dans les classes dérivées selon les besoins spécifiques.
        public Patient NextPatient()
        {
            return QueuePatient.Any() ? QueuePatient.Peek() : null;
        }
        public Patient ConsultNextPatient()
        {
            if (IsNoEmpty())
            {
                Patient nextConsultPatient = QueuePatient.Dequeue();
                return nextConsultPatient;
            }
            return null;
        }
        public virtual bool IsNoEmpty()
        {
            bool isPatient = QueuePatient.Any();
            return isPatient;
        }
        //Pour parcourir une file d'attente en respectant l'ordre d'ajout, il est recommandé 
        //d'utiliser une copie de la file d'attente ou 
        //d'utiliser une boucle while en vérifiant la condition Count pour terminer la boucle.
        public new string ToString()
        {
            while (QueuePatient.Count > 0)
            {
                Patient patient = QueuePatient.Dequeue();
                patient.ToString();
            }
            return $"Je suis QueuePatients.";
        }
        public void Attach(IObserver observer)
        {
            ((ISubject)QueuePatient).Attach(observer);
        }
        public void Detach(IObserver observer)
        {
            ((ISubject)QueuePatient).Detach(observer);
        }
        public void Notify()
        {
            ((ISubject)QueuePatient).Notify();
        }
    }
}