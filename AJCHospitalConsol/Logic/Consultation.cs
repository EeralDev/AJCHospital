using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    internal class Consultation
    {
        //Chaque patient dans une salle constitue une Consultation
        private int _consultationID ;
        private int _patID;
        private string _patSocialSecurityID;
        private string _docName;
        private int _docID;
        private DateTime _startTime;
        private int _roomNumber;
        private const Double _price = 23;

        public int ConsultationID
        {
            get { return _consultationID; }
            set { _consultationID = value ; }
        }
        public int PatID
        {
            get { return _patID; }
        }
        public string DocName
        {
            get { return _docName; }
        }
        public int DocID
        {
            get { return _docID; }
        }
        public DateTime StartTime
        {
            get { return _startTime; }
        }
        public double Price
        {
            get { return _price; }
        }
        public int RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; }
        }
        public Consultation()
        {
            Console.WriteLine("Numero de la consultation " + ConsultationID.ToString());
        }
        public Consultation(int patID, int docID, DateTime startTime) : this()
        {
            this._patID = patID;
            this._docID = docID;
            this._startTime = startTime;
        }
        public Consultation(int patID, int docID, DateTime startTime, int consultationID, int roomNumber) : this(patID, docID, startTime)
        {
            this._consultationID = consultationID;
            this._roomNumber = roomNumber;
        }
        public override string ToString()
        {
            string response = $"Eléments de la Consultatione :\n";
            response += $" dateTime Consultatione : {StartTime} \t numéro de la Consultatione = {ConsultationID}\n";
            response += $" id patient: {PatID} , id du médecin : {DocID} \t  nom du médecin : {DocName}\n";
            response += $" numéro de salle : {RoomNumber} , prix consultation : {Price}\n";

            return response;
        }
    }
}

