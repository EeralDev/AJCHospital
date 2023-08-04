using AJCHospitalConsol.DAL.DOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.DAL
{
    public class DALTest
    {
        public void testDAOConsultation()
        {
            //Création des User
            DAOUser myDAOUser = new DAOUser();
            User_T firstUser = new User_T { UserName = "YoussefE", Password = "123456", FirstName = "Youssef", LastName = "Esmaili", EmpCode = "DOCTOR" };
            User_T secondUser = new User_T { UserName = "NaimaE", Password = "123456", FirstName = "Naima", LastName = "Esmaili", EmpCode = "DOCTOR" };
            List<User_T> users = new List<User_T>();
            users.Add(firstUser);
            users.Add(secondUser);
            myDAOUser.Insert(users, out List<int> UserIDs);
            users = new List<User_T>();
            foreach (int userID in UserIDs)
            {
                users.Add(myDAOUser.SelectById(userID));
            }
            //Création des Patient
            DAOPatient myDAOPatient = new DAOPatient();
            Patient_T firstPatient = new Patient_T { SocialSecurityID = "196111411838147", FirstName = "Ilyes", LastName = "Esmaili", Age = 26, Adress = "12, Le Pré Normand, 50690 Sideville" };
            Patient_T secondPatient = new Patient_T { SocialSecurityID = "297056100625235", FirstName = "Clemence", LastName = "Gaultier", Age = 25, Adress = "La Fleurière, 61210 Putanges-le-lac" };
            List<Patient_T> patients = new List<Patient_T>();
            patients.Add(firstPatient);
            patients.Add(secondPatient);
            myDAOPatient.Insert(patients, out List<int> PatientIDs);
            patients = new List<Patient_T>();
            foreach (int patientID in PatientIDs)
            {
                patients.Add(myDAOPatient.SelectById(patientID));
            }

            //Création de l'objet DAO
            DAOConsultation myDAOConsultation = new DAOConsultation();
            //Creation de la liste d'entité
            Consultation_T firstConsultation = new Consultation_T { PatID = patients[0].PatientID,PatSocialSecurityID=patients[0].SocialSecurityID, DocID = users[0].UserID, DocName = users[0].UserName, StartTime = System.DateTime.Now, RoomNumber = 1, Price = 12.3 };
            Consultation_T secondConsultation = new Consultation_T { PatID = patients[1].PatientID, PatSocialSecurityID = patients[1].SocialSecurityID, DocID = users[1].UserID, DocName = users[1].UserName, StartTime = System.DateTime.Now, RoomNumber = 1, Price = 12.3 };
            List<Consultation_T> consultations = new List<Consultation_T>();
            consultations.Add(firstConsultation);
            consultations.Add(secondConsultation);
            // Test de Insert(List<T>)
            Console.WriteLine("------------------------Ici On Teste Insert(List<T>)-----------------");
            Console.WriteLine($"{myDAOConsultation.Insert(consultations, out List<int> IDs)} row are affected by insert");
            foreach (int item in IDs)
            {
                Console.WriteLine(item);
            }
            //Affichage de la list de créer
            foreach (Consultation_T item in myDAOConsultation.SelectAll())
            {
                Console.WriteLine(item.ConsultationID);
                Console.WriteLine(item.PatID);
                Console.WriteLine(item.PatSocialSecurityID);
                Console.WriteLine(item.DocID);
                Console.WriteLine(item.DocName);
                Console.WriteLine(item.StartTime);
                Console.WriteLine(item.RoomNumber);
                Console.WriteLine(item.Price);
            }
            // Modification de la liste de Consultation
            Console.WriteLine("------------------------Ici On Teste Update(List<T>)-----------------");
            foreach (Consultation_T item in consultations)
            {
                item.Price = item.Price + 10;
            }
            // Test de Update(List<T>)
            myDAOConsultation.Update(consultations);
            // Affichage de la liste Mise a jours
            foreach (Consultation_T item in myDAOConsultation.SelectAll())
            {
                Console.WriteLine(item.ConsultationID);
                Console.WriteLine(item.PatID);
                Console.WriteLine(item.PatSocialSecurityID);
                Console.WriteLine(item.DocID);
                Console.WriteLine(item.DocName);
                Console.WriteLine(item.StartTime);
                Console.WriteLine(item.RoomNumber);
                Console.WriteLine(item.Price);
            }
            //Suppression du contenu de la BDD
            Console.WriteLine("------------------------Ici On Teste Delete(List<T>)-----------------");
            Console.WriteLine(myDAOConsultation.Delete(myDAOConsultation.SelectAll()));

            //Debut des tests sur les méthodes monoligne 
            //Ici on créer le mono Utilisateur
            Consultation_T mySoloConsultation = new Consultation_T { PatID = patients[1].PatientID, PatSocialSecurityID = patients[1].SocialSecurityID, DocID = users[0].UserID, DocName = users[0].UserName, StartTime = System.DateTime.Now, RoomNumber=1, Price = 12.3 };
            //Ici on test Insert(T)
            Console.WriteLine("------------------------Ici On Teste Insert(T)-----------------");
            myDAOConsultation.Insert(mySoloConsultation, out int ID);
            Console.WriteLine($"La consultation fraichement créer est : {myDAOConsultation.SelectById(ID).ConsultationID}");
            //Ici on test Update(T)
            Console.WriteLine("------------------------Ici On Teste Update(T)-----------------");
            Consultation_T myConsultation = myDAOConsultation.SelectById(ID);
            myConsultation.Price = myConsultation.Price + 10;
            Console.WriteLine($"{myDAOConsultation.Update(myConsultation)} row was affected by the update.");
            Console.WriteLine($"La Consultation fraichement modifié est : {myDAOConsultation.SelectById(ID).Price}");
            foreach (Consultation_T item in myDAOConsultation.SelectAll())
            {
                Console.WriteLine(item.ConsultationID);
                Console.WriteLine(item.PatID);
                Console.WriteLine(item.PatSocialSecurityID);
                Console.WriteLine(item.DocID);
                Console.WriteLine(item.DocName);
                Console.WriteLine(item.StartTime);
                Console.WriteLine(item.RoomNumber);
                Console.WriteLine(item.Price);
            }
            //Ici on test Delete(T)
            Console.WriteLine("------------------------Ici On Teste Delete(T)-----------------");
            Console.WriteLine($"{myDAOConsultation.Delete(myConsultation)} row was affected by the delete");


            //Suppression de tout les users
            Console.WriteLine(myDAOUser.Delete(myDAOUser.SelectAll()));
            //Suppression de tout les patients
            Console.WriteLine(myDAOPatient.Delete(myDAOPatient.SelectAll()));
        }

        public void testDAOUser()
        {
            //Debut des tests sur les méthode multiLign 
            // Creation de l'objet DAO
            DAOUser myDAOUser = new DAOUser();
            //Création de la nouvelle List d'entité
            User_T firstUser = new User_T { UserName = "YoussefE", Password = "123456", FirstName = "Youssef", LastName = "Esmaili", EmpCode = "DOCTOR" };
            User_T secondUser = new User_T { UserName = "NaimaE", Password = "123456", FirstName = "Naima", LastName = "Esmaili", EmpCode = "DOCTOR" };
            User_T thirdUser = new User_T { UserName = "ImaneE", Password = "123456", FirstName = "Imane", LastName = "Esmaili", EmpCode = "DOCTOR" };
            List<User_T> users = new List<User_T>();
            users.Add(firstUser);
            users.Add(secondUser);
            users.Add(thirdUser);
            // Test de Insert(List<T>)
            Console.WriteLine("------------------------Ici On Teste Insert(List<T>)-----------------");
            myDAOUser.Insert(users, out List<int> IDs);
            foreach (int item in IDs)
            {
                Console.WriteLine(item);
            }
            //Afichage de la liste créer 
            foreach (User_T item in myDAOUser.SelectAll())
            {
                Console.WriteLine(item.UserID);
                Console.WriteLine(item.UserName);
                Console.WriteLine(item.Password);
                Console.WriteLine(item.FirstName);
                Console.WriteLine(item.LastName);
                Console.WriteLine(item.EmpCode);
            }
            // Modification de la liste de User
            Console.WriteLine("------------------------Ici On Teste Update(List<T>)-----------------");
            foreach (User_T item in users)
            {
                item.UserName = item.UserName.ToUpper();
            }
            // Test de Update(List<T>)
            myDAOUser.Update(users);
            // Affichage de la liste Mise a jours
            foreach (User_T item in myDAOUser.SelectAll())
            {
                Console.WriteLine(item.UserID);
                Console.WriteLine(item.UserName);
                Console.WriteLine(item.Password);
                Console.WriteLine(item.FirstName);
                Console.WriteLine(item.LastName);
                Console.WriteLine(item.EmpCode);
            }
            //Suppression du contenu de la BDD
            Console.WriteLine("------------------------Ici On Teste Delete(List<T>)-----------------");
            Console.WriteLine(myDAOUser.Delete(myDAOUser.SelectAll()));


            //Debut des tests sur les méthodes monoligne 
            //Ici on créer le mono Utilisateur
            User_T mySoloUser = new User_T { UserName = "NaimaE", Password = "123456", FirstName = "Naima", LastName = "Esmaili", EmpCode = "DOCTOR" };
            //Ici on test Insert(T)
            Console.WriteLine("------------------------Ici On Teste Insert(T)-----------------");
            myDAOUser.Insert(mySoloUser, out int ID);
            Console.WriteLine($"L'utilisateur fraichement créer est : {myDAOUser.SelectById(ID).UserName}");
            //Ici on test Update(T)
            Console.WriteLine("------------------------Ici On Teste Update(T)-----------------");
            User_T myUser = myDAOUser.SelectById(ID);
            myUser.UserName = "NAIMAEsmaili";
            Console.WriteLine($"{myDAOUser.Update(myUser)} row was affected by the update.");
            Console.WriteLine($"L'utilisateur fraichement modifié est : {myDAOUser.SelectById(ID).UserName}");
            foreach (User_T item in myDAOUser.SelectAll())
            {
                Console.WriteLine(item.UserID);
                Console.WriteLine(item.UserName);
                Console.WriteLine(item.Password);
                Console.WriteLine(item.FirstName);
                Console.WriteLine(item.LastName);
                Console.WriteLine(item.EmpCode);
            }
            //Ici on test Delete(T)
            Console.WriteLine("------------------------Ici On Teste Delete(T)-----------------");
            Console.WriteLine($"{myDAOUser.Delete(myUser)} row was affected by the delete");
        }

        public void testDAOPatient()
        {
            //Debut des tests sur les méthode multiLign 
            // Creation de l'objet DAO
            DAOPatient myDAOPatient = new DAOPatient();
            //Création de la nouvelle List d'entité
            Patient_T firstPatient = new Patient_T { SocialSecurityID = "196111411838147", FirstName = "Ilyes", LastName = "Esmaili", Age = 26, Adress = "12, Le Pré Normand, 50690 Sideville" };
            Patient_T secondPatient = new Patient_T { SocialSecurityID = "297056100625235", FirstName = "Clemence", LastName = "Gaultier", Age = 25, Adress = "La Fleurière, 61210 Putanges-le-lac" };
            List<Patient_T> patients = new List<Patient_T>();
            patients.Add(firstPatient);
            patients.Add(secondPatient);
            // Test de Insert(List<T>)
            Console.WriteLine("------------------------Ici On Teste Insert(List<T>)-----------------");
            myDAOPatient.Insert(patients, out List<int> IDs);
            foreach (int item in IDs)
            {
                Console.WriteLine(item);
            }
            //Afichage de la liste créer 
            foreach (Patient_T item in myDAOPatient.SelectAll())
            {
                Console.WriteLine(item.PatientID);
                Console.WriteLine(item.SocialSecurityID);
                Console.WriteLine(item.FirstName);
                Console.WriteLine(item.LastName);
                Console.WriteLine(item.Age);
                Console.WriteLine(item.Adress);
            }
            // Modification de la liste de User
            Console.WriteLine("------------------------Ici On Teste Update(List<T>)-----------------");
            foreach (Patient_T item in patients)
            {
                item.LastName = item.LastName.ToUpper();
            }
            // Test de Update(List<T>)
            myDAOPatient.Update(patients);
            // Affichage de la liste Mise a jours
            foreach (Patient_T item in myDAOPatient.SelectAll())
            {
                Console.WriteLine(item.PatientID);
                Console.WriteLine(item.SocialSecurityID);
                Console.WriteLine(item.FirstName);
                Console.WriteLine(item.LastName);
                Console.WriteLine(item.Age);
                Console.WriteLine(item.Adress);
            }
            //Suppression du contenu de la BDD
            Console.WriteLine("------------------------Ici On Teste Delete(List<T>)-----------------");
            Console.WriteLine(myDAOPatient.Delete(myDAOPatient.SelectAll()));


            //Debut des tests sur les méthodes monoligne 
            //Ici on créer le mono Utilisateur
            Patient_T mySoloPatient = new Patient_T { SocialSecurityID = "196111411838147", FirstName = "IlyesBis", LastName = "EsmailiBis", Age = 26, Adress = "12Bis, Le Pré Normand, 50690 Sideville" };
            //Ici on test Insert(T)
            Console.WriteLine("------------------------Ici On Teste Insert(T)-----------------");
            myDAOPatient.Insert(mySoloPatient, out int ID);
            Console.WriteLine($"L'utilisateur fraichement créer est : {myDAOPatient.SelectById(ID).FirstName}");
            //Ici on test Update(T)
            Console.WriteLine("------------------------Ici On Teste Update(T)-----------------");
            Patient_T myPatient = myDAOPatient.SelectById(ID);
            myPatient.FirstName = myPatient.FirstName.ToUpper();
            Console.WriteLine($"{myDAOPatient.Update(myPatient)} row was affected by the update.");
            Console.WriteLine($"L'utilisateur fraichement modifié est : {myDAOPatient.SelectById(ID).FirstName}");
            foreach (Patient_T item in myDAOPatient.SelectAll())
            {
                Console.WriteLine(item.PatientID);
                Console.WriteLine(item.SocialSecurityID);
                Console.WriteLine(item.FirstName);
                Console.WriteLine(item.LastName);
                Console.WriteLine(item.Age);
                Console.WriteLine(item.Adress);
            }
            //Ici on test Delete(T)
            Console.WriteLine("------------------------Ici On Teste Delete(T)-----------------");
            Console.WriteLine($"{myDAOPatient.Delete(myPatient)} row was affected by the delete");
        }
    }
}
