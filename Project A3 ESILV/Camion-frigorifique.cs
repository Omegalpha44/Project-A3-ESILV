using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Camion_frigorifique : Vehicule
    {
        #region Champs
        int capacite;
        #endregion

        #region Constructeur
        public Camion_frigorifique(string immatriculation, int capacite) : base(immatriculation)
        {
            this.capacite = capacite;
        }
        #endregion

        #region getter-setter
        public int Capacite
        {
            get { return capacite; }
            set { capacite = value; }
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return "Camion_frigorifique : " + base.ToString() + " capacite : " + capacite+"L";
        }
        #endregion
    }
}
