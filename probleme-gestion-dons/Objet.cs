using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Objet
    {
        int reference_objet;
        string type;
        string description_objet;
        double montant;

        public Objet(int reference_objet, string type, string description_objet)
        {
            this.reference_objet = reference_objet;
            this.type = type;
            this.description_objet = description_objet;
        }

        public Objet(int reference_objet, string type, string description_objet, double montant)
        {
            this.reference_objet = reference_objet;
            this.type = type;
            this.description_objet = description_objet;
            this.montant = montant;
        }

        public override string ToString()
        {
            return "Objet : " + this.reference_objet + " " + this.type + " " + this.description_objet + " " + this.montant;
        }
    }
}
