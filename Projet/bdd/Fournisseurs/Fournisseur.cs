using MySql.Data.MySqlClient;

namespace bdd
{
    public class Fournisseur
    {
        #region Attributs
        protected Int64 siret;
        protected string nomEntreprise;
        protected string contact;
        protected string adresse;
        protected string libelle;
        #endregion

        public Fournisseur(Int64 siret, string nomEntreprise, string contact, string adresse, string libelle)
        {
            this.siret = siret;
            this.nomEntreprise = nomEntreprise;
            this.contact = contact;
            this.adresse = adresse;
            this.libelle = libelle;
        }

        public bool AddToBdd()
        {
            BDD DATABASE = new BDD();
            DATABASE.Connect();
            if (DATABASE.Connected == true)
            {

                MySqlCommand requete = new MySqlCommand("INSERT INTO FOURNISSEUR(Siret_Fournisseur,NomEntreprise_Fournisseur,Contact_Fournisseur,Adresse_Fournisseur,Libelle_Fournisseur) VALUES(@Siret,@NomEntreprise,@Contact,@Adresse,@Libelle)", DATABASE.MySqlConnection);
                requete.Parameters.AddWithValue("@Siret", this.siret);
                requete.Parameters.AddWithValue("@NomEntreprise", this.nomEntreprise);
                requete.Parameters.AddWithValue("@Contact", this.contact);
                requete.Parameters.AddWithValue("@Adresse", this.adresse);
                requete.Parameters.AddWithValue("@Libelle", this.libelle);

                requete.ExecuteNonQuery();
                requete.Parameters.Clear();

                DATABASE.Disconnect();
                return true;
            }
            return false;
        }
    }
}
