namespace Project_A3_ESILV
{
    internal class Graphe
    {
        //-----------------------------------------------------------------------------------------------------------
        //  Choix d'un graphe ORIENTE car il peux y avoir des travaux dans un sens de circulation et pas dans l'autre
        //-----------------------------------------------------------------------------------------------------------

        #region Champs
        List<string> sommets; //liste des sommets du graphes, ici ce sont les noms des villes
        List<Arete> aretes;

        #endregion

        #region Constructeurs
        public Graphe(List<string> sommets, List<Arete> aretes)
        {
            this.sommets = sommets;
            this.aretes = aretes;
        }

        public Graphe()
        {
            this.sommets = new List<string>();
            this.aretes = new List<Arete>();
        }
        #endregion

        #region Propriétés
        public List<string> Sommets { 
            get => sommets; 
            set => sommets = value; 
        }

        public List<Arete> Aretes { 
            get => aretes; 
            set => aretes = value; 
        }
        #endregion

        #region Méthodes

        public float[,] GraphToMatr()
        {
            int n = sommets.Count();
            float[,] matr = new float[n, n];
            //initialisation de la matrice int.MaxValue représente +inf
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matr[i, j] = float.MaxValue;
                }
            }

            //remplissage de la matrice d'adjacence : à chaque nom de ville on associe un
            //entier via la position de la ville dans la liste Sommet
            //Exemple : si Paris est à la position 3 et Lyon à la 4, matr[3,4] donne la distance Paris->Lyon

