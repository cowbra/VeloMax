using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemeTransConnect
{
	/// <summary>
	/// Classe permettant de construire des objets de type camionnette
	/// </summary>
	public class Camionnette : Vehicule
	{
		#region Attributs
		private string usage;
		#endregion

		#region Constructeur
		public Camionnette(string usage)
		{
			this.usage = usage;
			this.type = "Camionnette";
		}
		#endregion

		#region Parametres_Attributs
		public string Usage
		{
			get { return usage; }
		}
		#endregion
	}
}
