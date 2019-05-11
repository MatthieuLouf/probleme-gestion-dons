using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Association
    {
        List<Personne_adherente> liste_adherent;
        List<Personne_beneficiaire> liste_beneficiaire;
        List<Don> dons_attente;
        List<Don> dons_valide;

        public Association(List<Personne_adherente> liste_adherent, List<Personne_beneficiaire> liste_beneficiaire)
        {
            this.liste_adherent = liste_adherent;
            this.liste_beneficiaire = liste_beneficiaire;
            this.dons_attente = new List<Don>();
            this.dons_valide = new List<Don>();
        }

        //-----findBy-----//

        public Personne_beneficiaire findByPhone_Beneficiaire(string numero)
        {
            return this.liste_beneficiaire.Find(x => x.Telephone == numero);
        }

        public Personne_beneficiaire findByNom_Beneficiaire(string nom)
        {
            return this.liste_beneficiaire.Find(x => x.Nom == nom);
        }

        public Personne_adherente findByPhone_Adherent(string numero)
        {
            return this.liste_adherent.Find(x => x.Telephone == numero);
        }

        public Personne_adherente findByNom_Adherent(string nom)
        {
            return this.liste_adherent.Find(x => x.Nom == nom);
        }

        //-----GET-SET-----//
        public List<Personne_adherente> Liste_adherent
        {
            get { return this.liste_adherent; }
        }
        public List<Don> Dons_attente
        {
            get { return this.dons_attente; }
        }
        public List<Don> Dons_valide
        {
            get { return this.dons_valide; }
        }


        //-----Autres-----//

        public void AjouterDonAttente(Don d)
        {
            this.dons_attente.Add(d);
        }

        public void ValiderDon(Don d,int choix)

        {
            if (dons_attente.Exists((x) => x.Id == d.Id))
            {
                if (choix == 1)
                {
                    this.dons_valide.Add(d);
                    Console.WriteLine("Don validé !");
                }
                if(choix==2)
                {
                    //Passage du don en Archivage
                    Console.WriteLine("Don refusé et archivé (en cours de dev) !");
                }
                this.dons_attente.Remove(d);
            }
        }

    }
}
