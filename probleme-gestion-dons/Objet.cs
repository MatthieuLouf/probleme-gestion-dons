﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace probleme_gestion_dons
{
    class Objet
    {
        static int compteur = 0;
        int reference_objet;
        string type;
        string description_objet;
        double montant;
        Don don_groupe;
        List<string[]> informations_supplementaires;

        public string Description
        {
            get
            {
                return description_objet;
            }
        }
        public Objet(string type, string description_objet, double montant, List<string[]> info_sup)
        {
            this.reference_objet = compteur;
            compteur++;
            this.type = type;
            this.description_objet = description_objet;
            this.montant = montant;
            this.informations_supplementaires = info_sup;
        }

        public Objet(string type, string description_objet, double montant) : this(type, description_objet, montant, new List<string[]>()) {}

        public override string ToString()
        {
            string str = "Objet n°" + this.reference_objet + " : " + this.type + " (" + this.description_objet + ") pour le prix de " + this.montant + "euros\n        Informations supplémentaires :";
            this.informations_supplementaires.ForEach(x => str += "\n        " + x[0] + " : " + x[1]);
            return str;
        }

        public Don Don_groupe
        {
            set { this.don_groupe = value; }
        }
    }
}
