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
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string ID = Lire["NumProgramme_Fidelio"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string DESC = Lire["Description_Fidelio"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string COUT = Lire["Cout_Fidelio"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string DUREE = Lire["Duree_Fidelio"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
#pragma warning disable CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.
                        string RABAIS = Lire["Rabais_Fidelio"].ToString();
#pragma warning restore CS8600 // Conversion de littéral ayant une valeur null ou d'une éventuelle valeur null en type non-nullable.

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
                MessageBox.Show("Veuillez sélectionner le type de Client !");
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
            string dateTrans = subsDate[2] +"-"+ subsDate[1] + "-" + subsDate[0];
            textBox8.Text = dateTrans;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
