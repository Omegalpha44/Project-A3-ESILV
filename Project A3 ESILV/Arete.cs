using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Arete
    {
        #region Champs

        string depart;
        string arrivee;
        float distance;

        #endregion

        #region Propriétés

        public float Distance { 
            get => distance; 
            set => distance = value; 
        }

        public string Depart { 
            get => depart; 
            set => depart = value; 
        }

        public string Arrivee { 
            get => arrivee; 
            set => arrivee = value; 
        }

        #endregion

        #region Constructeurs

        public Arete(string depart, string arrivee, float distance)
        {
            this.depart = depart;
            this.arrivee = arrivee;
            this.distance = distance;
        }

        #endregion

        #region Méthodes
        override public string ToString()
        {
            string descr = "("+this.depart+"->"+this.arrivee+","+this.distance+")";
            return descr;
        }
        #endregion


    }
}
