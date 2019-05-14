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

        public Transfert(double prix, DateTime date, Objet objet_transfert)
        {
            this.prix = prix;
            this.date = date;
            this.objet_transfert = objet_transfert;
            this.date = DateTime.Now;
        }
    }
}
