using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProblemeTransConnect
{
	/// <summary>
	/// Classe permettant de construire des objets de type Chauffeur
	/// </summary>
	public class Chauffeur : Salarie
    {
		private bool connected;
		private MySqlConnection mySqlConnection;


		protected bool livraisonEnCours;
        protected int nbLivraisons;
        public Chauffeur(string numSecu, string nom, string prenom, int datenaissance, string adresse, string mail, string tel, int dateEmbauche, string poste, double salaire, string superieurHierarchique, bool livraisonEnCours , int nbLivraisons) : base(numSecu, nom, prenom, datenaissance, adresse, mail, tel, dateEmbauche, poste, salaire, superieurHierarchique)
        {
            this.livraisonEnCours = livraisonEnCours;
            this.nbLivraisons = nbLivraisons;
        }

		public bool LivraisonEnCours
		{
			get { return this.livraisonEnCours; }
			set { this.livraisonEnCours = value; }
		}
		public int NbLivraisons
		{
			get { return this.nbLivraisons; }
			set { this.nbLivraisons = value; }
		}
		/// <summary>
		/// Méthode permettant l'ajout d'un objet 'Chauffeur' à notre base de données
		/// </summary>
		public override bool AddToDataBase()
		{
			// On établit la connexion MySql
			if (!connected)
			{
				mySqlConnection = new MySqlConnection("SERVER=93.10.83.45;PORT=3306;DATABASE=ProblemeCsharp;UID=problemecsharp;PWD=Hugoemma1320!;");
				try
				{
					if (mySqlConnection.State == ConnectionState.Closed) mySqlConnection.Open();
					connected = true;
				}
				catch (Exception ex) { MessageBox.Show(ex.Message); }
			}
			//Si pas d'erreur, Connexion effectuée

			if (connected)
			{// On ajoute dans la table Salaries nos attributs 
				MySqlCommand requete = new MySqlCommand("INSERT INTO Salaries(num_secu,nom,prenom,adresse,date_naissance,email,num_tel,date_embauche,poste,salaire,superieur_hierarchique,livraison_encours,nb_livraisons) VALUES(@num_secu,@nom,@prenom,@adresse,@date_naissance,@email,@num_tel,@date_embauche,@poste,@salaire,@suphiera,@livraisonEnCours,@livraisonQuot,@nbLivraisons)", mySqlConnection);
				requete.Parameters.AddWithValue("@num_secu", base.NumSecu);
				requete.Parameters.AddWithValue("@nom", base.Nom);
				requete.Parameters.AddWithValue("@prenom", base.Prenom);
				requete.Parameters.AddWithValue("@adresse", base.Adresse);
				requete.Parameters.AddWithValue("@date_naissance", base.Datenaissance);
				requete.Parameters.AddWithValue("@email", base.Mail);
				requete.Parameters.AddWithValue("@num_tel", base.Tel);
				requete.Parameters.AddWithValue("@date_embauche", base.DateEmbauche);
				requete.Parameters.AddWithValue("@poste", base.Poste);
				requete.Parameters.AddWithValue("@salaire", base.Salaire);
				requete.Parameters.AddWithValue("@suphiera", base.SuperieurHierarchique);

				requete.Parameters.AddWithValue("@livraisonEnCours", this.livraisonEnCours);
				requete.Parameters.AddWithValue("@nbLivraisons", this.nbLivraisons);

				requete.ExecuteNonQuery();
				requete.Parameters.Clear();

				mySqlConnection.Close();
				return true;

			}
			return false;
		}
	}
}
