using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            if (comboBox2.SelectedItem == null) MessageBox.Show("Veuillez sélectionner l'ID du client !");

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
