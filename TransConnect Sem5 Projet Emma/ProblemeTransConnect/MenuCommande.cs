using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProblemeTransConnect
{
    /// <summary>
    /// Méthode gérant l'affichage des commandes, de leur modification et l'ajout d'une nouvelle commande
    /// </summary>
    public partial class MenuCommande : Form
    {
        MySqlConnection mySqlConnection;
        bool connected = false;


        public MenuCommande()
        {
            InitializeComponent();
            EtablirConnection();
            Actualiser();
        }

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
        /// Méthode permettant de mettre à jour la liste des commandes actives
        /// </summary>
        private void Actualiser(string requeteSQL = "SELECT * FROM Commandes")
        {
            if (connected)
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, mySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string ID = Lire["id"].ToString();
                        string VilleD = Lire["ville_depart"].ToString();
                        string VilleA = Lire["ville_arrivee"].ToString();
                        string DateC = Lire["date_commande"].ToString();
                        string Volume = Lire["volume"].ToString();
                        string Marchandise = Lire["marchandise"].ToString();
                        string Chauffeur = Lire["chauffeur"].ToString();
                        string Vehicule = Lire["vehicule"].ToString();

                        string Client = Lire["client"].ToString();
                        string Parcours = Lire["parcours"].ToString();
                        string Km = Lire["kilometres"].ToString();
                        string tempsTra = Lire["temps_trajet"].ToString();
                        string Prix = Lire["prix"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { ID, VilleD, VilleA, DateC, Volume,  Marchandise, Chauffeur, Vehicule,Client, Parcours,Km,tempsTra,Prix}));
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
            NewCommande newc = new NewCommande();
            //mySqlConnection.Close();
            newc.ShowDialog();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Méthode permettant d'afficher les commandes entre 2 dates
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            int compteur = 0;
            string requete = "SELECT * FROM Commandes ";
            if(textBox1.Text != "")
            {
                if (textBox2.Text == "") MessageBox.Show("Entrez une date de fin !");
                else
                {
                    compteur++;
                    string[] dateDe = textBox1.Text.Split('/');
                    string dateDebut = dateDe[2] + dateDe[1] + dateDe[0];

                    string[] dateF = textBox2.Text.Split('/');
                    string dateFin = dateF[2] + dateF[1] + dateF[0];


                    requete += "WHERE date_commande BETWEEN " + dateDebut + " AND "+ dateFin;
                }
            }
            if(textBox3.Text !="")
            {
                if (compteur > 0)
                {
                    requete += " AND client='" + textBox3.Text + "';";
                }
                else requete += "WHERE client='" + textBox3.Text + "';";
                compteur++;
            }
            if (compteur !=0) Actualiser(requete);
            else Actualiser();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MenuClient_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Méthode permettant de supprimer une commande de la BDD
        /// </summary>
        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {//Suppression client
            if (connected)
            {
                if(listView1.SelectedItems.Count > 0)
                {
                    ListViewItem element = listView1.SelectedItems[0];
                    string id = element.SubItems[0].Text;
                    MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM Commandes WHERE id =@ID", mySqlConnection);
                    mySqlCommand.Parameters.AddWithValue("@ID", id);
                    mySqlCommand.ExecuteNonQuery();
                    element.Remove();
                    mySqlCommand.Parameters.Clear();
                }
            }
        }
        /// <summary>
        /// Méthode permettant de mettre à jour une commande existante dans la BDD
        /// </summary>
        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string ID = element.SubItems[0].Text;
                string Date = element.SubItems[3].Text;

                using (ModifierCommande modifier = new ModifierCommande())
                {
                    //Elements modifiables
                    modifier.Date = Date;

                    //Elements non modifiables
                    modifier.NCommande = ID;

                    if (modifier.ShowDialog() == DialogResult.Yes)
                    {//on met à jour les attributs de la commande sélectionnée dans la BDD
                            MySqlCommand requete = new MySqlCommand("UPDATE Commandes SET date_commande=@date_commande WHERE id=@id", mySqlConnection);
                            
                            requete.Parameters.AddWithValue("@id", ID.ToLower());
                            string[] subsDate = modifier.Date.Split('/');
                            string dateTrans = subsDate[2] + subsDate[1] + subsDate[0];
                            int dateC = Convert.ToInt32(dateTrans);
                            requete.Parameters.AddWithValue("@date_commande", dateC);
                            requete.ExecuteNonQuery(); 
                            element.SubItems[3].Text = modifier.Date;
                            requete.Parameters.Clear();
                            MessageBox.Show("Commande mise à jour.");
                    }
                }

            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        /// <summary>
        /// Fonction affichant le prix moyen des commandes 
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            double PrixMoyen = 0;
            int compteur = 0;
            foreach (ListViewItem element in listView1.Items)
            { 
                PrixMoyen += Convert.ToDouble(element.SubItems[12].Text);
                compteur++;
            }
            if (compteur == 0) MessageBox.Show("Aucune commande effectuée, pas de revenus");
            else
            {
                string affichage = "Prix Moyen des commandes : " + Convert.ToString(PrixMoyen/ compteur);
                MessageBox.Show(affichage);
            }

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
