﻿using System;
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
        List<Don> dons_valide;
        Archive archive_association;

        public Archive Archive_association
        {
            get
            {
                return this.archive_association;
            }
        }

        public Association(List<Personne_adherente> liste_adherent, List<Personne_beneficiaire> liste_beneficiaire)
        {
            this.liste_adherent = liste_adherent;
            this.liste_beneficiaire = liste_beneficiaire;
            this.dons_attente = new Queue<Don>();
            this.dons_donnateur = new Queue<Don>();
            this.dons_valide = new List<Don>();
            this.archive_association = new Archive(new List<Don>(), new List<Don>(), new List<Transfert>());
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
        public Queue<Don> Dons_attente
        {
            get { return this.dons_attente; }
        }
        public Queue<Don> Dons_donnateur
        {
            get { return this.dons_donnateur; }
        }
        public List<Don> Dons_valide
        {
            get { return this.dons_valide; }
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
                    this.dons_valide.Add(d);
                    Console.WriteLine("Don validé !");
                }
                if(choix==2)
                {
                    //Passage du don en Archivage
                    this.Archive_association.Add_don_refuse(d);
                    Console.WriteLine("Don refusé et archivé (en cours de dev) !");
                }
                this.dons_attente.Dequeue();
            }
        }

    }
}
