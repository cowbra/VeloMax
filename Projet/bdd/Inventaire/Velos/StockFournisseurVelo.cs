using MySql.Data.MySqlClient;

namespace bdd
{
    public partial class StockFournisseurVelo : Form
    {
        BDD DATABASE = new BDD();
        public StockFournisseurVelo(string id)
        {
            InitializeComponent();
            DATABASE.Connect();
            Fill(id);
            label2.Text = id;
        }

        private void StockFournisseurVelo_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DATABASE.Disconnect();
            this.Close();
        }

        private void Fill(string id)
        {
            string requeteSQL = "SELECT Siret_Fournisseur,Nom_Fournisseur,NumProduit_Fournisseur,Prix_Fournisseur,Quantite_Fournisseur,Delai_Fournisseur FROM FOURNIT WHERE Identifiant_Piece ='" + id + "'";

            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string SIRET = Lire["Siret_Fournisseur"].ToString();
                        string NOM_F = Lire["Nom_Fournisseur"].ToString();
                        string NUM_CATALOGUE = Lire["NumProduit_Fournisseur"].ToString();
                        string PRIX = Lire["Prix_Fournisseur"].ToString();
                        string QUANTITE = Lire["Quantite_Fournisseur"].ToString();
                        string DELAI = Lire["Delai_Fournisseur"].ToString();

                        listView1.Items.Add(new ListViewItem(new[] { SIRET, NOM_F, NUM_CATALOGUE, PRIX, QUANTITE, DELAI }));
                    }
                }
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void modifierLassociationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string IdPiece = label2.Text;
                string Siret = element.SubItems[0].Text;
                string IdCatalogue = element.SubItems[2].Text;
                string Prix = element.SubItems[3].Text;
                string Quantite = element.SubItems[4].Text;
                string Delai = element.SubItems[5].Text;

                //Recuperation des valeurs du form ModifierPiece
                using (ModifierPieceFournisseur modifier = new ModifierPieceFournisseur())
                {
                    //Elements modifiables
                    modifier.IdCatalogue = IdCatalogue;
                    modifier.Prix = Prix;
                    modifier.Quantite = Quantite;
                    modifier.Delai = Delai;

                    //Elements non modifiables
                    modifier.IdPiece = IdPiece;
                    modifier.Siret = Siret;

                    //Si bouton cliqué = modifier
                    if (modifier.ShowDialog() == DialogResult.Yes)
                    {
                        if (DATABASE.Connected)
                        {
                            MySqlCommand requete = new MySqlCommand("UPDATE FOURNIT SET NumProduit_Fournisseur=@catalogue, Prix_Fournisseur=@prix, Quantite_Fournisseur=@quantite, Delai_Fournisseur=@delai WHERE Siret_Fournisseur=@siret AND Identifiant_Piece=@id", DATABASE.MySqlConnection);
                            requete.Parameters.AddWithValue("@id", IdPiece);
                            requete.Parameters.AddWithValue("@siret", Siret);
                            requete.Parameters.AddWithValue("@catalogue", Convert.ToInt16(modifier.IdCatalogue));
                            requete.Parameters.AddWithValue("@prix", Convert.ToDouble(modifier.Prix));
                            requete.Parameters.AddWithValue("@quantite", Convert.ToInt16(modifier.Quantite));
                            requete.Parameters.AddWithValue("@delai", Convert.ToInt16(modifier.Delai));
                            requete.ExecuteNonQuery();
                            requete.Parameters.Clear();

                            element.SubItems[2].Text = modifier.IdCatalogue;
                            element.SubItems[3].Text = modifier.Prix;
                            element.SubItems[4].Text = modifier.Quantite;
                            element.SubItems[5].Text = modifier.Delai;
                            MessageBox.Show("Lien mis à jour avec succès.");
                        }
                        else { MessageBox.Show("Erreur de connexion avec la base de données."); }
                    }
                    else { MessageBox.Show("Modification annulée."); }
                }

            }
        }
    }
}
