using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    public class Personne_morale : Personne
    {
        string coordonnees;
        string type_activite;

        public Personne_morale(string coordonnees, string type_activite, string nom, int identifiant, string telephone) : base(nom, identifiant, telephone)
        {
            this.coordonnees = coordonnees;
            this.type_activite = type_activite;
        }

        public override string ToString()
        {
            return base.ToString() + "Coordonnees : " + this.coordonnees + "Type d'activite : " + this.type_activite;
        }
    }
}
