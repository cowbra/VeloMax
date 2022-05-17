using MySql.Data.MySqlClient;


namespace bdd
{
    public class Commande
    {
        #region Attributs
        protected int idClient;
        protected string adresse;
        protected string date;
        protected double prix_total;
        protected string type;
        protected int quantite;

        #endregion

        public Commande(int idCLient, string adresse, string type, int quantite)
        {
            this.idClient = idCLient;
            this.adresse = adresse;

            string[] subsDate = DateTime.Today.ToString("d").Split('/');
            this.date = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];
            this.type = type;
            this.quantite = quantite;
        }

        public bool AddToBdd()
        {
            BDD DATABASE = new BDD();
            DATABASE.Connect();
            if (DATABASE.Connected == true)
            {
                MySqlCommand requete = new MySqlCommand("INSERT INTO COMMANDE(Date_Commande,AdresseLivraison_Commande,ID_Client,Prix_Commande,NB_articles_Commande,Type_Commande) VALUES(@date,@adresse,@id,@prix,@quantite,@type)", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@id", this.idClient);
                requete.Parameters.AddWithValue("@adresse", this.adresse);
                requete.Parameters.AddWithValue("@date", this.date);
                requete.Parameters.AddWithValue("@prix", this.prix_total);
                requete.Parameters.AddWithValue("@quantite", this.quantite);
                requete.Parameters.AddWithValue("@type", this.type);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();


                return true;
            }
            return false;
        }

        public bool UpdatePrixTotal(string id, double prix, int quantite)
        {

            BDD DATABASE = new BDD();
            DATABASE.Connect();
            if (DATABASE.Connected)
            {
                MySqlCommand requete = new MySqlCommand("UPDATE COMMANDE SET Prix_Commande=@prix WHERE ID_Commande=@id", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@id", id);
                requete.Parameters.AddWithValue("@prix", prix * quantite);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                return true;

            }
            else { MessageBox.Show("Erreur de connexion avec la base de données."); }
            return false;
        }


    }
}
