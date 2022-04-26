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

namespace ProblemeTransConnect
{
    public partial class Modifier : Form
    {/// <summary>
     /// Classe permettant de mettre à jour un client existant
     /// </summary>

        MySqlConnection mySqlConnection;
        bool connected = false;
        public Modifier()
        {
            InitializeComponent();
            EtablirConnection();
        }
        private void EtablirConnection()
        {//on établit la connexion à la BDD
            if (!connected)
            {
                mySqlConnection = new MySqlConnection("SERVER=93.10.83.45;PORT=3306;DATABASE=ProblemeCsharp;UID=problemecsharp;PWD=Hugoemma1320!;");
                try
                {
                    if (mySqlConnection.State == ConnectionState.Closed) mySqlConnection.Open();
                    connected = true;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        public string Secu { set {  textBox1.Text =value; } }
        public string Nom { get { return textBox3.Text; } set { textBox3.Text = value; } }
        public string Prenom { set { textBox2.Text = value; } }
        public string Adresse { get { return textBox5.Text; } set { textBox5.Text = value; } }
        public string DateNaiss { set { textBox6.Text = value; } }
        public string Email { get { return textBox4.Text; } set { textBox4.Text = value; } }
        public string Telephone { get { return textBox9.Text; } set { textBox9.Text = value; } }
        public string Montant { get { return textBox8.Text; } set { textBox8.Text = value; } }
        
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void NewClient_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
