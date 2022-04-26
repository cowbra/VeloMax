/*
 * Afin de réaliser ce probleme nous avons stocké nos données sur une base de données MySql
 * Veuillez s'il-vous-plaît lire notre rapport en 1er lieu afin de pouvoir faire tourner notre solution :
 * - La dll Mysql.Data.dll doit être ajoutée en tant que référence projet (Projet -> ajouter une référence-> sélectionner la dll), ET
 * - puis cliquez sur le nom de la solution dans l'explorateur de solutions -> Gérer les packages NuGet -> Parcourir,
 * - et installez les packages suivants pour que Mysql fonctionne : Renci.SshNet.Async et SSH.NET
 * 
 * 
 * Projet réalisé sous Visual Studio Community 2022, avec l'option 'Développement .NET Desktop'
 * Un build de la solution finale est présent dans notre dossier rendu
 * 
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProblemeTransConnect
{
    public partial class Menu : Form
    {
        MySqlConnection mySqlConnection;
        bool connected = false;
        public Menu()
        {
            InitializeComponent();
            UpdateBddChauffeur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MenuClient form2 = new MenuClient();
            this.Hide();
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuCommande form = new MenuCommande();
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MenuSalarie form = new MenuSalarie();
            this.Hide();
            form.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Ajouter une distance et un temps entre nos villes
            NewVille form = new NewVille();
            //this.Hide();
            form.ShowDialog();
        }

        /// <summary>
        /// Methode qui sera utilisée tout au long du projet pour initier la connexion à la base de données
        /// </summary>
        private void EtablirConnection()
        {
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
        }
        /// <summary>
        /// Methode qui va vérifier si un chauffeur a une livraison de prévue aujourd'hui. Si oui sa disponibilité est changée
        /// --> 'livraison_encours' = 1 signifie un chauffeur occupé et '0' un chauffeur libre 
        /// Les chauffeurs à l'état 'livraison_encours=1' mais avec aucune livraison de prévue ou aujourd'hui seront switchés à 'livraison_encours=0'
        /// </summary>
        private void UpdateBddChauffeur()
        {
            #region Recuperation date du jour au format Int AAAAmmJJ
            DateTime aDateTime = DateTime.Now;
            string mois = Convert.ToString(aDateTime.Month);
            if (mois.Length == 1) mois = "0" + mois;
            string jour = Convert.ToString(aDateTime.Day);
            if (jour.Length == 1) jour = "0" + jour;
            string dateToday = Convert.ToString(aDateTime.Year) + mois + jour;
            #endregion

            List<string> chauffeurEnLivraisonAjd = new List<string>();
            List<int> chauffeurEnLivraisonAjdNb = new List<int>();
            List<string> chauffeurLibreAjd = new List<string>();
            #region Ajout des chauffeurs en livraison ajd dans la liste des livraisons
            if (!connected) EtablirConnection();
            else
            {
                //Lecture Table commandes pour recuperer la liste des chauffeurs en livraison
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM Commandes", mySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string dateCommande = Lire["date_commande"].ToString();
                        string Chauffeur = Lire["chauffeur"].ToString();
                        string ChauffeurNb = Lire["nb_livraisons"].ToString();
                        if (dateCommande == dateToday)
                        {
                            chauffeurEnLivraisonAjd.Add(Chauffeur);
                            chauffeurEnLivraisonAjdNb.Add(Convert.ToInt32(ChauffeurNb));
                        }
                        else chauffeurLibreAjd.Add(Chauffeur);
                    }
                }
                //On Switch la disponibilité de nos livreurs pour les rendre indisponibles
                for(int i =0;  i< chauffeurEnLivraisonAjd.Count; i++)
                {
                    MySqlCommand requete = new MySqlCommand("UPDATE Salaries SET livraison_encours=@livraison_encours,nb_livraisons=@nb_livraisons WHERE num_secu=@num_secu", mySqlConnection);

                    requete.Parameters.AddWithValue("@num_secu", chauffeurEnLivraisonAjd[i]);
                    requete.Parameters.AddWithValue("@livraison_encours", 1);
                    requete.Parameters.AddWithValue("@nb_livraisons", chauffeurEnLivraisonAjdNb[i]+1);
                    requete.ExecuteNonQuery();
                    requete.Parameters.Clear();
                }
                //On Switch les chauffeurs en mode disponibles s'ils n'ont aucune livraison à faire ajd
                for (int i = 0; i < chauffeurLibreAjd.Count; i++)
                {
                    MySqlCommand requete = new MySqlCommand("UPDATE Salaries SET livraison_encours=@livraison_encours WHERE num_secu=@num_secu", mySqlConnection);

                    requete.Parameters.AddWithValue("@num_secu", chauffeurLibreAjd[i]);
                    requete.Parameters.AddWithValue("@livraison_encours", 0);
                    requete.ExecuteNonQuery();
                    requete.Parameters.Clear();
                }
            }
            #endregion

        }
    }
}
