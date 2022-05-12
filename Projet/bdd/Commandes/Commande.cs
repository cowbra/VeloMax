using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace bdd
{
    public class Commande
    {
        #region Attributs
        protected int idClient;
        protected string adresse;
        protected string date;

        #endregion

        public Commande(int idCLient, string adresse)
        {
            this.idClient = idCLient;
            this.adresse = adresse;

            string[] subsDate = DateTime.Today.ToString("d").Split('/');
            this.date = subsDate[2] + "-" + subsDate[1] + "-" + subsDate[0];


        }

        public bool AddToBdd()
        {
            BDD DATABASE = new BDD();
            DATABASE.Connect();
            if (DATABASE.Connected == true)
            {
                MySqlCommand requete = new MySqlCommand("INSERT INTO COMMANDE(Date_Commande,AdresseLivraison_Commande,ID_Client) VALUES(@date,@adresse,@id)", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@id", this.idClient);
                requete.Parameters.AddWithValue("@adresse", this.adresse);
                requete.Parameters.AddWithValue("@date", this.date);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                DATABASE.Disconnect();
                return true;
            }
            return false;
        }

        
    }
}
