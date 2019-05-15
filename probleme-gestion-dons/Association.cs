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
        Queue<Don> dons_attente;
        Queue<Don> dons_donnateur;
        //List<Don> dons_valide;
        List<Lieu_Stockage> lieux_stock;
        Archive archive_association;

        public Archive Archive_association
        {
            get
            {
                return this.archive_association;
            }
        }

        public Association()
        {
            this.liste_adherent = new List<Personne_adherente>();
            this.liste_beneficiaire = new List<Personne_beneficiaire>();
            this.dons_attente = new Queue<Don>();
            this.dons_donnateur = new Queue<Don>();
            //this.dons_valide = new List<Don>();
            this.archive_association = new Archive(new List<Don>(), new List<Don>(), new List<Transfert>());

            this.lieux_stock = new List<Lieu_Stockage>();

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
        public List<Personne_beneficiaire> Liste_beneficiaire
        {
            get { return this.liste_beneficiaire; }
        }
        public Queue<Don> Dons_attente
        {
            get { return this.dons_attente; }
        }
        public Queue<Don> Dons_donnateur
        {
            get { return this.dons_donnateur; }
        }
        public List<Lieu_Stockage> Lieux_stock
        {
            get { return this.lieux_stock; }
        }

        public void Set_Utilisateurs(List<Personne_adherente> liste_adherent, List<Personne_beneficiaire> liste_beneficiaire)
        {
            this.liste_adherent = liste_adherent;
            this.liste_beneficiaire = liste_beneficiaire;
        }

        //-----Autres-----//

        public TimeSpan AvgAge_Beneficiaires
        {
            get
            {
                List<TimeSpan> liste = new List<TimeSpan>();
                foreach (Personne_beneficiaire a in liste_beneficiaire)
                {
                    liste.Add(DateTime.Now - a.DateNaissance);
                }

                TimeSpan avg = TimeSpan.FromMilliseconds(liste.Average(ts => ts.TotalMilliseconds));
                
                return avg;
            }
        }
        public void AjouterDonAttente(Don d)
        {
            if (d.Status != "attente") d.Status = "attente";
            this.dons_attente.Enqueue(d);
        }
        public void AjouterDonDonnateur(Don d)
        {
            if (d.Status != "chez_donnateur") d.Status = "chez_donnateur";
            this.dons_donnateur.Enqueue(d);
        }

        public void ValiderDon(Don d,int choix)
        {
            if (dons_attente.Contains(d))
            {
                if (choix == 1)
                {
                    d.Status = "valide";
                    this.Archive_association.Add_don_archive(d);
                    Console.WriteLine("Don validé !");
                }
                if(choix==2)
                {
                    d.Status = "refuse";
                    this.Archive_association.Add_don_refuse(d);
                    Console.WriteLine("Don refusé et archivé !");
                }
                this.dons_attente.Dequeue();
            }
        }
        public void Transferer_Objet(Lieu_Stockage lieu, Transfert trans)
        {
            lieu.Retirer_Objet(trans.Objet_transfert, trans.Prix);
            this.archive_association.Add_objet_transfere(trans);
        }

        public void Init()
        {
            // Lieux de stockages
            this.lieux_stock.Add(new Lieu_Stockage("association", "18 rue Jean-Moulin 75002 Paris", 200, 0));
            this.lieux_stock.Add(new Lieu_Stockage("entrepot", "1842 avenue Roger Salengro 75011 Paris", 400, 0));
            this.lieux_stock.Add(new Lieu_Stockage("depot_vente", "106 impasse du Chene 75019 Paris", 250, 1200));

            // dons_attente & dons_donnateur
            this.dons_attente.Enqueue(new Don(new DateTime(2019, 5, 1), "Don attente 1", "attente", new List<Objet>() { new Objet("Matelas", "un matelas normal", 0) }, this.findByNom_Adherent("Dupond")));
            this.dons_attente.Enqueue(new Don(new DateTime(2019, 5, 2), "Don attente 2", "attente", new List<Objet>() { new Objet("Couverts", "de beaux couverts pour enfant", 1, new List<string[]>() { new string[2] { "nb de pièces", "8" } } ) }, this.findByNom_Adherent("Dupond")));

            this.dons_donnateur.Enqueue(new Don(new DateTime(2019, 4, 25), "Don donnateur 1", "donnateur", new List<Objet>() { new Objet("Chevets", "un chevet normal", 0) }, this.findByNom_Adherent("Durand")));
            this.dons_donnateur.Enqueue(new Don(new DateTime(2019, 4, 24), "Don donnateur 2", "donnateur", new List<Objet>() { new Objet("Assiettes", "de belles assiettes argentés", 50, new List<string[]>() { new string[2] { "nb de pièces", "12" } }) }, this.findByNom_Adherent("Durand")));

        }

    }
}
