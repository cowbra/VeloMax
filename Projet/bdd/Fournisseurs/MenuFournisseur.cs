using System.ComponentModel;
using MySql.Data.MySqlClient;

namespace bdd
{
    public partial class MenuFournisseur : Form
    {
        BDD DATABASE = new BDD();

        public MenuFournisseur()
        {
            InitializeComponent();
            DATABASE.Connect();
            Actualiser();
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
            NewFournisseur newf = new NewFournisseur();
            newf.ShowDialog();
            Actualiser();
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Êtes - vous sûr de vouloir supprimer définitivement ce Fournisseur ? Toutes les pièces qu'il vend seront supprimées de l'inventaire", "Suppression Fournisseur", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (DATABASE.Connected)// on verifie que la connexion est bien effective
                    {

                        ListViewItem element = listView1.SelectedItems[0];
                        string Siret = element.SubItems[0].Text;
                        MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM FOURNISSEUR WHERE Siret_Fournisseur=@siret", DATABASE.MySqlConnection);
                        mySqlCommand.Parameters.AddWithValue("@siret", Siret);
                        mySqlCommand.ExecuteNonQuery();
                        element.Remove();
                        mySqlCommand.Parameters.Clear();

                        MessageBox.Show("Fournisseur supprimé avec succès.");
                    }
                    else { MessageBox.Show("Erreur de connexion avec la base de données lors de la tentative de suppression du fournisseur"); }
                }
            }

        }

        private void Actualiser(string requeteSQL = "SELECT * FROM FOURNISSEUR")
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
                        string Siret = Lire["Siret_Fournisseur"].ToString();
                        string Nom = Lire["NomEntreprise_Fournisseur"].ToString();
                        string Contact = Lire["Contact_Fournisseur"].ToString();
                        string Adresse = Lire["Adresse_Fournisseur"].ToString();
                        string Libelle = Lire["Libelle_Fournisseur"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { Siret, Nom, Contact, Adresse, Libelle }));
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Actualiser();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MenuClient_Load(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
