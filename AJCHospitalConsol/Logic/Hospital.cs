using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    // L'Hospital observe les patients : observateur , hérite de l'interface IObserver
    // L'Hospital est unique : un singleton en mode thread safe et utilise Lazy

    internal class Hospital : IObserver
    {
        private static readonly Lazy<Hospital> lazyInstanceHospital = new Lazy<Hospital>(() => new Hospital());
        // attributs
        // Propriété statique pour accéder à l'instance unique de l'hôpital
        private static Hospital InstanceHospital => lazyInstanceHospital.Value;
        private string _nameHospital;
        private UnikQueuePatient queuePatients;
        //private List<Doctors> _doctorList;

        public Secretary Secretary { get; private set; }
        
        // constructeurs
        public Hospital() 
        {
            // utilisation des singletons UnikQueuePatient et Secretary
            // dans le constructeur de la classe Hospital
            this.queuePatients = UnikQueuePatient.InstanceQueuePatient;
            Secretary = Secretary.InstanceSecretary;
        }
        public Hospital(string nameHospital)
        {
            this._nameHospital = nameHospital;
            //User = new User();
        }
    }
}
