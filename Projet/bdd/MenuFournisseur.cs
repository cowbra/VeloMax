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
    public partial class MenuFournisseur : Form
    {
        public MenuFournisseur()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Close();
            menu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewFournisseur newf = new NewFournisseur();
            newf.ShowDialog();
        }
    }
}
