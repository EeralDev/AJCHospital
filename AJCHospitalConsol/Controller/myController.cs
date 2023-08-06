using AJCHospitalConsol.DAL;
using AJCHospitalConsol.DAL.DOA;
using AJCHospitalConsol.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Controller
{
    public class myController
    {
        //Attribut

        private Hospital _myHospital;

        public Hospital MyHospital { get => this._myHospital; set => _myHospital = value; }

        public myController ()
        {
            this._myHospital = Hospital.Instance;
        }



        // Méthode connexion et deconnexion
        public User_T Authentication(string userName, string password)
        {
            User_T currentUser = new AJCHospitalEntities().User_T.FirstOrDefault(item => item.UserName == userName && item.Password == password);
            if (currentUser != null)
            {
                if (currentUser.EmpCode == "0")
                {
                    this.MyHospital.Secretary = currentUser;
                }
                else
                {
                    this._myHospital.Rooms.AddRoom(currentUser, this.MyHospital);
                }
            }
            return (currentUser != default(User_T)? currentUser : null);
        }

        public void Disconnect(User_T currentUser)
        {
            if (currentUser.EmpCode == "0")
            {
                this.MyHospital.Secretary = null;
            }
            else
            {
                this._myHospital.Rooms.CloseRoom(currentUser);
            }
        }

        // Méthode Base de donnée
        public Patient_T CreateNewPatient(string socialSecurityID, string firstName, string lastName, int Age, string Adress, string Telephone)
        { 
            DAOPatient DAOPatient = new DAOPatient();
            DAOPatient.Insert(new Patient_T {SocialSecurityID=socialSecurityID, FirstName=firstName, LastName=lastName, Age=Age, Adress=Adress, Tel=Telephone}, out int ID);
            return DAOPatient.SelectById(ID);
        }
        public Patient_T FindPatient (string socialSecurityID)
        {
            Patient_T result = new DAOPatient().SelectAll().FirstOrDefault(item => item.SocialSecurityID == socialSecurityID);
            return (result != default(Patient_T)? result : null);
        }
        public List<Patient_T> FindPatient(string lastName,string firstName)
        {
            List<Patient_T> patients = new DAOPatient().SelectAll().Where(item => item.LastName == lastName && item.FirstName == firstName).ToList();
            return (patients.Count == 0)? null :patients;
        }
        //TODO : A tester
        public int SaveConsultation(Consultation_T consultation)
        {
            return new DAOConsultation().Insert(consultation, out int ID);
        }
        //TODO : A tester
        public int SaveConsultation(List<Consultation_T> consultations)
        {
            return new DAOConsultation().Insert(consultations, out List<int> IDs);
        }
        //TODO : A tester
        public List<Consultation_T> getPatientConsultation (int patientID)
        {
            return new DAOPatient().SelectById(patientID).Consultation_T.ToList();
        }
        //TODO : A tester
        public List<Consultation_T> getDoctorConsultation(int userID)
        {
            return new DAOUser().SelectById(userID).Consultation_T.ToList();
        }
        
        //Méthode Objet Hospital
        public Queue<Patient_T> AddPatient(Patient_T patient)
        {
            this.MyHospital.Patients.Enqueue(patient);
            using (StreamWriter myStreamWriter = File.AppendText($"..\\..\\Log\\{System.DateTime.Today.Date.ToString("dd_MM_yyyy")}patient.log")) 
            {
                myStreamWriter.WriteLine($"- Heure d'arrivé du patient : {System.DateTime.Now} / " +
                    $"Identité du patient : {patient.FirstName} {patient.LastName} / " +
                    $"Numéro de sécurité Social du Patient : {patient.SocialSecurityID}");
            }
            return this.MyHospital.Patients;

        }
        public Queue<Patient_T> GetPatient()
        {            
            return this.MyHospital.Patients;
        }
        public Patient_T CleanRoom(User_T currentDoctor)
        {
            int myIndex = Array.FindIndex(this.MyHospital.Rooms.MyRoomArray, item => item.RoomDoctor.UserID == currentDoctor.UserID);
            if (myIndex == -1)
            {
                return null;
            }
            else 
            {
                this.MyHospital.Rooms.MyRoomArray[myIndex].EndConsultation();
                return this.MyHospital.Rooms.MyRoomArray[myIndex].RoomPatient;
            }
        }
        public  Patient_T NextPatient()
        {
            return this.MyHospital.ShowNextPatient();
        }
        public int SaveConsultationFromView(User_T currentDoctor)
        {
            int myIndex = Array.FindIndex(this.MyHospital.Rooms.MyRoomArray, item => item.RoomDoctor.UserID == currentDoctor.UserID);
            if (myIndex == -1)
            {
                return 0;
            }
            else
            {
                //this.MyHospital.Rooms.MyRoomArray[myIndex].EndConsultation();
                return this.MyHospital.Rooms.MyRoomArray[myIndex].RecordConsultation();
            }
        }
    }
}
