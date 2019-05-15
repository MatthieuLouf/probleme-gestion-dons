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

        //-----Moyennes-----//
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
        public TimeSpan AvgTemps_Avant_Transfert
        {
            get
            {
                List<TimeSpan> liste = new List<TimeSpan>();
                foreach (Transfert trans in this.archive_association.Liste_transferts)
                {
                    liste.Add(trans.Date - trans.Objet_transfert.Don_groupe.Date);
                }

                TimeSpan avg = TimeSpan.FromMilliseconds(liste.Average(ts => ts.TotalMilliseconds));

                return avg;
            }
        }
        public double AvgPrix_Garde_Meubles
        {
            get
            {
                int count_objets = 0;
                double somme_prix = 0;
                foreach(Lieu_Stockage lieu in this.lieux_stock)
                {
                    if(lieu.Type=="depot_vente")
                    {
                        foreach(Objet o in lieu.Liste_objets_stockes)
                        {
                            somme_prix += o.Montant;
                            count_objets++;
                        }
                    }
                }
                double moy = 0;
                if(count_objets!=0)
                {
                    moy=somme_prix / count_objets;
                }
                return moy;
            }
        }
        //-----Autres-----//
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
            this.dons_attente.Enqueue(new Don(new DateTime(2019, 5, 1), "Don attente 1", "attente", new List<Objet>() { new Objet_volumineux("Matelas", "un matelas normal", 0, new List<string[]>(), 0.2, 0.9, 2) }, this.findByNom_Adherent("Dupond")));
            this.dons_attente.Enqueue(new Don(new DateTime(2019, 5, 2), "Don attente 2", "attente", new List<Objet>() { new Objet("Couverts", "de beaux couverts pour enfant", 1, new List<string[]>() { new string[2] { "nb de pièces", "8" } } ) }, this.findByNom_Adherent("Dupond")));

            this.dons_donnateur.Enqueue(new Don(new DateTime(2019, 4, 25), "Don donnateur 1", "donnateur", new List<Objet>() { new Objet_volumineux("Chevets", "un chevet normal", 0, new List<string[]>(), 0.5, 0.6, 0.4) }, this.findByNom_Adherent("Durand")));
            this.dons_donnateur.Enqueue(new Don(new DateTime(2019, 4, 24), "Don donnateur 2", "donnateur", new List<Objet>() { new Objet("Assiettes", "de belles assiettes argentés", 50, new List<string[]>() { new string[2] { "nb de pièces", "12" } }) }, this.findByNom_Adherent("Durand")));

            //dons validé et archivé
            this.Archive_association.Add_don_archive(new Don(new DateTime(2019, 4, 1), "Don archive 1", "archive", new List<Objet>() { new Objet_volumineux("Armoire", "une petite armoire en bois", 0, new List<string[]>(), 1.5,1,0.5) }, this.findByNom_Adherent("Courty")));
            this.Archive_association.Add_don_archive(new Don(new DateTime(2019, 4, 2), "Don archive 2", "archive", new List<Objet>() { new Objet_volumineux("Armoire", "une grande armoire en bois", 0, new List<string[]>(), 2, 1.2, 0.8) }, this.findByNom_Adherent("Courty")));

            //don refusé
            this.Archive_association.Add_don_refuse(new Don(new DateTime(2019, 3, 25), "Don refuse 1", "refuse", new List<Objet>() { new Objet_volumineux("Chaise", "chaise avec un pied cassé", 0, new List<string[]>(), 1.2, 0.6, 0.6) }, this.findByNom_Adherent("Courty")));

            this.lieux_stock[0].Ajouter_Objet(new Objet("Couverts", "de beaux couverts pour enfant", 1, new List<string[]>() { new string[2] { "nb de pièces", "10" } }));
            this.lieux_stock[0].Ajouter_Objet(new Objet("Couverts", "de beaux couverts pour enfant", 1, new List<string[]>() { new string[2] { "nb de pièces", "4" } }));

            this.lieux_stock[1].Ajouter_Objet(new Objet_volumineux("Lave-linge", "un lave linge de 2010", 75, new List<string[]>(), 1.5, 1, 1));
            this.lieux_stock[1].Ajouter_Objet(new Objet_volumineux("Lave-linge", "un lave linge de 2010", 70, new List<string[]>(), 1.5, 1, 1));

            this.lieux_stock[2].Ajouter_Objet(new Objet_volumineux("Réfrigérateur", "un frigo de 2010", 65, new List<string[]>(), 1, 0.5, 0.6));
            this.lieux_stock[2].Ajouter_Objet(new Objet_volumineux("Réfrigérateur", "un frigo de 2010", 80, new List<string[]>(), 1.7, 0.7, 0.6));

        }

    }
}
