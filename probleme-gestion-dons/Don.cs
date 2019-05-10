using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Don
    {
        DateTime date_reception_don;
        string description_don;
        string status;
        List<Objet> liste_objets;
        Personne_adherente donateur;

        public Don(DateTime date_reception_don, string description_don, string status, List<Objet> liste_objets, Personne_adherente donateur)
        {
            this.date_reception_don = date_reception_don;
            this.description_don = description_don;
            this.status = status;
            this.liste_objets = liste_objets;
            this.donateur = donateur;
        }

        public override string ToString()
        {
            string result = " Déposé le : " + this.date_reception_don.ToShortDateString() + ", description : " + this.description_don + "\n -> status : " + this.status +"\n";
            for(int i =0;i<this.liste_objets.Count;i++)
            {
                result += "    " + this.liste_objets[i].ToString() + "\n";
            }
            return result;
        }
    }
}
