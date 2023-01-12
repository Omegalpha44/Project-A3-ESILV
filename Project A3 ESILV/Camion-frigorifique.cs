using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Camion_frigorifique : Camion
    {
        #region Champs
        int nbGroupeElectrogene; //un camion pourra réfrigérer les aliments que sur une distance maximum dépendant du nb de groupes électrogènes à bord
        int autonomie; // autonomie = 200km*nbGroupeElectrogene
        #endregion

        #region Constructeur
        public Camion_frigorifique(string immatriculation, int capacite, List<string> matieresTransportees, int nbGroupeElectrogene) : base(immatriculation, capacite, matieresTransportees)
        {
            this.nbGroupeElectrogene = nbGroupeElectrogene;
            this.autonomie = 200 * nbGroupeElectrogene;
        }


        #endregion

        #region getter-setter
        public int NbGroupeElectrogene { 
            get => nbGroupeElectrogene; 
            set => nbGroupeElectrogene = value; 
        }

        public int Autonomie { 
            get => autonomie; 
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return "Camion_frigorifique : " + base.ToString() + " Nb groupes électrogènes : " + nbGroupeElectrogene + " Autonomie : "+autonomie;
        }
        #endregion
    }
}
