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
    public partial class NewPiece : Form
    {
        BDD DATABASE = new BDD();
        // Liste pour verifier si la clé primaire est déjà enregistré dans la bdd
        List<string> listePieces = new List<string>();
        List<string> listeSiret = new List<string>();
        public NewPiece()
        {
            InitializeComponent();
            DATABASE.Connect();
            loadData();
        }
        private void loadData()
        {
            listeSiret.Clear();
            listePieces.Clear();
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT Identifiant_Piece FROM PIECE", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                    listePieces.Add(Lire["Identifiant_Piece"].ToString());
                    listBox2.Items.Add(Lire["Identifiant_Piece"].ToString());
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
                }
            }

            MySqlCommand mySqlCommand2 = new MySqlCommand("SELECT Siret_Fournisseur FROM FOURNISSEUR", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand2.ExecuteReader())
            {
                while (Lire.Read())
                {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                    listeSiret.Add(Lire["Siret_Fournisseur"].ToString());
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox2.Text == "") MessageBox.Show("Entrez le N° SIRET du Fournisseur !");
            else if (textBox1.Text == "" && radioButton2.Checked) MessageBox.Show("Entrez l'identifiant de la pièce créée !");
            else if (listBox1.SelectedIndex == 0 && radioButton2.Checked) MessageBox.Show("Sélectionnez le type de Pièce !");
            else if (listBox2.SelectedIndex == 0 && radioButton1.Checked) MessageBox.Show("Sélectionnez l'identifiant du modèle de Pièce !");
            else if (textBox6.Text == "Date1" && radioButton2.Checked) MessageBox.Show("Entrez la date d'introduction de la pièce sur le marché !");
            else if (textBox7.Text == "Date2" && radioButton2.Checked) MessageBox.Show("Entrez la date de fin de production de la pièce !");
            else if (textBox3.Text == "") MessageBox.Show("Entrez l'identifiant du produit dans le catalogue Fournisseur !");
            else if (textBox4.Text == "") MessageBox.Show("Entrez le prix unitaire de la pièce !");
            else if (textBox5.Text == "") MessageBox.Show("Entrez le délai d'approvisionnement de la pièce via ce fournisseur !");
            else if (textBox8.Text == "") MessageBox.Show("Entrez la quantité de pièces fournies via ce fournisseur !");

            else
            {
                double i;//pour verifier format prix
                int j;//pour verifier format quantite
                int k;//pour verifier format delai
                int h;//pour verifier format fournisseur
                Int64 l;//pour verifier format quantite
                if (listePieces.Contains(textBox1.Text) && radioButton2.Checked) MessageBox.Show("Cet identifiant de pièce existe déjà !");
                
                else if (Int64.TryParse(textBox2.Text, out l) == false) MessageBox.Show("Entrez un SIRET valide !");
                else if (Convert.ToDouble(textBox2.Text) < 0) MessageBox.Show("Entrez un SIRET valide !");

                else if (double.TryParse(textBox4.Text, out i) == false) MessageBox.Show("Entrez un prix valide !");
                else if (Convert.ToDouble(textBox4.Text)<0) MessageBox.Show("Entrez un prix valide !");

                else if (int.TryParse(textBox8.Text, out j) == false) MessageBox.Show("Entrez une quantité valide !");
                else if (Convert.ToInt32(textBox8.Text) <= 0) MessageBox.Show("Entrez une quantité valide !");

                else if (int.TryParse(textBox3.Text, out h) == false) MessageBox.Show("Entrez un identifiant du produit dans le catalogue Fournisseur valide !");
                else if (Convert.ToInt32(textBox3.Text) <= 0) MessageBox.Show("Entrez un identifiant du produit dans le catalogue Fournisseur valide !");

                else if (int.TryParse(textBox5.Text, out k) == false) MessageBox.Show("Entrez un délai d'approvisionnement valide !");
                else if (Convert.ToInt32(textBox5.Text) < 0) MessageBox.Show("Entrez un délai d'approvisionnement valide !");

                else if (!listeSiret.Contains(textBox2.Text))
                {
                    DialogResult dialogResult = MessageBox.Show("Ce N° SIRET ne correpond à aucun fournisseur ! En créer un nouveau ?", "Fournisseur introuvable", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        NewFournisseur newf = new NewFournisseur();
                        newf.ShowDialog();
                        loadData();
                    }
                }

                else
                {
                    Int64 siret = Convert.ToInt64(textBox2.Text);
                    int quantite = Convert.ToInt32(textBox8.Text);
                    int delai = Convert.ToInt32(textBox5.Text);
                    int numFour = Convert.ToInt32(textBox3.Text);
                    double prix = Convert.ToDouble(textBox4.Text);
                    string idPiece= listBox2.Text;
                    
                    if (radioButton2.Checked)
                    {
                        Piece piece = new Piece(textBox1.Text, listBox1.Text, textBox6.Text, textBox7.Text);
                        if (piece.AddToBdd()) MessageBox.Show("Pièce créée avec succès !");
                        else MessageBox.Show("Erreur de Connexion avec la Base de données.");
                        idPiece = textBox1.Text.ToUpper();
                    }

                    string req = "SELECT COUNT(1) FROM FOURNIT WHERE Siret_Fournisseur = @siret AND Identifiant_Piece = @id";
                    MySqlCommand requete = new MySqlCommand(req, DATABASE.MySqlConnection);
                    requete.Parameters.AddWithValue("@siret", siret);
                    requete.Parameters.AddWithValue("@id", idPiece);
                    string resultat = "";
                    using (MySqlDataReader Lire = requete.ExecuteReader())
                    {
                        while (Lire.Read())
                        {
                            resultat = Lire.GetString(0);
                        }
                    }
                    if (Convert.ToInt32(resultat) >0) MessageBox.Show("Ce fournisseur vend déjà cette pièce !");
                    else
                    {
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
            }


            if (listePieces.Contains(textBox1.Text) && radioButton2.Checked){
                MessageBox.Show("Cet identifiant de pièce existe déjà !");
            }

        }

        private void NewPiece_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked=false;

                textBox1.Enabled=true;
                textBox1.Visible = true;

                label2.Visible=true;
                listBox1.Enabled=true;
                listBox1.Visible = true;

                listBox2.Enabled = false;
                listBox2.Visible = false;

                label3.Visible = true;
                label3.Enabled = true;
                label5.Visible = true;
                label5.Enabled = true;

                dateTimePicker1.Enabled=true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Enabled = true;
                dateTimePicker2.Visible = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;

                textBox1.Enabled = false;
                textBox1.Visible = false;

                label2.Visible = false;
                listBox1.Enabled = false;
                listBox1.Visible = false;

                listBox2.Enabled = true;
                listBox2.Visible = true;

                label3.Visible = false;
                label3.Enabled = false;
                label5.Visible = false;
                label5.Enabled = false;

                dateTimePicker1.Enabled = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker2.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //textBox8.Text = dateTimePicker1.Value.ToString();
            string[] subsDate = dateTimePicker1.Value.ToShortDateString().Split('/');
            string dateTrans = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            textBox6.Text = dateTrans;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //textBox8.Text = dateTimePicker1.Value.ToString();
            string[] subsDate = dateTimePicker1.Value.ToShortDateString().Split('/');
            string dateTrans = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            textBox7.Text = dateTrans;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
