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
    public partial class NewVelo : Form
    {
        BDD DATABASE = new BDD();
        public NewVelo()
        {
            InitializeComponent();
            DATABASE.Connect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT Identifiant_Piece FROM PIECE", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                    listBox2.Items.Add(Lire["Identifiant_Piece"].ToString());
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
                }
            }
        }
    }
}
