using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace bdd
{
    public class Client
    {
        #region Attributs
        protected string tel;
        protected string email;
        protected string adresse;
        protected string nom;
        #endregion

        // Constructeur client particulier
        public Client(string tel, string email, string adresse, string nom)
        {
            this.tel = tel;
            this.email = email;
            this.adresse = adresse;
            this.nom = nom;
        }


    }
}
