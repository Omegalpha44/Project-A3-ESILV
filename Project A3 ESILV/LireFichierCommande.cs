using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class LireFichierCommande
    {
        Manager manager;
        string path;
        string pathClient;
        public LireFichierCommande(Manager manager, string path)
        {
            this.manager = manager;
            this.path = path;
        }
        public LireFichierCommande(string path)
        {
            this.manager = new Manager();
            this.path = path;
        }

        public Manager Manager { get; }
        public void ReadFile()
        {
            /*-------------------------------------------------------------------------------------------------------
            lis un fichier CSV et en tire les informations pour créer des objets de type Commande
            le fichier doit être de la forme suivante:
            idCommande,idClient,villeDepart,villeArrivee,idVehicule,idChauffeur,dateLivraison
            Example:
            02345,6578,Paris,Nancy,324,879,01/01/2023
            -------------------------------------------------------------------------------------------------------*/

            string filePath = "commandes.csv";
            if (!File.Exists(filePath))
            {
                // Création du fichier commandes.csv s'il n'existe pas
                StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine("idCommande,idClient,villeDepart,villeArrivee,idVehicule,idChauffeur,dateLivraison");
            }

            //on ouvre le fichier
            StreamReader sr = new StreamReader(path);
            string line = sr.ReadLine();

            //on lit le fichier ligne par ligne
            while (line != null)
            {
                //on découpe la ligne en fonction des virgules
                string[] mots = line.Split(", ");
                //on crée un objet de type Commande avec les informations de la ligne
                string[] livraison = mots[6].Split("/");
                DateTime dateLivraison = new DateTime(int.Parse(livraison[2]), int.Parse(livraison[1]), int.Parse(livraison[0]));

                int idClient = int.Parse(mots[1]);
                int idVehicule = int.Parse(mots[4]);
                Client cl = manager.Clients.Find(x => x.Id == idClient);
                //exception à ajouter 
                if (cl == null) Console.WriteLine("Votre client n°{0} n'a pas été trouvé dans la BDD, veuillez d'abord l'ajouter via le Module Client",idClient);
                else
                {
                    Commande commande = manager.GenerationDeCommande(manager.Commandes.Count, cl, mots[2], mots[3]);
                    Console.WriteLine(commande);
                    //on ajoute la commande à la liste des commandes
                    manager.Commandes.Add(commande);
                }
                //on lit la ligne suivante
                line = sr.ReadLine();
            }
            sr.Close();
        }
        //public void Add(Commande c, string nom, string prenom)
        //{
            
        //}
        //public void Remove(int idCommande)
        //{

        //}
    }
}
