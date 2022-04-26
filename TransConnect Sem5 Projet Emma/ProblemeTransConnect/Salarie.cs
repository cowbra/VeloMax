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
    public class Salarie
    {
		/// <summary>
		/// Classe nous permettant de créer des objets de type Salarié et de les ajouter à la BDD
		/// </summary>
		private bool connected;
		private MySqlConnection mySqlConnection;


		protected string numSecu;
		protected string nom;
		protected string prenom;
		protected int datenaissance;
		protected string adresse;
		protected string mail;
		protected string tel;
		protected int dateEmbauche;
		protected string poste;
		protected double salaire;

		protected string superieurHierarchique;

        #region Constructeur
        public Salarie(string numSecu, string nom,string prenom,int datenaissance,string adresse,string mail,string tel,int dateEmbauche,string poste,double salaire, string superieurHierarchique)
		{
			this.numSecu = numSecu;
			this.nom = nom;
			this.prenom = prenom;
			this.datenaissance = datenaissance;
			this.adresse = adresse;
			this.mail = mail;
			this.tel = tel;
			this.dateEmbauche = dateEmbauche;
			this.poste = poste;
			this.salaire = salaire;

			this.superieurHierarchique = superieurHierarchique;
		}
		#endregion
		#region Parametres attributs
		public string SuperieurHierarchique
		{
			get { return this.superieurHierarchique; }
			set { this.superieurHierarchique = value; }
		}
		public string NumSecu
		{
			get { return this.numSecu; }
		}

		public string Nom
		{
			get { return this.nom; }
			set { this.nom = value; }
		}

		public string Prenom
		{
			get { return this.prenom; }
		}

		public int Datenaissance
		{
			get { return this.datenaissance; }
		}

		public string Adresse
		{
			get { return this.adresse; }
			set { this.adresse = value; }
		}

		public string Mail
		{
			get { return this.mail; }
			set { this.mail = value; }
		}

		public string Tel
		{
			get { return this.tel; }
		}

		public int DateEmbauche
		{
			get { return this.dateEmbauche; }
		}

		public string Poste
		{
			get { return this.poste; }
			set { this.poste = value; }
		}

		public double Salaire
		{
			get { return this.salaire; }
			set { this.salaire = value; }
		}
        #endregion

        #region Methodes
        public virtual bool AddToDataBase()
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
			{ // on ajoute les attributs du l'objet client instancié à la bdd
				MySqlCommand requete = new MySqlCommand("INSERT INTO Salaries(num_secu,nom,prenom,adresse,date_naissance,email,num_tel,date_embauche,poste,salaire,superieur_hierarchique) VALUES(@num_secu,@nom,@prenom,@adresse,@date_naissance,@email,@num_tel,@date_embauche,@poste,@salaire,@suphiera)", mySqlConnection);
				requete.Parameters.AddWithValue("@num_secu", this.NumSecu);
				requete.Parameters.AddWithValue("@nom", this.nom);
				requete.Parameters.AddWithValue("@prenom", this.prenom);
				requete.Parameters.AddWithValue("@adresse", this.adresse);
				requete.Parameters.AddWithValue("@date_naissance", this.datenaissance);
				requete.Parameters.AddWithValue("@email", this.mail);
				requete.Parameters.AddWithValue("@num_tel", this.tel);

				requete.Parameters.AddWithValue("@date_embauche", this.dateEmbauche);
				requete.Parameters.AddWithValue("@poste", this.poste);
				requete.Parameters.AddWithValue("@salaire", this.salaire);
				requete.Parameters.AddWithValue("@suphiera", this.superieurHierarchique);

				requete.ExecuteNonQuery();
				requete.Parameters.Clear();

				mySqlConnection.Close();
				return true;

			}
			return false;
		}

		public virtual void AddCmdToDriver()
		{ }
		#endregion
	}
}
