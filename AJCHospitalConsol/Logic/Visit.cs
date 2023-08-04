using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.Logic
{
    internal class Visit
    {
        //Chaque patient dans une salle constitue une visite
        private int _countVisit = 0;
        private int? _patientId;
        private string  _doctorId;
        private DateTime _startTime;
        private int _numRoom;
        private const Double _price = 23;

        public double CountVisit
        {
            get { return _countVisit; }
        }
        public int? PatientId
        {
            get { return _patientId; }
        }
        public string DoctorId
        {
            get { return _doctorId; }
        }
        public DateTime StartTime
        {
            get { return _startTime; }
        }
        public double Price
        {
            get { return _price; }
        }
        public int NumRoom
        {
            get { return _numRoom; }
        }
        public Visit() 
        {
            //Each Time the Constructor is called, increment the Counter value by 1, auto increment AI
            _countVisit++;
            Console.WriteLine("Numero de la visite " + _countVisit.ToString());
        }
        public Visit(int? patientId, string doctorId, DateTime startTime) : this()
        {
            this._patientId = patientId;
            this._doctorId = doctorId;
            this._startTime =startTime;
        }
        public Visit(int? patientId, string doctorId, DateTime startTime, int countVisit, int numRoom) : this(patientId, doctorId, startTime)
        {
            this._countVisit = countVisit;
            this._numRoom = numRoom;
        }
        public override string ToString()
        {
            string response = $"Eléments de la visite :\n";
            response += $" dateTime visite : {StartTime} \t numéro de la visite = {CountVisit}\n";
            response += $" id patient: {PatientId} , id du médecin : {DoctorId} \n";
            response += $" numéro de salle : {NumRoom} , prix consultation : {Price}\n";

            return response;
        }
    }
}
