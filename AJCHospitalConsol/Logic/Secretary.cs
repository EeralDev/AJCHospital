using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    internal class Secretary: IObserver
    {
        private static readonly Lazy<Secretary> lazyInstanceSecretary = new Lazy<Secretary>(() => new Secretary());

        private string _firstname = "Ms_OneSecratary";
        private string _lastname = "From_Hospital";


        // Les propriétés de la secrétaire
        public string FirstNameSecretary { get; set; }
        public string LastNameSecretary { get; set; }

        private UnikQueuePatient queuePatients;

        // Constructeur privé pour empêcher l'instanciation directe
        private Secretary()
        {
            // Initialisation de la secrétaire
            this.queuePatients = UnikQueuePatient.InstanceQueuePatient;
        }

        // Propriété statique pour accéder à l'instance unique de la classe Secretary
        public static Secretary InstanceSecretary => lazyInstanceSecretary.Value;

        // Dans cette implémentation, nous utilisons Lazy<T> pour garantir une
        // initialisation unique et thread-safe de l'instance de Secretary.
        // L'utilisation de Lazy<T> garantit que l'instance est créée uniquement
        // lorsqu'elle est demandée pour la première fois
        //et que l'initialisation est effectuée de manière sûre pour les threads.

        //Vous pouvez ensuite accéder à l'instance unique de la secrétaire en
        //utilisant Secretary.InstanceSecretary.

        public void AddPatientInQueue(Patient patient) 
        {
            queuePatients.AddPatient(patient);
        }
        public void DisplayNextPatient()
        {
            Patient nextPatient = queuePatients.NextPatient();
            // Affiche les informations du prochain patient
            nextPatient.ToString();
        }
        public void DisplayQueuePatients()
        {
            queuePatients.ToString();
        }

        public void Update(ISubject subject)
        {
            ((IObserver)InstanceSecretary).Update(subject);
        }
        //Les opérations de la secrétaire :
        //1. Rajouter a la file d’attente un patient :
        //a.si existant, dans ce cas il faut le récupérer de la base de données
        //b.s’il est nouveau il faudra l’inscrire en base
        //2. Afficher la file d’attente
        //3. Afficher le prochain patient de la file(sans le retirer)
        //4. Sortir du menu et revenir au menu principal
        //La secrétaire permet d afficher, pour un patient la liste de ses visites en base, selon 
        //son identifiant
    }
}
