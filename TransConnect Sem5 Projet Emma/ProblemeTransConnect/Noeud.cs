using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemeTransConnect
{
	class Noeud
	{
		/// <summary>
		/// Classe nous permettant de réaliser Dijkstra en créant des liens entre les différentes villes
		/// </summary>
		string ville;
		List<Lien> adjacents;
		bool traite = false;
		List<Lien> arrivants = new List<Lien>();

		public Noeud(string ville)
        {
			this.ville = ville;
			adjacents = new List<Lien>();
        }

		public string Ville
        {
			get { return this.ville; }
			set { this.ville = value; }
        }

		public List<Lien> Arrivants
        {
			get { return this.arrivants; }
			set { this.arrivants = value; }
        }

		public bool Traite
        {
			get { return traite; }
			set { this.traite = value; }
        }

		public List<Lien> getAdjacents()
        {
			return this.adjacents;
        }

		public Noeud(string ville, List<Lien> adjacents): this(ville)
        {
			this.adjacents = adjacents;
        }


		public void AjouterLien(int km,int tempsMin, Noeud n)
        {
			Lien lien = new Lien(km, tempsMin, n);
			this.adjacents.Add(lien);
        }

		public void AjouterArrivant(int km,int tempsMin, Noeud n)
		{
			Lien lien = new Lien(km, tempsMin, n);
			this.arrivants.Add(lien);
		}

		public Lien minimumSurArrivant()
		{
			Lien result = this.arrivants[0];
			foreach(Lien i in this.arrivants)
			{
				if (i.Km < result.Km)
					result = i;
            }

			return result;
        }

        public override string ToString()
        {
			String s = "Noeud : " + this.ville;
			s += "\nles noeuds adjacents sont :";
			foreach (Lien l in this.adjacents)
			{
				s += l.GetNoeud().Ville + ", ";
			}
			s += "\n";
			return s;
		
		}
    }

}
