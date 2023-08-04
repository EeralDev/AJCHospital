using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    internal class Patient
    {
        // Observateur (Patient) qui observe l'hopital et réciproquement
        private int _patientID;
        private string _socialSecurityID;
        private string _firstname;
        private string _lastname;
        private int _age;
        private int _tel;
        private string _adress;

        // properties

        // Pas de setters properties pour ne pas etre en défaut par rapport
        // a la base de données DAL.DAO
        public int PatientID
        {
            get { return _patientID; }
        }
        public string SocialSecurityID
        {
            get { return _socialSecurityID; }
        }
        public int Age
        {
            get { return _age; }
        }
        public int Tel
        {
            get { return _tel; }
        }
        public string Lastname
        {
            get { return _lastname; }
        }
        public string Firstname
        {
            get { return _firstname; }
        }
        public string Adress
        {
            get { return _adress; }
        }
        // constructeurs
        public Patient() { }
        public Patient(int patientID, string socialSecurityID, string firstname, string lastname, int tel) : this()
        {
            this._patientID = patientID;
            this._socialSecurityID = socialSecurityID;
            this._firstname = firstname;
            this._lastname = lastname;
            this._tel = tel;
        }
        public Patient(int id,string socialSecurityID, string firstname, string lastname, int tel, int age, string adress) : this(id,socialSecurityID, firstname, lastname, tel)
        {
            this._age = age;
            this._adress = adress;
        }
        //Les patients sont censés remplir un formulaire de données personnelles
        //Les patients sont censés être connu dans les systèmes de l’hôpital 
        //pour ensuite prétendre être rajouté à la file d’attente

        //-----------------méthodes--------------

        public override string ToString()
        {
            string response = $"Eléments du patient : id : {PatientID} \t n° secu = {SocialSecurityID}\n";
            response += $" nom : {Lastname} , prenom : {Firstname} , telephone : {Tel}\n";
            response += $" age : {Age} , adresse : {Adress}\n";

            return response;
        }
    }
}

