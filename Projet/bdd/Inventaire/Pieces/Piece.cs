using MySql.Data.MySqlClient;


namespace bdd
{
    public class Piece
    {
        #region Attributs
        protected string idPiece;
        protected string typePiece;
        protected string dateIntroduction;
        protected string dateFin;

        #endregion

        // Constructeur client particulier
        public Piece(string idPiece, string typePiece, string dateIntroduction, string dateFin)
        {
            this.idPiece = idPiece;
            this.typePiece = typePiece;
            this.dateIntroduction = dateIntroduction;
            this.dateFin = dateFin;
        }

        public bool AddToBdd()
        {
            BDD DATABASE = new BDD();
            DATABASE.Connect();
            if (DATABASE.Connected == true)
            {
                MySqlCommand requete = new MySqlCommand("INSERT INTO PIECE(Identifiant_Piece,Description_Piece,DateDebut_Piece,DateFin_Piece) VALUES(@Identifiant_Piece,@Description_Piece,@DateDebut_Piece,@DateFin_Piece)", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@Identifiant_Piece", this.idPiece);
                requete.Parameters.AddWithValue("@Description_Piece", this.typePiece);
                requete.Parameters.AddWithValue("@DateDebut_Piece", this.dateIntroduction);
                requete.Parameters.AddWithValue("@DateFin_Piece", this.dateFin);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                DATABASE.Disconnect();
                return true;
            }
            return false;
        }


    }
}
