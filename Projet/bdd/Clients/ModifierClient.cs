namespace bdd
{
    public partial class ModifierClient : Form
    {
        public ModifierClient()
        {
            InitializeComponent();
        }

        private void ModifierClient_Load(object sender, EventArgs e)
        {

        }

        public string IdClient { set { label6.Text = value; } }
        public string TypeClient { set { label5.Text = value; } }
        public string NomClient { set { label4.Text = value; } }

        public string Tel { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string Mail { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string Adresse { get { return textBox3.Text; } set { textBox3.Text = value; } }

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
