using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace probleme_gestion_dons
{
    class Program
    {
        static string demanderString(string demande="")
        {
            if(demande!="")
            {
                Console.Write(demande +" : ");
            }

            return Console.ReadLine();
        }
        static int demanderInt(string demande="")
        {
            bool err = false;
            int result=0;
            do
            {
                try
                {
                    result = Convert.ToInt32(demanderString(demande));
                    err = false;
                }
                catch
                {
                    err = true;
                }
            } while (err == true);
            return result;
        }
        static Objet entrerObjet()
        {
            string type = demanderString("Entrez le type de l'objet");
            string description_objet = demanderString("Entrez la description de l'objet");
            int montant = demanderInt("Entrez le montant de l'objet");
            Objet result = new Objet(type, description_objet, montant);
            return result;
        }

        static Don entrerDon(Association assos)
        {
            Console.WriteLine("Saisie des informations du Don : \n");

            string status = "attente";
            DateTime date_reception_don = DateTime.Today;

            string nom_donateur;
            Personne_adherente p = null;
            do
            {
                nom_donateur = demanderString("Entrer le nom de la personne adhérente (donateur)");
                p = assos.findByNom_Adherent(nom_donateur);
            } while (p == null);

            string description_don = demanderString("Entrez la description du don");
            int nb_objets = demanderInt("\nEntrez le nombre d'objets dans le don");


            List<Objet> liste_objets = new List<Objet>();
            for (int i=0;i<nb_objets;i++)
            {
                Console.WriteLine("\n-> Saisie de l'objet n°" + (i+1));
                Objet o = entrerObjet();
                liste_objets.Add(o);
            }

            Don result = new Don(date_reception_don, description_don, status, liste_objets, p);
            return result;
        }

        public static List<Personne_adherente> lecture_personnes_adherente(string fileName)
        {
            List<Personne_adherente> liste = new List<Personne_adherente>();
            string[] fileString = File.ReadAllLines(fileName);

            for (int ligne = 0; ligne < fileString.Length - 1; ligne++)
            {
                string[] substring = fileString[ligne].Split(';');
                int identifiant = int.Parse(substring[0]);
                string nom = substring[1];
                string adresse = substring[2];
                string telephone = substring[3];
                string prenom = substring[4];
                string fonction = substring[5];
                liste.Add(new Personne_adherente(fonction, prenom, adresse, nom, identifiant, telephone));
            }
            return liste;
        }

        public static List<Personne_beneficiaire> lecture_personnes_beneficiaire(string fileName)
        {
            List<Personne_beneficiaire> liste = new List<Personne_beneficiaire>();
            string[] fileString = File.ReadAllLines(fileName);

            for (int ligne = 0; ligne < fileString.Length - 1; ligne++)
            {
                string[] substring = fileString[ligne].Split(';');
                int identifiant = int.Parse(substring[0]);
                string nom = substring[1];
                string adresse = substring[2];
                string telephone = substring[3];
                string prenom = substring[4];
                string date_naissance = substring[5];
                DateTime date = DateTime.Parse(date_naissance);
                liste.Add(new Personne_beneficiaire(date, prenom, adresse, nom, identifiant, telephone));
            }
            return liste;
        }

        static void Main(string[] args)
        {
            
            List<Personne_adherente> liste_adherent = lecture_personnes_adherente("..\\..\\data\\Adherents.txt");
            List<Personne_beneficiaire> liste_beneficiaire = lecture_personnes_beneficiaire("..\\..\\data\\Beneficiaires.txt");

            Association assos = new Association(liste_adherent, liste_beneficiaire);
            Console.WriteLine(assos.AvgAge_Beneficiaires.TotalDays/365 + "Years");
            Console.WriteLine(assos.findByNom_Beneficiaire("Lemarechal").ToString());

            Don cadeau = entrerDon(assos);
            Console.WriteLine("\n\n" + cadeau);

            

            Console.ReadKey();
        }
    }
}
