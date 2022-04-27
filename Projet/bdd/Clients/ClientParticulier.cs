using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace bdd
{
    public class ClientParticulier : Client
    {
		/// <summary>
		/// Classe héritée de Client afin de créer un client de type Particulier
		/// </summary>
		#region Attributs
		protected string prenom;
		protected int numFidelio;
		protected DateTime dateDebFidelio;
		#endregion

		#region Constructeur
		// Client particulier sans programme fidelio
		public ClientParticulier(string tel, string email, string adresse, string nom, string prenom) : base(tel,email,adresse,nom)
		{
			this.prenom = prenom;
			this.type = "Particulier";
			this.numFidelio = 0; //on met à 0 si pas de programme fielio
		}

		// Client particulier avec programme fidelio
		public ClientParticulier(string tel, string email, string adresse, string nom, string prenom, int numFidelio, DateTime dateDebFidelio) : base(tel, email, adresse, nom)
		{
			this.prenom = prenom;
			this.numFidelio = numFidelio;
			this.type = "Particulier";
			this.dateDebFidelio = dateDebFidelio;
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
				MySqlCommand requete = new MySqlCommand();
				if (this.numFidelio == 0)
                {
					requete = new MySqlCommand("INSERT INTO CLIENT(Type_Client,Tel_Client,Courriel_Client,Adresse_Client,Nom_Client,Prenom_Client) VALUES(@Type_Client,@Tel_Client,@Courriel_Client,@Adresse_Client,@Nom_Client,@Prenom_Client,)", DATABASE.MySqlConnection);

				}
				else
                {
					requete = new MySqlCommand("INSERT INTO CLIENT(Type_Client,Tel_Client,Courriel_Client,Adresse_Client,Nom_Client,Prenom_Client,NumProgramme_Fidelio,DateDebut_Fidelio) VALUES(@Type_Client,@Tel_Client,@Courriel_Client,@Adresse_Client,@Nom_Client,@Prenom_Client,@NumProgramme_Fidelio,@DateDebut_Fidelio)", DATABASE.MySqlConnection);
					requete.Parameters.AddWithValue("@NumProgramme_Fidelio", this.numFidelio);
					requete.Parameters.AddWithValue("@DateDebut_Fidelio", this.dateDebFidelio.ToString("yyyy-MM-dd"));
				}
				requete.Parameters.AddWithValue("@Type_Client", this.type);
				requete.Parameters.AddWithValue("@Tel_Client", this.tel);
				requete.Parameters.AddWithValue("@Courriel_Client", this.email);
				requete.Parameters.AddWithValue("@Adresse_Client", this.adresse);
				requete.Parameters.AddWithValue("@Nom_Client", this.nom);
				requete.Parameters.AddWithValue("@Prenom_Client", this.prenom);
				

				requete.ExecuteNonQuery();
				requete.Parameters.Clear();

				DATABASE.Disconnect();
				return true;
			}
			return false;
		}
	}
}
