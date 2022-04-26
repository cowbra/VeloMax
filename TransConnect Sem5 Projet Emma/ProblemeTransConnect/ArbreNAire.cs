using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemeTransConnect
{
    class ArbreNAire
    {
        /// <summary>
        /// Classe permettant d'afficher l'organigramme
        /// </summary>
        NoeudOrganigramme racine;
        public string res = "";
        public ArbreNAire()
        {
            this.racine = new NoeudOrganigramme();
        }

        public ArbreNAire(NoeudOrganigramme n)
        {
            this.racine = n;
        }
        public NoeudOrganigramme Racine
        {
            set { this.racine = value; }
            get { return this.racine; }
        }
        public bool EstArbreVide()
        {
            return (this.Racine.FilsGauche == null && this.Racine.Frere == null);
        }
        #region affichage arborescence

        public string AfficheOffset(int offset)
        {
            string ofset = "";
            for (int i = 0; i < offset; i++)
            {
                ofset += "\t\t"; // 2 tabulations
            }
            return ofset;
        }
        public void AffichageArborescence(NoeudOrganigramme n, int offset)
        {
            if (n != null)
            {
                res += "\n";
                res += AfficheOffset(offset);

                if (offset != 0)
                {
                    res += "|-";
                }
                res += (n.Valeur);


                if (!n.EstFeuille() && n.Frere == null)
                {
                    res += AfficheOffset(offset + 1);
                }
                AffichageArborescence(n.Frere, offset + 1);

                if (!n.EstFeuille() && n.FilsGauche == null)
                {
                    res += "\n";
                    res += AfficheOffset(offset);
                }
                AffichageArborescence(n.FilsGauche, offset);
            }
        }
        public string resu(NoeudOrganigramme n, int offset)
        {
            AffichageArborescence(n, offset);
            return res;
        }
        #endregion
    }
}
