using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.DAL.DOA
{
    public interface IDAO<T>
    {
        List<T> SelectAll();

        T SelectById(int id);

        int Insert(T entity, out int ID);

        int Insert(List<T> entities, out List<int> IDs);

        int Update(T entity);

        int Update(List<T> entities);

        int Delete(T entity);

        int Delete(List<T> entities);
    }
}
