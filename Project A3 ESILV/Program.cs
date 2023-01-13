using Project_A3_ESILV;

namespace Solution
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region test des arbres n-aire
            Salarie s0 = new Salarie(0, "Patrick", "Jean", new DateTime(1956, 5, 5), "0", "0", 0, new DateTime(10, 10, 10), "PDG", 0);
            Salarie s1 = new Salarie(1, "1", "1", new DateTime(1, 1, 1), "1", "1", 1, new DateTime(1, 1, 1), "chauffeur", 1);
            Salarie s2 = new Salarie(2, "2", "2", new DateTime(2, 2, 2), "2", "2", 2, new DateTime(2, 2, 2), "chauffeur", 2);
            Salarie s3 = new Salarie(3, "3", "3", new DateTime(3, 3, 3), "3", "3", 3, new DateTime(3, 3, 3), "chauffeur", 3);
            Salarie s4 = new Salarie(4, "4", "4", new DateTime(4, 4, 4), "4", "4", 4, new DateTime(4, 4, 4), "chauffeur", 4);
            Salarie s5 = new Salarie(5, "5", "5", new DateTime(5, 5, 5), "5", "5", 5, new DateTime(5, 5, 5), "chauffeur", 5);
            Salarie s6 = new Salarie(6, "6", "6", new DateTime(6, 6, 6), "6", "6", 6, new DateTime(6, 6, 6), "chauffeur", 6);
            Salarie s7 = new Salarie(7, "7", "7", new DateTime(7, 7, 7), "7", "7", 7, new DateTime(7, 7, 7), "chauffeur", 7);
            Salarie s8 = new Salarie(8, "8", "8", new DateTime(8, 8, 8), "8", "8", 8, new DateTime(8, 8, 8), "chauffeur", 8);
            Salarie s9 = new Salarie(9, "9", "9", new DateTime(9, 9, 9), "9", "9", 9, new DateTime(9, 9, 9), "chauffeur", 9);
            Salarie s10 = new Salarie(10, "10", "10", new DateTime(10, 10, 10), "10", "10", 10, new DateTime(10, 10, 10), "chauffeur", 10);
            Salarie s11 = new Salarie(11, "11", "11", new DateTime(11, 11, 11), "11", "11", 11, new DateTime(11, 11, 11), "chauffeur", 11);
            List<Salarie> listSalarie = new List<Salarie> { s1, s2, s3, s4, s5, s6, s7, s8, s9 };
            SalariesArbre salarieArbre = new SalariesArbre(s0);
            salarieArbre.CreerArbre(listSalarie);
            salarieArbre.AjouterSalarie(s10, "9", "9");
            salarieArbre.AjouterSalarie(s11, "9", "9");
            salarieArbre.AfficherHierarchie();
            salarieArbre.RetirerSalarie("7", "7", "chauffeur");
            salarieArbre.AfficherHierarchie();
            string[] tab = salarieArbre.CoupleSalarieEmployeur("1", "1", "chauffeur");
            Console.WriteLine(tab[0] + tab[1]);
            string[,] graphe = salarieArbre.GrapheAdjacence();
            for (int i = 0; i < graphe.GetLength(0); i++)
            {
                for (int j = 0; j < graphe.GetLength(1); j++)
                {
                    Console.Write(graphe[i, j] + " ");
                }
                Console.WriteLine();
            }
            salarieArbre.Affichage();
            #endregion
            AffichageGraphique graphique = new AffichageGraphique(new Manager());
            graphique.MainAffichage();

        }
    }


}