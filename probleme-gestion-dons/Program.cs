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

        public static List<Personne_adherente> lecture_personnes_adherente (string fileName)
        {
            List<Personne_adherente> liste = new List<Personne_adherente>();
            string[] fileString = File.ReadAllLines(fileName);

            for (int ligne = 0; ligne < fileString.Length-1; ligne++)
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
        static void Main(string[] args)
        {
            List<Personne_adherente> liste = lecture_personnes_adherente("..\\..\\data\\Adherents.txt");

            Console.ReadKey();
        }
    }
}
