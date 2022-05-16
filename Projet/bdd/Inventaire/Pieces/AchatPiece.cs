using MySql.Data.MySqlClient;


namespace bdd
{
    public partial class AchatPiece : Form
    {
        BDD DATABASE = new BDD();
        public AchatPiece(string id)
        {
            InitializeComponent();
            DATABASE.Connect();
            label4.Text = id;

            if (DATABASE.Connected)
            {
                string reqSQL = "SELECT Siret_Fournisseur FROM FOURNISSEUR WHERE Siret_Fournisseur NOT IN (SELECT Siret_Fournisseur FROM FOURNIT WHERE Identifiant_Piece=@id) ORDER BY Libelle_Fournisseur";
                MySqlCommand mySqlCommand = new MySqlCommand(reqSQL, DATABASE.MySqlConnection);
                mySqlCommand.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        comboBox2.Items.Add(Lire["Siret_Fournisseur"].ToString());
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            int j;
            int k;
            double l;
            if (comboBox2.SelectedItem == null) MessageBox.Show("Veuillez sélectionner l'ID du client !");
            else if (textBox3.Text == "") MessageBox.Show("Entrez le N° de produit dans le catalogue du Fournisseur !");
            else if (textBox4.Text == "") MessageBox.Show("Entrez le prix unitaire d'une pièce !");
            else if (textBox8.Text == "") MessageBox.Show("Entrez la quantité de pièces fournies !");
            else if (textBox5.Text == "") MessageBox.Show("Entrez le délai d'approvisionnement !");

            else if (int.TryParse(textBox8.Text, out i) == false) MessageBox.Show("Entrez une quantité valide !");
            else if (Convert.ToInt32(textBox8.Text) <= 0) MessageBox.Show("Entrez une quantité valide !");

            else if (int.TryParse(textBox3.Text, out j) == false) MessageBox.Show("Entrez N° de catalogue valide !");
            else if (Convert.ToInt32(textBox3.Text) <= 0) MessageBox.Show("Entrez N° de catalogue valide !");

            else if (int.TryParse(textBox5.Text, out k) == false) MessageBox.Show("Entrez un délai d'approvisionnement valide !");
            else if (Convert.ToInt32(textBox5.Text) <= 0) MessageBox.Show("Entrez un délai d'approvisionnement valide !");

            else if (double.TryParse(textBox5.Text, out l) == false) MessageBox.Show("Entrez un prix valide !");
            else if (Convert.ToDouble(textBox5.Text) <= 0) MessageBox.Show("Entrez un prix valide !");

            else
            {
                Int64 siret = Convert.ToInt64(comboBox2.SelectedItem.ToString());
                string idPiece = label4.Text;
                int quantite = Convert.ToInt32(textBox8.Text);
                int delai = Convert.ToInt32(textBox5.Text);
                int numFour = Convert.ToInt32(textBox3.Text);
                double prix = Convert.ToDouble(textBox4.Text);

                Fourni lien = new Fourni(siret, idPiece, quantite, delai, prix, numFour);
                if (lien.AddToBdd())
                {
                    MessageBox.Show("Pièce reliée au fournisseur avec succès !");
                    DATABASE.Disconnect();
                    this.Close();
                }
                // On laisse la fenetre de creation de fournisseur ouverte pour retenter une connexion à la bdd
                else MessageBox.Show("Erreur de Connexion avec la Base de données.");
            }
        }

        private void AchatPiece_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            DATABASE.Disconnect();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
