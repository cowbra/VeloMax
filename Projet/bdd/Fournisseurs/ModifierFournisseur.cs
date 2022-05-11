namespace bdd
{
    public partial class ModifierFournisseur : Form
    {
        public ModifierFournisseur()
        {
            InitializeComponent();
        }

        private void ModifierFournisseur_Load(object sender, EventArgs e)
        {

        }

        public string Siret { set { label2.Text = value; } }
        public string Nom { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string Contact { get { return textBox2.Text; } set { textBox2.Text = value; } }

        public string Adresse { get { return textBox3.Text; } set { textBox3.Text = value; } }
        public string Libelle { get { return textBox4.Text; } set { textBox4.Text = value; } }


        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(textBox4.Text, out i) == false) MessageBox.Show("Entrez un libelle valide (entre 1 et 4)!");
            else if (Convert.ToInt16(textBox4.Text) < 1 || Convert.ToInt16(textBox4.Text) > 4) MessageBox.Show("Entrez un libelle valide (entre 1 et 4)!");
            else DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
