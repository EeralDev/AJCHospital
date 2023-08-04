using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    internal class User
    {
        private int _idUser;
        private string _userName;
        private string _password;
        private string _codeRole;
        private string _firstNameUser;
        private string _lastNameUser;

        public int IdUser
        {
            get => _idUser;
        }
        public string UserName
        {
            get => _userName;
            set => _userName= (!string.IsNullOrWhiteSpace(value)) ? value : throw new ArgumentException("User name must not be blank");
        }
        public string Password
        {
            get => _password;
            set => _password = (!string.IsNullOrWhiteSpace(value)) ? value : throw new ArgumentException("Password User must not be blank");
        }
        public string CodeRole
        {
            get => _codeRole;
        }
        public string FirstName
        {
            get => _firstNameUser;
            set => _firstNameUser = (!string.IsNullOrWhiteSpace(value)) ? value : throw new ArgumentException("First name User must not be blank");
        }
        public string LastNameUser
        {
            get => _lastNameUser;
            set => _lastNameUser = (!string.IsNullOrWhiteSpace(value)) ? value : throw new ArgumentException("Last name User must not be blank");
        }
    }
}
