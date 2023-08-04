using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    internal class UnikQueuePatient :ISubject
    {

        //La classe UnikQueuePatient utilise une Queue<Patient> pour stocker les éléments de la file d'attente
        // cette classe est observé par l'hopital, la secrétaire et les médecins.
        // L'utilisation du modèle Singleton garantit qu'une seule instance 
        //de la classe UnikQueuePatient est utilisée dans toute l'application, ce qui permet 
        //de centraliser l'accès à la file d'attente et d'éviter les conflits d'accès concurrents.

        private static UnikQueuePatient instance = null;
        private static readonly object lockObject = new object();

        //Queue<Patient> est une classe du namespace System.Collections.Generic qui représente une 
        //file d'attente de Patients. Une file d'attente suit le principe du 
        //"premier entré, premier sorti" (First-In-First-Out ou FIFO)

        private Queue<Patient> queuePatient;

        //--------------properties-------------

        // Méthode statique pour accéeder à l'instance unique de la classe UnikQueuePatient
        public static UnikQueuePatient InstanceQueuePatient
        {
            get
            {
                lock (lockObject)
                {
                    if (instance is null)
                    {
                        instance = new UnikQueuePatient();
                    }
                    return instance;
                }
            }
        }
        public Queue<Patient> QueuePatient
        {
            get { return queuePatient; }
        }
        //-----------constructeurs--------------

        // Empêcher l'instanciation directe de la classe en rendant le constructeur privé
        // ou bien protégé si on a besoin d'hériter de la classe UnikQueuePatient
        private UnikQueuePatient()
        {
            queuePatient = new Queue<Patient>();
        }
        // méthodes 

        public void AddPatient(Patient patient)
        {
            lock (lockObject) 
            {
                queuePatient.Enqueue(patient);
            }
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
             lock (lockObject)
             {
                    return queuePatient.Any() ? queuePatient.Peek() : null;
             }
        }
        public Patient ConsultNextPatient()
        {
            lock (lockObject)
            {
                if (IsNoEmpty())
                {
                    Patient nextConsultPatient = queuePatient.Dequeue();
                    return nextConsultPatient;
                }
                return null;
            }
        }
        public virtual bool IsNoEmpty()
        {
            bool isPatient = queuePatient.Any();
            return isPatient;
        }
        //Pour parcourir une file d'attente en respectant l'ordre d'ajout, il est recommandé 
        //d'utiliser une copie de la file d'attente ou 
        //d'utiliser une boucle while en vérifiant la condition Count pour terminer la boucle.
        public new string ToString()
        {
            lock (lockObject)
            {
                while (QueuePatient.Count > 0)
                {
                    Patient patient = QueuePatient.Dequeue();
                    patient.ToString();

                    // Implémentation de la méthode ToString() pour le singleton unikQueuePatient
                }
                return $"Je suis l'instance unique du singleton unikQueuePatient.";
            }
        }

        public void Attach(IObserver observer)
        {
            ((ISubject)QueuePatient).Attach(observer);
        }
        public void Detach(IObserver observer)
        {
            ((ISubject)InstanceQueuePatient).Detach(observer);
        }
        public void Notify()
        {
            ((ISubject)InstanceQueuePatient).Notify();
        }
    }
}
        //la classe UnikQueuePatient est implémentée en utilisant le 
        //modèle Singleton avec un verrouillage explicite (lock) 
        //pour garantir un accès thread-safe à la file
        //d'attente des patients. Les méthodes pour ajouter 
        //un patient, consulter le prochain patient 
        //et consulter le patient suivant sont également protégées par des verrouillages.