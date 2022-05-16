using MySql.Data.MySqlClient;
using System;

namespace bdd
{
    public partial class MenuPiece : Form
    {
        BDD DATABASE = new BDD();
        List<int> rouge = new List<int>();
        List<int> orange = new List<int>();
        public MenuPiece()
        {
            InitializeComponent();
            DATABASE.Connect();
            Actualiser();
            
        }

        private void Actualiser()
        {
            /// <summary>
            /// Méthode qui nous permet d'actualiser les fournisseurs 
            /// </summary>
            /// 
            rouge.Clear();
            orange.Clear();
            

            #region REQUETE
            string requeteSQL = "SELECT Identifiant_Piece, Description_Piece, DateDebut_Piece, DateFin_Piece, SUM(Quantite_Fournisseur) AS Quantite_Total_Piece FROM PIECE NATURAL JOIN FOURNIT";

            #region requetes_Filtres
            int compteur = 0;
            if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || checkBox8.Checked || checkBox9.Checked || checkBox10.Checked || checkBox11.Checked || checkBox12.Checked)
            {
                requeteSQL += " WHERE Description_Piece=";
                if (checkBox1.Checked)
                {
                    requeteSQL += "'Cadre'";
                    compteur++;
                }
                if (checkBox2.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Guidon'";
                    else requeteSQL += "'Guidon'";
                    compteur++;
                }
                if (checkBox3.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Freins'";
                    else requeteSQL += "'Freins'";
                    compteur++;
                }
                if (checkBox4.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Dérailleur Arrière'";
                    else requeteSQL += "'Dérailleur Arrière'";
                    compteur++;
                }
                if (checkBox5.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Dérailleur Avant'";
                    else requeteSQL += "'Dérailleur Avant'";
                    compteur++;
                }
                if (checkBox6.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Selle'";
                    else requeteSQL += "'Selle'";
                    compteur++;
                }

                if (checkBox7.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Roue Avant'";
                    else requeteSQL += "'Roue Avant'";
                    compteur++;
                }
                if (checkBox8.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Roue Arrière'";
                    else requeteSQL += "'Roue Arrière'";
                    compteur++;
                }
                if (checkBox9.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Ordinateur'";
                    else requeteSQL += "'Ordinateur'";
                    compteur++;
                }
                if (checkBox10.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Pédalier'";
                    else requeteSQL += "'Pédalier'";
                    compteur++;
                }
                if (checkBox11.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Réflecteurs'";
                    else requeteSQL += "'Réflecteurs'";
                    compteur++;
                }
                if (checkBox12.Checked)
                {
                    if (compteur > 0) requeteSQL += " OR Description_Piece='Panier'";
                    else requeteSQL += "'Panier'";
                    compteur++;
                }
            }
            #endregion

            requeteSQL += " GROUP BY Identifiant_Piece";

            //REQUETE QUANTITE FIlTRE
            #region quantity check
            if (textBox1.Text != "")
            {
                int i;
                if (int.TryParse(textBox1.Text, out i) == false) MessageBox.Show("Entrez une quantité valide !");
                else if (Convert.ToInt16(textBox1.Text) < 0) MessageBox.Show("Entrez une quantité valide !");
                else
                {
                    requeteSQL += " HAVING Quantite_Total_Piece >=" + textBox1.Text;
                }
            }
            #endregion

            //Requetes pour TRI
            #region TRI SQL
            if (checkBox22.Checked || checkBox23.Checked || checkBox24.Checked)
            {
                compteur = 0;
                requeteSQL += " ORDER BY ";
                if (checkBox24.Checked)
                {
                    requeteSQL += "Quantite_Total_Piece DESC";
                    compteur++;
                }
                if (checkBox23.Checked)
                {
                    if (compteur > 0) requeteSQL += ",DateDebut_Piece DESC";
                    else requeteSQL += "DateDebut_Piece DESC";
                    compteur++;
                }
                if (checkBox22.Checked)
                {
                    if (compteur > 0) requeteSQL += ",DateFin_Piece DESC";
                    else requeteSQL += "DateFin_Piece DESC";
                    compteur++;
                }
            }
            #endregion

