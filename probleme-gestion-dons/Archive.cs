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

        public List<Transfert> Liste_transferts
        {
            get { return this.liste_transferts; }
        }
        /// <summary>
        /// Ajoute un don validé à l'archive
        /// </summary>
        /// <param name="don">don</param>
        public void Add_don_archive(Don don)
        {
            don.Status = "archive";
            this.liste_dons_archive.Add(don);
        }
        /// <summary>
        /// Ajoute un don à la liste des dons refusés
        /// </summary>
        /// <param name="don">don</param>
        public void Add_don_refuse(Don don)
        {
            don.Status = "refuse";
            this.liste_dons_refuse.Add(don);
        }
        /// <summary>
        /// Ajoute un Transfert à la liste des transferts effectués par l'assos
        /// </summary>
        /// <param name="trans"></param>
        public void Add_objet_transfere(Transfert trans)
        {
            this.liste_transferts.Add(trans);
        }

        public override string ToString()
        {
            string result = "";
            result += "Dons Validés archivés\n";
            this.liste_dons_archive.ForEach(x => result += x+"\n");

            result += "Dons Validés refusés\n";
            this.liste_dons_refuse.ForEach(x => result += x + "\n");

            result += "Transferts effectués\n";
            this.liste_transferts.ForEach(x => result += x + "\n");

            return result;
        }
    }
}
