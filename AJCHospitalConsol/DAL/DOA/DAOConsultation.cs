using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.DAL.DOA
{
    public class DAOConsultation : IDAO<Consultation_T>
    {
        public List<Consultation_T> SelectAll()
        {
            return new AJCHospitalEntities().Consultation_T.ToList();
        }

        public Consultation_T SelectById(int id)
        {
            return new AJCHospitalEntities().Consultation_T.Find(id);
        }

        public int Insert(Consultation_T entity, out int ID)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            myContext.Consultation_T.Add(entity);
            int result = myContext.SaveChanges();
            ID = entity.ConsultationID;
            return result;
        }

        public int Insert(List<Consultation_T> entities, out List<int> IDs)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            foreach (Consultation_T entity in entities)
            {
                myContext.Consultation_T.Add(entity);
            }
            int result = myContext.SaveChanges();
            IDs = new List<int>();
            foreach (Consultation_T entity in entities)
            {
                IDs.Add(entity.ConsultationID);
            }
            return result;
        }

        public int Update(Consultation_T entity)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            myContext.Entry(myContext.Consultation_T.Find(entity.ConsultationID)).CurrentValues.SetValues(entity);
            return myContext.SaveChanges();
        }


        public int Update(List<Consultation_T> entities)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            foreach (Consultation_T entity in entities)
            {
                myContext.Entry(myContext.Consultation_T.Find(entity.ConsultationID)).CurrentValues.SetValues(entity); ;
            }
            return myContext.SaveChanges();
        }

        public int Delete(Consultation_T entity)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            myContext.Consultation_T.Remove(myContext.Consultation_T.Find(entity.ConsultationID));
            return myContext.SaveChanges();
        }

        public int Delete(List<Consultation_T> entities)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            foreach (Consultation_T entity in entities)
            {
                myContext.Consultation_T.Remove(myContext.Consultation_T.Find(entity.ConsultationID));
            }
            return myContext.SaveChanges();
        }
    }
}
