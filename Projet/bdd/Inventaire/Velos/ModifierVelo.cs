namespace bdd
{
    public partial class ModifierVelo : Form
    {
        public ModifierVelo()
        {
            InitializeComponent();
        }

        private void ModifierVelo_Load(object sender, EventArgs e)
        {

        }


        public string Id { set { label5.Text = value; } }
        public string Nom { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string Prix { get { return textBox2.Text; } set { textBox2.Text = value; } }

        public string Date { get { return textBox3.Text; } set { textBox3.Text = value; } }




        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string[] subsDate = dateTimePicker1.Value.ToShortDateString().Split('/');
            string dateTrans = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            textBox3.Text = dateTrans;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double i;
            if (double.TryParse(textBox2.Text, out i) == false) MessageBox.Show("Entrez un Prix valide !");
            else if (Convert.ToDouble(textBox2.Text) <= 0) MessageBox.Show("Entrez un Prix valide !");
            else DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
