using MySql.Data.MySqlClient;


namespace bdd
{
    public partial class MenuCommande : Form
    {
        BDD DATABASE = new BDD();
        public MenuCommande()
        {
            InitializeComponent();
            DATABASE.Connect();
            Actualiser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewCommande commande = new NewCommande();
            commande.ShowDialog();
            Actualiser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            DATABASE.Disconnect();
            this.Close();
            menu.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Actualiser()
        {
            /// <summary>
            /// Méthode qui nous permet d'actualiser les commandes 
            /// </summary>
            /// 

            string requeteSQL = "SELECT * FROM COMMANDE";
            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string Id = Lire["ID_Commande"].ToString();
                        string Date = Lire["Date_Commande"].ToString();
                        string Adresse = Lire["AdresseLivraison_Commande"].ToString();
                        string IdClient = Lire["ID_Client"].ToString();
                        string Prix = Lire["Prix_Commande"].ToString();
                        string Tipe = Lire["Type_Commande"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { Id, IdClient, Date, Adresse, Prix, Tipe }));
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void détailsDeLaCommandeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string idCommande = element.SubItems[0].Text;
                string typeCommande = element.SubItems[5].Text;
                string dateCommande = element.SubItems[2].Text;
                string IDCLIENTCommande = element.SubItems[1].Text;
                string PrixCommande = element.SubItems[4].Text;
                string AdresseCommande = element.SubItems[3].Text;

                string idObjet = "";
                string nbArticles = "";
                string dateLivraison = "";

                string infos = "ID COMMANDE : " + idCommande + "\n\nTYPE DE COMMANDE : " + typeCommande + "\n\nDATE COMMANDE : " + dateCommande + "\n\nCLIENT : " + IDCLIENTCommande;
                infos += "\n\nPRIX TOTAL : " + PrixCommande + "\n\nADRESSE DE LIVRAISON : " + AdresseCommande;
                if (DATABASE.Connected)
                {
                    if (typeCommande == "piece")
                    {
                        string requeteSQL = "SELECT * FROM ACHAT_PIECE WHERE ID_Commande=" + idCommande;
                        MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                        using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                        {
                            while (Lire.Read())
                            {
                                idObjet = Lire["Identifiant_Piece"].ToString();
                                nbArticles = Lire["NombreArticles"].ToString();
                                dateLivraison = Lire["DateLivraison"].ToString();
                            }
                        }
                    }
                    else
                    {
                        string requeteSQL = "SELECT * FROM ACHAT_BICYCLETTE WHERE ID_Commande=" + idCommande;
                        MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                        using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                        {
                            while (Lire.Read())
                            {
                                idObjet = Lire["ID_Bicyclette"].ToString();
                                nbArticles = Lire["NombreArticles"].ToString();
                                dateLivraison = Lire["DateLivraison"].ToString();
                            }
                        }
                    }

                    infos += "\n\nOBJET COMMANDE : " + idObjet + "\n\nNOMBRE d'ARTICLES : " + nbArticles + "\n\nDATE LIVRAISON : " + dateLivraison;
                }

                MessageBox.Show(infos, "Informations Commande");
            }
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string IdCommande = element.SubItems[0].Text;
                string Adresse = element.SubItems[3].Text;


                //Recuperation des valeurs du form ModifierPiece
                using (ModifierCommande modifier = new ModifierCommande())
                {
                    //Elements modifiables
                    modifier.Adresse = Adresse;

                    //Elements non modifiables
                    modifier.IdCommande = IdCommande;


                    //Si bouton cliqué = modifier
                    if (modifier.ShowDialog() == DialogResult.Yes)
                    {
                        if (DATABASE.Connected)
                        {
                            MySqlCommand requete = new MySqlCommand("UPDATE COMMANDE SET AdresseLivraison_Commande=@adresse WHERE ID_Commande=@id", DATABASE.MySqlConnection);
                            requete.Parameters.AddWithValue("@id", IdCommande);
                            requete.Parameters.AddWithValue("@adresse", modifier.Adresse);

                            requete.ExecuteNonQuery();
                            requete.Parameters.Clear();

                            element.SubItems[3].Text = modifier.Adresse;
                            MessageBox.Show("Commande mise à jour avec succès.");
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
                DialogResult dialogResult = MessageBox.Show("Êtes - vous sûr de vouloir supprimer définitivement cette Commande ? ", "Suppression Commande", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (DATABASE.Connected)// on verifie que la connexion est bien effective
                    {

                        ListViewItem element = listView1.SelectedItems[0];
                        string idCommande = element.SubItems[0].Text;
                        MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM COMMANDE WHERE ID_Commande=@id", DATABASE.MySqlConnection);
                        mySqlCommand.Parameters.AddWithValue("@id", idCommande);
                        mySqlCommand.ExecuteNonQuery();
                        element.Remove();
                        mySqlCommand.Parameters.Clear();

                        MessageBox.Show("Commande supprimée avec succès.");
                    }
                    else { MessageBox.Show("Erreur de connexion avec la base de données lors de la tentative de suppression du fournisseur"); }
                }
            }
        }

        private void MenuCommande_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = "MOYENNE DU MONTANT DES COMMANDES : ";
            double somme = 0;
            for (int i = 0; i < listView1.SelectedItems.Count; i++) somme += Convert.ToInt32(listView1.Items[i].SubItems[4].Text);
            somme = somme / listView1.SelectedItems.Count;
            result += somme.ToString();
        }
    }
}
