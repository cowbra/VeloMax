using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemeTransConnect
{
    public abstract class Vehicule
    {
        /// <summary>
        /// Classe mère principale des différents véhicules
        /// </summary>
        protected string type;
        public  Vehicule()
        {
            this.type = "Vehicule";
        }
        public virtual string Type { 
            get { return this.type; }
            set { this.type = value; }
        }
    }
}
