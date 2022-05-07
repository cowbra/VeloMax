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

        private void Actualiser()
        {
            /// <summary>
            /// Méthode qui nous permet d'actualiser les fournisseurs 
            /// </summary>
            string requeteSQL = "SELECT Identifiant_Piece, Description_Piece, DateDebut_Piece, DateFin_Piece, SUM(Quantite_Fournisseur) AS Quantite_Total_Piece FROM PIECE NATURAL JOIN FOURNIT";

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

            requeteSQL += " GROUP BY Identifiant_Piece";
            //Requetes pour TRI
            #region TRI SQL
            if (checkBox22.Checked || checkBox23.Checked || checkBox24.Checked )
            {
                compteur = 0;
                requeteSQL += " ORDER BY ";
                if (checkBox24.Checked)
                {
                    requeteSQL += "Quantite_Total_Piece";
                    compteur++;
                }
                if (checkBox23.Checked)
                {
                    if (compteur>0) requeteSQL += ",DateDebut_Piece";
                    else requeteSQL += "DateDebut_Piece";
                    compteur++;
                }
                if (checkBox22.Checked)
                {
                    if (compteur > 0) requeteSQL += ",DateFin_Piece";
                    else requeteSQL += "DateFin_Piece";
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
                        string id = Lire["Identifiant_Piece"].ToString();
                        string type = Lire["Description_Piece"].ToString();
                        string date1 = Lire["DateDebut_Piece"].ToString();
                        string date2 = Lire["DateFin_Piece"].ToString(); 
                        string quantity = Lire["Quantite_Total_Piece"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { id, type, date1, date2, quantity }));
                    }
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
    }
}
