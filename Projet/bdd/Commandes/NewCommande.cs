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

        
    }
}
