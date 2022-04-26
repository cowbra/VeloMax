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
    public partial class ModifierCommande : Form
    {
        /// <summary>
        /// Classe permettant de mettre à jour une commande existante
        /// </summary>
        MySqlConnection mySqlConnection;
        bool connected = false;
        public ModifierCommande()
        {
            InitializeComponent();
            EtablirConnection();
        }
        private void EtablirConnection()
        {//On établit la connxion à la BDD
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
        public string NCommande { set {  textBox1.Text =value; } }
        public string Date { get { return textBox3.Text; } set { textBox3.Text = value; } }

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
