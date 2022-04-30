using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace bdd
{
    public class Piece
    {
        #region Attributs
        protected string typePiece;
        protected string dateIntroduction;
        protected string dateFin;

        #endregion

        // Constructeur client particulier
        public Piece(string typePiece, string dateIntroduction, string dateFin)
        {
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
                MySqlCommand requete = new MySqlCommand("INSERT INTO PIECE(Description_Piece,DateDebut_Piece,DateFin_Piece) VALUES(@Description_Piece,@DateDebut_Piece,@DateFin_Piece)", DATABASE.MySqlConnection);
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
