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
    public partial class NewVille : Form
    {
        /// <summary>
        /// Classe permettant de relier 2 villes entre elles 
        /// </summary>
        MySqlConnection mySqlConnection;
        bool connected = false;
        public NewVille()
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

        private void NewVille_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mySqlConnection.Close();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") MessageBox.Show("Entrez une ville de départ !");
            else if (textBox2.Text == "") MessageBox.Show("Entrez une ville d'arrivée !");
            else if (textBox3.Text == "") MessageBox.Show("Entrez une distance en Km !");
            else if (textBox4.Text == "") MessageBox.Show("Entrez un temps en minutes !");
            else
            {
                if (connected)
                {
                    MySqlCommand requete = new MySqlCommand("INSERT INTO Villes(villeA,villeB,distance,temps_trajet) VALUES(@villeA,@villeB,@distance,@temps_trajet)", mySqlConnection);
                    requete.Parameters.AddWithValue("@villeA", textBox1.Text);
                    requete.Parameters.AddWithValue("@villeB", textBox2.Text);
                    requete.Parameters.AddWithValue("@distance", Convert.ToInt32(textBox3.Text));
                    requete.Parameters.AddWithValue("@temps_trajet", Convert.ToInt32(textBox4.Text));

                    requete.ExecuteNonQuery();
                    requete.Parameters.Clear();

                    
                    mySqlConnection.Close();
                    MessageBox.Show("Villes ajoutées avec succès !");
                    this.Close(); 
                    
                }
            }
        }
    }
}
