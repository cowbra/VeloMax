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
    public partial class NewClient : Form
    {
        public NewClient()
        {
            InitializeComponent();
        }

        private void NewClient_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

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
                MessageBox.Show("Veuillez sélectionner le type de Client !");
            }
        }
    }
}
