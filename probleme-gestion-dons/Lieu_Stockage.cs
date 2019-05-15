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
        double solde;
        List<Objet> objets_stockes;

        public string Type
        {
            get
            {
                return this.type;
            }
        }

        public List<Objet> Liste_objets_stockes
        {
            get
            {
                return this.objets_stockes;
            }
        }
        public Lieu_Stockage(string type, string adresse, double volume,double solde)
        {
            compteur++;
            this.solde = solde;
            this.id = compteur;
            this.type = type;
            this.adresse = adresse;
            this.volume = volume;
            this.objets_stockes = new List<Objet>();
        }
        
        public void Ajouter_Objet(Objet o)
        {
            this.objets_stockes.Add(o);
            if (typeof(Objet_volumineux)==o.GetType())
            {
                Objet_volumineux ov = (Objet_volumineux)o;
                this.volume -= ov.Volume;
            }
        }

        public void Retirer_Objet(Objet o, double prix)
        {
            if(this.objets_stockes.Contains(o))
            {
                this.objets_stockes.Remove(o);
                this.solde += prix;
            }
            else
            {
                Console.WriteLine("Objet non present dans le lieu de stockage");
            }
        }

        public double Volume_Restant()
        {
            double volume_restant = volume;
            for(int i =0;i<this.objets_stockes.Count;i++)
            {
                if(typeof(Objet_volumineux)==objets_stockes[i].GetType())
                {
                    Objet_volumineux ov = (Objet_volumineux)objets_stockes[i];
                    volume_restant -= ov.Volume;
                }
            }
            return volume_restant;
        }

        public override string ToString()
        {
            return "Lieu n°" + this.id + " : Type : " + this.type + ", Adresse : " + this.adresse + ", Volume restant : " + this.Volume_Restant() + ", Solde : " + this.solde;
        }
    }
}
