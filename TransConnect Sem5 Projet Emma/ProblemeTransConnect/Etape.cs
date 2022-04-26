using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProblemeTransConnect
{
	/// <summary>
	/// Classe servant à calculer le chemin le plus court pour Dijkstra
	/// </summary>
	class Etape
	{
		Noeud precedent;
		int km;
		int tempsMin;
		Noeud arrive;

		public Etape(int km,int tempsMin, Noeud precedent, Noeud arrive)
		{
			this.km = km;
			this.tempsMin = tempsMin;
			this.precedent = precedent;
			this.arrive = arrive;
		}

		public Noeud Precedent
		{
			get { return precedent; }
			set { this.precedent = value; }
		}

		public Noeud Arrive
		{
			get { return arrive; }
			set { this.arrive = value; }
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

		public override string ToString()
		{
			return "précédent " + this.precedent.Ville + " KM=" + this.km + " Arrive " + this.arrive.Ville;
		}
	}
}
