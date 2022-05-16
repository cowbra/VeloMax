using MySql.Data.MySqlClient;
using System;


namespace bdd
{
    public class Fourni
    {
        #region Attributs
        public Int64 siret;
        public string idPiece;
        public int quantite;
        public int delai;
        public double prix;
        public int numFournisseur;

        #endregion

        // Constructeur client particulier
        public Fourni(Int64 siret, string idPiece, int quantite, int delai, double prix, int numFournisseur)
        {
            this.siret = siret;
            this.idPiece = idPiece;
            this.quantite = quantite;
            this.delai = delai;
            this.prix = prix;
            this.numFournisseur = numFournisseur;

        }
        private Fourni()
        {
            this.siret = 0;
            this.idPiece = "null";
            this.prix = 0;
            this.numFournisseur = 0;
            this.quantite=0;
            this.delai = 0;
        }
        public bool AddToBdd()
        {
            BDD DATABASE = new BDD();
            DATABASE.Connect();
            if (DATABASE.Connected == true)
            {
                MySqlCommand requete = new MySqlCommand("INSERT INTO FOURNIT(Siret_Fournisseur,Identifiant_Piece,NumProduit_Fournisseur,Prix_Fournisseur,Quantite_Fournisseur,Delai_Fournisseur) VALUES(@Siret_Fournisseur,@Identifiant_Piece,@NumProduit_Fournisseur,@Prix_Fournisseur,@Quantite_Fournisseur,@Delai_Fournisseur)", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@Siret_Fournisseur", this.siret);
                requete.Parameters.AddWithValue("@Identifiant_Piece", this.idPiece);
                requete.Parameters.AddWithValue("@NumProduit_Fournisseur", this.numFournisseur);
                requete.Parameters.AddWithValue("@Prix_Fournisseur", this.prix);
                requete.Parameters.AddWithValue("@Quantite_Fournisseur", this.quantite);
                requete.Parameters.AddWithValue("@Delai_Fournisseur", this.delai);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                DATABASE.Disconnect();
                return true;
            }
            return false;
        }


    }
}


