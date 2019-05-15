using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Don : IIdentifiable, IComparable
    {
        static int compteur = 0;
        int id;
        DateTime date_reception_don;
        string description_don;
        string status;
        List<Objet> liste_objets;
        Personne_adherente donateur;

        public DateTime Date
        {
            get
            {
                return this.date_reception_don;
            }
        }

        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public string Nom_Donateur
        {
            get
            {
                return this.donateur.Nom;
            }
        }

        public string Description
        {
            get
            {
                return this.description_don;
            }
        }

        public Don(DateTime date_reception_don, string description_don, string status, List<Objet> liste_objets, Personne_adherente donateur)
        {
            compteur++;
            this.id = compteur;
            this.date_reception_don = date_reception_don;
            this.description_don = description_don;
            this.status = status;
            this.liste_objets = liste_objets;
            for(int i=0;i<liste_objets.Count;i++)
            {
                liste_objets[i].Don_groupe = this;
            }
            this.donateur = donateur;
        }

        public string Description_don
        {
            get { return this.description_don; }
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public List<Objet> Liste_objets
        {
            get { return this.liste_objets; }
        }

        public override string ToString()
        {
            string result = " Don n°" + this.id + "- Déposé le : " + this.date_reception_don.ToShortDateString() + ", description : " + this.description_don + "\n -> status : " + this.status + "\n";
            for (int i = 0; i < this.liste_objets.Count; i++)
            {
                result += "    " + this.liste_objets[i].ToString() + "\n";
            }
            return result;
        }

        public int CompareTo(object o)
        {
            Don d = (Don)o;
            return this.date_reception_don.CompareTo(d.Date);
        }
    }
}
