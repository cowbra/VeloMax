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
    public partial class MenuPiece : Form
    {
        public MenuPiece()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewPiece piece = new NewPiece();

            piece.ShowDialog();
            //Actualiser();
        }

        private void MenuPiece_Load(object sender, EventArgs e)
        {

        }
    }
}
