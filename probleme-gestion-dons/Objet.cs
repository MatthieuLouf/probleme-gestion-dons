using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Objet
    {
        static int compteur = 0;
        int reference_objet;
        string type;
        string description_objet;
        double montant;
        Don don_groupe;

        public Objet(string type, string description_objet, double montant)
        {
            this.reference_objet = compteur;
            compteur++;
            this.type = type;
            this.description_objet = description_objet;
            this.montant = montant;
        }

        public override string ToString()
        {
            return "Objet n°" + this.reference_objet + " : " + this.type + " (" + this.description_objet + ") pour le prix de " + this.montant;
        }

        public Don Don_groupe
        {
            set { this.don_groupe = value; }
        }
    }
}
