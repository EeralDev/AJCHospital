using AJCHospitalConsol.Controller;
using AJCHospitalConsol.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace AJCHospitalConsol.Logic
{
    internal class Rooms : IObserver
    {
        //Attribut propre au métiers 

        private Room[] _myRoomArray;

        public Room[] MyRoomArray 
        { 
            get { return _myRoomArray ;} 
            set { _myRoomArray = value ;}
        }

        //Méthode métiers

        public Rooms()
        {
            this._myRoomArray= null;
        }
        //Lorsqu'un Docteur se connecte
        public void AddRoom(User_T doctor, Hospital hospital)
        {
            int indexFirstFree = Array.FindIndex(MyRoomArray, element => element == null);
            this._myRoomArray[indexFirstFree] = new Room(indexFirstFree, doctor, hospital);
        }

        //Lorsque'un Docteur se déconnecte 
        //public void CloseRoom(User_T mondoctor)
        //{
        //    this._myRoomArray.Firstouroom.monDoctor() == null;
        //}

        // Méthode propre a IObserver
        //void Update(Hospital hospital)
        //{
        //    int indexFirst = Array.FindIndex(item => item.patient == null);
        //    this._myRoomArray.first().startConsultation();
        //}
        public void Update(ISubject subject)
        {
            throw new NotImplementedException();
        }
    }
}
