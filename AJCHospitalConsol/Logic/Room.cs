using AJCHospitalConsol.Controller;
using AJCHospitalConsol.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    internal class Room : ISubject
    {
        // Attributs propre métier

        private int _roomNumber;
        private User_T _roomDoctor;
        private Patient_T _roomPatient;
        private Double _price = 23;

        Consultation_T[] _consultationArray;
        Consultation_T _currentConsultation;

        //Attribut propre au pattern Observeur à notifier
        private Hospital _myHospital;

        // properties

        public int RoomNumber 
        {
            get { return _roomNumber; }
            set { _roomNumber = value; }
        }

        public Patient_T RoomPatient
        {
            get { return _roomPatient; }
            set { _roomPatient = value; }
        }
        public User_T RoomDoctor { get { return _roomDoctor; } }

        public Double Price { get { return _price; } }
        public Consultation_T[] ConsultationArray { get { return _consultationArray;  } }
        public Consultation_T CurrentConsultation { get { return _currentConsultation; } }

        public Hospital MyHospital { get { return _myHospital; } }

        // méthode ISubject
        public void Attach(Hospital hospital)
        {
            Console.WriteLine("Je suis la méthode attach de room qui attache un hopital");
            this._myHospital = hospital;
        }
        public void Detach(Hospital hospital)
        {
            Console.WriteLine("Je suis la méthode detach de room qui detache un hopital");
            this._myHospital = null;
        }
        public void Notify()
        {
            Console.WriteLine("Je suis la méthode notify de room qui notifie mon hopital");
            this._myHospital.Update(this);
        }

        // Méthode propre au métier
        public Room(int RoomNumber, User_T myDoctor, Hospital hospital)
        {
            this._roomNumber = RoomNumber;
            this._roomDoctor = myDoctor;
            this.Attach(hospital);
            this.StartConsultation();
        }
        //Ne pas oublier la situation ou il n'y a pas de patient dans l'hopital
        public void StartConsultation()
        {
            this.Notify();
            //Construction d'objet en inline cad relié directement à la base de données crée par EF
            this._currentConsultation = new Consultation_T
            {
                PatID = _roomPatient.PatientID,
                PatSocialSecurityID = _roomPatient.SocialSecurityID,
                DocID = RoomDoctor.UserID,
                DocName = _roomDoctor.UserName,
                StartTime = System.DateTime.Now,
                RoomNumber = this._roomNumber,
                Price = this.Price
            };
        }

        //Que se passe-t-il lorsque ConsultationArray est plein
        public void EndConsultation()
        {
            int indexNull= ConsultationArray.ToList().FindIndex(consultation => consultation == null);
            this.ConsultationArray[indexNull] = this.CurrentConsultation;
            this.StartConsultation();
        }
        public void RecordConsultation()
        {
            new myController().SaveConsultation(ConsultationArray.ToList());
            ConsultationArray[ConsultationArray.Length - 1] = null;
        }
        public void Attach(IObserver observer)
        {
            throw new NotImplementedException();
        }
        public void Detach(IObserver observer)
        {
            throw new NotImplementedException();
        }
    }
}
