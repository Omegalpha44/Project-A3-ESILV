using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    abstract internal class Vehicule
    {
        // les attributs ne sont pas définis dans le sujet, je prends l'exemple donc de Uber en ne définissant que l'immatriculation du véhicule pour le reconnaitre. Cette classe est abstracte
        string immatriculation;
        bool disponible;

        //constructeur
        public Vehicule(string immatriculation)
        {
            this.immatriculation = immatriculation;
            this.disponible = true;
        }

        //getter-setter
        public string Immatriculation
        {
            get { return immatriculation; }
        }
        public bool Disponible
        {
            get { return disponible; }
            set { disponible = value; }
        }
        

    }
}
