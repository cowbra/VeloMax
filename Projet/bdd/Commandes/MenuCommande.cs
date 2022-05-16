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
    public partial class MenuCommande : Form
    {
        BDD DATABASE = new BDD();
        public MenuCommande()
        {
            InitializeComponent();
            DATABASE.Connect();
            Actualiser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewCommande commande = new NewCommande();
            commande.ShowDialog();
            Actualiser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            DATABASE.Disconnect();
            this.Close();
            menu.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
         private void Actualiser()
        {
            /// <summary>
            /// Méthode qui nous permet d'actualiser les commandes 
            /// </summary>
            /// 

            string requeteSQL = "SELECT * FROM COMMANDE";
            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string Id = Lire["ID_Commande"].ToString();
                        string Date = Lire["Date_Commande"].ToString();
                        string Adresse = Lire["AdresseLivraison_Commande"].ToString();
                        string IdClient = Lire["ID_Client"].ToString();
                        string Prix = Lire["Prix_Commande"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { Id, IdClient, Date, Adresse, Prix }));
                    }
                }
            }
        }
    }
}
