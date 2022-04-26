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

namespace ProblemeTransConnect
{
    public partial class NewSalarie : Form
    {
        /// <summary>
        /// Classe permettant de créer un nouveau salarié
        /// </summary>
        MySqlConnection mySqlConnection;
        bool connected = false;
        bool chauffeur = false;
        public NewSalarie()
        {
            InitializeComponent();
            EtablirConnection();
        }
        private void EtablirConnection()
        {
            if (!connected)
            {
                mySqlConnection = new MySqlConnection("SERVER=93.10.83.45;PORT=3306;DATABASE=ProblemeCsharp;UID=problemecsharp;PWD=Hugoemma1320!;");
                try
                {
                    if (mySqlConnection.State == ConnectionState.Closed) mySqlConnection.Open();
                    connected = true;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        /// <summary>
        /// Méthode récupérant les string des textbox et qui créé  puis ajoute le Salarié à la BDD
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "") MessageBox.Show("Entrez un N° de Sécurité Sociale !");
            else if (textBox3.Text == "") MessageBox.Show("Entrez un Nom !");
            else if (textBox2.Text == "") MessageBox.Show("Entrez un Prénom !");
            else if (textBox5.Text == "") MessageBox.Show("Entrez une Adresse !");
            else if (textBox6.Text == "") MessageBox.Show("Entrez une Date de Naissance !");
            else if (textBox4.Text == "") MessageBox.Show("Entrez une Adresse mail !");
            else if (textBox9.Text == "") MessageBox.Show("Entrez un N° de téléphone !");

            else if (textBox8.Text == "") MessageBox.Show("Entrez une date d'embauche !");
            else if (textBox7.Text == "") MessageBox.Show("Entrez un poste !");
            else if (textBox10.Text == "") MessageBox.Show("Entrez un salaire !");
            else if (textBox14.Text == "") MessageBox.Show("Entrez le N° de Sécurité Sociale du Supérieur Hiérarchique direct !");
            else
            {
                string[] subsDate = textBox6.Text.Split('/');
                string dateTrans = subsDate[2] + subsDate[1] + subsDate[0];
                int dateN = Convert.ToInt32(dateTrans);

                string[] subsDateE = textBox8.Text.Split('/');
                string dateTransE = subsDateE[2] + subsDateE[1] + subsDateE[0];
                int dateE = Convert.ToInt32(dateTransE);


                // On établit la connexion MySql
                if (!connected)
                {
                    mySqlConnection = new MySqlConnection("SERVER=93.10.83.45;PORT=3306;DATABASE=ProblemeCsharp;UID=problemecsharp;PWD=Hugoemma1320!;");
                    try
                    {
                        if (mySqlConnection.State == ConnectionState.Closed) mySqlConnection.Open();
                        connected = true;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                //Si pas d'erreur, Connexion effectuée

                if (connected)
                {
                    if (!chauffeur)
                    {
                        MySqlCommand requete = new MySqlCommand("INSERT INTO Salaries(num_secu,nom,prenom,adresse,date_naissance,email,num_tel,date_embauche,poste,salaire,superieur_hierarchique) VALUES(@num_secu,@nom,@prenom,@adresse,@date_naissance,@email,@num_tel,@date_embauche,@poste,@salaire,@suphiera)", mySqlConnection);
                        requete.Parameters.AddWithValue("@num_secu", textBox1.Text);
                        requete.Parameters.AddWithValue("@nom", textBox3.Text);
                        requete.Parameters.AddWithValue("@prenom", textBox2.Text);
                        requete.Parameters.AddWithValue("@adresse", textBox5.Text);
                        requete.Parameters.AddWithValue("@date_naissance", dateN);
                        requete.Parameters.AddWithValue("@email", textBox4.Text);
                        requete.Parameters.AddWithValue("@num_tel", textBox9.Text);

                        requete.Parameters.AddWithValue("@date_embauche", dateE);
                        requete.Parameters.AddWithValue("@poste", textBox7.Text);
                        requete.Parameters.AddWithValue("@salaire", Convert.ToDouble(textBox10.Text));
                        requete.Parameters.AddWithValue("@suphiera", textBox14.Text);

                        requete.ExecuteNonQuery();
                        requete.Parameters.Clear();
                    }
                    else
                    {
                        MySqlCommand requete = new MySqlCommand("INSERT INTO Salaries(num_secu,nom,prenom,adresse,date_naissance,email,num_tel,date_embauche,poste,salaire,superieur_hierarchique,livraison_encours,nb_livraisons) VALUES(@num_secu,@nom,@prenom,@adresse,@date_naissance,@email,@num_tel,@date_embauche,@poste,@salaire,@suphiera,@livencours,@nbliv)", mySqlConnection);
                        requete.Parameters.AddWithValue("@num_secu", textBox1.Text);
                        requete.Parameters.AddWithValue("@nom", textBox3.Text);
                        requete.Parameters.AddWithValue("@prenom", textBox2.Text);
                        requete.Parameters.AddWithValue("@adresse", textBox5.Text);
                        requete.Parameters.AddWithValue("@date_naissance", dateN);
                        requete.Parameters.AddWithValue("@email", textBox4.Text);
                        requete.Parameters.AddWithValue("@num_tel", textBox9.Text);

                        requete.Parameters.AddWithValue("@date_embauche", dateE);
                        requete.Parameters.AddWithValue("@poste", textBox7.Text);
                        requete.Parameters.AddWithValue("@salaire", Convert.ToDouble(textBox10.Text));
                        requete.Parameters.AddWithValue("@suphiera", textBox14.Text);

                        int livraisonencours;
                        int nblivr;

                        // resultat binaire (1=occupé/0=dispo)
                        if (textBox11.Text == "") livraisonencours = 0;
                        else livraisonencours = Convert.ToInt32(textBox11.Text);

                        if (textBox13.Text == "") nblivr = 0;
                        else nblivr = Convert.ToInt32(textBox13.Text);

                        requete.Parameters.AddWithValue("@livencours", livraisonencours);
                        requete.Parameters.AddWithValue("@nbliv", nblivr);

                        requete.ExecuteNonQuery();
                        requete.Parameters.Clear();
                    }
                    mySqlConnection.Close();
                    MessageBox.Show("Salarié créé avec succès !");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur de Connexion avec la Base de données.");
                    // On laisse la fenetre de creation de clients ouverte pour retenter une connexion à la bdd
                }
            }
        }

        private void NewClient_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (int indexChecked in checkedListBox1.CheckedIndices)
            {
                if (indexChecked == 0) chauffeur = false;
                if (indexChecked == 1) chauffeur = true;
            }

            if (chauffeur)
            {
                label11.Enabled = true;
                label11.Visible = true;
                label13.Enabled = true;
                label13.Visible = true;
                label14.Enabled = true;
                label14.Visible = true;

                textBox11.Visible = true;
                textBox11.Enabled = true;
                textBox13.Visible = true;
                textBox13.Enabled = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
