using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Archive
    {
        List<Don> liste_dons;
        List<Transfert> liste_transferts;

        public Archive(List<Don> liste_dons, List<Transfert> liste_transferts)
        {
            this.liste_dons = liste_dons;
            this.liste_transferts = liste_transferts;
        }

        public void Add_don(Don don)
        {
            this.liste_dons.Add(don);
        }
    }
}
