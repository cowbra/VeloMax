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
    public partial class ModifierPiece : Form
    {
        public ModifierPiece()
        {
            InitializeComponent();
        }

        private void ModifierPiece_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string[] subsDate = dateTimePicker1.Value.ToShortDateString().Split('/');
            string dateTrans = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            textBox1.Text = dateTrans;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string[] subsDate = dateTimePicker2.Value.ToShortDateString().Split('/');
            string dateTrans = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            textBox2.Text = dateTrans;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        public string IdPiece { set { label4.Text = value; } }
        public string DateIntro { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string DateFin { get { return textBox2.Text; } set { textBox2.Text = value; } }
    }
}
