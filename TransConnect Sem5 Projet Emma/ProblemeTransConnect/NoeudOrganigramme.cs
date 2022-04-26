using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemeTransConnect
{
    class NoeudOrganigramme
    {
        /// <summary>
     /// Classe nous permettant de générer des Noeuds entre nos différents salariés pour l'organigramme
     /// </summary>
        string valeur;
        NoeudOrganigramme frere;
        NoeudOrganigramme filsGauche;

        public NoeudOrganigramme(){
            this.valeur = "";
            this.frere = null;
            this.filsGauche = null;
        }
        public NoeudOrganigramme(string v){
            this.valeur = v;
            this.frere = null;
            this.filsGauche = null;
        }

        public NoeudOrganigramme(string v, NoeudOrganigramme x, NoeudOrganigramme y){
            this.valeur = v;
            this.frere = x;
            this.filsGauche = y;
        }

        public string Valeur
        {
            get { return this.valeur; }
            set { this.valeur = value; }
        }

        public NoeudOrganigramme Frere{
            get { return this.frere; }
            set { this.frere = value; }
        }
        public NoeudOrganigramme FilsGauche{
            get { return this.filsGauche; }
            set { this.filsGauche = value; }
        }

        public bool AssocierNoeudFilsGauche(NoeudOrganigramme enfant){
            bool ok = false;
            if (this != null)
            {
                if (this.FilsGauche == null && enfant != null){
                    this.FilsGauche = enfant;
                    ok = true;
                }
            }
            return ok;
        }

        public bool AssocierNoeudFrere(NoeudOrganigramme enfant)
        {
            bool ok = false;
            if (this != null){
                if (this.Frere == null && enfant != null){
                    this.Frere = enfant;
                    ok = true;
                }
            }
            return ok;
        }

        public bool EstFeuille(){
            return (this != null && this.filsGauche == null);
        }

    }
}
