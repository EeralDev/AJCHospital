using AJCHospitalConsol.DAL;
using AJCHospitalConsol.DAL.DOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Controller
{
    public class myController
    {
        public User_T Authentication(string userName, string password)
        {
            User_T result = new AJCHospitalEntities().User_T.FirstOrDefault(item => item.UserName == userName && item.Password == password);
            return (result != default(User_T)? result : null);
        }

        public Patient_T CreateNewPatient(string socialSecurityID, string firstName, string lastName, int Age, string Adress)
        { 
            DAOPatient DAOPatient = new DAOPatient();
            DAOPatient.Insert(new Patient_T {SocialSecurityID=socialSecurityID, FirstName=firstName, LastName=lastName, Age=Age, Adress=Adress}, out int ID);
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
    }
}
