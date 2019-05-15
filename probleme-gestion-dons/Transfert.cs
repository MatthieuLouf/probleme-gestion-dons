using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Transfert
    {
        double prix;
        DateTime date;
        Lieu_Stockage provenance;
        Objet objet_transfert;
        Personne_beneficiaire beneficiaire;

        public Transfert(double prix, Objet objet_transfert, Personne_beneficiaire beneficiaire, Lieu_Stockage provenance)
        {
            this.prix = prix;
            this.objet_transfert = objet_transfert;
            this.beneficiaire = beneficiaire;
            this.date = DateTime.Now;
            this.provenance = provenance;
        }

        public double Prix
        {
            get { return this.prix; }
        }

        public Objet Objet_transfert
        {
            get { return this.objet_transfert; }
        }

        public DateTime Date
        {
            get { return this.date; }
        }

        public override string ToString()
        {
            return "Vente pour " + prix + " euros, de :\n" + objet_transfert + "\n venant de " + provenance + "\transfere à " + beneficiaire;
        }
    }
}
