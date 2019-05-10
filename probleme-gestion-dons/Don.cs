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
        string adresse;
        string description_don;
        string status;
        List<Objet> liste_objets;

        public Don(DateTime date_reception_don, string adresse, string description_don, string status, List<Objet> liste_objets)
        {
            this.date_reception_don = date_reception_don;
            this.adresse = adresse;
            this.description_don = description_don;
            this.status = status;
            this.liste_objets = liste_objets;
        }

        public override string ToString()
        {
            string result = " Déposé le : " + this.date_reception_don.ToShortDateString() + ", adresse : " + this.adresse + ", description : " + this.description_don + "\n -> status : " + this.status +"\n";
            for(int i =0;i<this.liste_objets.Count;i++)
            {
                result += "    " + this.liste_objets[i].ToString() + "\n";
            }
            return result;
        }
    }
}
