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
    public partial class NewFournisseur : Form
    {
        BDD DATABASE = new BDD();
        // Liste pour verifier si la clé primaire est déjà enregistré dans la bdd
        List<string> listeSiret = new List<string>();
        public NewFournisseur()
        {
            InitializeComponent();
            DATABASE.Connect();
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT Siret_Fournisseur FROM FOURNISSEUR", DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
            {
                while (Lire.Read())
                {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                    listeSiret.Add(Lire["Siret_Fournisseur"].ToString());
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
                }
            }
        }

        private void NewFournisseur_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] Listlibelle = { "1","2","3","4"};
            long i;
            if (textBox1.Text == "" || Int64.TryParse(textBox1.Text, out i)==false) MessageBox.Show("Entrez le N° Siret de l'entreprise !");
            else if (textBox1.Text.Length<14||Convert.ToInt64(textBox1.Text) <0 || Convert.ToInt64(textBox1.Text)>99999999999999) MessageBox.Show("Entrez un N° de Siret valide (14 chiffres) !");
            else if (textBox2.Text == "") MessageBox.Show("Entrez le nom de l'entreprise !");
            else if (textBox3.Text == "") MessageBox.Show("Entrez le contact de l'entreprise !");
            else if (textBox4.Text == "") MessageBox.Show("Entrez l'Adresse de l'entreprise !");
            else if (textBox5.Text == "" || Listlibelle.Contains(textBox5.Text) == false) MessageBox.Show("Entrez le libelle de l'entreprise ! (1,2,3 ou 4)");
            else if (listeSiret.Contains(textBox1.Text))
            {
                MessageBox.Show("Ce numéro de SIRET est déjà enregistré !");
            }
            else
            {
                Fournisseur f = new Fournisseur(Convert.ToInt64(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                if (f.AddToBdd())
                {
                    MessageBox.Show("Fournisseur créé avec succès !");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur de Connexion avec la Base de données.");
                    // On laisse la fenetre de creation de fournisseur ouverte pour retenter une connexion à la bdd
                }

                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
