namespace bdd
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MenuGestion gestion = new MenuGestion();
            this.Hide();
            gestion.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MenuFournisseur fournisseur = new MenuFournisseur();
            this.Hide();
            fournisseur.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuClient client = new MenuClient();
            this.Hide();
            client.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            MenuCommande commande = new MenuCommande();
            this.Hide();
            commande.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MenuStatistiques stats = new MenuStatistiques();
            this.Hide();
            stats.Show();
        }
    }
}