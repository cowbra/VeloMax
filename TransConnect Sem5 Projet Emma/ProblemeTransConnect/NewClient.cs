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
    public partial class NewClient : Form
    {
        /// <summary>
        /// Classe permettant de créer un nouveau client
        /// </summary>
        MySqlConnection mySqlConnection;
        bool connected = false;
        public NewClient()
        {
            InitializeComponent();
            EtablirConnection();
        }
        private void EtablirConnection()
        {
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
        /// <summary>
        /// Méthode récupérant les strings des textbox et qui ajoute le client à la BDD
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "") MessageBox.Show("Entrez un N° de Sécurité Sociale !");
            else if (textBox3.Text == "") MessageBox.Show("Entrez un Nom !");
            else if (textBox2.Text == "") MessageBox.Show("Entrez un Prénom !");
            else if (textBox5.Text == "") MessageBox.Show("Entrez une Adresse !");
            else if (textBox6.Text == "") MessageBox.Show("Entrez une Date de Naissance !");
            else if (textBox4.Text == "") MessageBox.Show("Entrez une Adresse mail !");
            else if (textBox9.Text == "") MessageBox.Show("Entrez un N° de téléphone !");
            else
            {
                string[] subsDate = textBox6.Text.Split('/');
                string dateTrans = subsDate[2] + subsDate[1] + subsDate[0];



                double montantTotal = 0;
                if (textBox8.Text != "") montantTotal = Convert.ToDouble(textBox8.Text);

                Client client1 = new Client(textBox1.Text, textBox2.Text, textBox3.Text, Convert.ToInt32(dateTrans), textBox5.Text, textBox4.Text, textBox9.Text, montantTotal);
                if (client1.AddToDataBase())
                {
                    MessageBox.Show("Client créé avec succès !");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur de Connexion avec la Base de données.");
                    // On laisse la fenetre de creation de clients ouverte pour retenter une connexion à la bdd
                }
            }

            
            

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
