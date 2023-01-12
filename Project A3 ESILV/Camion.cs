using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Camion: Vehicule
    {
        #region Champs
        int capacite;
        List<string> matiereTransportees;
        #endregion

        #region constructeur
        public Camion(string immatriculation,int capacite, List<string> matiereTransportees) : base(immatriculation)
        {
            this.capacite = capacite;
            this.matiereTransportees = matiereTransportees;
        }
        #endregion

        #region getter-setter
        public List<string> MatieresTransportees
        {
            get { return matiereTransportees; }
            set { matiereTransportees = value; }
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            string res = base.ToString();
            res += " capacité : " + capacite;
            res += " matières transportées : ";
            foreach (string s in matiereTransportees)
            {
                res += s + " ";
            }
            return res;
        }
        #endregion
    }
}
