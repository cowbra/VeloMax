using MySql.Data.MySqlClient;

namespace bdd
{
    public partial class NewVelo : Form
    {
        BDD DATABASE = new BDD();
        public NewVelo()
        {
            InitializeComponent();
            DATABASE.Connect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
            listBox3.SelectedIndex = 0;
            listBox4.SelectedIndex = 0;
            listBox5.SelectedIndex = 0;
            listBox6.SelectedIndex = 0;
            listBox7.SelectedIndex = 0;
            listBox8.SelectedIndex = 0;
            listBox9.SelectedIndex = 0;
            listBox10.SelectedIndex = 0;
            listBox11.SelectedIndex = 0;
            listBox12.SelectedIndex = 0;
            listBox13.SelectedIndex = 0;
            listBox14.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadData()
        {
            string sql = "SELECT Identifiant_Piece FROM PIECE WHERE Description_Piece=";
            #region SELECTIONS PIECES
            // Remplissage des listbox avec les id des pieces du type correspondant depuis la bdd
            #region Selection Cadre
            MySqlCommand mySqlCommand = new MySqlCommand(sql + "'Cadre'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox2.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Guidon
            mySqlCommand = new MySqlCommand(sql + "'Guidon'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox3.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Roue Avant
            mySqlCommand = new MySqlCommand(sql + "'Roue Avant'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox4.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Roue Arriere
            mySqlCommand = new MySqlCommand(sql + "'Roue Arrière'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox5.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Pedalier
            mySqlCommand = new MySqlCommand(sql + "'Pédalier'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox6.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Freins
            mySqlCommand = new MySqlCommand(sql + "'Freins'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox7.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Derailleur Avant
            mySqlCommand = new MySqlCommand(sql + "'Dérailleur Avant'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox8.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Derailleur Arriere
            mySqlCommand = new MySqlCommand(sql + "'Dérailleur Arrière'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox9.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Reflecteurs
            mySqlCommand = new MySqlCommand(sql + "'Réflecteurs'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox10.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Selle
            mySqlCommand = new MySqlCommand(sql + "'Selle'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox11.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection ordinateur 
            mySqlCommand = new MySqlCommand(sql + "'Ordinateur'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox12.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Panier
            mySqlCommand = new MySqlCommand(sql + "'Panier'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox13.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #endregion

        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string[] subsDate = dateTimePicker1.Value.ToShortDateString().Split('/');
            string dateTrans = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            textBox3.Text = dateTrans;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string[] subsDate = dateTimePicker2.Value.ToShortDateString().Split('/');
            string dateTrans = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            textBox4.Text = dateTrans;
        }

        private void listBox14_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double i;
            if (textBox1.Text == "") MessageBox.Show("Veuillez entrer le nom du modèle !");
            else if (listBox14.SelectedIndex == 0) MessageBox.Show("Veuillez sélectionner le Type de vélo !");
            else if (listBox1.SelectedIndex == 0) MessageBox.Show("Veuillez sélectionner la Grandeur du vélo !");
            else if (textBox3.Text == "") MessageBox.Show("Veuillez entrer la date d'introduction du modèle !");
            else if (textBox4.Text == "") MessageBox.Show("Veuillez entrer la date de fin de production du modèle !");
            else if (listBox2.SelectedIndex == 0) MessageBox.Show("Veuillez sélectionner le Cadre du vélo !");
            else if (listBox3.SelectedIndex == 0) MessageBox.Show("Veuillez sélectionner le Guidon du vélo !");
            else if (listBox4.SelectedIndex == 0) MessageBox.Show("Veuillez sélectionner la Roue Avant du vélo !");
            else if (listBox5.SelectedIndex == 0) MessageBox.Show("Veuillez sélectionner la Roue Arrière du vélo !");
            else if (listBox6.SelectedIndex == 0) MessageBox.Show("Veuillez sélectionner le Pédalier du vélo !");
            else if (listBox11.SelectedIndex == 0) MessageBox.Show("Veuillez sélectionner la Selle du vélo !");
            else if (textBox2.Text == "") MessageBox.Show("Veuillez entrer le Prix du modèle !");

            else if (double.TryParse(textBox2.Text, out i) == false) MessageBox.Show("Entrez un Prix valide !");
            else if (Convert.ToDouble(textBox2.Text) <= 0) MessageBox.Show("Entrez un Prix valide !");

            else
            {
                //On verifie la connxion à la bdd
                if (DATABASE.Connected)
                {
                    string nom = textBox1.Text;
                    string grandeur = listBox1.SelectedItem.ToString();
                    string type = listBox14.SelectedItem.ToString();
                    double prix = Convert.ToDouble(textBox2.Text);
                    string date1 = textBox3.Text;
                    string date2 = textBox4.Text;
                    Velo velo = new Velo(nom, grandeur, prix, type, date1, date2);

                    // AJOUT DU MODELE A LA BDD
                    if (velo.AddToBdd())
                    {
                        //ON RECUPERE L'ID DU DERNIER MODELE DE BICYCLETTE AJOUTE
                        string idBicyclette = "";
                        MySqlCommand requete = new MySqlCommand("SELECT ID_Bicyclette FROM BICYCLETTE ORDER BY ID_Bicyclette DESC LIMIT 1", DATABASE.MySqlConnection);
                        using (MySqlDataReader Lire = requete.ExecuteReader()) while (Lire.Read()) idBicyclette = Lire.GetString(0);

                        //AJOUT DES PIECES OBLIGATOIRES POUR LA CREATION DU MODELE
                        #region MODELE ASSEMBLER AVEC
                        MySqlCommand assembler_avec = new MySqlCommand("INSERT INTO ASSEMBLER_PAR(ID_Bicyclette,Identifiant_Piece) VALUES (@ID_Bicyclette,@id1), (@ID_Bicyclette,@id2),(@ID_Bicyclette,@id3),(@ID_Bicyclette,@id4),(@ID_Bicyclette,@id5),(@ID_Bicyclette,@id6)", DATABASE.MySqlConnection);
                        assembler_avec.Parameters.AddWithValue("@ID_Bicyclette", idBicyclette);

                        assembler_avec.Parameters.AddWithValue("@id1", listBox2.SelectedItem.ToString());
                        assembler_avec.Parameters.AddWithValue("@id2", listBox3.SelectedItem.ToString());
                        assembler_avec.Parameters.AddWithValue("@id3", listBox4.SelectedItem.ToString());
                        assembler_avec.Parameters.AddWithValue("@id4", listBox5.SelectedItem.ToString());
                        assembler_avec.Parameters.AddWithValue("@id5", listBox6.SelectedItem.ToString());
                        assembler_avec.Parameters.AddWithValue("@id6", listBox11.SelectedItem.ToString());

                        assembler_avec.ExecuteNonQuery();
                        assembler_avec.Parameters.Clear();
                        #endregion

                        //AJOUT DES PIECES FACULTATIVES LIEES AU MODELE DU VELO CREE
                        #region ASSEMNLER_PAR
                        MySqlCommand assembler_par = new MySqlCommand("INSERT INTO ASSEMBLER_PAR(ID_Bicyclette,Identifiant_Piece) VALUES(@ID_Bicyclette,@Identifiant_Piece)", DATABASE.MySqlConnection);
                        #region LIER DERAILLEUR AVANT AU MODELE
                        if (listBox8.SelectedIndex != 0)
                        {
                            assembler_par.Parameters.AddWithValue("@ID_Bicyclette", idBicyclette);
                            assembler_par.Parameters.AddWithValue("@Identifiant_Piece", listBox8.SelectedItem.ToString());
                            assembler_par.ExecuteNonQuery();
                            assembler_par.Parameters.Clear();
                        }
                        #endregion

                        #region LIER DERAILLEUR ARRIERE AU MODELE
                        if (listBox9.SelectedIndex != 0)
                        {
                            assembler_par.Parameters.AddWithValue("@ID_Bicyclette", idBicyclette);
                            assembler_par.Parameters.AddWithValue("@Identifiant_Piece", listBox9.SelectedItem.ToString());
                            assembler_par.ExecuteNonQuery();
                            assembler_par.Parameters.Clear();
                        }
                        #endregion

                        #region LIER FREINS AU MODELE
                        if (listBox7.SelectedIndex != 0)
                        {
                            assembler_par.Parameters.AddWithValue("@ID_Bicyclette", idBicyclette);
                            assembler_par.Parameters.AddWithValue("@Identifiant_Piece", listBox7.SelectedItem.ToString());
                            assembler_par.ExecuteNonQuery();
                            assembler_par.Parameters.Clear();
                        }
                        #endregion

                        #region LIER REFLECTEUR AU MODELE
                        if (listBox10.SelectedIndex != 0)
                        {
                            assembler_par.Parameters.AddWithValue("@ID_Bicyclette", idBicyclette);
                            assembler_par.Parameters.AddWithValue("@Identifiant_Piece", listBox10.SelectedItem.ToString());
                            assembler_par.ExecuteNonQuery();
                            assembler_par.Parameters.Clear();
                        }
                        #endregion

                        #region LIER ORDINATEUR AU MODELE
                        if (listBox12.SelectedIndex != 0)
                        {
                            assembler_par.Parameters.AddWithValue("@ID_Bicyclette", idBicyclette);
                            assembler_par.Parameters.AddWithValue("@Identifiant_Piece", listBox12.SelectedItem.ToString());
                            assembler_par.ExecuteNonQuery();
                            assembler_par.Parameters.Clear();
                        }
                        #endregion

                        #region LIER PANIER AU MODELE
                        if (listBox13.SelectedIndex != 0)
                        {
                            assembler_par.Parameters.AddWithValue("@ID_Bicyclette", idBicyclette);
                            assembler_par.Parameters.AddWithValue("@Identifiant_Piece", listBox13.SelectedItem.ToString());
                            assembler_par.ExecuteNonQuery();
                            assembler_par.Parameters.Clear();
                        }
                        #endregion

                        #endregion
                        MessageBox.Show("Modèle créé avec Succès !");
                        this.Close();
                        DATABASE.Disconnect();
                    }
                    else MessageBox.Show("Erreur de connexion avec la base de données !");




                }
            }
        }
        #region methodes objets vides
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox11_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox13_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
