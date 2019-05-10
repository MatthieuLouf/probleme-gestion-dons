using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Personne_adherente : Personne_physique
    {
        string fonction;

        public Personne_adherente(string fonction, string prenom, string adresse, string nom, int identifiant, string telephone) : base (prenom, adresse, nom, identifiant, telephone)
        {
            this.fonction = fonction;
        }

        public override string ToString()
        {
            return base.ToString() + " Fonction : " + this.fonction;
        }
    }
}
