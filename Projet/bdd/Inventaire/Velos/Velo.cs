using MySql.Data.MySqlClient;


namespace bdd
{
    public class Velo
    {
        #region Attributs

        protected string nom;
        protected string grandeur;
        protected double prix;
        protected string typeVelo;
        protected string dateIntro;
        protected string dateFin;

        protected string cadre;
        protected string guidon;
        protected string? freins;
        protected string selle;
        protected string? derailleurAvant;
        protected string? derailleurArriere;
        protected string roueAvant;
        protected string roueArriere;
        protected string? reflecteurs;
        protected string pedalier;
        protected string? ordinateur;
        protected string? panier;
        #endregion


        #region Constructeur
        public Velo(string nom, string grandeur, double prix, string typeVelo, string dateIntro, string dateFin, string cadre, string guidon, string freins, string selle,
            string derailleurAvant, string derailleurArriere, string roueAvant, string roueArriere, string reflecteurs, string pedalier, string ordinateur, string panier)
        {
            this.nom = nom;
            this.grandeur = grandeur;
            this.prix = prix;
            this.typeVelo = typeVelo;
            this.dateIntro = dateIntro;
            this.dateFin = dateFin;


            this.cadre = cadre;
            this.guidon = guidon;
            this.freins = freins;
            this.selle = selle;
            this.derailleurAvant = derailleurAvant;
            this.derailleurArriere = derailleurArriere;
            this.roueAvant = roueAvant;
            this.roueArriere = roueArriere;
            this.reflecteurs = reflecteurs;
            this.ordinateur = ordinateur;
            this.panier = panier;
            this.pedalier = pedalier;

        }
        #endregion

        public bool AddToBdd()
        {
            BDD DATABASE = new BDD();
            DATABASE.Connect();
            if (DATABASE.Connected == true)
            {
                MySqlCommand requete = new MySqlCommand("INSERT INTO BICYCLETTE(Nom_Bicyclette,Grandeur_Bicyclette,Prix_Bicyclette,Type_Bicyclette,DateIntroduction_Bicyclette,DateFin_Bicyclette) VALUES(@Nom_Bicyclette,@Grandeur_Bicyclette,@Prix_Bicyclette,@Type_Bicyclette,@DateIntroduction_Bicyclette,@DateFin_Bicyclette)", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@Nom_Bicyclette", this.nom);
                requete.Parameters.AddWithValue("@Grandeur_Bicyclette", this.grandeur);
                requete.Parameters.AddWithValue("@Prix_Bicyclette", this.prix);
                requete.Parameters.AddWithValue("@Type_Bicyclette", this.typeVelo);
                requete.Parameters.AddWithValue("@DateIntroduction_Bicyclette", this.dateIntro);
                requete.Parameters.AddWithValue("@DateFin_Bicyclette", this.dateIntro);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                //ON RECUPERE L'ID DU DERNIER MODELE DE BICYCLETTE AJOUTEE
                string idBicyclette = "";
                requete = new MySqlCommand("SELECT LAST_INSERT_ID() FROM BICYCLETTE", DATABASE.MySqlConnection);
                using (MySqlDataReader Lire = requete.ExecuteReader())
                {
                    while (Lire.Read())
                    {
                        idBicyclette = Lire.GetString(0);
                    }
                }





                //DATABASE.Disconnect();
                return true;
            }
            return false;
        }
    }
}
