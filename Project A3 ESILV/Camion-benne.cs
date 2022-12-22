using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Camion_benne : Vehicule
    {
        List<string> equipements;

        //constructeur
        public Camion_benne(string immatriculation, List<string> equipements) : base(immatriculation)
        {
            this.equipements = equipements;
        }

        //getter-setter
        public List<string> Equipements
        {
            get { return equipements; }
            set { equipements = value; }
        }

        //méthodes
        public override string ToString()
        {
            string res = "Camion_benne : " + base.ToString() + " equipements : ";
            foreach (string s in equipements)
            {
                res += s + " ";
            }
            return res;
        }
        
    }
}
