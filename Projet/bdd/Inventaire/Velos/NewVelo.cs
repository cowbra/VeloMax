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
            loadData();
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
            listBox3.SelectedIndex = 0;
            listBox4.SelectedIndex = 0;
            listBox5.SelectedIndex = 0;
            listBox6.SelectedIndex = 0;
            listBox7.SelectedIndex = 0;
            listBox8.SelectedIndex = 0;
            listBox9.SelectedIndex = 0;
            listBox10.SelectedIndex = 0;
            listBox11.SelectedIndex = 0;
            listBox12.SelectedIndex = 0;
            listBox13.SelectedIndex = 0;
            listBox14.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadData()
        {
            string sql = "SELECT Identifiant_Piece FROM PIECE WHERE Description_Piece=";
        #region SELECTIONS PIECES
            // Remplissage des listbox avec les id des pieces du type correspondant depuis la bdd
            #region Selection Cadre
            MySqlCommand mySqlCommand = new MySqlCommand(sql+"'Cadre'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox2.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Guidon
            mySqlCommand = new MySqlCommand(sql + "'Guidon'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox3.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Roue Avant
            mySqlCommand = new MySqlCommand(sql + "'Roue Avant'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox4.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Roue Arriere
            mySqlCommand = new MySqlCommand(sql + "'Roue Arrière'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox5.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Pedalier
            mySqlCommand = new MySqlCommand(sql + "'Pédalier'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox6.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Freins
            mySqlCommand = new MySqlCommand(sql + "'Freins'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox7.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Derailleur Avant
            mySqlCommand = new MySqlCommand(sql + "'Dérailleur Avant'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox8.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Derailleur Arriere
            mySqlCommand = new MySqlCommand(sql + "'Dérailleur Arrière'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox9.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Reflecteurs
            mySqlCommand = new MySqlCommand(sql + "'Réflecteurs'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox10.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Selle
            mySqlCommand = new MySqlCommand(sql + "'Selle'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox11.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection ordinateur 
            mySqlCommand = new MySqlCommand(sql + "'Ordinateur'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox12.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
            #region Selection Panier
            mySqlCommand = new MySqlCommand(sql + "'Panier'", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
                    listBox13.Items.Add(Lire["Identifiant_Piece"].ToString());
                }
            }
            #endregion
        #endregion

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox11_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox13_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string[] subsDate = dateTimePicker1.Value.ToShortDateString().Split('/');
            string dateTrans = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            textBox3.Text = dateTrans;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string[] subsDate = dateTimePicker1.Value.ToShortDateString().Split('/');
            string dateTrans = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            textBox4.Text = dateTrans;
        }

        private void listBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
