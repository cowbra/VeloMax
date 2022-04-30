using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        }

        private void MenuGestion_Load(object sender, EventArgs e)
        {

        }
    }
}
