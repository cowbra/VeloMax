using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProblemeTransConnect
{
    public partial class NewCommande : Form
    {
        /// <summary>
        /// Classe permettant de créer une nouvelle commande
        /// </summary>
        /// 
        MySqlConnection mySqlConnection;
        bool connected = false;
        bool clientExiste = false;
        Client client1 = null;
        Vehicule vehicule = null;
        Chauffeur chauffeur1 = null;
        Livraison colis = null;

        Graphe graphe = null;
        public NewCommande()
        {
            InitializeComponent();
            EtablirConnection();
            Actualiser();
            VillesDsponibles();

            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Aucun chauffeur disponible actuellement !");
            }
            string message = "Pour afficher un chauffeur disponible à la date de livraison, Veuillez cliquer sur le bouton 'Actualiser' une fois la date entrée.\n\nDouble cliquez ensuite sur le chauffeur pour le sélectionner!\n\nPS : Affichage des chaufeurs disponible aujourd'hui par défaut.";
            MessageBox.Show(message, "Attention !",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Méthode affichant les chauffeurs disponible à la date du jour
        /// </summary>
        private void Actualiser(string requeteSQL = "SELECT * FROM Salaries WHERE livraison_encours=0 AND poste='chauffeur'")
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
                        string Tel = Lire["num_tel"].ToString();
                        string Salaire = Lire["salaire"].ToString();
                        string nbLivraison = Lire["nb_livraisons"].ToString();
                        listView1.Items.Add(new ListViewItem(new[] { NumeroSS, Nom, Prenom, Adresse, Tel, Salaire, nbLivraison }));
                    }
                }
            }
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
        /// Méthode créant la commande en elle même
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "") MessageBox.Show("Entrez un N° de Sécurité Sociale !");
            else if (textBox1.Text == "") MessageBox.Show("Entrez une ville de départ !");
            else if (textBox2.Text == "") MessageBox.Show("Entrez une ville d'arrivée !");
            else if (textBox3.Text == "") MessageBox.Show("Entrez la quantité / le volume / l'usage de votre marchandise ");
            else if (textBox4.Text == "") MessageBox.Show("Entrez la date de livraison pour la commande !");
            else if (listBox1.SelectedIndex == 0) MessageBox.Show("Choisissez un type de livraison !");
            else
            {
                string[] subsDate = textBox4.Text.Split('/');
                string dateTrans = subsDate[2] + subsDate[1] + subsDate[0];
                int dateC = Convert.ToInt32(dateTrans);

                #region Création du client 
                if (radioButton1.Checked)
                {
                    string[] subsDateC = textBox9.Text.Split('/');
                    string dateTransC = subsDateC[2] + subsDateC[1] + subsDateC[0];
                    int dateN = Convert.ToInt32(dateTransC);

                    if (textBox6.Text == "") MessageBox.Show("Entrez un Nom !");
                    else if (textBox7.Text == "") MessageBox.Show("Entrez un Prénom !");
                    else if (textBox8.Text == "") MessageBox.Show("Entrez une Adresse !");
                    else if (textBox9.Text == "") MessageBox.Show("Entrez une Date de Naissance !");
                    else if (textBox10.Text == "") MessageBox.Show("Entrez une Adresse mail !");
                    else if (textBox11.Text == "") MessageBox.Show("Entrez un N° de téléphone !");

                    client1 = new Client(textBox5.Text,  textBox7.Text, textBox6.Text, dateN, textBox8.Text, textBox10.Text, textBox11.Text, 0);
                    if (client1.AddToDataBase())
                    {
                        MessageBox.Show("Client créé avec succès !");
                        //this.Close();
                        clientExiste = true;
                    }
                    else
                    {
                        MessageBox.Show("Erreur de Connexion avec la Base de données.");
                        // On laisse la fenetre ouverte pour retenter une connexion à la bdd
                    }
                }
                #endregion

                #region Ou Récupération d'un client existant
                else
                { //Récupérer client dans bdd avec le N° de Secu
                    if (connected)
                    {
                        MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM Clients WHERE num_secu=@num_secu", mySqlConnection);
                        mySqlCommand.Parameters.AddWithValue("@num_secu", textBox5.Text);
                        using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                        {
                            string NumeroSS = "";
                            string Nom = "";
                            string Prenom = "";
                            string Adresse = "";
                            string DateNaissance = "";
                            string Email = "";
                            string Tel = "";
                            string MontantTotal = "";
                            while (Lire.Read())
                            {
                                NumeroSS = Lire["num_secu"].ToString();
                                Nom = Lire["nom"].ToString();
                                Prenom = Lire["prenom"].ToString();
                                Adresse = Lire["adresse"].ToString();
                                DateNaissance = Lire["date_naissance"].ToString();
                                Email = Lire["email"].ToString();
                                Tel = Lire["num_tel"].ToString();
                                MontantTotal = Lire["montant_total"].ToString();
                                //MessageBox.Show(NumeroSS + Nom + Prenom + Adresse + DateNaissance + Email + Tel + MontantTotal);
                            }
                            if (MontantTotal == "")
                            {
                                MessageBox.Show("Le client n'existe pas !");
                            }
                            else
                            {
                                clientExiste = true;
                                client1 = new Client(NumeroSS, Prenom, Nom, Convert.ToInt32(DateNaissance), Adresse, Email, Tel, Convert.ToDouble(MontantTotal));
                                //MessageBox.Show("Client récupéré avec succès !");
                            }
                        }
                    }
                }
                #endregion
                #region Récupération du Chauffeur sélectionné
                //On va récupéré toute les infos à partir de la bdd et pas avec la listview, car celle-ci ne contient que des informations partielles d'un chauffeur
                if (listView1.SelectedItems.Count > 0 && connected)
                {
                    ListViewItem element = listView1.SelectedItems[0];
                    string numSecu = element.SubItems[0].Text;

                    MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM Salaries WHERE num_secu=@num_secu", mySqlConnection);
                    mySqlCommand.Parameters.AddWithValue("@num_secu", numSecu);
                    using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                    {
                        string Nom = "";
                        string Prenom = "";
                        string Adresse = "";
                        string DateNaissance = "";
                        string Email = "";
                        string Tel = "";
                        string DateEmbauche = "";
                        string Poste = "";
                        string Salaire = "";
                        string Superieur = "";
                        string LivrCours = "";
                        string NbLivr = "";
                        while (Lire.Read())
                        {
                            Nom = Lire["nom"].ToString();
                            Prenom = Lire["prenom"].ToString();
                            Adresse = Lire["adresse"].ToString();
                            DateNaissance = Lire["date_naissance"].ToString();
                            Email = Lire["email"].ToString();
                            Tel = Lire["num_tel"].ToString();
                            DateEmbauche = Lire["date_embauche"].ToString();
                            Poste = Lire["poste"].ToString();
                            Salaire = Lire["salaire"].ToString();
                            Superieur = Lire["superieur_hierarchique"].ToString();
                            LivrCours = Lire["livraison_encours"].ToString();
                            NbLivr = Lire["nb_livraisons"].ToString();

                        }
                        bool lc = false;
                        if (LivrCours != "0") lc = true;
                        //MessageBox.Show(DateNaissance);
                        //MessageBox.Show(DateEmbauche);

                        chauffeur1 = new Chauffeur(numSecu, Nom, Prenom, Convert.ToInt32(DateNaissance), Adresse, Email, Tel, Convert.ToInt32(DateEmbauche), Poste, Convert.ToDouble(Salaire), Superieur, lc, Convert.ToInt32(NbLivr));
                        //MessageBox.Show("Chauffeur récupéré avec succès !");
                    }

                }
                #endregion

                #region Création du véhicule approprié
                string marchandise = "";
                if (listBox1.SelectedIndex == 1)
                {
                    vehicule = new Voiture(Convert.ToInt32(textBox3.Text));
                    marchandise = "Passagers";
                }

                else if (listBox1.SelectedIndex == 2)
                {
                    vehicule = new Camionnette(textBox3.Text);
                    marchandise = "Objets encombrants";
                }

                else if (listBox1.SelectedIndex == 3)
                {
                    vehicule = new CamionCiterne(textBox3.Text);
                    marchandise = "Gaz / Liquide";
                }

                else if (listBox1.SelectedIndex == 4)
                {
                    vehicule = new CamionBenne(textBox3.Text);
                    marchandise = "Gros matériaux";
                }

                else if (listBox1.SelectedIndex == 5)
                {
                    vehicule = new CamionFrigorifique(textBox3.Text);
                    marchandise = "marchandise perissables";
                }

                //Création de l'objet de type Livraison
                colis = new Livraison(textBox3.Text, marchandise);
                #endregion
                //Il nous reste plus qu'à créer notre commande :
                #region Creation Commande
                //Tentative  Dijkstra

                string solution=FindChemilCourt();
                if(solution != "Erreur-Parcours" && clientExiste)
                {
                    string[] sol = solution.Split('/');
                    string parcours = sol[0];
                    int distanceKm = Convert.ToInt32(sol[1]);
                    int TempsTrajet = Convert.ToInt32(sol[2]);

                    //On créé notre commande avec nos données
                    Commande commande = new Commande(textBox1.Text, textBox2.Text, dateC, client1, chauffeur1, vehicule, colis, parcours, distanceKm, TempsTrajet);
                    // On met à jous son coût en fonction du trajet du vehicule, tarif chauffeur et remises applicables
                    commande.calculPrix();
                    // Une fois la commande paramétrée correctement, on l'ajoute à la BDD
                    if (commande.AddToDataBase())
                    {
                        MessageBox.Show("Commande créée avec succès !");
                        client1.AddMoneyToClient(commande.Prix);
                        this.Close();
                    }
                    else MessageBox.Show("Erreur lors de la création de la commande : Communication avec la base de données interrompue !");
                } 
                #endregion
            }
        }
        #region Methodes vides Interface graphique
        private void NewClient_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            textBox11.Enabled = true;

            textBox6.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = true;
            textBox9.Visible = true;
            textBox10.Visible = true;
            textBox11.Visible = true;

            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;

            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;

            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //mySqlConnection.Close();
            //Menu menu = new Menu();
            this.Close();
            //menu.Show();
        }
        public List<string> GetCity()
        {
            List<string> villes = new List<string>();
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
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM Villes", mySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string villeA = Lire["VilleA"].ToString();
                        string villeB = Lire["VilleB"].ToString();
                        if (!villes.Contains(villeA))
                        {
                            villes.Add(villeA);
                        }
                        if (!villes.Contains(villeB))
                        {
                            villes.Add(villeB);
                        }
                    }
                }
            }
            return villes;
        }

        /// <summary>
        /// Méthode appliquant Dijkstra pour trouver le plus court chemin et connaître ainsi la distance à parcourir
        /// </summary>
        public string FindChemilCourt()
        {
            List<string> villes = GetCity();
            List<Noeud> listeNoeud = new List<Noeud>();

            if (!villes.Contains(textBox1.Text))
            {
                MessageBox.Show("départ:" + textBox1.Text);
                MessageBox.Show("La ville de départ n'est pas couverte pas notre service de livraison !");
                return "Erreur-Parcours";
            }
            else if (!villes.Contains(textBox2.Text))
            {
                MessageBox.Show("arrivée:" + textBox2.Text);
                MessageBox.Show("La ville d'arrivée n'est pas couverte pas notre service de livraison !");
                return "Erreur-Parcours";
            }
            else
            {
                string rp= "Erreur-Parcours";
                foreach (string villeName in villes)
                {
                    listeNoeud.Add(new Noeud(villeName));
                }
                for (int i = 0; i < listeNoeud.Count; i++)
                {
                    if (connected)
                    {
                        MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM Villes", mySqlConnection);
                        using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                        {
                            while (Lire.Read())
                            {
                                string villeA = Lire["VilleA"].ToString();
                                string villeB = Lire["VilleB"].ToString();
                                string distance = Lire["distance"].ToString();
                                string tempsTrajet = Lire["temps_trajet"].ToString();

                                if (listeNoeud[i].Ville == villeA)
                                {
                                    foreach (Noeud noeud in listeNoeud)
                                    {
                                        if (noeud.Ville == villeB) listeNoeud[i].AjouterLien(Convert.ToInt32(distance),Convert.ToInt32(tempsTrajet), noeud);
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (Noeud n in listeNoeud)
                {
                    if (n.Ville == textBox1.Text)
                    {
                        graphe = new Graphe(n, listeNoeud);
                        break;
                    }
                }

                foreach (Noeud n in listeNoeud)
                {
                    if (n.Ville == textBox2.Text)
                    {
                        rp= graphe.Dijkstra(n);
                        break;
                    }
                }
                return rp;
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void VillesDsponibles()
        {
            List<string> villes = GetCity();
            foreach(string v in villes)
            {
                listView2.Items.Add(new ListViewItem(new[] { v })) ;
            }
        }

        /// <summary>
        /// Méthode récupérant tous les chauffeurs de la BDD
        /// </summary>
        private List<string> GetDrivers()
        {
            List<string> drivers = new List<string>();
            if (connected)
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT num_secu FROM Salaries WHERE poste='chauffeur'", mySqlConnection);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string NumeroSS = Lire[0].ToString();
                        drivers.Add(NumeroSS);
                    }
                }
            }
            else
            {
                MessageBox.Show("Erreur de connexion avec la base de données !\nVeuillez réessayer");
                EtablirConnection();
            }
            return drivers;
        }

        /// <summary>
        /// Méthode récupérant tous les chauffeurs occupés à la date sélectionnée
        /// </summary>
        private List<string> GetDriverOccupedAtDAte(int date)
        {
            List<string> driversOccupated = new List<string>();
            if (connected)
            {
                listView1.Items.Clear();
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT chauffeur FROM Commandes WHERE date_commande=@datec", mySqlConnection);
                mySqlCommand.Parameters.AddWithValue("@datec", date);
                using (MySqlDataReader Lire = mySqlCommand.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        string NumeroSS = Lire[0].ToString();
                        driversOccupated.Add(NumeroSS);
                    }
                }
            }
            else
            {
                MessageBox.Show("Erreur de connexion avec la base de données !\nVeuillez réessayer");
                EtablirConnection();
            }
            return driversOccupated;
        }

        /// <summary>
        /// Méthode affichant tous les chauffeurs disponible à la date indiquée en supprimant les
        /// chauffeurs occupés de la liste de tous les chauffeurs
        /// </summary>
        private List<string> GetDriversAvailable(List<string> Alldrivers, List<string> DriversOccuped)
        {
            foreach (string driver in DriversOccuped)
            {
                Alldrivers.Remove(driver);
            }
            return Alldrivers;
        }
        /// <summary>
        /// Méthode actualisant les chauffeurs disponibles à la date choisie
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "") MessageBox.Show("Veuillez entrer une date de livraison !");
            else
            {
                string[] sol = textBox4.Text.Split('/');
                string date = sol[2] + sol[1] + sol[0];
                int dateC = Convert.ToInt32(date);
                List<string> drivers = GetDriversAvailable(GetDrivers(), GetDriverOccupedAtDAte(dateC));

                string requete = "SELECT * FROM Salaries WHERE (livraison_encours = 0 AND poste = 'chauffeur') AND  (";
                for(int i=0; i< drivers.Count-1;i++)
                {
                    requete += "num_secu ='" + drivers[i] + "' OR ";
                }
                requete += "num_secu ='"+drivers[drivers.Count-1] +"');";
                Actualiser(requete);
                MessageBox.Show("Chauffeurs disponibles le "+ textBox4.Text+" mis à jour");
            }
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
