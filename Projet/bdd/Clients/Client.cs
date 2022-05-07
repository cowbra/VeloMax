namespace bdd
{
    public abstract class Client
    {
        #region Attributs
        protected string tel;
        protected string email;
        protected string adresse;
        protected string nom;
        protected string type;
        #endregion

        // Constructeur client particulier
        public Client(string tel, string email, string adresse, string nom)
        {
            this.tel = tel;
            this.email = email;
            this.adresse = adresse;
            this.nom = nom;
            this.type = "Undefined";
        }

        public abstract string Type
        {
            get;
        }

        // Methode qui doit obligatoirement être definie dans les classes héritées
        public abstract bool AddToBdd();

    }
}
