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
        //Fonctions demande valeurs
        static string demanderString(string demande="")
        {
            if(demande!="")
            {
                Console.Write(demande +" : ");
            }

            return Console.ReadLine();
        }
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
                type_objets_volumineux.ForEach(x => Console.WriteLine(x.ToString()));
                do
                {
                    type = demanderString("Entrez le type de l'objet");
                } while (!type_objets_volumineux.Contains(type));
                
            }
            else
            {
                type_objets_non_volumineux.ForEach(x => Console.WriteLine(x.ToString()));
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
            Console.WriteLine("Saisie des informations du Don : \n");

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
            int nb_objets = demanderInt("\nEntrez le nombre d'objets dans le don",1);


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
            return liste;
        }

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
            return liste;
        }

        public static void Creation_Don(Association asso)
        {
            asso.Liste_adherent.ForEach(x => Console.WriteLine(x));
            Don d = entrerDon(asso);
            asso.AjouterDonAttente(d);
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
                } while (ls[choix - 1].Volume_Restant() < ov.Volume);
            }
            else
            {
                choix = demanderInt("Dans quel lieu voulez vous le stocker", 1, ls.Count);
            }
            ls[choix - 1].Ajouter_Objet(o);
        }
      
        public static void Lister_Dons_vendus(Association assos, Comparison<Don> methode)
        {
            List<Don> liste = assos.Archive_association.Dons_archive;
            liste.Sort(methode);
            liste.ForEach(x => Console.WriteLine(x.ToString()));
        }

        public static void Lister_Dons_refuse_date(Association assos)
        {
            List<Don> liste = assos.Archive_association.Dons_refuses;
            liste.Sort((a, b) => a.Date.CompareTo(b.Date));
            liste.ForEach(x => Console.WriteLine(x.ToString()));
        }
        public static void Lister_Dons_en_traitement(Association assos, Comparison<Don> methode)
        {
            List<Don> liste = assos.Dons_attente.ToList<Don>();
            assos.Dons_donnateur.ToList<Don>().ForEach(x => liste.Add(x));
            liste.Sort(methode);
            liste.ForEach(x => Console.WriteLine(x.ToString()));
        }
        

        static void Main(string[] args)
        {
            List<Personne_adherente> liste_adherent = lecture_personnes_adherente("..\\..\\data\\Adherents.txt");
            List<Personne_beneficiaire> liste_beneficiaire = lecture_personnes_beneficiaire("..\\..\\data\\Beneficiaires.txt");

            Association asso = new Association(liste_adherent, liste_beneficiaire);
           
            bool fin = false;

            do
            {
                fin = false;
                Console.WriteLine();
                Console.WriteLine("1 : Ajouter un don au logiciel");
                Console.WriteLine("2 : Accepter ou refuser un don");
                Console.WriteLine("3 : Afficher dons");
                Console.WriteLine("4 : Liste dons refusés triés par date");
                Console.WriteLine("5 : Liste dons en traitement par Id et par Nom");
                Console.WriteLine("6 : Liste dons vendus par mois et par numéro de bénéficiaires");
                Console.WriteLine("10 : Fin du programme");

                int lecture = demanderInt("Choisissez votre programme", 1, 10);

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
                        Console.WriteLine("Dons en attente :");
                        //asso.Dons_attente.ForEach(x => Console.WriteLine(x));
                        Console.WriteLine("Dons valides :");
                        asso.Archive_association.Dons_archive.ForEach(x => Console.WriteLine(x));
                        break;
                    
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Liste dons triés par date");
                        Lister_Dons_refuse_date(asso);
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("Liste dons en traitement triés par ID");
                        Lister_Dons_en_traitement(asso, (a, b) => a.Id.CompareTo(b.Id));
                        Console.ReadKey();
                        Console.WriteLine("\nListe dons en traitement triés par Nom");
                        Lister_Dons_en_traitement(asso, (a, b) => a.Nom_Donateur.CompareTo(b.Nom_Donateur));
                        break;

                    case 6:
                        Console.Clear();
                        Console.WriteLine("Liste dons vendus triés par mois");
                        //Lister_Dons_en_traitement(asso, (a, b) => a.Date.CompareTo(b.Date));
                        Console.ReadKey();
                        Console.WriteLine("\nListe dons vendus triés par numéro de bénéficiaires");
                        //Lister_Dons_en_traitement(asso, (a, b) => a.Id.CompareTo(b.Id));
                        break;

                    case 10:
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
