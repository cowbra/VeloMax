using MySql.Data.MySqlClient;

namespace bdd
{
    public partial class NewCommande : Form
    {
        BDD DATABASE = new BDD();
        public NewCommande()
        {
            InitializeComponent();
            DATABASE.Connect();
            label1.Visible = false;
            label3.Visible = false;

            if (DATABASE.Connected)
            {
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT ID_Client FROM CLIENT", DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        comboBox1.Items.Add(Lire["ID_Client"].ToString());
                    }
                }
            }
        }

        private void MenuCommande_Load(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //ON MODIFIE LES COLONNES DE LA LISTVIEW
            listView1.Clear();
            listView1.Columns.Add("ID", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Type de pièce", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Date d'Introduction", 180, HorizontalAlignment.Left);
            listView1.Columns.Add("Date de Fin de Production", 180, HorizontalAlignment.Center);

            //ACTIVATION INTERFACE FILTRE DES PIECES
            #region Enabled true pieces
            checkBox1.Enabled = true;
            checkBox1.Visible = true;

            checkBox2.Enabled = true;
            checkBox2.Visible = true;

            checkBox3.Enabled = true;
            checkBox3.Visible = true;

            checkBox4.Enabled = true;
            checkBox4.Visible = true;

            checkBox5.Enabled = true;
            checkBox5.Visible = true;

            checkBox6.Enabled = true;
            checkBox6.Visible = true;

            checkBox7.Enabled = true;
            checkBox7.Visible = true;

            checkBox8.Enabled = true;
            checkBox8.Visible = true;

            checkBox9.Enabled = true;
            checkBox9.Visible = true;

            checkBox10.Enabled = true;
            checkBox10.Visible = true;

            checkBox11.Enabled = true;
            checkBox11.Visible = true;

            checkBox12.Enabled = true;
            checkBox12.Visible = true;

            checkBox23.Enabled = true;
            checkBox23.Visible = true;
            checkBox24.Enabled = true;
            checkBox24.Visible = true;
            #endregion

            //DESACTIVATION INTERFACE  DES VELOS
            #region Enabled False velos
            label3.Visible = false;
            label1.Visible = false;

            checkBox13.Enabled = false;
            checkBox13.Visible = false;

            checkBox14.Enabled = false;
            checkBox14.Visible = false;

            checkBox15.Enabled = false;
            checkBox15.Visible = false;

            checkBox16.Enabled = false;
            checkBox16.Visible = false;

            checkBox17.Enabled = false;
            checkBox17.Visible = false;

            checkBox18.Enabled = false;
            checkBox18.Visible = false;

            checkBox19.Enabled = false;
            checkBox19.Visible = false;

            checkBox20.Enabled = false;
            checkBox20.Visible = false;

            checkBox21.Enabled = false;
            checkBox21.Visible = false;

            checkBox22.Enabled = false;
            checkBox22.Visible = false;
            checkBox25.Enabled = false;
            checkBox25.Visible = false;
            checkBox26.Enabled = false;
            checkBox26.Visible = false;
            checkBox27.Enabled = false;
            checkBox27.Visible = false;
            #endregion

            Fill();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            listView1.Clear();
            listView1.Columns.Add("ID", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Nom du modèle", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Grandeur", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Prix", 80, HorizontalAlignment.Center);
            listView1.Columns.Add("Type de modèle", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Date d'Introduction", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Date de Fin de Production", 150, HorizontalAlignment.Center);

            //DESACTIVE INTERFACE  DES PIECES
            #region Enabled false pieces
            checkBox1.Enabled = false;
            checkBox1.Visible = false;

            checkBox2.Enabled = false;
            checkBox2.Visible = false;

            checkBox3.Enabled = false;
            checkBox3.Visible = false;

            checkBox4.Enabled = false;
            checkBox4.Visible = false;

            checkBox5.Enabled = false;
            checkBox5.Visible = false;

            checkBox6.Enabled = false;
            checkBox6.Visible = false;

            checkBox7.Enabled = false;
            checkBox7.Visible = false;

            checkBox8.Enabled = false;
            checkBox8.Visible = false;

            checkBox9.Enabled = false;
            checkBox9.Visible = false;

            checkBox10.Enabled = false;
            checkBox10.Visible = false;

            checkBox11.Enabled = false;
            checkBox11.Visible = false;

            checkBox12.Enabled = false;
            checkBox12.Visible = false;

            checkBox23.Enabled = false;
            checkBox23.Visible = false;
            checkBox24.Enabled = false;
            checkBox24.Visible = false;

            #endregion

            //ACTIVATION INTERFACE  DES VELOS
            #region Enabled True velos
            label3.Visible = true;
            label1.Visible = true;

            checkBox13.Enabled = true;
            checkBox13.Visible = true;

            checkBox14.Enabled = true;
            checkBox14.Visible = true;

            checkBox15.Enabled = true;
            checkBox15.Visible = true;

            checkBox16.Enabled = true;
            checkBox16.Visible = true;

            checkBox17.Enabled = true;
            checkBox17.Visible = true;

            checkBox18.Enabled = true;
            checkBox18.Visible = true;

            checkBox19.Enabled = true;
            checkBox19.Visible = true;

            checkBox20.Enabled = true;
            checkBox20.Visible = true;

            checkBox21.Enabled = true;
            checkBox21.Visible = true;

            checkBox22.Enabled = true;
            checkBox22.Visible = true;

            checkBox25.Enabled = true;
            checkBox25.Visible = true;
            checkBox26.Enabled = true;
            checkBox26.Visible = true;
            checkBox27.Enabled = true;
            checkBox27.Visible = true;
            #endregion

            Fill();
        }



        private void Fill()
        {
            string requeteSQL = "";
            //ON REMPLIT LA LISTVIEW AVEC LES INFOS DE LA BDD
            #region AFFICHAGE DES PIECES

            if (radioButton1.Checked)
            {

                #region REQUETE
                requeteSQL = "SELECT Identifiant_Piece, Description_Piece, DateDebut_Piece, DateFin_Piece FROM PIECE ";

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

                //Requetes pour TRI
                #region TRI SQL
                if (checkBox23.Checked || checkBox24.Checked)
                {
                    compteur = 0;
                    requeteSQL += " ORDER BY ";

                    if (checkBox24.Checked)
                    {
                        requeteSQL += "DateDebut_Piece DESC";
                        compteur++;
                    }
                    if (checkBox23.Checked)
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
                            listView1.Items.Add(new ListViewItem(new[] { id, type, date1, date2 }));
                        }
                    }
                }
            }
            #endregion


            #region AFFICHAGE DES VELOS
            if (radioButton2.Checked)
            {

                requeteSQL = "SELECT * FROM BICYCLETTE";

                //Requete modifiee pour filtrer les donnees
                #region Requetes filtres
                int compteur = 0;
                if (checkBox22.Checked || checkBox21.Checked || checkBox20.Checked || checkBox19.Checked || checkBox18.Checked || checkBox17.Checked ||
                    checkBox16.Checked || checkBox15.Checked || checkBox14.Checked || checkBox13.Checked)
                {
                    requeteSQL += " WHERE ";
                    if (checkBox22.Checked || checkBox21.Checked || checkBox20.Checked || checkBox19.Checked)
                    {
                        requeteSQL += " ( ";
                        if (checkBox22.Checked)
                        {
                            requeteSQL += "Type_Bicyclette='VTT'";
                            compteur++;
                        }

                        if (checkBox21.Checked)
                        {
                            if (compteur > 0) requeteSQL += " OR Type_Bicyclette='Course'";
                            else requeteSQL += "Type_Bicyclette='Course'";
                            compteur++;
                        }

                        if (checkBox20.Checked)
                        {
                            if (compteur > 0) requeteSQL += " OR Type_Bicyclette='Classique'";
                            else requeteSQL += "Type_Bicyclette='Classique'";
                            compteur++;
                        }

                        if (checkBox19.Checked)
                        {
                            if (compteur > 0) requeteSQL += " OR Type_Bicyclette='BMX'";
                            else requeteSQL += "Type_Bicyclette='BMX'";
                            compteur++;
                        }
                        requeteSQL += " ) ";
                    }

                    if (checkBox13.Checked || checkBox14.Checked || checkBox15.Checked || checkBox16.Checked || checkBox17.Checked || checkBox18.Checked)
                    {
                        if (compteur > 0) requeteSQL += " AND ( ";
                        else requeteSQL += " ( ";
                        compteur = 0;
                        if (checkBox18.Checked)
                        {
                            requeteSQL += "Grandeur_Bicyclette='Adultes'";
                            compteur++;
                        }

                        if (checkBox17.Checked)
                        {
                            if (compteur > 0) requeteSQL += " OR Grandeur_Bicyclette='Jeunes'";
                            else requeteSQL += "Grandeur_Bicyclette='Jeunes'";
                            compteur++;
                        }

                        if (checkBox16.Checked)
                        {
                            if (compteur > 0) requeteSQL += " OR Grandeur_Bicyclette='Hommes'";
                            else requeteSQL += "Grandeur_Bicyclette='Hommes'";
                            compteur++;
                        }

                        if (checkBox15.Checked)
                        {
                            if (compteur > 0) requeteSQL += " OR Grandeur_Bicyclette='Dames'";
                            else requeteSQL += "Grandeur_Bicyclette='Dames'";
                            compteur++;
                        }

                        if (checkBox14.Checked)
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
                if (checkBox27.Checked || checkBox26.Checked || checkBox25.Checked)
                {
                    compteur = 0;
                    requeteSQL += " ORDER BY ";
                    if (checkBox26.Checked)
                    {
                        requeteSQL += "Prix_Bicyclette DESC";
                        compteur++;
                    }
                    if (checkBox27.Checked)
                    {
                        if (compteur > 0) requeteSQL += ",Nom_Bicyclette";
                        else requeteSQL += "Nom_Bicyclette";
                        compteur++;
                    }
                    if (checkBox25.Checked)
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
            #endregion
        }
        #region detect checkbox

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }
        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Fill();
        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private int Select_Piece(string idPiece, int quantitee_Commandee)
        {
            ///DONNE LE MEILLEUR FOURNISSEUR POUR UNE PIECE SOUHAITEE///
            ///RETOURNE LE TEMPS D'APPROVISIONNEMENT NECESSAIRE POUR LA COMMANDE
            /// SI PIECE EN STOCK : 0 
            /// SINON DELAI DU MEILLEUR FOURNISSEUR
            /// 

            #region recupere fournisseur
            string sql = "SELECT FOURNIT.Siret_Fournisseur,Nom_Fournisseur,NumProduit_Fournisseur,Prix_Fournisseur,Quantite_Fournisseur,Delai_Fournisseur FROM FOURNIT,FOURNISSEUR WHERE Identifiant_Piece =@id GROUP BY FOURNIT.Siret_Fournisseur ORDER BY FOURNISSEUR.Libelle_Fournisseur, FOURNIT.Quantite_Fournisseur DESC, FOURNIT.Delai_Fournisseur ASC LIMIT 1";

            MySqlCommand mySqlCommand = new MySqlCommand(sql, DATABASE.MySqlConnection);
            mySqlCommand.Parameters.AddWithValue("@id", idPiece);

            string SIRET = "";
            string NOM_F = "";
            string NUM_CATALOGUE = "";
            string PRIX = "";
            string QUANTITE = "";
            string DELAI = "";


            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    SIRET = Lire["Siret_Fournisseur"].ToString();
                    NOM_F = Lire["Nom_Fournisseur"].ToString();
                    NUM_CATALOGUE = Lire["NumProduit_Fournisseur"].ToString();
                    PRIX = Lire["Prix_Fournisseur"].ToString();
                    QUANTITE = Lire["Quantite_Fournisseur"].ToString();
                    DELAI = Lire["Delai_Fournisseur"].ToString();
                }
            }
            #endregion

            // SI POUR CETTE PIECE PAS DE STOCK :
            // SELECTIONNE LE MEILLEUR FOURNISSEUR PUIS AJOUTE DELAI APPROVISIONNEMENT AU DELAI LIVRAISON

            if (Convert.ToInt32(QUANTITE) == 0)
            {
                // -> DELAI DE LIVRAISON = 7 jours + DELAI FOURNISSEUR 
                //ICI pas besoin de modifier la quantité, car la quantité commandée au fournisseur sera livrée au client
                // Voir alertes quantites par pieces pour commandee des pieces
                return Convert.ToInt32(DELAI);
            }

            else if (Convert.ToInt32(QUANTITE) < quantitee_Commandee)
            {
                //RETIRER LE STOCK ACTUEL POUR LA PIECE ET LE RESTE SERA COMMANDE
                // -> DELAI DE LIVRAISON = 7 jours + DELAI FOURNISSEUR
                MySqlCommand requete = new MySqlCommand("UPDATE FOURNIT SET Quantite_Fournisseur=0 WHERE Siret_Fournisseur=@siret AND Identifiant_Piece=@id", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@id", idPiece);
                requete.Parameters.AddWithValue("@siret", SIRET);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                return Convert.ToInt32(DELAI);
            }
            else
            {
                //RETIRER LE STOCK COMMANDE POUR LA PIECE 
                // -> DELAI DE LIVRAISON = 7 jours CAR EN STOCK
                MySqlCommand requete = new MySqlCommand("UPDATE FOURNIT SET Quantite_Fournisseur=@quantite WHERE Siret_Fournisseur=@siret AND Identifiant_Piece=@id", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@id", idPiece);
                requete.Parameters.AddWithValue("@quantite", Convert.ToInt32(QUANTITE) - quantitee_Commandee);
                requete.Parameters.AddWithValue("@siret", SIRET);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();
                return 0;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adresse = textBox2.Text;
            int i;

            if (comboBox1.SelectedItem == null) MessageBox.Show("Veuillez sélectionner l'ID du client !");
            else if (int.TryParse(textBox1.Text, out i) == false) MessageBox.Show("Entrez une quantité valide !");
            else if (Convert.ToInt32(textBox1.Text) <= 0) MessageBox.Show("Entrez une quantité valide !");
            else if (adresse == "") MessageBox.Show("Entrez l'Adresse de Livraison !");
            else if (listView1.SelectedItems.Count == 0) MessageBox.Show("Veuillez sélectionner un produit à Commander !");

            else
            {
                int idClient = Convert.ToInt32(comboBox1.SelectedItem.ToString());
                int quantiteCommandee = Convert.ToInt32(textBox1.Text);
                string type = "piece";
                if (radioButton2.Checked) type = "bicyclette";

                //Creation de la commande
                Commande commande = new Commande(idClient, adresse, type, quantiteCommandee);
                if (!DATABASE.Connected) DATABASE.Connect();
                if (DATABASE.Connected)
                {

                    if (commande.AddToBdd())
                    {
                        //ON RECUPERE L'ID DE LA COMMANDE CREE POUR LES FOREIGN KEY
                        string idCommande = "";
                        MySqlCommand requete = new MySqlCommand("SELECT ID_Commande FROM COMMANDE ORDER BY ID_Commande DESC LIMIT 1", DATABASE.MySqlConnection);
                        using (MySqlDataReader Lire = requete.ExecuteReader()) while (Lire.Read()) idCommande = Lire.GetString(0);


                        //ON RECUPERE L'ID DU PRODUIT COMMANDE (PIECE OU VELO)
                        ListViewItem element = listView1.SelectedItems[0];
                        string Id = element.SubItems[0].Text;

                        int delai_Livraison = 7;


                        //CALCULER DELAI LIVRAISON
                        #region SI COMMANDE DE PIECES
                        if (radioButton1.Checked)
                        //LA COMMANDE EST UNE PIECE
                        {
                            // delai de livraison de 7 jours + si necessairez deai approvisionnement piece fournisseur
                            delai_Livraison += Select_Piece(Id, quantiteCommandee);

                            TimeSpan duration = new System.TimeSpan(delai_Livraison, 0, 0, 0);
                            DateTime dateLivraison = DateTime.Today.Add(duration);
                            string[] subsDate = dateLivraison.ToString("d").Split('/');
                            string date = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];


                            // ON FAIT LE LIEN ENTRE LA COMMANDE ET LE PRODUIT COMMANDé DANS LA BDD
                            if (PieceLinkWithCommand_ToBdd(Convert.ToInt32(idCommande), Id, quantiteCommandee, date))
                            {
                                if (DATABASE.Connected)
                                {
                                    double prix = 0;
                                    string sql = "SELECT Prix_Fournisseur FROM FOURNIT,FOURNISSEUR WHERE Identifiant_Piece =@id GROUP BY FOURNIT.Siret_Fournisseur ORDER BY FOURNISSEUR.Libelle_Fournisseur, FOURNIT.Quantite_Fournisseur DESC, FOURNIT.Delai_Fournisseur ASC LIMIT 1";

                                    MySqlCommand mySqlCommand = new MySqlCommand(sql, DATABASE.MySqlConnection);
                                    mySqlCommand.Parameters.AddWithValue("@id", Id);
                                    using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                                        while (Lire.Read())
                                        {
                                            prix = Convert.ToDouble(Lire["Prix_Fournisseur"].ToString());

                                        }

                                    ///CALCUL REDUCTION EN FONCTION DE LA REMISE COMPAGNIE
                                    double reduction_Compagnie = 0;

                                    string sql2 = "select coalesce(RemiseCompagnie_Client, 0) as RemiseCompagnie_Client from CLIENT where ID_Client=@id";
                                    MySqlCommand mySqlCommand2 = new MySqlCommand(sql2, DATABASE.MySqlConnection);
                                    mySqlCommand2.Parameters.AddWithValue("@id", idClient);
                                    using (MySqlDataReader Lire = mySqlCommand2.ExecuteReader())
                                        while (Lire.Read())
                                        {
                                            reduction_Compagnie += Convert.ToDouble(Lire["RemiseCompagnie_Client"].ToString());
                                        }

                                    ///CALCUL REDUCTION EN FONCTION DU FIDELIO
                                    sql2 = "select coalesce(Rabais_Fidelio, 0) as Rabais_Fidelio from FIDELIO,CLIENT where FIDELIO.NumProgramme_Fidelio=CLIENT.NumProgramme_Fidelio AND ID_Client=@id";
                                    MySqlCommand mySqlCommand3 = new MySqlCommand(sql2, DATABASE.MySqlConnection);
                                    mySqlCommand3.Parameters.AddWithValue("@id", idClient);
                                    using (MySqlDataReader Lire = mySqlCommand3.ExecuteReader())
                                        while (Lire.Read())
                                        {
                                            reduction_Compagnie += Convert.ToDouble(Lire["Rabais_Fidelio"].ToString());
                                        }
                                    //MessageBox.Show(reduction_Compagnie.ToString());




                                    if (commande.UpdatePrixTotal(idCommande, Math.Round(prix - reduction_Compagnie,2), quantiteCommandee))
                                    {
                                        MessageBox.Show("Commande réussie");
                                        MessageBox.Show("Délai de Livraison estimé : " + delai_Livraison + " jours");
                                        this.Close();
                                    }
                                    else MessageBox.Show("Erreur de Connexion avec la Base de données.");

                                }
                            }
                            else
                            {
                                MessageBox.Show("Erreur de Connexion avec la Base de données.");
                                MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM COMMANDE WHERE ID_Commande = @id", DATABASE.MySqlConnection);
                                mySqlCommand.Parameters.AddWithValue("@id", idCommande);
                                mySqlCommand.ExecuteNonQuery();
                                mySqlCommand.Parameters.Clear();
                            }

                        }
                        #endregion

                        #region SI COMMANDE DE VELO
                        else
                        // LA COMMANDE EST UN VELO
                        {
                            // on va checker les stocks pour toutes les pieces constituant le modele de velo
                            // si 1 ou plusieurs pieces plus en stock, on prend le MAX des delais fournisseurs entre les differentes pieces manquantes

                            List<string> pieces = new List<string>();
                            List<int> delais = new List<int>();

                            #region recuperation pieces du modele
                            string requeteSQL = "SELECT * FROM PIECE NATURAL JOIN ASSEMBLER_PAR WHERE ID_Bicyclette =" + Id;
                            if (DATABASE.Connected)// on verifie que la connexion est bien effective
                            {
                                listView1.Items.Clear();
                                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                                {
                                    while (Lire.Read())
                                    {
                                        pieces.Add(Lire["Identifiant_Piece"].ToString());

                                    }
                                }
                            }
                            #endregion

                            //AJOUT DES DELAIS FOURNISSEURS SI BESOIN POUR LES PECES
                            //MISE A JOUR DES STOCKS EN FONCTION DES PIECES COMMANDEES
                            foreach (string piece in pieces)
                            {
                                delais.Add(Select_Piece(piece, quantiteCommandee));
                            }

                            delai_Livraison += delais.Max();

                            TimeSpan duration = new System.TimeSpan(delai_Livraison, 0, 0, 0);
                            DateTime dateLivraison = DateTime.Today.Add(duration);
                            string[] subsDate = dateLivraison.ToString("d").Split('/');
                            string date = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];

                            // ON FAIT LE LIEN ENTRE LA COMMANDE ET LE PRODUIT COMMANDé DANS LA BDD
                            if (VeloLinkWithCommand_ToBdd(Convert.ToInt32(idCommande), Convert.ToInt32(Id), quantiteCommandee, date))
                            {
                                if (DATABASE.Connected)
                                {
                                    double prix = 0;
                                    string sql = "SELECT Prix_Bicyclette FROM BICYCLETTE WHERE ID_Bicyclette=@id";

                                    MySqlCommand mySqlCommand = new MySqlCommand(sql, DATABASE.MySqlConnection);
                                    mySqlCommand.Parameters.AddWithValue("@id", Id);
                                    using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                                    {
                                        while (Lire.Read())
                                        {
                                            prix = Convert.ToDouble(Lire["Prix_Bicyclette"].ToString());
                                        }
                                    }



                                    ///CALCUL REDUCTION EN FONCTION DE LA REMISE COMPAGNIE
                                    double reduction_Compagnie = 0;

                                    string sql2 = "select coalesce(RemiseCompagnie_Client, 0) as RemiseCompagnie_Client from CLIENT where ID_Client=@id";
                                    MySqlCommand mySqlCommand2 = new MySqlCommand(sql2, DATABASE.MySqlConnection);
                                    mySqlCommand2.Parameters.AddWithValue("@id", idClient);
                                    using (MySqlDataReader Lire = mySqlCommand2.ExecuteReader())
                                        while (Lire.Read())
                                        {
                                            reduction_Compagnie += Convert.ToDouble(Lire["RemiseCompagnie_Client"].ToString());
                                        }

                                    ///CALCUL REDUCTION EN FONCTION DU FIDELIO
                                    sql2 = "select coalesce(Rabais_Fidelio, 0) as Rabais_Fidelio from FIDELIO,CLIENT where FIDELIO.NumProgramme_Fidelio=CLIENT.NumProgramme_Fidelio AND ID_Client=@id";
                                    MySqlCommand mySqlCommand3 = new MySqlCommand(sql2, DATABASE.MySqlConnection);
                                    mySqlCommand3.Parameters.AddWithValue("@id", idClient);
                                    using (MySqlDataReader Lire = mySqlCommand3.ExecuteReader())
                                        while (Lire.Read())
                                        {
                                            reduction_Compagnie += Convert.ToDouble(Lire["Rabais_Fidelio"].ToString());
                                        }
                                    //MessageBox.Show(reduction_Compagnie.ToString());




                                    if (commande.UpdatePrixTotal(idCommande, Math.Round(prix - reduction_Compagnie,2), quantiteCommandee))
                                    {
                                        MessageBox.Show("Commande réussie");
                                        MessageBox.Show("Délai de Livraison estimé : " + delai_Livraison + " jours");
                                        this.Close();
                                    }
                                    else MessageBox.Show("Erreur de Connexion avec la Base de données.");

                                }
                            }

                            else
                            {
                                MessageBox.Show("Erreur de Connexion avec la Base de données.");
                                MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM COMMANDE WHERE ID_Commande = @id", DATABASE.MySqlConnection);
                                mySqlCommand.Parameters.AddWithValue("@id", idCommande);
                                mySqlCommand.ExecuteNonQuery();
                                mySqlCommand.Parameters.Clear();
                            }

                        }


                        #endregion

                    }
                    else
                    {
                        MessageBox.Show("Erreur de Connexion avec la Base de données.");
                        // On laisse la fenetre de creation de fournisseur ouverte pour retenter une connexion à la bdd

                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public bool PieceLinkWithCommand_ToBdd(int idC, string idP, int quantite, string date)
        {
            if (DATABASE.Connected)
            {
                MySqlCommand requete = new MySqlCommand("INSERT INTO ACHAT_PIECE(ID_Commande,Identifiant_Piece,DateLivraison) VALUES(@idC,@idP,@date)", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@idC", idC);
                requete.Parameters.AddWithValue("@idP", idP);
                requete.Parameters.AddWithValue("@date", date);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                return true;
            }
            return false;
        }

        public bool VeloLinkWithCommand_ToBdd(int idC, int idV, int quantite, string date)
        {
            if (DATABASE.Connected)
            {
                MySqlCommand requete = new MySqlCommand("INSERT INTO ACHAT_BICYCLETTE(ID_Commande,ID_Bicyclette,DateLivraison) VALUES(@idC,@idB,@date)", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@idC", idC);
                requete.Parameters.AddWithValue("@idB", idV);
                requete.Parameters.AddWithValue("@date", date);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                return true;
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
