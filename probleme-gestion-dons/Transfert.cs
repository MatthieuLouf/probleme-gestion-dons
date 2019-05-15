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
        Objet objet_transfert;
        Personne_beneficiaire beneficiaire;

        public Transfert(double prix, Objet objet_transfert, Personne_beneficiaire beneficiaire)
        {
            this.prix = prix;
            this.objet_transfert = objet_transfert;
            this.beneficiaire = beneficiaire;
            this.date = DateTime.Now;
        }

        public double Prix
        {
            get { return this.prix; }
        }

        public Objet Objet_transfert
        {
            get { return this.objet_transfert; }
        }
    }
}
