using MySql.Data.MySqlClient;
using System.Data;


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
            string conn_info = "Server="+ApplicationState.GetValue<string>("host")+"; Port="+ ApplicationState.GetValue<string>("port")+"; Database=VeloMax;Uid=" + id + ";PWD=" + mdp;
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
                        MessageBox.Show("Serveur introuvable ! Vérifier votre connexion internet", "SERVEUR INTROUVABLE");
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
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.
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
