using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//fini

namespace ProblemeTransConnect
{
    /// <summary>
	/// Classe servant à calculer le chemin le plus court pour Dijkstra
	/// </summary>
    class Lien
    {
        Noeud noeud;
        int km;
        int tempsMin;

        public Lien(int km,int tempsMin, Noeud noeud)
        {
            this.noeud = noeud;
            this.km = km;
            this.tempsMin = tempsMin;
        }

        public int Km
        {
            get { return km; }
            set { this.km = value; }
        }
        public int TempsMin
        {
            get { return tempsMin; }
            set { this.tempsMin = value; }
        }
        public Noeud GetNoeud()
        {
            return this.noeud;
        }
        public override string ToString()
        {
            return "Km: " + km + ", Noeud :" + noeud.Ville;
        }
    }
}
