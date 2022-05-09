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
    }
}
