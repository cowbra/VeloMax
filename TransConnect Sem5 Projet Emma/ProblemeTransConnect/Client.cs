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
	/// Classe créant nos objets de type Client avec un envoi vers la BDD
	/// </summary>
    public class Client
    {
        #region Attributs
        protected string secu;
        protected string prenom;
        protected string nom;
        protected int dateNaissance;
        protected string adresse;
        protected string mail;
        protected string tel;
        protected double montantTotalAchats;

        private bool connected;
        private MySqlConnection mySqlConnection;
        #endregion

        public Client(string secu, string prenom, string nom, int dateNaissance, string adresse, string mail, string tel, double montantTotalAchats=0)
        {
            this.secu = secu;
            this.prenom = prenom;
            this.nom = nom;
            this.dateNaissance = dateNaissance;
            this.adresse = adresse;
            this.mail = mail;
            this.tel = tel;

            this.montantTotalAchats = montantTotalAchats;
        }


        public string Secu
        {
            get { return secu; }
        }

        public string Prenom
        {
            get { return prenom; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public int DateNaissance
        {
            get { return dateNaissance; }
        }

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        public double MontantTotalAchats
        {
            get { return montantTotalAchats;}
            set { montantTotalAchats = value;}
        }
        /// <summary>
        /// Méthode qui ajoute l'instance en cours à la base de données
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
            {// On ajoute dans la table Clients les attributs de l'instance en cours
                MySqlCommand requete = new MySqlCommand("INSERT INTO Clients(num_secu,nom,prenom,adresse,date_naissance,email,num_tel,montant_total) VALUES(@num_secu,@nom,@prenom,@adresse,@date_naissance,@email,@num_tel,@montant_total)", mySqlConnection);
                requete.Parameters.AddWithValue("@num_secu", this.secu);
                requete.Parameters.AddWithValue("@nom", this.nom);
                requete.Parameters.AddWithValue("@prenom", this.prenom);
                requete.Parameters.AddWithValue("@adresse", this.adresse);
                requete.Parameters.AddWithValue("@date_naissance", this.dateNaissance);
                requete.Parameters.AddWithValue("@email", this.mail);
                requete.Parameters.AddWithValue("@num_tel", this.tel);
                requete.Parameters.AddWithValue("@montant_total", this.montantTotalAchats);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                mySqlConnection.Close();
                return true;

            }
            return false;
        }
        /// <summary>
        /// Méthode ajoutant le montant <paramref name="prix" /> de la commande effectuée par le client à son  montant total
        /// </summary>
        /// <param name=prix> est le prix de la commande en fonction du type de véhicule, la distance,....</param>
        public void AddMoneyToClient(double prix)
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
            {
                MySqlCommand requete = new MySqlCommand("UPDATE Clients SET montant_total=@montant_total WHERE num_secu=@num_secu", mySqlConnection);
                requete.Parameters.AddWithValue("@num_secu", this.secu);
                requete.Parameters.AddWithValue("@montant_total", prix);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();
            }
        }
    }
}
