using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Lieu_Stockage : IIdentifiable,IComparable
    {
        static int compteur = 0;
        int id;
        string type;
        string adresse;
        double volume;
        double solde;
        List<Objet> objets_stockes;

        public Lieu_Stockage(string type, string adresse, double volume, double solde)
        {
            compteur++;
            this.solde = solde;
            this.id = compteur;
            this.type = type;
            this.adresse = adresse;
            this.volume = volume;
            this.objets_stockes = new List<Objet>();
        }

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
        public double Volume_Restant
        {
            get
            {
                double volume_restant = volume;
                for (int i = 0; i < this.objets_stockes.Count; i++)
                {
                    if (typeof(Objet_volumineux) == objets_stockes[i].GetType())
                    {
                        Objet_volumineux ov = (Objet_volumineux)objets_stockes[i];
                        volume_restant -= ov.Volume;
                    }
                }
                return volume_restant;
            }
        }
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public override string ToString()
        {
            return "Lieu n°" + this.id + " : Type : " + this.type + ", Adresse : " + this.adresse + ", Volume restant : " + this.Volume_Restant + ", Solde : " + this.solde;
        }
        public int CompareTo(object o)
        {
            Lieu_Stockage l = (Lieu_Stockage)o;
            return this.id.CompareTo(l.Id);
        }

        /// <summary>
        /// Ajouter un objet au lieu de stockage
        /// </summary>
        /// <param name="o">Objet à ajouter au lieu de stockage</param>
        public void Ajouter_Objet(Objet o)
        {
            this.objets_stockes.Add(o);
            /*if (typeof(Objet_volumineux) == o.GetType())
            {
                Objet_volumineux ov = (Objet_volumineux)o;
                this.volume -= ov.Volume;
            }*/
        }
        /// <summary>
        /// Retirer un objet du lieu de stockage en vérifiant s'il y était bien, et incrémenter le solde dans le cas d'un dépot vente
        /// </summary>
        /// <param name="o">Objet à retirer</param>
        /// <param name="prix">Prix auquel a été transféré l'objet</param>
        public void Retirer_Objet(Objet o, double prix)
        {
            if (this.objets_stockes.Contains(o))
            {
                this.objets_stockes.Remove(o);
                this.solde += prix;
            }
            else
            {
                Console.WriteLine("Objet non present dans le lieu de stockage");
            }
        }
    }
}
