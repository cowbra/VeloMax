using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;


namespace ProblemeTransConnect
{
    /// <summary>
	/// Classe affichant nos clients à partir de la bdd et qui permet d'en créer de nouveau,
    /// ou de modifier les clienst existant
	/// </summary>
    public partial class MenuClient : Form
    {
        MySqlConnection mySqlConnection;
        bool connected = false;


        public MenuClient()
        {
            InitializeComponent();
            EtablirConnection();
            Actualiser();
        }
        /// <summary>
        /// Méthode qui établit la connexion à notre BDD
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

        private void Actualiser(string requeteSQL = "SELECT * FROM Clients")
        {
            /// <summary>
            /// Méthode qui nous permet d'actualiser les clients 
            /// </summary>
            if (connected)
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, mySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string NumeroSS = Lire["num_secu"].ToString();
                        string Nom = Lire["nom"].ToString();
                        string Prenom = Lire["prenom"].ToString();
                        string Adresse = Lire["adresse"].ToString();
                        string DateNaissance = Lire["date_naissance"].ToString();
                        string Email = Lire["email"].ToString();
                        string Tel = Lire["num_tel"].ToString();
                        string MontantTotal = Lire["montant_total"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { NumeroSS, Nom, Prenom, Adresse, DateNaissance,  Email, Tel, MontantTotal }));
                    }
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            mySqlConnection.Close();
            Menu menu = new Menu();
            this.Close();
            menu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewClient newc = new NewClient();
            //mySqlConnection.Close();
            newc.ShowDialog();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            int compteur = 0;
            string requete = "SELECT * FROM Clients ORDER BY ";
            foreach (int indexChecked in checkedListBox1.CheckedIndices)
            {
                // On vérifie si un choix de tri est coché pour nos clients
                if (indexChecked == 0)
                {
                    requete += "nom";
                    compteur++;
                }
                else if (indexChecked == 1)
                {
                    compteur++;
                    if (compteur == 2) requete += " AND adresse";
                    else requete += "adresse";
                }
                else if (indexChecked == 2)
                {
                    compteur++;
                    if (compteur >=2) requete += " AND montant_total DESC";
                    else requete += "montant_total DESC";
                }
            }
            if (compteur != 0)
            {
                //MessageBox.Show(requete);
                Actualiser(requete);
            }

            else Actualiser();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MenuClient_Load(object sender, EventArgs e)
        {

        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {//Suppression client
            if (connected)
            {
                if(listView1.SelectedItems.Count > 0)
                {
                    ListViewItem element = listView1.SelectedItems[0];
                    string numSecu = element.SubItems[0].Text;
                    MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM Clients WHERE num_secu =@numsecu", mySqlConnection);
                    mySqlCommand.Parameters.AddWithValue("@numsecu", numSecu);
                    mySqlCommand.ExecuteNonQuery();
                    element.Remove();
                    mySqlCommand.Parameters.Clear();
                }
            }
        }

        /// <summary>
        /// Méthode permettant de mettre à jour les données d'un client dans la BDD
        /// </summary>
        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string Nom = element.SubItems[1].Text;
                string Adresse = element.SubItems[3].Text;
                string Mail = element.SubItems[5].Text;
                string Tel = element.SubItems[6].Text;
                string Montant = element.SubItems[7].Text;

                string Secu = element.SubItems[0].Text;
                string Prenom = element.SubItems[2].Text;
                string Date = element.SubItems[4].Text;

                //On check les textes des textbox du fichier cs 'Modifier'
                using (Modifier modifier = new Modifier())
                {
                    //Elements modifiables
                    modifier.Nom = Nom;
                    modifier.Adresse = Adresse;
                    modifier.Email = Mail;
                    modifier.Telephone = Tel;
                    modifier.Montant = Montant;

                    //Elements non modifiables
                    modifier.Secu = Secu;
                    modifier.Prenom = Prenom;
                    modifier.DateNaiss = Date;

                    if (modifier.ShowDialog() == DialogResult.Yes)
                    {//on met à jour le client sélectionné dans la bdd avec nos nouvelles valeurs
                            MySqlCommand requete = new MySqlCommand("UPDATE Clients SET nom=@nom, adresse=@adresse, email=@email, num_tel=@num_tel, montant_total=@montant_total WHERE num_secu=@num_secu", mySqlConnection);
                            requete.Parameters.AddWithValue("@num_secu", Secu.ToLower());
                            requete.Parameters.AddWithValue("@nom", modifier.Nom.ToLower());
                            requete.Parameters.AddWithValue("@adresse", modifier.Adresse.ToLower());
                            requete.Parameters.AddWithValue("@email", modifier.Email.ToLower());
                            requete.Parameters.AddWithValue("@num_tel", modifier.Telephone.ToLower());
                            requete.Parameters.AddWithValue("@montant_total", modifier.Montant.ToLower());

                            requete.ExecuteNonQuery();
                            requete.Parameters.Clear();

                            element.SubItems[1].Text = modifier.Nom;
                            element.SubItems[3].Text = modifier.Adresse;
                            element.SubItems[5].Text = modifier.Email;
                            element.SubItems[6].Text = modifier.Telephone;
                            element.SubItems[7].Text = modifier.Montant;
                            MessageBox.Show("Client mis à jour.");
                    }
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        /// <summary>
        /// Fonction nous permettant d'afiicher la moyenne du montant total de tous les clients 
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            double montantTotal = 0;
            int compteur = 0;
            foreach (ListViewItem element in listView1.Items)
            {
                montantTotal += Convert.ToDouble(element.SubItems[7].Text);
                compteur++;
            }
            if (compteur == 0) MessageBox.Show("IL n'existe pas de clients à cet instant, veuillez en créer un en premier lieu");
            else
            {
                string affichage = "Montant total moyen des clients : " + Convert.ToString(montantTotal / compteur);
                MessageBox.Show(affichage);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
