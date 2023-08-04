using AJCHospitalConsol.Controller;
using AJCHospitalConsol.DAL;
using AJCHospitalConsol.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AJCHospitalConsol.View
{
    internal class ViewTest
    {
        //string str = "hello";

        public void EcranDemmarage()
        {



            Console.WriteLine("*************** HOPITAL******************");

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
                catch(FormatException )
                {
                    Console.WriteLine("Erreur de saisie");
                }
            


                if (nb1 == 2)
                {

                    sortie = true;
                }








                if (nb1 == 1)
                {
                   
                    //  ecranConnexion();
                    TestConnexion();
                }


            } while (sortie == false);

        }

        public  void TestConnexion()
        {
          

            string login_user;

            string password_user;


            Queue<Patient_T> file = new Queue<Patient_T>();

            //test authenitfiaction

            User_T b  = new User_T();

            do
            {


              
                
                    Console.WriteLine("Entrez votre login");
                    login_user = Console.ReadLine();
                    Console.WriteLine("Entrez votre mot de passe");
                    password_user = Console.ReadLine();
                    b = new myController().Authentication(login_user, password_user);
                 
                 



                if (b == null)
                {


                    Console.WriteLine("Le login ou le mot de passe est incorrect ");


                }



                else
                {




                    if (b.EmpCode.Equals("0"))   //test objet user ! null code_user=secretaire
                    {
                        ecranSecretaire(b);
                    }

                    //SI TEST OK 

                    if (b.EmpCode.Equals("1"))
                    {
                        ecranMedecin(b);
                    }


                }
            

            } while (b==null);

        }



        public  void ecranMedecin(User_T a)
        {
         
            int rep = 0;
            Console.WriteLine("Bienvennue " + " "+a.LastName + " " + a.FirstName);




            Queue<Patient_T> file = new Queue<Patient_T>();
            Patient_T p1 = new Patient_T();

            do
            {

                try
                {
                    Console.WriteLine("1-Rendre la salle dispo ");
                    Console.WriteLine("2-Afficher la file d'attente");
                    Console.WriteLine("3-Sauvegarder les visites");
              
                    Console.WriteLine("4-Afficher  la liste des visites");
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
                        Console.WriteLine("1-Rendre la salle dispo");
                        p1 = new myController().CleanRoom();
                     //afficher patient



                        break;
                    case 2:
                        Console.WriteLine("Afficher la file d'atttente");

                        file = new myController().GetPatient();

                        break;
                    case 3:
                        Console.WriteLine("Sauvegarder les visites ");
                        int S = new myController().SaveConsultationFromView();
                       // int SaveConsultation//
                        break;
                  
                    case 4:

                        break;

                    case 5:
                        Console.WriteLine("Quitter ");
                        EcranDemmarage();
                        break;




                    default:


                        break;










                }


                Console.WriteLine("****************************************");


            } while (rep != 4);

        }




        public  void ecranSecretaire(User_T b)
        {
          
            Queue<Patient_T> file = new Queue<Patient_T>();
            int rep = 0;
            int Id_secu = 0;
            Console.WriteLine("Bienvennue " + " "+b.LastName+" "+b.FirstName);
            Patient_T p1 = new Patient_T();
            string confirmation = "o";

            do
            {

                try
                {


                    Console.WriteLine("1-Ajouter un patient à la file d'attente");
                    Console.WriteLine("2-Afficher la file d'atttente");
                    Console.WriteLine("3-Afficher le prochain patient de la file d'atttente");
                    Console.WriteLine("4-Afficher  la liste des visites d'un patient ");
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

                        Console.WriteLine(" Numéro Securite sociale ? ");
                    string    Securite_social = Console.ReadLine();



                        Patient_T p = new myController().FindPatient(Securite_social);



                        //chercher dans la base si bon id



                        if (p!=null)

                        {
                            //AJOUT PATIENT
                            Console.WriteLine("Un patient à été ajouter à la file ");
                            file = new myController().AddPatient(p);

                        }
                        else
                        {

                            Console.WriteLine("Le patient n'a pas été trouvé dans la base,voulez vous le creé   O/N ? ");
                            string reponse = Console.ReadLine();





                            if (reponse.ToUpper() == "O")
                            {

                                do
                                {
                                    try
                                    {
                                        Console.WriteLine(" Id Securite sociale ? ");
                                        string secu = Console.ReadLine();
                                    
                                        Console.WriteLine("Nom ?");

                                        string lastname = Console.ReadLine();



                                        Console.WriteLine("Prenom ?");

                                        string firstname = Console.ReadLine();

                                        Console.WriteLine("Age? ");
                                        int age = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine("Adresse?");

                                        string Adresse = Console.ReadLine();














                                        Console.WriteLine("Souhaitez-vous confirmez O/N ? ");
                                        confirmation = Console.ReadLine().ToUpper();


                                        if (confirmation.ToUpper() == "O")
                                        {
                                            Patient_T Pnouveau = new Patient_T();
                                            Pnouveau = new myController().CreateNewPatient(secu, firstname, lastname, age, Adresse);
                                        }



                                    }
                                    catch (FormatException e)
                                    {
                                        Console.WriteLine("Erreur de saisie sur l'age  ");

                                    }



                                } while (confirmation.ToUpper() != "O");



                            }


                        }














                        break;
                    case 2:
                        Console.WriteLine("Affichage ");

                        file = new myController().GetPatient();

                        break;
                    case 3:
                        Console.WriteLine("Affichage prochain patient file ");

                        p1 = new myController().NextPatient();
                        //Patient NextPatient
                        break;

                    case 4:
                        //try
                        //{
                        //    Console.WriteLine("Numéro Securite Sociale du Patient ?");
                        // Id_secu = Convert.ToInt32(Console.ReadLine());



                        //    Patient_T p1 = new myController().FindPatient(Id_secu);







                        //}
                        //catch(FormatException e)
                        //{
                        //    Console.WriteLine("Erreur saisie");
                        //}
                        

                        //new myController().getPatientConsultation(Id_secu);


                        break;
                        







                    case 5:
                        Console.WriteLine("Quitter ");
                        EcranDemmarage();
                        break;

                    default:

                        Console.WriteLine("Merci de saisir une valeur correcte");

                        break;










                }


                Console.WriteLine("****************************************");


            } while (rep != 5);









        }













      
      


    }

}



























