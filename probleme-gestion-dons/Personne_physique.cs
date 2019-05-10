using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    public abstract class Personne_physique:Personne
    {
        protected string prenom;
        protected string adresse;

        protected Personne_physique(string prenom, string adresse, string nom, int identifiant, string telephone) : base(nom, identifiant, telephone)
        {
            this.prenom = prenom;
            this.adresse = adresse;
        }

        public override string ToString()
        {
            return "Personne n°" + this.identifiant + " Nom :" + this.nom + " Prenom : "+this.prenom+" Tel : " + this.telephone +" Adresse : " + this.adresse;
        }
    }
}
