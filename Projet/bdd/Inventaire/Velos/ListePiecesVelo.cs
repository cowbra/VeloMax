using MySql.Data.MySqlClient;

namespace bdd
{
    public partial class ListePiecesVelo : Form
    {
        BDD DATABASE = new BDD();
        public ListePiecesVelo(string idBicyclette)
        {
            InitializeComponent();
            DATABASE.Connect();
            label2.Text = idBicyclette;
            Fill(idBicyclette);
        }

        private void ListePiecesVelo_Load(object sender, EventArgs e)
        {

        }

        private void Fill(string idBicyclette)
        {
            string requeteSQL = "SELECT * FROM PIECE NATURAL JOIN ASSEMBLER_PAR WHERE ID_Bicyclette =" + idBicyclette;

            if (DATABASE.Connected)// on verifie que la connexion est bien effective
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string id = Lire["Identifiant_Piece"].ToString();
                        string type = Lire["Description_Piece"].ToString();
                        string date1 = Lire["DateDebut_Piece"].ToString();
                        string date2 = Lire["DateFin_Piece"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { id, type, date1, date2 }));
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
