using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProblemeTransConnect
{
	/// <summary>
	/// Classe gérant nos objets de type 'Commandes'
	/// </summary>
	public class Commande
    {
		//instance mysql
		private bool connected;
		private MySqlConnection mySqlConnection;
		#region Attributs
		protected string depart;
		protected string arrivee;
		protected int date;

		protected Salarie chauffeur;
		protected Client client;
		protected Vehicule vehicule;
		protected Livraison colis;

		protected string parcours;
		protected double km;
		protected int  tempsTrajet;

		protected double prix;
		#endregion

		#region Constructeur
		public Commande(string depart, string arrivee, int date, Client client, Salarie chauffeur,Vehicule vehicule,Livraison colis,string parcours,int km, int TempsTrajet)
		{
			this.depart = depart;
			this.arrivee = arrivee;
			this.prix = 0;
			this.date = date;

			this.client = client;
			this.chauffeur = chauffeur;
			this.vehicule = vehicule;
			this.colis = colis;
			
			this.parcours = parcours;
			this.km = km;
			this.tempsTrajet = TempsTrajet;

			this.prix = 0;			
		}
		#endregion
		#region Proprietees_Attributs
		public string Depart
		{
			get { return depart; }
		}

		public string Arrivee
		{
			get { return arrivee; }
		}

		public int Date
		{
			get { return date; }
		}

		public string Parcours
		{
			get { return parcours; }
			set { parcours = value; }
		}

		public double Km
		{
			get { return km; }
			set { km = value; }
		}

		public double Prix
		{
			get { return prix; }
			set { prix = value; }
		}
		public int TempsTrajet
		{
			get { return tempsTrajet; }
			set { tempsTrajet = value; }
		}

		public Salarie Chauffeur
		{
			get { return chauffeur; }
			set { chauffeur = value; }
		}

		public Client Client
		{
			get { return client; }
			set { client = value; }
		}

		public Livraison Colis
		{
			get { return colis; }
			set { colis = value; }
		}

		public Vehicule Vehicule
		{
			get { return vehicule; }
			set { vehicule = value; }
		}

        public int Property
        {
            get => default;
            set
            {
            }
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Méthode qui calcule le prix de la commande en fonction de la distance, du vehicule, du salaire du chauffeur
        /// et des réductions applicables
        /// </summary>
        public void calculPrix()
        {
			string dateEmbauche = "";
			string numS = "";
			double newSalaire = 0;
			int reduction_client = 30;

			//Prix de base = 50 centimes / kilomètre arrondi à l'entier supérieur
			this.prix = Math.Ceiling(0.5 *this.km);
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
			{
                
				#region Mise a jour salaire chauffeur Anciennete
				//On met à jour le salaire du chauffeur en fonction de son ancienneté
				string dateToday = "";
                MySqlCommand requete = new MySqlCommand("SELECT * FROM Salaries", mySqlConnection);
				using (MySqlDataReader Lire = requete.ExecuteReader())
				{
					while (Lire.Read())
					{
						numS = Lire["num_secu"].ToString();				
						if (numS == this.chauffeur.NumSecu)
						{
							dateEmbauche = Lire["date_embauche"].ToString();
							string Salaire = Lire["salaire"].ToString();
							DateTime aDateTime = DateTime.Now;
							string mois = Convert.ToString(aDateTime.Month);
							if (mois.Length == 1) mois = "0" + mois;
							string jour = Convert.ToString(aDateTime.Day);
							if (jour.Length == 1) jour = "0" + jour;
							dateToday = Convert.ToString(aDateTime.Year) + mois + jour;
		
							newSalaire =this.chauffeur.Salaire+0.0000001*Math.Abs(Convert.ToInt32(dateEmbauche)-Convert.ToInt32(dateToday));
							break;
						}
					}
				}		
				//On envoie à la base de données le nouveau salaire du chauffeur utilisé
				MySqlCommand sql = new MySqlCommand("UPDATE Salaries SET salaire=@salaire WHERE num_secu=@num_secu", mySqlConnection);
				sql.Parameters.AddWithValue("@num_secu", numS);
				sql.Parameters.AddWithValue("@salaire", newSalaire);
				sql.ExecuteNonQuery();
				sql.Parameters.Clear();
				#endregion
				#region Reduction clients pour fidélité
				/*On attribut des réductions à nos clients en fonction de leur montant total d'achat suivant le patern suivant :
				* 1er client : 30% de réduction
				* 2eme client : 25% de réduction
				* ...
				* 6eme client : 5% de réduction
				* 7eme client et plus : pas de réduction
				*/
				MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM Clients ORDER BY montant_total DESC", mySqlConnection);
				using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
				{
					while (Lire.Read())
					{
						numS = Lire["num_secu"].ToString();
						if (numS == this.client.Secu || reduction_client <= 0) break;
						reduction_client = reduction_client - 5;
					}
				}
				#endregion
			}
			#region Calcul Prix en fonction du vehicule
			//Un prix de base est attribuée en fonction du type de véhicule choisi
			if (this.vehicule.Type == "Camion Benne") this.prix += 150;
			else if (this.vehicule.Type == "Camion citerne") this.prix += 250;
			else if (this.vehicule.Type == "Camion frigorifique") this.prix += 400;
			else if (this.vehicule.Type == "Camionnette") this.prix += 100;
			else if (this.vehicule.Type == "Voiture") this.prix += 50;
			#endregion
			//On applique la remise du client
			this.prix -=this.prix*reduction_client/100;
			this.prix+=  this.tempsTrajet / 60 * this.chauffeur.Salaire;
		}
		/// <summary>
		/// Méthode ajoutant l'instance courante à la BDD
		/// </summary>
		public bool AddToDataBase()
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
			{//On ajoute, une fois la commande créée, les attributs dans la table Commandes de notre BDD
				MySqlCommand requete = new MySqlCommand("INSERT INTO Commandes(ville_depart,ville_arrivee,date_commande,volume,marchandise,chauffeur,vehicule,client,parcours,kilometres,temps_trajet,prix) VALUES(@ville_depart,@ville_arrivee,@date_commande,@volume,@marchandise,@chauffeur,@vehicule,@client,@parcours,@kilometres,@temps_trajet,@prix)", mySqlConnection);
				requete.Parameters.AddWithValue("@ville_depart", this.depart);
				requete.Parameters.AddWithValue("@ville_arrivee", this.arrivee);
				requete.Parameters.AddWithValue("@date_commande", this.date);
				requete.Parameters.AddWithValue("@volume", this.colis.Volume);
				requete.Parameters.AddWithValue("@marchandise", this.colis.Marchandise);
				requete.Parameters.AddWithValue("@chauffeur", this.chauffeur.NumSecu);
				requete.Parameters.AddWithValue("@vehicule", this.vehicule.Type);
				requete.Parameters.AddWithValue("@client", this.client.Secu);
				requete.Parameters.AddWithValue("@parcours", this.parcours);
				requete.Parameters.AddWithValue("@kilometres", this.km);
				requete.Parameters.AddWithValue("@temps_trajet", this.tempsTrajet);
				requete.Parameters.AddWithValue("@prix", this.prix);
				requete.ExecuteNonQuery();
				requete.Parameters.Clear();

				//On ajoute une livraison à notre chauffeur et on le rend inactif
				this.chauffeur.AddCmdToDriver();
				mySqlConnection.Close();
				return true;
			}
			return false;
		}
		#endregion

	}
}
