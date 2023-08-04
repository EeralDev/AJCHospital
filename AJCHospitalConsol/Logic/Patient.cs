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
        private int _id;
        private string _socialSecurityID;
        private string _firstname;
        private string _lastname;
        private int _age;
        private int _tel;
        private string _adress;

        // properties
        public int Id
        {
            get { return _id; }
        }
        public string SocialSecurityID
        {
            get { return _socialSecurityID; }
            set { _socialSecurityID = value; }
        }
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
        public int Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }
        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }
        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }
        public string Adress
        {
            get { return _adress; }
            set { _adress = value; }
        }

        // constructeurs
        public Patient() { }
        public Patient(int id, string socialSecurityID, string firstname, string lastname, int tel) : this()
        {
            this._id = id;
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
            string response = $"Eléments du patient : id : {Id} \t n° secu = {SocialSecurityID}\n";
            response += $" nom : {Lastname} , prenom : {Firstname} , telephone : {Tel}\n";
            response += $" age : {Age} , adresse : {Adress}\n";

            return response;
        }
    }
}

