using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace ProblemeTransConnect
{
    public partial class MenuSalarie : Form
    {
        MySqlConnection mySqlConnection;
        bool connected = false;

        
        public MenuSalarie()
        {
            InitializeComponent();
            EtablirConnection();
            Actualiser();
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

        private void Actualiser(string requeteSQL = "SELECT * FROM Salaries")
        {
            if (connected)
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand(requeteSQL, mySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string NumeroSS = Lire["num_secu"].ToString();
                        string Nom = Lire["nom"].ToString();
                        string Prenom = Lire["prenom"].ToString();
                        string Adresse = Lire["adresse"].ToString();
                        string DateNaissance = Lire["date_naissance"].ToString();
                        string Email = Lire["email"].ToString();
                        string Tel = Lire["num_tel"].ToString();
                        string Dateembauche = Lire["date_embauche"].ToString();
                        string Poste = Lire["poste"].ToString();
                        string SPH = Lire["superieur_hierarchique"].ToString();
                        string Salaire = Lire["salaire"].ToString();
                        string nbLivraison = Lire["nb_livraisons"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { NumeroSS, Nom, Prenom, Adresse, DateNaissance, Email, Tel, Dateembauche, Poste, SPH, Salaire, nbLivraison }));
                    }
                }
            }
        }

        #region methodes Vides
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MenuSalarie_Load(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion


        private void button1_Click_1(object sender, EventArgs e)
        {//Affichage de la fenetre permettant de créer un nouveau salarié
            NewSalarie news = new NewSalarie();
            //mySqlConnection.Close();
            news.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {//retour au menu principal
            mySqlConnection.Close();
            Menu menu = new Menu();
            this.Close();
            menu.Show();
        }

        /// <summary>
        /// Méthode permettant d'afficher les salariés selon les critères de tri sélectionnés
        /// </summary>
        private void button3_Click_1(object sender, EventArgs e)
        {
            int compteurTri = 0;
            string requete = "SELECT * FROM Salaries ";
            foreach (int indexChecked in checkedListBox1.CheckedIndices)
            {
                // On vérifie si un choix de tri est coché pour nos salaries
                if (indexChecked == 0)
                {
                    requete += "ORDER BY nom";
                    compteurTri++;
                }
                else if (indexChecked == 1)
                {
                    compteurTri++;
                    if (compteurTri == 2) requete += " AND adresse";
                    else requete += "ORDER BY adresse";
                }
                else if (indexChecked == 2)
                {
                    compteurTri++;
                    if (compteurTri >= 2) requete += " AND poste";
                    else requete += "ORDER BY poste";
                }
                else if (indexChecked == 3)
                {
                    compteurTri++;
                    if (compteurTri >= 2) requete += " AND salaire DESC";
                    else requete += "ORDER BY salaire DESC";
                }
                else if (indexChecked == 4)
                {
                    compteurTri++;
                    if (compteurTri >= 2) requete += " AND nb_livraisons DESC";
                    else requete += "ORDER BY nb_livraisons DESC";
                }
            }

            int compteurAffich = 0;
            foreach (int indexChecked in checkedListBox2.CheckedIndices)
            {
                // On vérifie si un choix d'affichage est coché pour nos salaries
                if (indexChecked == 0)
                {
                    requete += " WHERE poste='chauffeur'";
                    compteurAffich++;
                }
                else if (indexChecked == 1)
                {
                    compteurAffich++;
                    if (compteurAffich == 2) requete += " AND livraison_encours=0";
                    else requete += " WHERE livraison_encours=0";
                }
            }
            //MessageBox.Show(requete);
                if (compteurTri != 0 || compteurAffich!=0)
            {
                //MessageBox.Show(requete);
                Actualiser(requete);
            }
            else Actualiser();
        }

        /// <summary>
        /// Méthode permettant de supprimer un salarié de la BDD
        /// </summary>
        private void supprimerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (connected)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem element = listView1.SelectedItems[0];
                    string numSecu = element.SubItems[0].Text;
                    MySqlCommand mySqlCommand = new MySqlCommand("DELETE FROM Salaries WHERE num_secu =@numsecu", mySqlConnection);
                    mySqlCommand.Parameters.AddWithValue("@numsecu", numSecu);
                    mySqlCommand.ExecuteNonQuery();
                    element.Remove();
                    mySqlCommand.Parameters.Clear();
                }
            }
        }

        /// <summary>
        /// Méthode permettant de mettre à jour un salarié dans la BDD
        /// </summary>
        private void modifierToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string Nom = element.SubItems[1].Text;
                string Adresse = element.SubItems[3].Text;
                string Mail = element.SubItems[5].Text;
                string Tel = element.SubItems[6].Text;

                string Poste = element.SubItems[8].Text;
                string Salaire = element.SubItems[10].Text;

                string Secu = element.SubItems[0].Text;

                using (ModifierSalarie modifier = new ModifierSalarie())
                {
                    //Elements modifiables
                    modifier.Nom = Nom;
                    modifier.Adresse = Adresse;
                    modifier.Email = Mail;
                    modifier.Telephone = Tel;
                    modifier.Poste = Poste;
                    modifier.Salaire = Salaire;

                    //Elements non modifiables
                    modifier.Secu = Secu;
                    
                    if (modifier.ShowDialog() == DialogResult.Yes)
                    {//On met à jour les valeurs dans la BDD

                        MySqlCommand requete = new MySqlCommand("UPDATE Salaries SET nom=@nom, adresse=@adresse, email=@email, num_tel=@num_tel, poste=@poste, salaire=@salaire WHERE num_secu=@num_secu", mySqlConnection);

                        requete.Parameters.AddWithValue("@num_secu", Secu);
                        requete.Parameters.AddWithValue("@nom", modifier.Nom);
                        requete.Parameters.AddWithValue("@adresse", modifier.Adresse);
                        requete.Parameters.AddWithValue("@email", modifier.Email);
                        requete.Parameters.AddWithValue("@num_tel", modifier.Telephone);
                        requete.Parameters.AddWithValue("@poste", modifier.Poste);
                        requete.Parameters.AddWithValue("@salaire", modifier.Salaire);

                        requete.ExecuteNonQuery();
                        requete.Parameters.Clear();

                        element.SubItems[1].Text = modifier.Nom;
                        element.SubItems[3].Text = modifier.Adresse;
                        element.SubItems[5].Text = modifier.Email;
                        element.SubItems[6].Text = modifier.Telephone;
                        element.SubItems[8].Text = modifier.Poste;
                        element.SubItems[10].Text = modifier.Salaire;
                        MessageBox.Show("Salarié mis à jour.");
                    }
                }

            }
        }

        /// <summary>
        /// Méthode permettant de créer un noeud pour chaque employé afin d'afficher par la suite un organigramme
        /// </summary>
        private List<NoeudOrganigramme> GetSalariesNode()
        {
            List<NoeudOrganigramme> noeudsEmployes = new List<NoeudOrganigramme>();

            if (connected)
            {
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT num_secu FROM Salaries", mySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string NumeroSS = Lire[0].ToString();
                        noeudsEmployes.Add(new NoeudOrganigramme(NumeroSS));
                    }
                }
            }
            return noeudsEmployes;
        }
        /// <summary>
        /// Méthode permettant de récupérer le supérieur hiérarchique de chaque employé pour créer par la suite l'organigramme
        /// </summary>
        private List<string> GetSupHiera()
        {
            List<string> SuperieurHierarchique = new List<string>();

            if (connected)
            {
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT superieur_hierarchique FROM Salaries", mySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string SupHiera = Lire[0].ToString();
                        SuperieurHierarchique.Add(SupHiera);
                    }
                }
            }
            return SuperieurHierarchique;
        }

        /// <summary>
        /// Méthode permettant d'ajouter un fils ou un frère à chaque noeud de l'arbre n-aire de l'organigramme
        /// </summary>
        private void Assiciation()
        {
            List<string> sup = GetSupHiera();
            List<string> waza = sup.Distinct().ToList();

            int compteur = 0;
            
            List<NoeudOrganigramme> NoeudSalaries = GetSalariesNode();

            List<string> ss = new List<string>();

            for (int i = 1; i < waza.Count; i++)
            {
                List<int> pos = new List<int>();
                compteur = 0;
                for (int l = 1; l < sup.Count; l++)
                {

                    if (sup[l] == waza[i] && compteur == 0)
                    {
                        NoeudSalaries[l - 1].AssocierNoeudFrere(NoeudSalaries[l]);
                        compteur++;
                    }
                    else
                    {
                        if (sup[l] == waza[i] ) NoeudSalaries[l - 1].AssocierNoeudFilsGauche(NoeudSalaries[l]);
                    }

                }
            }

            ArbreNAire tree = new ArbreNAire(NoeudSalaries[0]);
            string resultat =tree.resu(tree.Racine, 0);
            MessageBox.Show(resultat);

        }

        /// <summary>
        /// Méthode permettant d'afficher l'organigramme
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            Assiciation();
        }
    }
}
