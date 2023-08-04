using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    internal class Secretary: IObserver
    {
        // Les propriétés de la secrétaire
        public string FirstNameSecretary { get; set; }
        public string LastNameSecretary { get; set; }

        public Secretary() {}
        
        public void Update(ISubject subject)
        {
            throw new NotImplementedException();
        }
        //Les opérations de la secrétaire :
        //1. Rajouter a la file d’attente un patient :
        //a.si existant, dans ce cas il faut le récupérer de la base de données
        //b.s’il est nouveau il faudra l’inscrire en base
        //2. Afficher la file d’attente
        //3. Afficher le prochain patient de la file(sans le retirer)
        //4. Sortir du menu et revenir au menu principal
        //La secrétaire permet d afficher, pour un patient la liste de ses visites en base, selon 
        //son identifiant
    }
}
