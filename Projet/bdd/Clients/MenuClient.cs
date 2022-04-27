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
    public partial class MenuClient : Form
    {
        BDD DATABASE = new BDD();
        public MenuClient()
        {
            InitializeComponent();
            DATABASE.Connect();
            Actualiser();
        }

        private void MenuClient_Load(object sender, EventArgs e)
        {

        }

        private void Actualiser(string requeteSQL = "SELECT * FROM CLIENT")
        {
            /// <summary>
            /// Méthode qui nous permet d'actualiser les fournisseurs 
            /// </summary>
            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {

#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string ID = Lire["ID_Client"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string Type = Lire["Type_Client"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string Tel = Lire["Tel_Client"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string EMAIL = Lire["Courriel_Client"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string Adresse = Lire["Adresse_Client"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string Nom = Lire["Nom_Client"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string Prenom = Lire["Prenom_Client"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string Fidelio = Lire["NumProgramme_Fidelio"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string Compagnie = Lire["NomCompagnie_Client"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string Remise = Lire["RemiseCompagnie_Client"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string DateDeb = Lire["DateDebut_Fidelio"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string DateFin = Lire["DateFin_Fidelio"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.


                        listView1.Items.Add(new ListViewItem(new[] { ID, Type, Tel, EMAIL, Adresse,Nom,Prenom,Fidelio,Compagnie,Remise,DateDeb,DateFin }));
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            DATABASE.Disconnect();
            this.Close();
            menu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewClient newc = new NewClient();
            newc.ShowDialog();
            Actualiser();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem element = listView1.SelectedItems[0];
                    string ID = element.SubItems[0].Text;
                    MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM CLIENT WHERE ID_Client=@id", DATABASE.MySqlConnection);
                    mySqlCommand.Parameters.AddWithValue("@id", ID);
                    mySqlCommand.ExecuteNonQuery();
                    element.Remove();
                    mySqlCommand.Parameters.Clear();
                }
                MessageBox.Show("Client supprimé avec succès.");
            }
            else { MessageBox.Show("Erreur de connexion avec la base de données lors de la tentative de suppression du client"); }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                Actualiser("SELECT * FROM CLIENT WHERE Type_Client = 'Particulier'");
            }
            else Actualiser();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                Actualiser("SELECT * FROM CLIENT WHERE Type_Client = 'Entreprise'");
            }
            else Actualiser();
        }
    }
}
