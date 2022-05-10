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

        #endregion


        #region Constructeur
        public Velo(string nom, string grandeur, double prix, string typeVelo, string dateIntro, string dateFin)
        {
            this.nom = nom;
            this.grandeur = grandeur;
            this.prix = prix;
            this.typeVelo = typeVelo;
            this.dateIntro = dateIntro;
            this.dateFin = dateFin;


        }
        #endregion

        public bool AddToBdd()
        {
            BDD DATABASE = new BDD();
            DATABASE.Connect();
            if (DATABASE.Connected == true)
            {
                #region Creation du modele de Bicyclette
                MySqlCommand requete = new MySqlCommand("INSERT INTO BICYCLETTE(Nom_Bicyclette,Grandeur_Bicyclette,Prix_Bicyclette,Type_Bicyclette,DateIntroduction_Bicyclette,DateFin_Bicyclette) VALUES(@Nom_Bicyclette,@Grandeur_Bicyclette,@Prix_Bicyclette,@Type_Bicyclette,@DateIntroduction_Bicyclette,@DateFin_Bicyclette)", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@Nom_Bicyclette", this.nom);
                requete.Parameters.AddWithValue("@Grandeur_Bicyclette", this.grandeur);
                requete.Parameters.AddWithValue("@Prix_Bicyclette", this.prix);
                requete.Parameters.AddWithValue("@Type_Bicyclette", this.typeVelo);
                requete.Parameters.AddWithValue("@DateIntroduction_Bicyclette", this.dateIntro);
                requete.Parameters.AddWithValue("@DateFin_Bicyclette", this.dateFin);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();
                #endregion


                DATABASE.Disconnect();
                return true;
            }
            return false;
        }
    }
}
