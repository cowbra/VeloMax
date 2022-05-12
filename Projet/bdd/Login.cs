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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool check_user = Check_USER_LOGIN(textBox1.Text, textBox2.Text);
            if (check_user)
            {
                ApplicationState.SetValue("user", textBox1.Text);
                ApplicationState.SetValue("password", textBox2.Text);
                Menu menu = new Menu();
                this.Hide();
                menu.Show();
            }
            


        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        public static bool Check_USER_LOGIN(string id, string mdp)
        {
            string conn_info = "Server=2.11.7.149;Port=3306;Database=VeloMax;Uid="+id+";PWD="+mdp;
            bool isConnected = false;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(conn_info);
                conn.Open();
                isConnected = true;
            }
            
            catch (MySqlException ex)
            {
                isConnected = false;
                switch (ex.Number)
                {
                    case 1042: // Erreur de connexion avec le serveur spécifié (verifier hôte et port)
                        MessageBox.Show("Serveur introuvable ! Vérifier votre connexion internet","SERVEUR INTROUVABLE");
                        break;
                    case 0: // Acces refusé, verifie login
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect ! Veuillez vérifier vos identifiants.", "LOGIN FAILED");
                        break;
                    default:
                        break;
                }
            }
            
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return isConnected;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
