using Project_A3_ESILV;
using System.Security.Cryptography.X509Certificates;

namespace Solution
{
    public class Program
    {
        static void Main(string[] args)
        {
            Manager MainManager = new Manager(); // on créé un manager vide
            AffichageGraphique graphique = new AffichageGraphique(@"employee.csv",@"client.csv",@"distances.csv",@"commandes.csv",MainManager); // on créé un afficheur graphique contenant les différents path vers les fichiers étudiés
            graphique.fileExplorerDistances.InitialisationGraphe(); //on génère le graphe à partir du fichier "distances.csv"
            graphique.MainAffichage(); // MainLoop de l'affichage. Dispose d'un système anti-erreur et est récursive.
            
                
        }
    }


}