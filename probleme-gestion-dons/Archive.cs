using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Archive
    {
        List<Don> liste_dons_archive;
        List<Don> liste_dons_refuse;
        List<Transfert> liste_transferts;

        public List<Don> Dons_refuses
        {
            get
            {
                return this.liste_dons_refuse;
            }
        }
        public List<Don> Dons_archive
        {
            get
            {
                return this.liste_dons_archive;
            }
        }

        public Archive(List<Don> liste_dons_archive, List<Don> liste_dons_refuse, List<Transfert> liste_transferts)
        {
            this.liste_dons_archive = liste_dons_archive;
            this.liste_dons_refuse = liste_dons_refuse;
            this.liste_transferts = liste_transferts;
        }

        public void Add_don_archive(Don don)
        {
            don.Status = "archive";
            this.liste_dons_archive.Add(don);
        }

        public void Add_don_refuse(Don don)
        {
            don.Status = "refuse";
            this.liste_dons_refuse.Add(don);
        }

        public void Add_objet_transfere(Transfert trans)
        {
            this.liste_transferts.Add(trans);
        }
    }
}
