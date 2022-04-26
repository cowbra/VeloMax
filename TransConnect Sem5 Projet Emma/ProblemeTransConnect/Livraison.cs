using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemeTransConnect
{
    /// <summary>
	/// Classe créant un objet de type Livraison avec un 'Volume' et un 'Type'
	/// </summary>
    public class Livraison
    {
        protected string volume;
        protected string marchandise;

        public Livraison ()
        {
            this.volume = "";
            this.marchandise = "aucune";
        }

        public Livraison(string volume,string marchandise)
        {
            this.volume = volume;
            this.marchandise=marchandise;
        }

        public string Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        public string Marchandise
        {
            get { return marchandise; }
            set { marchandise = value; }
        }
    }
}
