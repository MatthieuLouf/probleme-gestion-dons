using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    public abstract class Personne
    {
        protected string nom;
        protected int identifiant;
        protected string telephone;

        public string Telephone
        {
            get
            {
                return this.telephone;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }
        }

        protected Personne(string nom, int identifiant, string telephone)
        {
            this.nom = nom;
            this.identifiant = identifiant;
            this.telephone = telephone;
        }

        public override string ToString()
        {
            return "Personne n°" + this.identifiant + " Nom :" + this.nom + " Tel : " + this.telephone ;
        }
    }
}
