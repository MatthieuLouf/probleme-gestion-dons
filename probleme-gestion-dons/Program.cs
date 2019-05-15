using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace probleme_gestion_dons
{
    class Program
    {
        #region Fonctions Saisie Valeurs

        /// <summary>
        /// demande de saisie de String et affichage de la demande
        /// </summary>
        /// <param name="demande">Phrase demandant le string à saisir</param>
        /// <returns>le string saisi</returns>
        static string demanderString(string demande="")
        {
            if(demande!="")
            {
                Console.Write(demande +" : ");
            }
            return Console.ReadLine();
        }

        /// <summary>
        /// demande de saisie sécurisée d'un Int, qui réutilise demanderString(...), avec des valeurs min et max optionnelles
        /// </summary>
        /// <param name="demande">Phrase demandant le int à saisir</param>
        /// <param name="min">Valeur minimale à saisir, optionnelle</param>
        /// <param name="max">Valeur maximale à saisir, optionnelle</param>
        /// <returns>le int saisi</returns>
        static int demanderInt(string demande="",int min = -1, int max = -1)
        {
            bool err = false;
            bool est_minmax = false;
            bool est_min = false;
            if (min != -1 && max != -1)
            {
                est_minmax = true;
            }
            else if (max == -1 && min != -1)
            {
                est_min = true;
            }

            int result = -2;

            do
            {
                try
                {
                    result = Convert.ToInt32(demanderString(demande));
                    err = false;
                }
                catch
                {
                    err = true;
                }
            } while (err == true || (est_minmax == true && (result < min || result > max)) || (est_min == true && (result < min)));
            return result;
        }

        /// <summary>
        /// demande de saisie sécurisée d'un double, qui réutilise demanderString(...)
        /// </summary>
        /// <param name="demande">Phrase demandant le double à saisir</param>
        /// <param name="min">Valeur minimale à saisir, optionnelle</param>
        /// <param name="max">Valeur maximale à saisir, optionnelle</param>
        /// <returns>le double saisi</returns>
        static double demanderDouble(string demande = "", double min = -1, double max = -1)
        {
            bool err = false;
            bool est_minmax = false;
            bool est_min = false;
            if (min != -1 && max != -1)
            {
                est_minmax = true;
            }
            else if(max==-1 && min!=-1)
            {
                est_min = true;
            }
                
            double result = -2;

            do
            {
                try
                {
                    result = Convert.ToDouble(demanderString(demande));
                    err = false;
                }
                catch
                {
                    err = true;
                }
            } while (err == true || (est_minmax == true && (result < min || result > max)) || (est_min==true && (result<min)) );
            return result;
        }
        #endregion

        #region Fonctions Module Don
        static Objet entrerObjet()
        {
            List<string> type_objets_volumineux = new List<string>();
            type_objets_volumineux.Add("Matelas");
            type_objets_volumineux.Add("Chevets");
            type_objets_volumineux.Add("Armoires");
            type_objets_volumineux.Add("Table");
            type_objets_volumineux.Add("Chaises");
            type_objets_volumineux.Add("Cuisinière");
            type_objets_volumineux.Add("Réfrigérateurs");
            type_objets_volumineux.Add("Lave-Linge");

            List<string> type_objets_non_volumineux = new List<string>();
            type_objets_non_volumineux.Add("Couverts");
            type_objets_non_volumineux.Add("Assiettes");

            Objet result=null;
            int choix = demanderInt("Est-ce un objet volumineux ? 1-Oui 2-Non", 1, 2);
            List<string[]> info_sup = new List<string[]>();

            string type = "";
            if (choix == 1)
            {
                type_objets_volumineux.ForEach(x => Console.Write(x.ToString()+", "));
                Console.WriteLine();
                do
                {
                    type = demanderString("Entrez le type de l'objet");
                } while (!type_objets_volumineux.Contains(type));
                
            }
            else
            {
                type_objets_non_volumineux.ForEach(x => Console.Write(x.ToString() + ", "));
                Console.WriteLine();

                do
                {
                    type = demanderString("Entrez le type de l'objet");
                } while (!type_objets_non_volumineux.Contains(type));
            }

            if (type == "Table")
            {
                
                string[] type_table = { "type", demanderString("Entrez le type de table : cuisine ou salon") };
                string[] forme_table = { "forme", demanderString("Entrez la forme de la table : rectangulaire, carré ou ronde") };
                info_sup.Add(type_table);
                info_sup.Add(forme_table);
            }
            else if (type == "Cuisinière")
            {
                string[] puissance_cuisiniere = { "puissance", demanderString("Entrez la puissance") };
                string[] nb_plaque_cuisiniere = { "nombre de plaques", demanderString("Entrez le nombre de plaques") };
                info_sup.Add(puissance_cuisiniere);
                info_sup.Add(nb_plaque_cuisiniere);
            }
            else if (type == "Couverts" || type == "Assiettes")
            {
                string[] nb_pieces = { "nombre de pièces", demanderString("Entrez le nombre de pièces") };
                info_sup.Add(nb_pieces);
            }

            string description_objet = demanderString("Entrez la description de l'objet");
            int montant = demanderInt("Entrez le montant de l'objet");

            if(choix == 1)
            {
                double hauteur = demanderDouble("Sa hauteur",0);
                double largeur = demanderDouble("Sa largeur",0);
                double longueur = demanderDouble("Sa longueur",0);
                result = new Objet_volumineux(type, description_objet, montant, info_sup, hauteur, largeur, longueur);
            }
            else
            {
                result = new Objet(type, description_objet, montant, info_sup);
            }
            
            return result;
        }
        static Don entrerDon(Association assos)
        {
            Console.WriteLine("\nSaisie des informations du Don : \n");

            string status = "attente";
            DateTime date_reception_don = DateTime.Today;

            string nom_donateur;
            Personne_adherente p = null;
            do
            {
                nom_donateur = demanderString("Entrer le nom de la personne adhérente (donateur)");
                p = assos.findByNom_Adherent(nom_donateur);
            } while (p == null);

            string description_don = demanderString("Entrez la description du don");
            int nb_objets = demanderInt("Entrez le nombre d'objets dans le don",1);


            List<Objet> liste_objets = new List<Objet>();
            for (int i=0;i<nb_objets;i++)
            {
                Console.WriteLine("\n-> Saisie de l'objet n°" + (i+1));
                Objet o = entrerObjet();
                liste_objets.Add(o);
            }

            Don result = new Don(date_reception_don, description_don, status, liste_objets, p);
            return result;
        }
        public static void Creation_Don(Association asso)
        {
            asso.Liste_adherent.ForEach(x => Console.WriteLine(x));
            if(asso.Liste_adherent.Count!=0)
            {
                Don d = entrerDon(asso);
                asso.AjouterDonAttente(d);
            }
            else { Console.WriteLine("Aucun adhérent rentré dans le logiciel!"); }
        }
        public static void Gestion_Dons_Attente(Association asso)
        {
            if(asso.Dons_attente.Count!=0)
            { 
                Don d = asso.Dons_attente.Dequeue();
                Console.WriteLine(d);
                asso.ValiderDon(d, demanderInt("1-Valider ce don 2-Refuser le don", 1, 2));
                int stocker_don = demanderInt("1-le stocker 2-le laisser chez le donnateur", 1,2);
                if(stocker_don==2)
                {
                    asso.AjouterDonDonnateur(d);
                }
                else
                {
                    Gestion_Stockage(d,asso);
                }
            }
            else { Console.WriteLine("Pas de dons en attende de validation !"); }
        }
        public static void Gestion_Dons_Chez_Donnateur(Association asso)
        {
            if (asso.Dons_donnateur.Count != 0)
            {
                Don d = asso.Dons_donnateur.Dequeue();
                Console.WriteLine(d);
                int stocker_don = demanderInt("1-le stocker 2-le laisser chez le donnateur", 1, 2);
                if (stocker_don == 2)
                {
                    asso.Dons_donnateur.Enqueue(d);
                }
                else
                {
                    Gestion_Stockage(d, asso);
                }
            }
            else { Console.WriteLine("Pas de dons en attente chez les donnateurs !"); }
        }
        public static void Gestion_Stockage(Don d,Association asso)
        {
            List<Objet> ls = d.Liste_objets;
            for(int i=0;i<ls.Count;i++)
            {
                Stocker_Objet(ls[i], asso);
            }
        }
        public static void Stocker_Objet(Objet o, Association a)
        {
            List<Lieu_Stockage> ls = a.Lieux_stock;
            ls.ForEach(x => Console.WriteLine(x));
            Console.WriteLine("\n" + o);

            int choix = -1;

            if (typeof(Objet_volumineux) == o.GetType())
            {
                Objet_volumineux ov = (Objet_volumineux)o;
                do
                {
                    choix = demanderInt("Dans quel lieu voulez vous le stocker cet objet lourd", 1, ls.Count);
                } while (ls[choix - 1].Volume_Restant < ov.Volume);
            }
            else
            {
                choix = demanderInt("Dans quel lieu voulez vous le stocker", 1, ls.Count);
            }
            ls[choix - 1].Ajouter_Objet(o);
        }
        public static void Gestion_Transfert(Association asso)
        {
            asso.Lieux_stock.ForEach(x => Console.WriteLine(x));
            int choix = demanderInt("\nAvec quel lieu de stockage voulez vous faire un transfert - n°",1, asso.Lieux_stock.Count);

            Lieu_Stockage stock = asso.Lieux_stock[choix - 1];
            if(stock.Liste_objets_stockes.Count!=0)
            {
                List<Objet> ls_objets = stock.Liste_objets_stockes;
                ls_objets.ForEach(x => Console.WriteLine(x));
                int choix2 = -1;
                do
                {
                    choix2 = demanderInt("\nQuel Objet voulez-vous transférer? - n°", 1);
                } while (ls_objets.Find(x => x.Id == choix2)==null);


                asso.Liste_beneficiaire.ForEach(x => Console.WriteLine(x));
                string nom_donateur;
                Personne_beneficiaire p = null;
                do
                {
                    nom_donateur = demanderString("Entrer le nom de la personne adhérente (donateur)");
                    p = asso.findByNom_Beneficiaire(nom_donateur);
                } while (p == null);

                double prix = 0;

                if(stock.Type=="depot_vente")
                {
                    prix = demanderDouble("Pour quel prix ?", 0, -1);
                }

                Transfert trans = new Transfert(prix, ls_objets.Find(x => x.Id == choix2), p,stock);
                asso.Transferer_Objet(stock, trans);
                Console.WriteLine("Objet transféré à " + p.Nom + "!");
            }
            else
            {
                Console.WriteLine("Lieu de stockage vide!");
            }


        }
        #endregion

        #region Fonctions Module Tri
        /// <summary>
        /// Listing des Objets vendus ou transférés par l'association
        /// </summary>
        /// <param name="assos">Association dans laquelle lister les objets transférés</param>
        /// <param name="methode">Méthode de comparaison pour sort les objets</param>
        public static void Lister_Dons_vendus(Association assos, Comparison<Don> methode)
        {
            List<Don> liste = assos.Archive_association.Dons_archive;
            liste.Sort(methode);
            liste.ForEach(x => Console.WriteLine(x.ToString()));
        }

        /// <summary>
        /// Listing des dons refusés par date
        /// </summary>
        /// <param name="assos">Association dans laquelle lister les objets transférés</param>
        public static void Lister_Dons_refuse_date(Association assos)
        {
            List<Don> liste = assos.Archive_association.Dons_refuses;
            liste.Sort((a, b) => a.Date.CompareTo(b.Date));
            liste.ForEach(x => Console.WriteLine(x.ToString()));
        }

        /// <summary>
        /// Listing des dons en traitement, c'est à dire en attente de validation ou laissés chez le donnateur
        /// </summary>
        /// <param name="assos">Association dans laquelle lister les objets transférés</param>
        /// <param name="methode">Méthode de comparaison pour sort les objets</param>
        public static void Lister_Dons_en_traitement(Association assos, Comparison<Don> methode)
        {
            List<Don> liste = assos.Dons_attente.ToList<Don>();
            assos.Dons_donnateur.ToList<Don>().ForEach(x => liste.Add(x));
            liste.Sort(methode);
            liste.ForEach(x => Console.WriteLine(x.ToString()));
        }

        /// <summary>
        /// Listing des dons stockés par entrepot
        /// </summary>
        /// <param name="assos">Association dans laquelle lister les objets transférés</param>
        /// <param name="methode">Méthode de comparaison pour sort les objets</param>
        public static void Lister_Dons_par_entrepots(Association assos, Comparison<Objet> methode)
        {
            List<Objet> liste = new List<Objet>();
            assos.Lieux_stock.ForEach(delegate (Lieu_Stockage l) {
                l.Liste_objets_stockes.Sort(methode);
                l.Liste_objets_stockes.ForEach(x => Console.WriteLine(x.ToString()));
            });
            
            liste.ForEach(x => Console.WriteLine(x.ToString()));
        }
        #endregion

        #region Fonctions Module Personne
        /// <summary>
        /// Fonction de lecture automatique des personnes adhérentes dans un fichier .txt
        /// </summary>
        /// <param name="fileName">chemin et nom du fichier à lire</param>
        /// <returns>la liste des personnes adhérentes créée à partir  </returns>
        public static void Lister_Dons_Volumineux_par_entrepots(Association assos, Comparison<Objet_volumineux> methode)
        {
            List<Objet> liste = new List<Objet>();
            assos.Lieux_stock.ForEach(delegate (Lieu_Stockage l) {
                List<Objet_volumineux> liste_obj_volumineux = new List<Objet_volumineux>();
                l.Liste_objets_stockes.ForEach(delegate (Objet obj)
                {
                    if (obj.GetType() == typeof(Objet_volumineux)){
                        liste_obj_volumineux.Add((Objet_volumineux)obj);
                    }
                });
                liste_obj_volumineux.Sort(methode);
            liste_obj_volumineux.ForEach(x => Console.WriteLine(x.ToString()));
            });
            liste.ForEach(x => Console.WriteLine(x.ToString()));
        }

        public static void Lister_Dons_par_depot_vente(Association assos, Comparison<Objet> methode)
        {
            assos.Lieux_stock.ForEach(delegate (Lieu_Stockage l) {
                if (l.Type == "depot_vente")
                {
                    l.Liste_objets_stockes.Sort(methode);
                    l.Liste_objets_stockes.ForEach(x => Console.WriteLine(x.ToString()));
                }
            });
        }

        public static List<Personne_adherente> lecture_personnes_adherente(string fileName)
        {
            List<Personne_adherente> liste = new List<Personne_adherente>();
            string[] fileString = File.ReadAllLines(fileName);

            for (int ligne = 0; ligne < fileString.Length - 1; ligne++)
            {
                string[] substring = fileString[ligne].Split(';');
                int identifiant = int.Parse(substring[0]);
                string nom = substring[1];
                string adresse = substring[2];
                string telephone = substring[3];
                string prenom = substring[4];
                string fonction = substring[5];
                liste.Add(new Personne_adherente(fonction, prenom, adresse, nom, identifiant, telephone));
            }
            Console.WriteLine("Adhérents :");
            liste.ForEach(x => Console.WriteLine(x));
            Console.WriteLine();
            return liste;
        }

        /// <summary>
        /// Fonction de lecture automatique des personnes bénéficia dans un fichier .txt
        /// </summary>
        /// <param name="fileName">chemin et nom du fichier à lire</param>
        /// <returns></returns>
        public static List<Personne_beneficiaire> lecture_personnes_beneficiaire(string fileName)
        {
            List<Personne_beneficiaire> liste = new List<Personne_beneficiaire>();
            string[] fileString = File.ReadAllLines(fileName);

            for (int ligne = 0; ligne < fileString.Length - 1; ligne++)
            {
                string[] substring = fileString[ligne].Split(';');
                int identifiant = int.Parse(substring[0]);
                string nom = substring[1];
                string adresse = substring[2];
                string telephone = substring[3];
                string prenom = substring[4];
                string date_naissance = substring[5];
                DateTime date = DateTime.Parse(date_naissance);
                liste.Add(new Personne_beneficiaire(date, prenom, adresse, nom, identifiant, telephone));
            }
            Console.WriteLine("Bénéficiaires :");
            liste.ForEach(x => Console.WriteLine(x));
            Console.WriteLine();
            return liste;
        }
        #endregion

        #region Fonctions Menu des Modules
        static void Module_Personne(Association asso)
        {
            bool fin = false;

            do
            {
                fin = false;
                Console.WriteLine();
                Console.WriteLine("1 : Lire les fichiers utilisateurs");
                Console.WriteLine("2 : Recherche par téléphone sur les bénéficiaires");
                Console.WriteLine("3 : Recherche par nom sur les bénéficiaires");
                Console.WriteLine("4 : Recherche par téléphone sur les adhérents");
                Console.WriteLine("5 : Recherche par nom sur les adhérents");

                Console.WriteLine("\n0 : Revenir au menu général");

                int lecture = demanderInt("\nChoisissez votre programme", 0, 5);
                string recherche = "";
                switch (lecture)
                {
                    case 1:
                        Console.Clear();
                        List<Personne_adherente> liste_adherent = lecture_personnes_adherente("..\\..\\data\\Adherents.txt");
                        List<Personne_beneficiaire> liste_beneficiaire = lecture_personnes_beneficiaire("..\\..\\data\\Beneficiaires.txt");
                        asso.Set_Utilisateurs(liste_adherent, liste_beneficiaire);
                        Console.WriteLine("Fichiers lus!");
                        break;
                    case 2:
                        Console.Clear();
                        recherche = demanderString("Quel numéro de téléphone de bénéficiaire?");
                        Console.WriteLine(asso.findByPhone_Beneficiaire(recherche));
                        break;
                    case 3:
                        Console.Clear();
                        recherche = demanderString("Quel nom de bénéficiaire?");
                        Console.WriteLine(asso.findByNom_Beneficiaire(recherche));
                        break;
                    case 4:
                        Console.Clear();
                        recherche = demanderString("Quel numéro de téléphone d'adhérent?");
                        Console.WriteLine(asso.findByPhone_Adherent(recherche));
                        break;
                    case 5:
                        Console.Clear();
                        recherche = demanderString("Quel nom d'adhérent?");
                        Console.WriteLine(asso.findByNom_Adherent(recherche));
                        break;
                    case 0:
                        Console.Clear();
                        fin = true;
                        break;

                    default:
                        Console.WriteLine("\nchoix non valide => faites un autre choix....");
                        break;
                }
            } while (!fin);
        }
        static void Module_Don(Association asso)
        {
            bool fin = false;

            do
            {
                fin = false;
                Console.WriteLine();
                Console.WriteLine("1 : Ajouter un don au logiciel");
                Console.WriteLine("2 : Accepter ou refuser un don");
                Console.WriteLine("3 : Déménager un don du donnateur");
                Console.WriteLine("4 : Transferer un objet à un bénéficiaire");

                Console.WriteLine("\n0 : Revenir au menu général");

                int lecture = demanderInt("\nChoisissez votre programme", 0, 4);

                switch (lecture)
                {
                    case 1:
                        Console.Clear();
                        Creation_Don(asso);
                        break;
                    case 2:
                        Console.Clear();
                        Gestion_Dons_Attente(asso);
                        break;
                    case 3:
                        Console.Clear();
                        Gestion_Dons_Chez_Donnateur(asso);
                        break;
                    case 4:
                        Console.Clear();
                        Gestion_Transfert(asso);
                        break;
                    case 0:
                        Console.Clear();
                        fin = true;
                        break;

                    default:
                        Console.WriteLine("\nchoix non valide => faites un autre choix....");
                        break;
                }
            } while (!fin);
        }
        static void Module_Tri(Association asso)
        {

            bool fin = false;

            do
            {
                fin = false;
                Console.WriteLine();
                Console.WriteLine("1 : Afficher dons");
                Console.WriteLine("2 : Liste dons refusés triés par date");
                Console.WriteLine("3 : Liste dons en traitement par Id et par Nom");
                Console.WriteLine("4 : Liste dons vendus par mois et par numéro de bénéficiaires");
                Console.WriteLine("5 : Liste dons stockés par entrepôt et par catégorie/description");
                Console.WriteLine("6 : Liste dons stockés par entrepôt et par volume");
                Console.WriteLine("7 : Liste dons par dépôt-vente et par prix");

                Console.WriteLine("\n0 : Revenir au menu général");

                int lecture = demanderInt("\nChoisissez votre programme", 0, 7);

                switch (lecture)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Dons en attente :");
                        asso.Dons_attente.ToList().ForEach(x => Console.WriteLine(x));
                        Console.WriteLine("Dons valides :");
                        asso.Archive_association.Dons_archive.ForEach(x => Console.WriteLine(x));
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("Liste dons triés par date");
                        Lister_Dons_refuse_date(asso);
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Liste dons en traitement triés par ID");
                        Lister_Dons_en_traitement(asso, (a, b) => a.Id.CompareTo(b.Id));
                        Console.ReadKey();
                        Console.WriteLine("\nListe dons en traitement triés par Nom");
                        Lister_Dons_en_traitement(asso, (a, b) => a.Nom_Donateur.CompareTo(b.Nom_Donateur));
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("Liste dons vendus triés par mois");
                        Lister_Dons_vendus(asso, (a, b) => a.Date.CompareTo(b.Date));
                        Console.ReadKey();
                        Console.WriteLine("\nListe dons vendus triés par numéro de bénéficiaires");
                        Lister_Dons_vendus(asso, (a, b) => a.Id.CompareTo(b.Id));
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("Liste dons stockés par entrepots, par catégorie/description");
                        Lister_Dons_par_entrepots(asso, (a, b) => a.Description.CompareTo(b.Description));
                        break;
                    
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Lister les dons volumineux stockés par entrepôt et par volume");
                        Lister_Dons_Volumineux_par_entrepots(asso, (a, b) => a.Volume.CompareTo(b.Volume));
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Liste dons par dépôt-vente et par prix");
                        Lister_Dons_par_depot_vente(asso, (a, b) => a.Prix.CompareTo(b.Prix));
                        break;


                    case 0:
                        Console.Clear();
                        fin = true;
                        break;

                    default:
                        Console.WriteLine("\nchoix non valide => faites un autre choix....");
                        break;
                }
            } while (!fin);
        }
        static void Module_Stats(Association asso)
        {
            bool fin = false;
            do
            {
                fin = false;
                Console.WriteLine();
                Console.WriteLine("1 : Moyenne de temps avant transfert");
                Console.WriteLine("2 : Moyenne des prix dans les depots-ventes");
                Console.WriteLine("3 : Moyenne age des bénéficiaires");

                Console.WriteLine("\n0 : Revenir au menu général");
                int lecture = demanderInt("\nChoisissez votre programme", 0, 3);

                switch (lecture)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("La moyenne de temps de vente entre la validation des dons et leur transfert est de : " + asso.AvgTemps_Avant_Transfert.Days + " jours");
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("La moyenne des prix des objets stockés en depot_ventes est de " + asso.AvgPrix_Garde_Meubles + " euros");
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("La moyenne d'âge des bénéficiaires est : "+asso.AvgAge_Beneficiaires.Days/365 + "ans.");
                        break;
                    case 0:
                        Console.Clear();
                        fin = true;
                        break;

                    default:
                        Console.WriteLine("\nchoix non valide => faites un autre choix....");
                        break;
                }
            } while (!fin);
        }
        static void Module_Autre(Association asso)
        {
            bool fin = false;
            do
            {
                fin = false;
                Console.WriteLine();
                Console.WriteLine("1 : Initialiser les objets");
                Console.WriteLine("2 : A venir");
                Console.WriteLine("3 : A venir");

                Console.WriteLine("\n0 : Revenir au menu général");
                int lecture = demanderInt("\nChoisissez votre programme", 0, 1);

                switch (lecture)
                {
                    case 1:
                        Console.Clear();
                        //Vérification si la liste des utilisateurs est déjà remplie
                        if (asso.Liste_adherent.Count < 1 || asso.Liste_beneficiaire.Count < 1)
                        {
                            List<Personne_adherente> liste_adherent = lecture_personnes_adherente("..\\..\\data\\Adherents.txt");
                            List<Personne_beneficiaire> liste_beneficiaire = lecture_personnes_beneficiaire("..\\..\\data\\Beneficiaires.txt");
                            asso.Set_Utilisateurs(liste_adherent, liste_beneficiaire);
                            Console.WriteLine("Fichiers lus!");
                        }
                        asso.Init();
                        break;
                    case 2:
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("La moyenne d'âge des bénéficiaires est : " + asso.AvgAge_Beneficiaires.Days / 365);
                        break;
                    case 0:
                        Console.Clear();
                        fin = true;
                        break;

                    default:
                        Console.WriteLine("\nchoix non valide => faites un autre choix....");
                        break;
                }
            } while (!fin);
        }
        #endregion

        static void Main(string[] args)
        {
            Association asso = new Association();
           
            bool fin = false;

            do
            {
                fin = false;
                Console.WriteLine();
                Console.WriteLine("1 : Module Personne");
                Console.WriteLine("2 : Module Don");
                Console.WriteLine("3 : Module Tri");
                Console.WriteLine("4 : Module Stats");
                Console.WriteLine("5 : Module Autre");

                Console.WriteLine("\n0 : Fin du programme");

                int lecture = demanderInt("\nChoisissez votre programme", 0, 5);

                switch (lecture)
                {
                    case 1:
                        Console.Clear();
                        Module_Personne(asso);
                        break;
                    case 2:
                        Console.Clear();
                        Module_Don(asso);
                        break;
                    case 3:
                        Console.Clear();
                        Module_Tri(asso);
                        break;
                    case 4:
                        Console.Clear();
                        Module_Stats(asso);
                        break;
                    case 5:
                        Console.Clear();
                        Module_Autre(asso);
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("fin de programme...");
                        Console.ReadKey();
                        fin = true;
                        break;
                    
                    default:
                        Console.WriteLine("\nchoix non valide => faites un autre choix....");
                        break;
                }
            } while (!fin);

        }
    }
}
