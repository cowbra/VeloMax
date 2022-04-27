using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace bdd
{
	public class ClientEntreprise : Client
	{
		/// <summary>
		/// Classe héritée de Client afin de créer un client de type Entreprise
		/// </summary>
		#region Attributs
		protected string nomCompagnie;
		protected double remiseCompagnie;
		#endregion

		#region Constructeur
		public ClientEntreprise(string tel, string email, string adresse, string nom, string nomCompagnie, double remiseCompagnie) : base(tel, email, adresse, nom)
		{
			this.nomCompagnie = nomCompagnie;
			this.remiseCompagnie = remiseCompagnie;
			this.type = "Entreprise";
		}
		#endregion
		#region Parametres_Attributs
		public override string Type
		{
			get { return this.type; }
		}
		#endregion

		public override bool AddToBdd()
		{
			BDD DATABASE = new BDD();
			DATABASE.Connect();
			if (DATABASE.Connected == true)
			{

				MySqlCommand requete = new MySqlCommand("INSERT INTO CLIENT(Type_Client,Tel_Client,Courriel_Client,Adresse_Client,Nom_Client,NomCompagnie_Client,RemiseCompagnie_Client) VALUES(@Type_Client,@Tel_Client,@Courriel_Client,@Adresse_Client,@Nom_Client,@NomCompagnie_Client,@RemiseCompagnie_Client)", DATABASE.MySqlConnection);
				requete.Parameters.AddWithValue("@Type_Client", this.type);
				requete.Parameters.AddWithValue("@Tel_Client", this.tel);
				requete.Parameters.AddWithValue("@Courriel_Client", this.email);
				requete.Parameters.AddWithValue("@Adresse_Client", this.adresse);
				requete.Parameters.AddWithValue("@Nom_Client", this.nom);
				requete.Parameters.AddWithValue("@NomCompagnie_Client", this.nomCompagnie);
				requete.Parameters.AddWithValue("@RemiseCompagnie_Client", this.remiseCompagnie);

				requete.ExecuteNonQuery();
				requete.Parameters.Clear();

				DATABASE.Disconnect();
				return true;
			}
			return false;
		}
	}
}
