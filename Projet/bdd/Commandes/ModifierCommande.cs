﻿namespace bdd
{
    public partial class ModifierCommande : Form
    {
        public ModifierCommande()
        {
            InitializeComponent();
        }

        private void ModifierCommande_Load(object sender, EventArgs e)
        {

        }

        public string IdCommande { set { label2.Text = value; } }
        public string Adresse { get { return textBox1.Text; } set { textBox1.Text = value; } }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
