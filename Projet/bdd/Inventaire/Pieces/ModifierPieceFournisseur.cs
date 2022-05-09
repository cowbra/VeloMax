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
    public partial class ModifierPieceFournisseur : Form
    {
        public ModifierPieceFournisseur()
        {
            InitializeComponent();
        }

        private void ModifierPieceFournisseur_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            double j;
            int k;
            int l;
            if (textBox1.Text == "" || Int32.TryParse(textBox1.Text, out i) == false) MessageBox.Show("Entrez un N° de produit dans le catalogue fournisseur valide !");
            else if (Convert.ToInt64(textBox1.Text) < 0 ) MessageBox.Show("Entrez un N° de produit dans le catalogue fournisseur valide !");
            else if (textBox2.Text == "" || Double.TryParse(textBox2.Text, out j) == false) MessageBox.Show("Entrez un prix valide !");
            else if (Convert.ToDouble(textBox2.Text) < 0) MessageBox.Show("Entrez un prix valide !");
            else if (textBox3.Text == "" || Int32.TryParse(textBox3.Text, out k) == false) MessageBox.Show("Entrez une quantité valide !");
            else if (Convert.ToInt64(textBox3.Text) < 0) MessageBox.Show("Entrez une quantité valide !");
            else if (textBox4.Text == "" || Int32.TryParse(textBox4.Text, out l) == false) MessageBox.Show("Entrez un délai valide !");
            else if (Convert.ToInt64(textBox4.Text) < 0) MessageBox.Show("Entrez un délai valide !");

            else { DialogResult = DialogResult.Yes; }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        public string IdPiece { set { label5.Text = value; } }
        public string Siret { set { label7.Text = value; } }
        public string IdCatalogue { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string Prix { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string Quantite { get { return textBox3.Text; } set { textBox3.Text = value; } }
        public string Delai { get { return textBox4.Text; } set { textBox4.Text = value; } }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
