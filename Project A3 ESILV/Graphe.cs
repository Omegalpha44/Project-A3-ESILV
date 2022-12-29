using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Graphe
    {
        //----------------------------------------------------------------------------------------
        //  Choix d'un graphe ORIENTE car il peux y avoir des travaux dans un sens de circulation
        //----------------------------------------------------------------------------------------

        #region Champs
        List<String> sommets; //liste des sommets du graphes, ici ce sont les noms des villes
        List<(String, String, int)> aretes;


        #endregion

        #region Constructeurs
        public Graphe(List<string> sommets, List<(string, string, int)> aretes)
        {
            sommets = sommets;
            aretes = aretes;
        }
        #endregion

        #region Propriétés
        #endregion

        #region Méthodes
        public int[] Dijkstra(Graphe graphe,string sommetDepart)
        {
            // Initialise le tableau des distances, des parents, et des sommets à visiter
            int[] distances = new int[graphe.sommets.Count];
            string[] parents = new string[graphe.sommets.Count];
            string[] sommetsRestants = new string[graphe.sommets.Count];
            for (int i = 0; i < graphe.sommets.Count; i++)
            {
                distances[i] = -1; //-1 correspond à une distance infinie ici
                parents[i] = "";
                sommetsRestants[i] = graphe.sommets[i];
            }

           
        }
        #endregion
    }
}
