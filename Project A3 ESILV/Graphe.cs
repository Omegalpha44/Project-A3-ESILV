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
            return null;
        }

        public static string Dijkstra(string deb, string fin, List<Tuple<string, string, int>> arretes) // algorithme de Dijkstra
                                                                                                        // deb = ville de départ, fin = ville d'arrivée, arretes = liste des arretes du graphe
        {
            List<string> sommets = new List<string>();
            List<string> sommetsTraites = new List<string>();
            List<string> sommetsNonTraites = new List<string>();
            Dictionary<string, int> distances = new Dictionary<string, int>(); // distance entre le sommet de départ et le sommet en question
            Dictionary<string, string> predecesseurs = new Dictionary<string, string>(); // sommet précédent
            foreach (Tuple<string, string, int> arrete in arretes)
            {
                if (!sommets.Contains(arrete.Item1)) sommets.Add(arrete.Item1);
                if (!sommets.Contains(arrete.Item2)) sommets.Add(arrete.Item2);
            }
            foreach (string sommet in sommets) // on reset les distances
            {
                distances.Add(sommet, int.MaxValue);
                predecesseurs.Add(sommet, null);
                sommetsNonTraites.Add(sommet);
            }
            distances[deb] = 0;
            while (sommetsNonTraites.Count > 0)
            {
                string sommetActuel = sommetsNonTraites[0];
                foreach (string sommet in sommetsNonTraites)
                {
                    if (distances[sommet] < distances[sommetActuel]) sommetActuel = sommet;
                }
                sommetsNonTraites.Remove(sommetActuel);
                sommetsTraites.Add(sommetActuel);
                foreach (Tuple<string, string, int> arrete in arretes)
                {
                    if (arrete.Item1 == sommetActuel && !sommetsTraites.Contains(arrete.Item2))
                    {
                        int distance = distances[sommetActuel] + arrete.Item3;
                        if (distance < distances[arrete.Item2])
                        {
                            distances[arrete.Item2] = distance;
                            predecesseurs[arrete.Item2] = sommetActuel;
                        }
                    }
                }
            }
            string chemin = fin;
            string predecesseur = predecesseurs[fin];
            while (predecesseur != null)
            {
                chemin = predecesseur + " -> " + chemin;
                predecesseur = predecesseurs[predecesseur];
            }
            return chemin;
        }
        #endregion
    }
}
