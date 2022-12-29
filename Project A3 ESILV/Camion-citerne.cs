using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Camion_citerne : Vehicule
    {
        #region Champs
        int capacite;
        string type;
        #endregion

        #region Constructeur
        public Camion_citerne(string immatriculation, int capacite, string type) : base(immatriculation)
        {
            this.capacite = capacite;
            this.type = type;
        }
        #endregion

        #region getter-setter
        public int Capacite
        {
            get { return capacite; }
            set { capacite = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return "Camion_citerne : " + base.ToString() + " attributs : " + capacite + " " + type;
        }
        #endregion
    }
}
