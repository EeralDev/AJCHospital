using AJCHospitalConsol.Controller;
using AJCHospitalConsol.DAL;
using AJCHospitalConsol.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.View
{
    internal class ViewTest
    {

        private myController _controller;

        public myController Controller
        { get { return _controller; } }

        public ViewTest() 
        { 
            this._controller = new myController();
        }

        public void EcranDemmarage()
        {

            Console.WriteLine("***************BIMHOSPITAL******************");
            int nb1 = 0;           
            bool sortie = false;
            do
            {
                try
                {
                    Console.WriteLine("1-Connexion");
                    Console.WriteLine("2-Quitter");
                    nb1 = Convert.ToInt32(Console.ReadLine());
                }
                catch(FormatException)
                {
                    Console.WriteLine("Erreur de saisie");
                }
                if (nb1 == 1)
                {
                    Launch();
                }
                if (nb1 == 2)
                {
                    sortie = true;
                }
            } while (sortie == false);
        }

        public void Launch()
        {
            string login_user;
            string password_user;
            Queue<Patient_T> file = new Queue<Patient_T>();
            User_T b  = new User_T();
            do
            {  
                Console.WriteLine("Entrez votre login");
                login_user = Console.ReadLine();
                Console.WriteLine("Entrez votre mot de passe");
                password_user = Console.ReadLine();
                b = this.Controller.Authentication(login_user, password_user);
                if (b == null)
                {
                    Console.WriteLine("Le login ou le mot de passe est incorrect ");
                    Console.WriteLine("Voulez-vous réessayer ? O/N");
                    if (Console.ReadLine() != "O")
                    { break; };
                }
                else
                {
                    //test objet user ! null code_user=secretaire
                    if (b.EmpCode.Equals("0"))   
                    {
                        ecranSecretaire(b);
                        this.Controller.Disconnect(b);
                    }
                    //SI TEST OK 
                    if (b.EmpCode.Equals("1"))
                    {
                        ecranMedecin(b);
                        this.Controller.Disconnect(b);
                    }
                }
            } while (b==null);
        }

        public void ecranMedecin(User_T a)
        {         
            int rep = 0;
            Console.WriteLine("Bienvenue" + " "+a.LastName + " " + a.FirstName);
            Queue<Patient_T> currentPatientQueue = new Queue<Patient_T>();
            Patient_T returnedPatient = new Patient_T();
            do
            {
                try
                {
                    Console.WriteLine("1-Rendre la salle disponible");
                    Console.WriteLine("2-Afficher la file d'attente");
                    Console.WriteLine("3-Sauvegarder les visites");              
                    Console.WriteLine("4-Afficher la liste des visites");
                    Console.WriteLine("5-Quitter");
                    Console.WriteLine("Votre premier patient : ");
                    DisplayPatient(this.Controller.MyHospital.Rooms.MyRoomArray[Array.FindIndex(this.Controller.MyHospital.Rooms.MyRoomArray,
                        item => item.RoomDoctor.UserID == a.UserID)].RoomPatient);
                    rep = Convert.ToInt32(Console.ReadLine());

                }
                catch (FormatException )
                {
                    Console.WriteLine("Erreur de saisie");
                }
                switch (rep)
                {
                    case 1:
                        Console.WriteLine("1-Rendre la salle disponible");
                        returnedPatient = this.Controller.CleanRoom(a);                        
                        if (returnedPatient != null)
                        {
                            Console.WriteLine("Nouveau patient dans la salle : ");
                            DisplayPatient(returnedPatient);
                        }
                        else 
                        {
                            Console.WriteLine("Il n'y a aucun patient dans la salle d'attente.");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Afficher la file d'attente");
                        currentPatientQueue = this.Controller.GetPatient();
                        if (currentPatientQueue.Count > 0)
                        {
                            foreach (Patient_T item in currentPatientQueue)
                            {
                                Console.WriteLine($"Patient numéro {currentPatientQueue.ToList().FindIndex(patient => patient == item)} :");
                                DisplayPatient(item);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Il n'y a aucun patient dans la file d'attente");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Sauvegarder les visites");
                        Console.WriteLine($"{this.Controller.SaveConsultationFromView(a)} visites ont été ajoutés à la base de données.");
                        break;                  
                    case 4:
                        Console.WriteLine("Afficher la liste des visites");
                        foreach (Consultation_T item in a.Consultation_T)
                        {
                            Console.WriteLine($"Consultation numéro {a.Consultation_T.ToList().FindIndex(consultation => consultation == item)} :");
                            DisplayConsultation(item);
                        }
                        break;
                    case 5:
                        // Ici il ne faut pas rappeler Ecran de Démarrage il faut quitter la méthode pour retourner 
                        // à la méthode test Connexion et ensuite ecran de démarrage initial.
                        Console.WriteLine("Quitter ");
                        break;
                    default:
                        break;
                }
                Console.WriteLine("****************************************");
            } while (rep != 5);
        }

        public void ecranSecretaire(User_T b)
        {          
            Queue<Patient_T> currentPatientQueue = new Queue<Patient_T>();
            int rep = 0;        
            Console.WriteLine("Bienvenue " + " "+b.LastName+" "+b.FirstName);
            Patient_T p1 = new Patient_T();
            string confirmation = "o";
            do
            {
                try
                {
                    Console.WriteLine("1-Ajouter un patient à la file d'attente");
                    Console.WriteLine("2-Afficher la file d'attente");
                    Console.WriteLine("3-Afficher le prochain patient de la file d'attente");
                    Console.WriteLine("4-Afficher la liste des visites d'un patient ");
                    Console.WriteLine("5-Quitter");
                    rep = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Erreur de saisie");
                }
                switch (rep)
                {
                    case 1:
                        Console.WriteLine("Numéro de securité sociale ? ");
                        string Securite_social = Console.ReadLine();
                        Patient_T p = new myController().FindPatient(Securite_social);
                        //chercher dans la base si bon id
                        if (p!=null)
                        {
                            //AJOUT PATIENT
                            if (this.Controller.GetPatient().FirstOrDefault(item => item.PatientID == p.PatientID) != default(Patient_T))
                            {
                                Console.WriteLine("Le patient est déjà en file d'attente.");
                            }
                            else 
                            {
                                Console.WriteLine("Un patient à été ajouté à la file ");
                                currentPatientQueue = this.Controller.AddPatient(p);
                            }
                            Console.WriteLine("Etat de la file d'attente : ");
                            foreach (Patient_T item in currentPatientQueue)
                            {
                                Console.WriteLine($"Patient numéro {currentPatientQueue.ToList().FindIndex(patient => patient == item)} :");
                                DisplayPatient(item);   
                            }
                        }
                        else
                        {
                            Console.WriteLine("Le patient n'a pas été trouvé dans la base,voulez-vous le créer ? O/N");
                            string reponse = Console.ReadLine();
                            if (reponse.ToUpper() == "O")
                            {
                                do
                                {
                                    try
                                    {
                                        //Console.WriteLine("N° de securité sociale ? ");
                                        //string secu = Console.ReadLine();                                    
                                        Console.WriteLine("Nom ?");
                                        string lastname = Console.ReadLine();
                                        Console.WriteLine("Prénom ?");
                                        string firstname = Console.ReadLine();
                                        Console.WriteLine("Âge? ");
                                        int age = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Adresse?");
                                        string Adresse = Console.ReadLine();
                                        Console.WriteLine("Téléphone?");
                                        string Telephone = Console.ReadLine();
                                        Console.WriteLine("Souhaitez-vous confirmez ? O/N");
                                        confirmation = Console.ReadLine().ToUpper();
                                        if (confirmation.ToUpper() == "O")
                                        {
                                            Patient_T Pnouveau = new Patient_T();
                                            Pnouveau = new myController().CreateNewPatient(Securite_social, firstname, lastname, age, Adresse, Telephone);
                                            Console.WriteLine("Un patient à été ajouté à la file ");
                                            currentPatientQueue = this.Controller.AddPatient(Pnouveau);
                                            Console.WriteLine("Etat de la file d'attente : ");
                                            foreach (Patient_T item in currentPatientQueue)
                                            {
                                                Console.WriteLine($"Patient numéro {currentPatientQueue.ToList().FindIndex(patient => patient == item)} :");
                                                DisplayPatient(item);
                                            }
                                        }
                                        else
                                        { break; }
                                    }
                                    catch (FormatException e)
                                    {
                                        Console.WriteLine("Erreur de saisie sur l'âge");

                                    }
                                } while (confirmation.ToUpper() != "O");
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("File d'attente");
                        currentPatientQueue = this.Controller.GetPatient();
                        if (currentPatientQueue.Count > 0)
                        {
                            foreach (Patient_T item in currentPatientQueue)
                            {
                                Console.WriteLine($"Patient numéro {currentPatientQueue.ToList().FindIndex(patient => patient == item)} :");
                                DisplayPatient(item);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Il n'y a aucun patient dans la file d'attente");
                        }
                        break;
                    case 3:                        
                        if (this.Controller.NextPatient() != null)
                        {
                            Console.WriteLine("Le prochain patient dans la file est :");
                            DisplayPatient(this.Controller.NextPatient());
                        }
                        else
                        {
                            Console.WriteLine("La file d'attente est vide.");
                        }
                        //Patient NextPatient
                        break;
                    case 4:
                        Console.WriteLine("Afficher la liste des visites");
                        Console.WriteLine("Veuillez entrer le numéro de sécurité social du patient que vous voulez afficher ?");
                        Patient_T myPatient = Controller.FindPatient(Console.ReadLine());
                        if (myPatient != null)
                        {
                            if (myPatient.Consultation_T.Count > 0)
                            {
                                foreach (Consultation_T item in myPatient.Consultation_T)
                                {
                                    Console.WriteLine($"Consultation numéro {myPatient.Consultation_T.ToList().FindIndex(consultation => consultation == item)} :");
                                    DisplayConsultation(item);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ce patient n'a eu aucune visite.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Le patient recherché n'existe pas.");
                        }
                        break;
                    case 5:
                        // Ici il ne faut pas rappeler Ecran de Démarrage il faut quitter la méthode pour retourner 
                        // à la méthode test Connexion et ensuite ecran de démarrage initial.
                        Console.WriteLine("Quitter ");
                        break;
                    default:
                        Console.WriteLine("Merci de saisir une valeur correcte");
                        break;
                }
                Console.WriteLine("****************************************");
            } while (rep != 5);
        }
    
        public void DisplayPatient(Patient_T patient)
        {
            Console.WriteLine($"Numéro de sécurité sociale : {patient.SocialSecurityID}\n" +
                $"Prénom :{patient.FirstName}\n" +
                $"Nom de famille :{patient.LastName}\n" +
                $"Âge :{patient.Age}\n" +
                $"Adresse :{patient.Adress}\n" +
                $"Téléphone :{patient.Tel}");
        }

        public void DisplayConsultation(Consultation_T consultation)
        {
            Console.WriteLine($"Numéro de sécurité sociale du patient :{consultation.PatSocialSecurityID}\n" +
            $"Nom d'utilisateur du médecin : {consultation.DocName}\n" +
            $"Heure du début de la consultation : {consultation.StartTime}\n" +
            $"Numéro de salle de consultation: {consultation.RoomNumber}\n" +
            $"Prix de la consultation : {consultation.Price}\n");
        }
    }
}



























