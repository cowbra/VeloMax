using MySql.Data.MySqlClient;


namespace bdd
{
    public partial class MenuStatistiques : Form
    {
        BDD DATABASE = new BDD();
        public MenuStatistiques()
        {
            InitializeComponent();
            DATABASE.Connect();
            Fill();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void MenuStatistiques_Load(object sender, EventArgs e)
        {

        }

        private void Fill()
        {
            string req1 = "SELECT Identifiant_Piece, SUM(NB_articles_Commande) as Quantite_vendue FROM ACHAT_PIECE NATURAL JOIN COMMANDE WHERE Type_Commande = 'piece' GROUP BY Identifiant_Piece order by Quantite_vendue DESC";
            string req2 = "SELECT ID_Bicyclette, SUM(NB_articles_Commande) as Quantite_vendue FROM ACHAT_BICYCLETTE NATURAL JOIN COMMANDE WHERE Type_Commande = 'bicyclette' GROUP BY ID_Bicyclette order by Quantite_vendue desc";
            string req3 = "select ID_Client,NumProgramme_Fidelio,DateFin_Fidelio from CLIENT where NumProgramme_Fidelio is not null ORDER by NumProgramme_Fidelio";

            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            MySqlCommand sql1 = new MySqlCommand(req1, DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = sql1.ExecuteReader())
            {
                while (Lire.Read())
                {
                    string id = Lire["Identifiant_Piece"].ToString();
                    string quantite = Lire["Quantite_vendue"].ToString();
                    listView1.Items.Add(new ListViewItem(new[] { id, quantite }));
                }
            }

            MySqlCommand sql2 = new MySqlCommand(req2, DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = sql2.ExecuteReader())
            {
                while (Lire.Read())
                {
                    string id = Lire["ID_Bicyclette"].ToString();
                    string quantite = Lire["Quantite_vendue"].ToString();
                    listView2.Items.Add(new ListViewItem(new[] { id, quantite }));
                }
            }

            MySqlCommand sql3 = new MySqlCommand(req3, DATABASE.MySqlConnection);
            using (MySqlDataReader Lire = sql3.ExecuteReader())
            {
                while (Lire.Read())
                {
                    string id = Lire["ID_Client"].ToString();
                    string Fidelio = Lire["NumProgramme_Fidelio"].ToString();
                    string dateFin = Lire["DateFin_Fidelio"].ToString();
                    listView3.Items.Add(new ListViewItem(new[] { Fidelio, id, dateFin }));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            DATABASE.Disconnect();
            this.Close();
            menu.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox1.Visible = true;
                groupBox2.Visible = true;

                groupBox4.Enabled = false;
                groupBox4.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox1.Visible = false;
                groupBox2.Visible = false;

                groupBox4.Enabled = true;
                groupBox4.Visible = true;
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
