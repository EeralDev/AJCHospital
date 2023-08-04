using AJCHospitalConsol.Controller;
using AJCHospitalConsol.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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

            int nb1;
            string login_user = "df";
            string mp_user = "15465";
            bool sortie = false;
            do
            {
                Console.WriteLine("1-Connexion");
                Console.WriteLine("2-Quitter");
                nb1 = Convert.ToInt32(Console.ReadLine());
             

                if (nb1 == 2)
                {

                    sortie = true;
                }








                if (nb1 == 1)
                {
                   
                    //  ecranConnexion();
                    TestConnexion(null);
                }


            } while ((nb1!=2||sortie == false));

        }

        public  void TestConnexion(User a1)
        {
            string rep = " ";

            string login_user;

            string password_user;

            a1 = null;



            //test authenitfiaction

            User_T b  = new User_T();

            do
            {


              
                
                    Console.WriteLine("Entrez votre login");
                    login_user = Console.ReadLine();
                    Console.WriteLine("Entrez votre mot de passe");
                    password_user = Console.ReadLine();
                    b = new myController().Authentication(login_user, password_user);
                    Console.WriteLine(b == null);
                    Console.ReadLine();
                


               
                if (b == null)
                {


                    Console.WriteLine("Le login ou le mot de passe est incorrect ");


                }








                if (b.EmpCode.Equals("0") )   //test objet user ! null code_user=secretaire
                {
                    ecranSecretaire(b);
                }
                //SI TEST OK 

                if (b.EmpCode.Equals("1"))
                {
                    ecranMedecin(b);
                }
                //else


                //{
                //    if(u) code_user=s ou m


                //}

            } while (a1 == null);

        }



        public  void ecranMedecin(User_T a)
        {
            int c = 1;
            int rep = 0;
            Console.WriteLine("Bienvennue " + " "+a.LastName + " " + a.FirstName);








            do
            {

                try
                {
                    Console.WriteLine("1-Rendre la salle dispo ");
                    Console.WriteLine("2-Afficher la file d'atttente");
                    Console.WriteLine("3-Sauvegarder les visites");
                    Console.WriteLine("4-Afficher file d'attente");
                    Console.WriteLine("5-Afficher  la liste des visites");
                    Console.WriteLine("6-Quitter");

                    rep = Convert.ToInt32(Console.ReadLine());

                }
                catch (FormatException e)
                {
                    Console.WriteLine("Erreur de saisie");

                }




                switch (rep)
                {
                    case 1:
                        Console.WriteLine("Un patient à été  à la file ");
                        break;
                    case 2:
                        Console.WriteLine("Affichage ");
                        break;
                    case 3:
                        Console.WriteLine("Affichage prochain patient file ");
                        break;
                    case 4:

                        break;
                    case 5:

                        break;

                    case 6:
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
            int c = 1;
            int rep = 0;
            Console.WriteLine("Bienvennue " + " "+b.LastName+" "+b.FirstName);
          
            string confirmation = "o";

            do
            {

                try
                {


                    Console.WriteLine("1-Ajouter un patient à la file d'attente");
                    Console.WriteLine("2-Afficher la file d'atttente");
                    Console.WriteLine("3-Afficher le prochain patient de la file d'atttente");
                    Console.WriteLine("4-Quitter");
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
                            Console.WriteLine("Un patient à été  à la file ");
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
                                        Console.WriteLine("Securite sociale ? ");
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
                        break;
                    case 3:
                        Console.WriteLine("Affichage prochain patient file ");
                        break;
                    case 4:
                        Console.WriteLine("Quitter ");
                        EcranDemmarage();
                        break;

                    default:

                        Console.WriteLine("Merci de saisir une valeur correcte");

                        break;










                }


                Console.WriteLine("****************************************");


            } while (rep != 4);









        }













      
      


    }

}



























