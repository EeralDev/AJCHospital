using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.DAL.DOA
{
    public class DAOPatient : IDAO<Patient_T>
    {
        public List<Patient_T> SelectAll()
        {
            return new AJCHospitalEntities().Patient_T.ToList();
        }

        public Patient_T SelectById(int id)
        {
            return new AJCHospitalEntities().Patient_T.Find(id);
        }

        public int Insert(Patient_T entity, out int ID)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            myContext.Patient_T.Add(entity);
            int result = myContext.SaveChanges();
            ID = entity.PatientID;
            return result;
        }

        public int Insert(List<Patient_T> entities, out List<int> IDs)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            foreach (Patient_T entity in entities)
            {
                myContext.Patient_T.Add(entity);

            }
            int result = myContext.SaveChanges();
            IDs = new List<int>();
            foreach (Patient_T entity in entities)
            {
                IDs.Add(entity.PatientID);
            }
            return result;
        }

        public int Update(Patient_T entity)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            myContext.Entry(myContext.Patient_T.Find(entity.PatientID)).CurrentValues.SetValues(entity);
            return myContext.SaveChanges();
        }

        public int Update(List<Patient_T> entities)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            foreach (Patient_T entity in entities)
            {
                myContext.Entry(myContext.Patient_T.Find(entity.PatientID)).CurrentValues.SetValues(entity); ;
            }
            return myContext.SaveChanges();
        }

        // Mettre a jour le delete pour supprimer les lignes de consultation avant de supprimer les Users
        public int Delete(Patient_T entity)
        {
            AJCHospitalEntities myDeleteContext = new AJCHospitalEntities();
            //Suppression des lignes consultation rattaché à l'utilisateur.
            int result = new DAOConsultation().Delete(myDeleteContext.Patient_T.Find(entity.PatientID).Consultation_T.ToList());
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            myContext.Patient_T.Remove(myContext.Patient_T.Find(entity.PatientID));
            return myContext.SaveChanges() + result;
        }

        public int Delete(List<Patient_T> entities)
        {
            AJCHospitalEntities myDeleteContext = new AJCHospitalEntities();
            int result = 0;
            foreach (Patient_T entity in entities)
            {
                result = result + new DAOConsultation().Delete(myDeleteContext.Patient_T.Find(entity.PatientID).Consultation_T.ToList());
            }
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            foreach (Patient_T entity in entities)
            {
                myContext.Patient_T.Remove(myContext.Patient_T.Find(entity.PatientID));
            }
            return myContext.SaveChanges() + result;
        }
    }
}
