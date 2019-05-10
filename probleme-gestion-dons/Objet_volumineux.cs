using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Objet_volumineux : Objet
    {
        double hauteur;
        double largeur;
        double longueur;

        public Objet_volumineux(double hauteur, double largeur, double longueur, int reference_objet, string type, string description_objet, int montant) : base(reference_objet, type, description_objet,montant)
        {
            this.hauteur = hauteur;
            this.largeur = largeur;
            this.longueur = longueur;
        }
    }
}
