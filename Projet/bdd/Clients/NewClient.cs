using MySql.Data.MySqlClient;

namespace bdd
{
    public partial class NewClient : Form
    {
        BDD DATABASE = new BDD();
        public NewClient()
        {
            InitializeComponent();
            DATABASE.Connect();
        }

        private void NewClient_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
        }
        private void ShowFidelio()
        {
            /// <summary>
            /// Méthode qui nous permet d'actualiser les fournisseurs 
            /// </summary>
            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM FIDELIO", DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string ID = Lire["NumProgramme_Fidelio"].ToString();
                        string DESC = Lire["Description_Fidelio"].ToString();
                        string COUT = Lire["Cout_Fidelio"].ToString();
                        string DUREE = Lire["Duree_Fidelio"].ToString();
                        string RABAIS = Lire["Rabais_Fidelio"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { ID, DESC, COUT, DUREE, RABAIS }));
                    }
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = radioButton1.Checked;
            groupBox2.Enabled = radioButton1.Checked;
            ShowFidelio();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBox1.SelectedItem.Equals("Particulier"))
            {
                label5.Visible = true;
                label5.Text = "Prénom :";
                label7.Visible = true;
                label7.Text = "Programme Fidélio ?";
                textBox5.Enabled = true;
                textBox6.Enabled = false;
                textBox5.Visible = true;
                textBox6.Visible = false;

                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
            }

            else if (listBox1.SelectedItem.Equals("Entreprise"))
            {
                label5.Visible = true;
                label5.Text = "Nom Compagnie :";
                label7.Visible = true;
                label7.Text = "Remise Compagnie (en %) :";
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox5.Visible = true;
                textBox6.Visible = true;

                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
            }
            else
            {
                label5.Visible = false;
                label7.Visible = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox5.Visible = false;
                textBox6.Visible = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = !radioButton2.Checked;
            groupBox2.Enabled = !radioButton2.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DATABASE.Disconnect();
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //textBox8.Text = dateTimePicker1.Value.ToString();
            string[] subsDate = dateTimePicker1.Value.ToShortDateString().Split('/');
            string dateTrans = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            textBox8.Text = dateTrans;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool problem = true;
            string[] ListFidelio = { "1", "2", "3", "4" };
            int i;
            int j;
            if (listBox1.SelectedIndex == 0) MessageBox.Show("Double-cliquez sur le type du client pour le sélectionner.");
            else if (listBox1.SelectedItem.Equals("Particulier"))
            {
                if (textBox5.Text == "") MessageBox.Show("Entrez le Prénom du client!");
                else if (!radioButton1.Checked && !radioButton2.Checked) MessageBox.Show("Indiquez si le client adhère à un programme Fidélio !");
                if (radioButton1.Checked)
                {
                    if (textBox7.Text == "" || int.TryParse(textBox7.Text, out i) == false) MessageBox.Show("Entrez un N° Fidélio valide ! (1,2,3 ou 4)");
                    else if (ListFidelio.Contains(textBox7.Text) == false) MessageBox.Show("Entrez un N° Fidélio valide ! (1,2,3 ou 4)");
                    else if (textBox8.Text == "") MessageBox.Show("Entrez la date d'inscription au programme Fidélio du client!");
                    else problem = false;
                }
                if (radioButton2.Checked && textBox5.Text != "") problem = false;
            }

            else if (listBox1.SelectedItem.Equals("Entreprise"))
            {
                if (textBox5.Text == "") MessageBox.Show("Entrez le Nom de la Compagnie !");
                else if (textBox6.Text == "") MessageBox.Show("Entrez le pourcentage de remise de la Compagnie !");
                else if (int.TryParse(textBox6.Text, out j) == false) MessageBox.Show("Entrez un pourcentage de remise valide !");
                else if (Convert.ToInt16(textBox6.Text) < 0 || Convert.ToInt16(textBox6.Text) > 100) MessageBox.Show("Entrez un pourcentage de remise valide !");
                else { problem = false; }
            }
            if (textBox1.Text == "") MessageBox.Show("Entrez le N° de téléphone du client!");
            else if (textBox2.Text == "") MessageBox.Show("Entrez l'E-mail du client !");
            else if (textBox3.Text == "") MessageBox.Show("Entrez l'Adresse du client !");
            else if (textBox4.Text == "") MessageBox.Show("Entrez le Nom du client !");
            else
            {
                if (!problem)
                {
                    Client? c = null;
                    #region Creation client Particulier
                    // Si le client est un particulier
                    if (listBox1.SelectedItem.Equals("Particulier"))
                    {
                        // Si le client a un programme fidelio
                        if (radioButton1.Checked)
                        {
                            c = new ClientParticulier(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, Convert.ToInt16(textBox7.Text), textBox8.Text);
                        }
                        // Sinon créé un client particulier sans programme de remise
                        else
                        {
                            c = new ClientParticulier(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                        }
                    }
                    #endregion

                    #region Creation client Entreprise
                    else if (listBox1.SelectedItem.Equals("Entreprise"))
                    {
                        c = new ClientEntreprise(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, Convert.ToDouble(textBox6.Text) / 100);
                    }
                    #endregion

                    if (c != null && c.AddToBdd())
                    {
                        MessageBox.Show("Client créé avec succès !");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Erreur de Connexion avec la Base de données.");
                        // On laisse la fenetre de creation de Client ouverte pour retenter une connexion à la bdd
                    }

                }
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
