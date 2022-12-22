using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Camion_frigorifique : Vehicule
    {
        int capacite;

        //constructeur
        public Camion_frigorifique(string immatriculation, int capacite) : base(immatriculation)
        {
            this.capacite = capacite;
        }

        //getter-setter
        public int Capacite
        {
            get { return capacite; }
            set { capacite = value; }
        }

        //méthodes
        public override string ToString()
        {
            return "Camion_frigorifique : " + base.ToString() + " capacite : " + capacite+"L";
        }
    }
}
