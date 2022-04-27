using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bdd
{
    public partial class NewFournisseur : Form
    {
        public NewFournisseur()
        {
            InitializeComponent();
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
            else if (textBox2.Text == "") MessageBox.Show("Entrez le nom de l'entreprise !");
            else if (textBox3.Text == "") MessageBox.Show("Entrez le contact de l'entreprise !");
            else if (textBox4.Text == "") MessageBox.Show("Entrez l'Adresse de l'entreprise !");
            else if (textBox5.Text == "" || Listlibelle.Contains(textBox5.Text) == false) MessageBox.Show("Entrez le libelle de l'entreprise ! (1,2,3 ou 4)");
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
    }
}
