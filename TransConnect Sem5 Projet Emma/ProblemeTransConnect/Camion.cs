using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemeTransConnect
{
	/// <summary>
	/// Classe mère des différents types de Poids-lourds, mais classe fille de la classe Voiture
	/// </summary>
	public abstract class Camion : Vehicule
	{
		#region Attributs
		protected string volume;
		#endregion

		#region Constructeur
		public Camion(string volume )
		{
			this.volume = volume;
		}
		#endregion

		#region Parametres_Attributs
		public abstract string Volume
		{
			get;
		}
		#endregion
	}
}