            foreach (Arete arete in aretes)
            {
                matr[sommets.IndexOf(arete.Depart), sommets.IndexOf(arete.Arrivee)] = arete.Distance;
            }
            return matr;
        }

        public static void AfficheMatrice(float[,] mat)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    Console.Write(mat[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private int MinDistance(float[] distances, List<string> sommetsRestants)
        {
            int minIndex = 0;
            float minValue = int.MaxValue;
            for (int i = 0; i < sommetsRestants.Count; i++)
            {
                int index = sommets.IndexOf(sommetsRestants[i]);
                if (distances[index] <= minValue)
                {
                    minValue = distances[index];
                    minIndex = index;
                }
            }
            return minIndex;
        }

        // Méthode RemoveSommet à utiliser si on ne souhaite pas utiliser la méthode Remove déjà implémentée sur les listes C#
        // /!\ RemoveSommet ~ O(n) et Remove ~ O(n) aussi mais + optimisée

        private List<string> Removesommet(List<string> sommetsrestants, string sommet)
        {
            List<string> temp = new List<string>();
            for (int i = 0; i < sommetsrestants.Count(); i++)
            {
                if (sommetsrestants[i] != sommet)
                {
                    temp.Add(sommetsrestants[i]);
                }
            }
            return temp;
        }

        public void Relachement(float[] distances, float[,] matrAdj, string[] parent, string ville1, string ville2)
        {
            int u = sommets.IndexOf(ville1);
            int v = sommets.IndexOf(ville2);

            if (distances[v] > distances[u] + matrAdj[u,v]) 
            {
                distances[v] = distances[u] + matrAdj[u, v];
                parent[v] = ville1;
            }
        }

        public List<Arete> Dijkstra(string sommetDepart, string arrivee)
        {
        
        
            // Initialise le tableau des distances, des parents, et des sommets à visiter
            int n = sommets.Count;
            float[,] matrAdj = this.GraphToMatr();
            float[] distances = new float[n];
            string[] parents = new string[n];
            List<string> sommetsRestants = new List<string>();
            List<Arete> res = new List<Arete>();
            for (int i = 0; i < n; i++)
            {
                distances[i] = int.MaxValue; //correspond à une distance infinie ici
                sommetsRestants.Add(sommets[i]);
            }
            distances[sommets.IndexOf(sommetDepart)] = 0;

            // Boucle principale de l'algorithme
            while (sommetsRestants.Count > 0)
            {
                //foreach (string sommet in sommetsRestants) Console.WriteLine(sommet);

                // Trouve le sommet avec la distance minimale
                int u = MinDistance(distances, sommetsRestants);
                //Console.WriteLine("indice min : "+u);

                // Pour chaque voisin v du sommet u
                for (int v = 0; v < n; v++)
                {
                    if (matrAdj[u, v] <float.MaxValue) // s'il existe une arête (u,v)
                    {
                        Relachement(distances, matrAdj, parents, sommets[u], sommets[v]);
                    }
                }

                // Retire le sommet trouvé de la liste des sommets restants
                sommetsRestants.Remove(sommets[u]);
                
            }

            //Console.WriteLine("distances : ");
            //for (int i = 0; i < sommets.Count; i++)
            //{
            //    Console.WriteLine(sommets[i] + " : " + "distance=" + distances[i] + ", parent = " + parents[i]);
            //}

            //A partir de distances et parents on construit l'itinéraire
            string ville = arrivee;
            string parent = parents[sommets.IndexOf(ville)];

            //cas livraisons dans une même ville. exemple : Paris-->Paris
            if (sommetDepart == arrivee)
            {
                res.Add(new Arete(sommetDepart, arrivee, 0, new TimeSpan(0)));
            }

            //cas où le trajet n'est pas possible avec les arêtes de distances.csv 
            else if (parent == null)
            {
                Console.WriteLine("Aucun itinéraire n'a été trouvé, veuillez vérifier le fichier distances.csv");
            }

            //cas où le trajet est possible
            else
            {
                int indexVille = sommets.IndexOf(ville);
                int indexParent = sommets.IndexOf(parent);
                int nMax = 0; //gère le cas de graphe non connexe où le chemin ville départ --> ville arrivée n'existe pas
                TimeSpan duree = new TimeSpan(0);
                while (parent != sommetDepart && nMax < sommets.Count)
                {
                    duree = aretes.Find(x => x.Depart == parent && x.Arrivee == ville).Duree;
                    res.Insert(0, new Arete(parent, ville, matrAdj[indexParent, indexVille],duree));
                    ville = parent;
                    parent = parents[sommets.IndexOf(ville)];
                    indexVille = sommets.IndexOf(ville);
                    indexParent = sommets.IndexOf(parent);
                    nMax++;
                }
                duree = aretes.Find(x => x.Depart == parent && x.Arrivee == ville).Duree;
                res.Insert(0, new Arete(parent, ville, matrAdj[indexParent, indexVille], duree));
                //foreach (Arete arete in res) Console.WriteLine(arete.ToString());
            }
            return res;
        }

        #region autre implémentation de Dijkstra (avec dictionnaire, tuples...)
        //public static string Dijkstra(string deb, string fin, List<Tuple<string, string, int>> arretes) // algorithme de Dijkstra
        //                                                                                                // deb = ville de départ, fin = ville d'arrivée, arretes = liste des arretes du graphe
        //{
        //    List<string> sommets = new List<string>();
        //    List<string> sommetsTraites = new List<string>();
        //    List<string> sommetsNonTraites = new List<string>();
        //    Dictionary<string, int> distances = new Dictionary<string, int>(); // distance entre le sommet de départ et le sommet en question
        //    Dictionary<string, string> predecesseurs = new Dictionary<string, string>(); // sommet précédent
        //    foreach (Tuple<string, string, int> arrete in arretes)
        //    {
        //        if (!sommets.Contains(arrete.Item1)) sommets.Add(arrete.Item1);
        //        if (!sommets.Contains(arrete.Item2)) sommets.Add(arrete.Item2);
        //    }
        //    foreach (string sommet in sommets) // on reset les distances
        //    {
        //        distances.Add(sommet, int.MaxValue);
        //        predecesseurs.Add(sommet, null);
        //        sommetsNonTraites.Add(sommet);
        //    }
        //    distances[deb] = 0;
        //    while (sommetsNonTraites.Count > 0)
        //    {
        //        string sommetActuel = sommetsNonTraites[0];
        //        foreach (string sommet in sommetsNonTraites)
        //        {
        //            if (distances[sommet] < distances[sommetActuel]) sommetActuel = sommet;
        //        }
        //        sommetsNonTraites.Remove(sommetActuel);
        //        sommetsTraites.Add(sommetActuel);
        //        foreach (Tuple<string, string, int> arrete in arretes)
        //        {
        //            if (arrete.Item1 == sommetActuel && !sommetsTraites.Contains(arrete.Item2))
        //            {
        //                int distance = distances[sommetActuel] + arrete.Item3;
        //                if (distance < distances[arrete.Item2])
        //                {
        //                    distances[arrete.Item2] = distance;
        //                    predecesseurs[arrete.Item2] = sommetActuel;
        //                }
        //            }
        //        }
        //    }
        //    string chemin = fin;
        //    string predecesseur = predecesseurs[fin];
        //    while (predecesseur != null)
        //    {
        //        chemin = predecesseur + " -> " + chemin;
        //        predecesseur = predecesseurs[predecesseur];
        //    }
        //    return chemin;
        //}
        #endregion

        #endregion
    }
}
