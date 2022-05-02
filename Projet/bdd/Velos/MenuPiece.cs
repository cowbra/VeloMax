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
                //string quantite = "";
                //oh oui
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

        private void MenuPiece_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }
}
