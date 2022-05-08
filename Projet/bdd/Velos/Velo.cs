using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace bdd
{
    public class Velo
    {
        #region Attributs
        protected string nom;
        protected string grandeur;
        protected string cadre;
        protected string guidon;
        protected string? freins;
        protected string? selle;
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
        public Velo(string nom,string grandeur,string cadre,string guidon,string freins,string selle,string derailleurAvant,
            string derailleurArriere,string roueAvant,string roueArriere,string reflecteurs,string pedalier,string ordinateur, string panier) 
        {
            this.nom = nom;
            this.grandeur = grandeur;
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
                MySqlCommand requete = new MySqlCommand("INSERT INTO PIECE(Identifiant_Piece,Description_Piece,DateDebut_Piece,DateFin_Piece) VALUES(@Identifiant_Piece,@Description_Piece,@DateDebut_Piece,@DateFin_Piece)", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@Identifiant_Piece", this.nom);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                DATABASE.Disconnect();
                return true;
            }
            return false;
        }
    }
}
