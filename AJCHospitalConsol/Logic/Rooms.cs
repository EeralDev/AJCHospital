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
    public class Rooms
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
            this._myRoomArray = new Room[] { null, null };
        }
        //Lorsqu'un Docteur se connecte
        public void AddRoom(User_T doctor, Hospital hospital)
        {
            int indexFirstFree = Array.FindIndex(MyRoomArray, element => element == null);
            this._myRoomArray[indexFirstFree] = new Room(indexFirstFree + 1, doctor, hospital);
        }

        //Lorsque'un Docteur se déconnecte 
        public void CloseRoom(User_T myDoctor)
        {
            // on cherche l'index du tableau MyRoomArray ou l'UserId du docteur
            // corrrespond a celui de la salle, on le met à null
            int myIndex = Array.FindIndex(this.MyRoomArray, item => item.RoomDoctor.UserID == myDoctor.UserID);
            this._myRoomArray[myIndex].RecordConsultation();
            this._myRoomArray[myIndex] = null;
        }

        // Méthode propre a IObserver
        public void Update(Hospital hospital)
        {
            // on cherchd lindex de MyRoomArray ou la salle du patient vaut null
            // et lui notifie : de notifier l'hopital pour commencer une consultation
            int indexFirst = Array.FindIndex(MyRoomArray, item => item.RoomPatient == null);
            this.MyRoomArray[indexFirst].StartConsultation();
          }

    }
}
