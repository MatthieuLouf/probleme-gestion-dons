using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Objet result = new Objet(1, type, description_objet, montant);
            return result;
        }

        static void Main(string[] args)
        {
            Objet o = entrerObjet();
            Console.WriteLine(o.ToString());

            Console.ReadKey();
        }
    }
}
