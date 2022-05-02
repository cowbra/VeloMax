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
    public partial class MenuPiece : Form
    {
        BDD DATABASE = new BDD();
        public MenuPiece()
        {
            InitializeComponent();
            DATABASE.Connect();
            Actualiser();
        }

        private void Actualiser(string requeteSQL = "SELECT * FROM PIECE")
        {
            /// <summary>
            /// Méthode qui nous permet d'actualiser les fournisseurs 
            /// </summary>
            /// 
            #region requetes_Filtrees
            int compteur = 0;
            if (checkBox1.Checked|| checkBox2.Checked || checkBox3.Checked || checkBox4.Checked || checkBox5.Checked || checkBox6.Checked || checkBox7.Checked || checkBox8.Checked || checkBox9.Checked || checkBox10.Checked || checkBox11.Checked || checkBox12.Checked)
            {
                requeteSQL += " WHERE Description_Piece=";
                if (checkBox1.Checked)
                {
                    requeteSQL += "'Cadre'";
                    compteur ++;
                }
                if (checkBox2.Checked)
                {
                    if (compteur>0) requeteSQL += " OR Description_Piece='Guidon'";
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


            List<string> idPIECE = new List<string>();
            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string id = Lire["Identifiant_Piece"].ToString();
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                        idPIECE.Add(id);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string type = Lire["Description_Piece"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string date1 = Lire["DateDebut_Piece"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string date2 = Lire["DateFin_Piece"].ToString();

                        listView1.Items.Add(new ListViewItem(new[] { id, type, date1, date2,null}));
                    }
                }


                int taille = listView1.Items.Count;
                string requiem = "SELECT SUM(Quantite_Fournisseur) FROM FOURNIT WHERE Identifiant_Piece=@id";
                MySqlCommand requete = new MySqlCommand(requiem, DATABASE.MySqlConnection);

                for (int i = 0; i < taille; i++)
                {
                    requete.Parameters.AddWithValue("@id", idPIECE[i]);

                    listView1.Items[i].SubItems[4].Text = Convert.ToString((decimal)(requete.ExecuteScalar()));
                    requete.Parameters.Clear();
                }
            }
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
    }
}
