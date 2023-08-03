using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.DAL.DOA
{
    public class DAOUser : IDAO<User_T>
    {
        public List<User_T> SelectAll()
        {
            return new AJCHospitalEntities().User_T.ToList();
        }

        public User_T SelectById(int id)
        {
            return new AJCHospitalEntities().User_T.Find(id);
        }

        public int Insert(User_T entity, out int ID)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            myContext.User_T.Add(entity);
            int result = myContext.SaveChanges();
            ID = entity.UserID;
            return result;
        }

        public int Insert(List<User_T> entities, out List<int> IDs)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            foreach (User_T entity in entities)
            {
                myContext.User_T.Add(entity);

            }
            int result = myContext.SaveChanges();
            IDs = new List<int>();
            foreach (User_T entity in entities)
            {
                IDs.Add(entity.UserID);
            }
            return result;
        }

        public int Update(User_T entity)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            myContext.Entry(myContext.User_T.Find(entity.UserID)).CurrentValues.SetValues(entity);
            return myContext.SaveChanges();
        }

        public int Update(List<User_T> entities)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            foreach (User_T entity in entities)
            {
                myContext.Entry(myContext.User_T.Find(entity.UserID)).CurrentValues.SetValues(entity); ;
            }
            return myContext.SaveChanges();
        }

        // Mettre a jour le delete pour supprimer les lignes de consultation avant de supprimer les Users 
        public int Delete(User_T entity)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            myContext.User_T.Remove(myContext.User_T.Find(entity.UserID));
            return myContext.SaveChanges();
        }

        public int Delete(List<User_T> entities)
        {
            AJCHospitalEntities myContext = new AJCHospitalEntities();
            foreach (User_T entity in entities)
            {
                myContext.User_T.Remove(myContext.User_T.Find(entity.UserID));
            }
            return myContext.SaveChanges();
        }
    }
}
