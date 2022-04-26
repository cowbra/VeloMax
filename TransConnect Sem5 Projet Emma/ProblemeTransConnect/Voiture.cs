using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemeTransConnect
{
	public class Voiture : Vehicule
	{
		/// <summary>
		/// Classe héritée de véhicule afin de créer une voiture
		/// </summary>
		#region Attributs
		protected int nbPassagers;
		#endregion

		#region Constructeur
		public Voiture(int nbPassagers)
		{
			this.nbPassagers = nbPassagers;
			this.type = "Voiture";
		}
		#endregion

		#region Parametres_Attributs
		public int NbPassagers
		{
			get { return nbPassagers; }
		}
		public override string Type
		{
			get { return this.type; }
			set { this.type = value; }
		}
		#endregion
	}
}
