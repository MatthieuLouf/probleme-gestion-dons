using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Personne_beneficiaire : Personne_physique
    {
        DateTime date_naissance;

        public Personne_beneficiaire(DateTime date_naissance, string prenom, string adresse, string nom, int identifiant, string telephone) : base(prenom, adresse, nom, identifiant, telephone)
        {
            this.date_naissance = date_naissance;
        }

        public DateTime DateNaissance
        {
            get
            {
                return this.date_naissance;
            }
        }

        public override string ToString()
        {
            return base.ToString() + " Naissance : " + this.date_naissance.ToShortDateString();
        }
    }
}
