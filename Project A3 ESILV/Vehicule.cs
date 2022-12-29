using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    abstract internal class Vehicule
    {
        #region Champs
        // les attributs ne sont pas définis dans le sujet, je prends l'exemple donc de Uber en ne définissant que l'immatriculation du véhicule pour le reconnaitre. Cette classe est abstracte
        string immatriculation;
        bool disponible;
        #endregion

        #region Constructeur
        public Vehicule(string immatriculation)
        {
            this.immatriculation = immatriculation;
            this.disponible = true;
        }
        #endregion

        #region getter-setter
        public string Immatriculation
        {
            get { return immatriculation; }
        }
        public bool Disponible
        {
            get { return disponible; }
            set { disponible = value; }
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return "Vehicule : " + immatriculation + " disponible : " + disponible;
        }
        #endregion
    }
}
