using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        }

        private void ListePiecesVelo_Load(object sender, EventArgs e)
        {

        }

        private void Fill(string id)
        {
            string sql ="SELECT * FROM PIECE WHERE Identifiant_Piece ='" + id + "'";
        }
    }
}
