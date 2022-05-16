using MySql.Data.MySqlClient;

namespace bdd
{
    public partial class MenuClient : Form
    {
        BDD DATABASE = new BDD();
        public MenuClient()
        {
            InitializeComponent();
            DATABASE.Connect();
            Actualiser();
        }

        private void MenuClient_Load(object sender, EventArgs e)
        {

        }

        private void Actualiser()
        {
            /// <summary>
            /// Méthode qui nous permet d'actualiser les fournisseurs 
            /// </summary>
            /// 

            string requeteSQL = "SELECT * FROM CLIENT";

            
            if(checkBox4.Checked) requeteSQL += " natural join COMMANDE ";
            if (checkBox1.Checked) requeteSQL += " WHERE Type_Client = 'Particulier'";
            else if (checkBox2.Checked) requeteSQL += " WHERE Type_Client = 'Entreprise'";

            if(checkBox4.Checked) requeteSQL += " group by ID_Client order by Prix_Commande";


            MessageBox.Show(requeteSQL);
            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string ID = Lire["ID_Client"].ToString();
                        string Type = Lire["Type_Client"].ToString();
                        string Tel = Lire["Tel_Client"].ToString();
                        string EMAIL = Lire["Courriel_Client"].ToString();
                        string Adresse = Lire["Adresse_Client"].ToString();
                        string Nom = Lire["Nom_Client"].ToString();
                        string Prenom = Lire["Prenom_Client"].ToString();
                        string Fidelio = Lire["NumProgramme_Fidelio"].ToString();
                        string Compagnie = Lire["NomCompagnie_Client"].ToString();
                        string Remise = Lire["RemiseCompagnie_Client"].ToString();
                        string DateDeb = Lire["DateDebut_Fidelio"].ToString();
                        string DateFin = Lire["DateFin_Fidelio"].ToString();

                        listView1.Items.Add(new ListViewItem(new[] { ID, Type, Tel, EMAIL, Adresse, Nom, Prenom, Fidelio, Compagnie, Remise, DateDeb, DateFin }));
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            DATABASE.Disconnect();
            this.Close();
            menu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewClient newc = new NewClient();
            newc.ShowDialog();
            Actualiser();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (DATABASE.Connected)// on verifie que la connexion est bien effective
                {

                    ListViewItem element = listView1.SelectedItems[0];
                    string ID = element.SubItems[0].Text;
                    MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM CLIENT WHERE ID_Client=@id", DATABASE.MySqlConnection);
                    mySqlCommand.Parameters.AddWithValue("@id", ID);
                    mySqlCommand.ExecuteNonQuery();
                    element.Remove();
                    mySqlCommand.Parameters.Clear();

                    MessageBox.Show("Client supprimé avec succès.");
                }
                else { MessageBox.Show("Erreur de connexion avec la base de données lors de la tentative de suppression du client"); }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                Actualiser();
            }
            else Actualiser();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                Actualiser();
            }
            else Actualiser();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string IdClient = element.SubItems[0].Text;
                string TypeClient = element.SubItems[1].Text;
                string NomClient = element.SubItems[5].Text;

                string Tel = element.SubItems[2].Text;
                string Mail = element.SubItems[3].Text;
                string Adresse = element.SubItems[4].Text;

                //Recuperation des valeurs du form ModifierPiece
                using (ModifierClient modifier = new ModifierClient())
                {
                    //Elements modifiables
                    modifier.Tel = Tel;
                    modifier.Mail = Mail;
                    modifier.Adresse = Adresse;

                    //Elements non modifiables
                    modifier.IdClient = IdClient;
                    modifier.TypeClient = TypeClient;
                    modifier.NomClient = NomClient;

                    //Si bouton cliqué = modifier
                    if (modifier.ShowDialog() == DialogResult.Yes)
                    {
                        if (DATABASE.Connected)
                        {
                            MySqlCommand requete = new MySqlCommand("UPDATE CLIENT SET Tel_Client=@tel, Courriel_Client=@mail,Adresse_Client=@adresse WHERE ID_Client=@id", DATABASE.MySqlConnection);
                            requete.Parameters.AddWithValue("@id", IdClient);
                            requete.Parameters.AddWithValue("@tel", modifier.Tel);
                            requete.Parameters.AddWithValue("@mail", modifier.Mail);
                            requete.Parameters.AddWithValue("@adresse", modifier.Adresse);

                            requete.ExecuteNonQuery();
                            requete.Parameters.Clear();

                            element.SubItems[2].Text = modifier.Tel;
                            element.SubItems[3].Text = modifier.Mail;
                            element.SubItems[4].Text = modifier.Adresse;
                            MessageBox.Show("Client mis à jour avec succès.");
                        }
                        else { MessageBox.Show("Erreur de connexion avec la base de données."); }
                    }
                    else { MessageBox.Show("Modification annulée."); }
                }

            }
        }



        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) checkBox4.Checked = false;
            Actualiser();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) checkBox3.Checked = false;
            Actualiser();
        }
    }
}
