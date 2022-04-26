using System;
using System.Collections.Generic;
using System.Linq;

//fini

namespace ProblemeTransConnect
{
    /// <summary>
	/// Classe servant à calculer le chemin le plus court pour Dijkstra
	/// </summary>
    class Graphe
    {
        List<Noeud> listNoeuds = new List<Noeud>();
        Noeud sommet;

        public Graphe(Noeud sommet, List<Noeud> listNoeuds)
        {
            this.listNoeuds = listNoeuds;
            this.sommet = sommet;
        }

        public override string ToString()
        {
            string s = "Le sommet du graphe est " + this.sommet.Ville + "\n";
            s += "les noeuds du graphe avec leurs adjacents sont: " + "\n";
            foreach(Noeud n in listNoeuds)
            {
                s += n + "\n";
            }

            return s;
        }


        public void CalculDetails(Etape etape)
        {
            foreach (Lien i in etape.Arrive.getAdjacents())
        {
                i.GetNoeud().AjouterArrivant(etape.Km + i.Km,etape.TempsMin + i.TempsMin,etape.Arrive);
            }
        }

        public Etape ChercherPrecedent(Noeud noeud, List<Etape> list)
        {
            Etape result = list[0];
            foreach (Etape e in list)
            {
                if (e.Arrive == noeud) result = e;
            }
            return result;
        }

        /// <summary>
        /// Méthode  Dijkstra en elle même
        /// </summary>
        public string Dijkstra(Noeud destination)
        {
            List<Etape> etapeCalcul = new List<Etape>();
            Etape etape = new Etape(0,0, sommet, sommet);
            etapeCalcul.Add(etape);
            Noeud noeudCourant = sommet;
            List<Etape> cheminsPossibles;
            Lien lePlusCourtChemin;
            Etape optimum;
            cheminsPossibles = new List<Etape>();
            while (noeudCourant != destination)
            {
                Etape last = etapeCalcul.Last();
                CalculDetails(last);
                foreach (Noeud n in listNoeuds)
                {
                    if (!n.Traite)
                    {
                        if (n.Arrivants.Count > 0)
                        {
                            lePlusCourtChemin = n.minimumSurArrivant();
                            cheminsPossibles.Add(new Etape(lePlusCourtChemin.Km, lePlusCourtChemin.TempsMin, lePlusCourtChemin.GetNoeud(), n));
                        }
                    }
                }
                optimum = cheminsPossibles[0];
                foreach (Etape s in cheminsPossibles)
                {
                    if (s.Km <= optimum.Km)
                        optimum = s;
                }
                etapeCalcul.Add(optimum);
                noeudCourant.Traite = true;
                noeudCourant = optimum.Arrive;
                List<Etape> nouveauxCheminsPossibles = new List<Etape>();
                foreach(Etape s in cheminsPossibles)
                {
                    if (s.Arrive != noeudCourant)
                        nouveauxCheminsPossibles.Add(s);
                }
                cheminsPossibles = nouveauxCheminsPossibles;

            }
            List<Etape> solution = new List<Etape>();
            Etape final = etapeCalcul.Last();
            solution.Add(final);
            etape = final;
            Noeud noeud;
            while(etape.Precedent != sommet)
            {
                noeud = etape.Precedent;
                etape = ChercherPrecedent(noeud, etapeCalcul);
                solution.Add(etape);
            }

            string solu = sommet.Ville;
            for (int i = solution.Count-1; i >= 0; i--)
            {
                solu+="->"+solution[i].Arrive.Ville;
            }
            return (solu+"/"+Convert.ToString(solution[0].Km) + "/" + Convert.ToString(solution[0].TempsMin));
        }
    }
}
