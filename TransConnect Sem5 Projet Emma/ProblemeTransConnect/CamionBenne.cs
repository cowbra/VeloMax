using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemeTransConnect
{
	/// <summary>
	/// Classe permettant de construire des objets de type camion benne
	/// </summary>
	internal class CamionBenne : Camion
	{
		#region Constructeur
		public CamionBenne(string volume ) : base(volume)
		{
			this.type = "Camion Benne";
		}
		#endregion

		#region Parametres_Attributs
		public override string Volume
		{
			get { return volume; }
		}

		public override string Type
		{
			get { return this.type; }
			set { this.type = value; }
		}

		#endregion

	}
}
