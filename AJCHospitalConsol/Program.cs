using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new DAL.DALTest().testDAOConsultation();
            new DAL.DALTest().testDAOPatient();
            new DAL.DALTest().testDAOUser();
        }
    }
}
