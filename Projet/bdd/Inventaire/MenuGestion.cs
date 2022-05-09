namespace bdd
{
    public partial class MenuGestion : Form
    {
        public MenuGestion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MenuPiece menuPiece = new MenuPiece();
            this.Hide();
            menuPiece.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuVelo menuVelo = new MenuVelo();
            this.Hide();
            menuVelo.Show();
        }

        private void MenuGestion_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MenuVelo menuVelo = new MenuVelo();
            this.Hide();
            menuVelo.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MenuPiece menuPiece = new MenuPiece();
            this.Hide();
            menuPiece.Show();
        }
    }
}
