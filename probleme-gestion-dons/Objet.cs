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

        public Objet(int reference_objet, string type, string description_objet)
        {
            this.reference_objet = reference_objet;
            this.type = type;
            this.description_objet = description_objet;
        }
    }
}
