using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemeTransConnect
{
	/// <summary>
	/// Classe permettant de construire des objets de type camion frigorifique
	/// </summary>
	public class CamionFrigorifique : Camion
	{
		#region Constructeur
		public CamionFrigorifique(string volume ) : base(volume)
		{
			this.type = "Camion frigorifique";
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
