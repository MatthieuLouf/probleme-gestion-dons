﻿using System;
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

        public Objet_volumineux(string type, string description_objet, int montant,double hauteur, double largeur, double longueur) : base(type, description_objet, montant)
        {
            this.hauteur = hauteur;
            this.largeur = largeur;
            this.longueur = longueur;
        }

        public override string ToString()
        {
            return base.ToString() + ", -> Lourd : hauteur : " + this.hauteur + " largeur : " + this.largeur + " longueur : " + this.longueur;
        }
    }
}
