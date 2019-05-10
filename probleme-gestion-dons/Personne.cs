using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    public abstract class Personne
    {
        string nom;
        int identifiant;
        string telephone;

        protected Personne(string nom, int identifiant, string telephone)
        {
            this.nom = nom;
            this.identifiant = identifiant;
            this.telephone = telephone;
        }
    }
}
