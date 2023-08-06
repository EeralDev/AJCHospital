using AJCHospitalConsol.DAL;
using AJCHospitalConsol.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AJCHospitalConsol.Logic
{
    // L'Hospital observe l'occupation des salles : observateur , hérite de l'interface IObserver
    // et l'Hospital est observé par les salles.
    // L'Hospital est unique : un singleton en mode thread safe et utilise Lazy

    public class Hospital
    {
        //la classe Hopital utilise le modèle Singleton avec un verrouillage
        //explicite pour garantir un accès thread-safe à la liste de patients.
        //Les méthodes pour ajouter un patient et obtenir la liste de patients
        //sont protégées par des verrouillages pour assurer la sécurité
        //dans un environnement multithread.

        private static readonly Lazy<Hospital> lazyInstanceHospital = new Lazy<Hospital>(() => new Hospital());
        private static readonly object lockObject = new object();

        // attributs  propre métiers 
        private string _nameHospital;
        private User_T secretary;

        private Queue<Patient_T> _patients; 
        public Queue<Patient_T> Patients { get => _patients; set => _patients=value; }

        //Attribut propre au ISubject => l'observer a notifier

        private Rooms _rooms;
        
        public Rooms Rooms
        { 
            get { return _rooms; }
            set { _rooms = value; }
        }

        // properties Hospital
        public string NameHospital { get { return _nameHospital; } }

        public User_T Secretary { get; set; }

        // constructeurs
        private Hospital()
        {
            this._nameHospital = "BIMHospital";
            // initialisation de l'attribut Patients à une liste de patients vide
            this.Patients = new Queue<Patient_T>();
            this.Rooms = new Rooms();
        }
        // Propriété statique pour accéder à l'instance unique de l'hôpital
        public static Hospital Instance => lazyInstanceHospital.Value;

        // Méthodes : 
        // Méthode propre a IObserver avec comme sujet Room
        // Ne pas oublier les autres situation ex : il n'y a aucun Patient en fil d'attente
        public void Update(Room NotifyRoom)
        {
            if (Patients.Count > 0)
            {
                NotifyRoom.RoomPatient = Patients.Dequeue();
            }
            else
            {
                NotifyRoom.RoomPatient = null;
            }
        }
        //Méthode propre à ISubject avec comme observateur Rooms
        // Que faire lorsque un room est déjà attacher ou qu'il est déja null
        public void Attach(Rooms rooms)
        {
            this._rooms = rooms;
        }
        public void Detach(Hospital hospital)
        {
            this._rooms = null;
        }
        public void Notify()
        {
            this._rooms.Update(this);
        }
        // Méthode métiers
        // Méthode métiers en liens avec attribut secrétaire 
        // Rajouter le role de l'utilisateur
        public void GetSecretary(User_T mySecretary)
        {
            this.secretary = mySecretary;
        }
        public void DisconnectSecretary()
        {
            this.secretary = null;
        }
        //Méthode métiers en lien avec la Queue<Patient_T>
        //Penser au situation ou patient etc ...
        //NextPatient retire l'élément en tête de la file d'attente
        //Si file d'attente vide et que vous appelez la méthode Dequeue(), 
        // une exception de type InvalidOperationException sera levée
        //il est recommandé de vérifier la taille de la file avant de retirer un élément
        //en utilisant la propriété Count ou en vérifiant si la file est vide  avec la méthode Any() ou Count == 0.

        public void AddPatient(Patient_T patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }
            lock (lockObject)
            {
                Patients.Enqueue(patient);
            }
        }
        // Affiche le patient sans le retirer de la file
        public Patient_T ShowNextPatient()
        {
            return Patients.Any() ? Patients.Peek() : null;
        }
        // Recupere le patient n de la file en retirant le patient n-1
        public Patient_T GetNextPatient()
        {
            return Patients.Any() ? Patients.Dequeue() : null;
        }
        //Pour afficher une file d'attente en respectant l'ordre d'ajout, il est recommandé 
        //d'utiliser une copie de la file d'attente ou d'utiliser une boucle while en
        // vérifiant la condition Count pour terminer la boucle.
        public new string ToString()
        {
            while (Patients.Count > 0)
            {
                Patient_T patient = Patients.Dequeue();
                patient.ToString();
            }
            return $"Je suis Patient_T.";
        }
    }
}


