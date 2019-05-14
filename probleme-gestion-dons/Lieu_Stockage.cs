using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Lieu_Stockage
    {
        static int compteur = 0;
        int id;
        string type;
        string adresse;
        double volume;
        List<Objet> objets_stockes;

        public Lieu_Stockage(string type, string adresse, double volume)
        {
            compteur++;
            this.id = compteur;
            this.type = type;
            this.adresse = adresse;
            this.volume = volume;
            this.objets_stockes = new List<Objet>();
        }
        
        public void Ajouter_Objet(Objet o)
        {
            if(typeof(Objet)==o.GetType())
            {
                this.objets_stockes.Add(o);
            }
            
        }
    }
}
