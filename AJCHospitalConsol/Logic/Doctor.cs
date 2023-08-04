using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    internal class Doctor: IObserver
    {
        public string Id { get; }
        public string NameDoctor { get; set; }
        public int NumeroRoom { get; set; }

        public void Update(ISubject subject)
        {
            throw new NotImplementedException();
        }
    }

    public enum Room
    {
        Room1=1,
        Room2=2
    }
}

