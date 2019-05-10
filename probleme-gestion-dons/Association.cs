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

        public Association(List<Personne_adherente> liste_adherent, List<Personne_beneficiaire> liste_beneficiaire)
        {
            this.liste_adherent = liste_adherent;
            this.liste_beneficiaire = liste_beneficiaire;
        }

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
    }
}