            #endregion


            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string id = Lire["Identifiant_Piece"].ToString();
                        string type = Lire["Description_Piece"].ToString();
                        string date1 = Lire["DateDebut_Piece"].ToString();
                        string date2 = Lire["DateFin_Piece"].ToString();
                        string quantity = Lire["Quantite_Total_Piece"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { id, type, date1, date2, quantity }));
                    }
                }
                listView1.Items[4].BackColor = Color.Red;
            }
            for (int i =0;i<listView1.Items.Count;i++) listView1.Items[i].BackColor = Color.White;
            
            


            for (int i=0;i<listView1.Items.Count;i++)
            {
                if (Convert.ToInt32(listView1.Items[i].SubItems[4].Text) == 0) rouge.Add(i);
                else if (Convert.ToInt32(listView1.Items[i].SubItems[4].Text) <= 10 && Convert.ToInt32(listView1.Items[i].SubItems[4].Text)>0) orange.Add(i);
                


            }
            

            foreach(int i in rouge) listView1.Items[i].BackColor = Color.OrangeRed;
            foreach (int i in orange) listView1.Items[i].BackColor = Color.Orange;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewPiece piece = new NewPiece();
            piece.ShowDialog();
            Actualiser();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            DATABASE.Disconnect();
            this.Close();
            menu.Show();
        }
        #region Actualiser_filtre


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }
        #endregion

        private void MenuPiece_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void afficherSockFournisseursToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stocksFournisseursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string Piece = element.SubItems[0].Text;
                StockFournisseurPiece stockFournisseur = new StockFournisseurPiece(Piece);
                stockFournisseur.ShowDialog();
            }
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Êtes - vous sûr de vouloir supprimer définitivement cette Pièce ? Tous les liens entre les fournisseurs et cette pièce seront supprimés.", "Suppression Pièce", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (DATABASE.Connected)// on verifie que la connexion est bien effective
                    {
                        ListViewItem element = listView1.SelectedItems[0];
                        string Piece = element.SubItems[0].Text;
                        MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM PIECE WHERE Identifiant_Piece =@idPiece", DATABASE.MySqlConnection);
                        mySqlCommand.Parameters.AddWithValue("@idPiece", Piece);
                        mySqlCommand.ExecuteNonQuery();
                        element.Remove();
                        mySqlCommand.Parameters.Clear();
                        MessageBox.Show("Pièce supprimée avec succès.");
                    }
                    else { MessageBox.Show("Erreur de connexion avec la base de données lors de la tentative de suppression de la pièce"); }
                }
            }
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string IdPiece = element.SubItems[0].Text;
                string DateIntro = element.SubItems[2].Text;
                string DateFin = element.SubItems[3].Text;

                //Recuperation des valeurs du form ModifierPiece
                using (ModifierPiece modifier = new ModifierPiece())
                {
                    //Elements modifiables
                    modifier.DateIntro = DateIntro;
                    modifier.DateFin = DateFin;

                    //Elements non modifiables
                    modifier.IdPiece = IdPiece;

                    //Si bouton cliqué = modifier
                    if (modifier.ShowDialog() == DialogResult.Yes)
                    {
                        if (DATABASE.Connected)
                        {
                            MySqlCommand requete = new MySqlCommand("UPDATE PIECE SET DateDebut_Piece=@date1, DateFin_Piece=@date2 WHERE Identifiant_Piece=@id", DATABASE.MySqlConnection);
                            requete.Parameters.AddWithValue("@id", IdPiece);
                            requete.Parameters.AddWithValue("@date1", modifier.DateIntro);
                            requete.Parameters.AddWithValue("@date2", modifier.DateFin);

                            requete.ExecuteNonQuery();
                            requete.Parameters.Clear();

                            element.SubItems[2].Text = modifier.DateIntro;
                            element.SubItems[3].Text = modifier.DateFin;
                            MessageBox.Show("Pièce mise à jour.");
                        }
                        else { MessageBox.Show("Erreur de connexion avec la base de données."); }
                    }
                    else { MessageBox.Show("Modification annulée."); }
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Int64> SIRETS = new List<long>();
            List<string> PIECES = new List<string>();
            List<int> NUM_CATALOGUE = new List<int>();
            List<double> PRIX = new List<double>();
            List<int> QUANTITES = new List<int>();
            List<int> DELAIS = new List<int>();

            string req = "SELECT Siret_Fournisseur,Identifiant_Piece,Nom_Fournisseur,NumProduit_Fournisseur,Prix_Fournisseur,Quantite_Fournisseur,Delai_Fournisseur FROM FOURNIT WHERE Identifiant_Piece IN (SELECT Identifiant_Piece FROM PIECE NATURAL JOIN FOURNIT GROUP BY Identifiant_Piece HAVING SUM(Quantite_Fournisseur) <= 10);";
            if (DATABASE.Connected)
            {
                int i = 0;
                TextWriter writer = new StreamWriter("export.xml");

                MySqlCommand mySqlCommand = new MySqlCommand(req, DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        Int64 Siret = Convert.ToInt64(Lire["Siret_Fournisseur"].ToString());
                        string Id = Lire["Identifiant_Piece"].ToString();
                        int Num = Convert.ToInt32(Lire["NumProduit_Fournisseur"].ToString());
                        double Prix = Convert.ToDouble(Lire["Prix_Fournisseur"].ToString());
                        int Quantite = Convert.ToInt32(Lire["Quantite_Fournisseur"].ToString());
                        int Delai = Convert.ToInt32(Lire["Delai_Fournisseur"].ToString());
                        
                        SIRETS.Add(Siret);
                        PIECES.Add(Id);
                        NUM_CATALOGUE.Add(Num);
                        PRIX.Add(Prix);
                        QUANTITES.Add(Quantite);
                        DELAIS.Add(Delai);
                    }
                }



                foreach (Int64 siret in SIRETS)
                {
                    Fourni f = new Fourni(siret,PIECES[i], QUANTITES[i], DELAIS[i], PRIX[i], NUM_CATALOGUE[i]);
                    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(f.GetType());
                    x.Serialize(writer, f);


                }
                writer.Close();
                MessageBox.Show("Fichier 'export.xml' exporté dans le dossier bin de la solution avec succès !");
            }
        }
    }
}
