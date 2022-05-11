using MySql.Data.MySqlClient;

namespace bdd
{
    public partial class MenuVelo : Form
    {
        BDD DATABASE = new BDD();
        public MenuVelo()
        {
            InitializeComponent();
            DATABASE.Connect();
            Actualiser();
        }

        private void MenuVelo_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewVelo velo = new NewVelo();
            velo.ShowDialog();
            Actualiser();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            DATABASE.Disconnect();
            this.Close();
            menu.Show();
        }

        private void Actualiser()
        {
            /// <summary>
            /// Méthode qui nous permet d'actualiser les vélos 
            /// </summary>

            string requeteSQL = "SELECT * FROM BICYCLETTE";

            //Requete modifiee pour filtrer les donnees
            #region Requetes filtres
            int compteur = 0;
            if (checkBox1.Checked || checkBox2.Checked || checkBox6.Checked || checkBox7.Checked || checkBox8.Checked || checkBox9.Checked ||
                checkBox10.Checked || checkBox11.Checked || checkBox12.Checked || checkBox13.Checked)
            {
                requeteSQL += " WHERE ";
                if (checkBox1.Checked || checkBox2.Checked || checkBox6.Checked || checkBox7.Checked)
                {
                    requeteSQL += " ( ";
                    if (checkBox1.Checked)
                    {
                        requeteSQL += "Type_Bicyclette='VTT'";
                        compteur++;
                    }

                    if (checkBox2.Checked)
                    {
                        if (compteur > 0) requeteSQL += " OR Type_Bicyclette='Course'";
                        else requeteSQL += "Type_Bicyclette='Course'";
                        compteur++;
                    }

                    if (checkBox6.Checked)
                    {
                        if (compteur > 0) requeteSQL += " OR Type_Bicyclette='Classique'";
                        else requeteSQL += "Type_Bicyclette='Classique'";
                        compteur++;
                    }

                    if (checkBox7.Checked)
                    {
                        if (compteur > 0) requeteSQL += " OR Type_Bicyclette='BMX'";
                        else requeteSQL += "Type_Bicyclette='BMX'";
                        compteur++;
                    }
                    requeteSQL += " ) ";
                }

                if (checkBox8.Checked || checkBox9.Checked || checkBox10.Checked || checkBox11.Checked || checkBox12.Checked || checkBox13.Checked)
                {
                    if (compteur > 0) requeteSQL += " AND ( ";
                    else requeteSQL += " ( ";
                    compteur = 0;
                    if (checkBox8.Checked)
                    {
                        requeteSQL += "Grandeur_Bicyclette='Adultes'";
                        compteur++;
                    }

                    if (checkBox9.Checked)
                    {
                        if (compteur > 0) requeteSQL += " OR Grandeur_Bicyclette='Jeunes'";
                        else requeteSQL += "Grandeur_Bicyclette='Jeunes'";
                        compteur++;
                    }

                    if (checkBox10.Checked)
                    {
                        if (compteur > 0) requeteSQL += " OR Grandeur_Bicyclette='Hommes'";
                        else requeteSQL += "Grandeur_Bicyclette='Hommes'";
                        compteur++;
                    }

                    if (checkBox11.Checked)
                    {
                        if (compteur > 0) requeteSQL += " OR Grandeur_Bicyclette='Dames'";
                        else requeteSQL += "Grandeur_Bicyclette='Dames'";
                        compteur++;
                    }

                    if (checkBox12.Checked)
                    {
                        if (compteur > 0) requeteSQL += " OR Grandeur_Bicyclette='Filles'";
                        else requeteSQL += "Grandeur_Bicyclette='Filles'";
                        compteur++;
                    }

                    if (checkBox13.Checked)
                    {
                        if (compteur > 0) requeteSQL += " OR Grandeur_Bicyclette='Garçons'";
                        else requeteSQL += "Grandeur_Bicyclette='Garçons'";
                        compteur++;
                    }
                    requeteSQL += " ) ";
                }

            }

            #endregion

            //Requete modifiee pour trier les donnees
            #region Requete tri
            if (checkBox3.Checked || checkBox4.Checked || checkBox5.Checked)
            {
                compteur = 0;
                requeteSQL += " ORDER BY ";
                if (checkBox3.Checked)
                {
                    requeteSQL += "Prix_Bicyclette DESC";
                    compteur++;
                }
                if (checkBox4.Checked)
                {
                    if (compteur > 0) requeteSQL += ",Nom_Bicyclette";
                    else requeteSQL += "Nom_Bicyclette";
                    compteur++;
                }
                if (checkBox5.Checked)
                {
                    if (compteur > 0) requeteSQL += ",Type_Bicyclette";
                    else requeteSQL += "Type_Bicyclette";
                    compteur++;
                }
            }
            #endregion



            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string Id = Lire["ID_Bicyclette"].ToString();
                        string Nom = Lire["Nom_Bicyclette"].ToString();
                        string Grandeur = Lire["Grandeur_Bicyclette"].ToString();
                        string Prix = Lire["Prix_Bicyclette"].ToString();
                        string Type = Lire["Type_Bicyclette"].ToString();
                        string Date1 = Lire["DateIntroduction_Bicyclette"].ToString();
                        string Date2 = Lire["DateFin_Bicyclette"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { Id, Nom, Grandeur, Prix, Type, Date1, Date2 }));
                    }
                }
            }
        }
        #region Checkbos Actualiser
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }
        #endregion

        private void voirLesPiècesDuModèleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string IdBicyclette = listView1.SelectedItems[0].SubItems[0].Text;
                ListePiecesVelo velo = new ListePiecesVelo(IdBicyclette);
                velo.ShowDialog();
            }
        }

        private void modifierToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string Id = element.SubItems[0].Text;
                string Nom = element.SubItems[1].Text;
                string Prix = element.SubItems[3].Text;
                string Date = element.SubItems[6].Text;



                //Recuperation des valeurs du form ModifierPiece
                using (ModifierVelo modifier = new ModifierVelo())
                {
                    //Elements modifiables
                    modifier.Nom = Nom;
                    modifier.Prix = Prix;
                    string[] subsDate = Date.Split(' ');
                    string[] date2 =  subsDate[0].Split('/');
                    Date = date2[2] + "-" + date2[1] + "-" + date2[0];
                    MessageBox.Show(Date);
                    modifier.Date = Date;

                    //Elements non modifiables
                    modifier.Id = Id;


                    //Si bouton cliqué = modifier
                    if (modifier.ShowDialog() == DialogResult.Yes)
                    {
                        if (DATABASE.Connected)
                        {
                            MySqlCommand requete = new MySqlCommand("UPDATE BICYCLETTE SET Nom_Bicyclette=@nom, Prix_Bicyclette=@prix,DateFin_Bicyclette=@date WHERE ID_Bicyclette=@id", DATABASE.MySqlConnection);
                            requete.Parameters.AddWithValue("@id", Id);
                            requete.Parameters.AddWithValue("@nom", modifier.Nom);
                            requete.Parameters.AddWithValue("@prix", Convert.ToDouble(modifier.Prix));
                            requete.Parameters.AddWithValue("@date", modifier.Date);

                            requete.ExecuteNonQuery();
                            requete.Parameters.Clear();

                            element.SubItems[1].Text = modifier.Nom;
                            element.SubItems[3].Text = modifier.Prix;
                            element.SubItems[6].Text = modifier.Date;
                            MessageBox.Show("Vélo mis à jour avec succès.");
                        }
                        else { MessageBox.Show("Erreur de connexion avec la base de données."); }
                    }
                    else { MessageBox.Show("Modification annulée."); }
                }

            }
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer définitivement ce Modèle ? Tous les liens entre les pièces et ce modèle seront supprimés.", "Suppression Modèle de Vélo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (DATABASE.Connected)// on verifie que la connexion est bien effective
                    {
                        ListViewItem element = listView1.SelectedItems[0];
                        string IdBicyclette = element.SubItems[0].Text;
                        MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM BICYCLETTE WHERE ID_Bicyclette = @id", DATABASE.MySqlConnection);
                        mySqlCommand.Parameters.AddWithValue("@id", IdBicyclette);
                        mySqlCommand.ExecuteNonQuery();
                        element.Remove();
                        mySqlCommand.Parameters.Clear();
                        MessageBox.Show("Bicyclette supprimée avec succès.");
                    }
                    else { MessageBox.Show("Erreur de connexion avec la base de données lors de la tentative de suppression du modèle."); }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Actualiser();
        }
    }
}
