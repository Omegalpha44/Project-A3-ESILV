using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    public delegate void Ui();
    internal class AffichageGraphique
    {
        
        Manager manager;

        void ExceptionManager(Ui func) // permet, si une erreur est détecté, de relancer la méthode
        {
            try { func(); }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Une erreur est survenue, veuillez vérifier vos réponses");
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                ExceptionManager(Affichage);
            }
        }
        public AffichageGraphique(Manager manager)
        {
            this.manager = manager;
        }
        int GoodValue(int a, int b)
        {
            int r = int.Parse(Console.ReadLine());
            if (a <= r && r <= b)
            {
                return r;
            }
            else
            {
                Console.WriteLine("Valeur incorrecte");
                return GoodValue(a, b);
            }
        }
        public void MainAffichage()
        {
            ExceptionManager(Affichage);
        }
        void Affichage()
        {
            Console.Clear();
            Console.WriteLine("Bienvenue chez TransConnect");
            Console.WriteLine("Choississez votre module");
            Console.WriteLine("================");
            Console.WriteLine("1. Gestion des clients");
            Console.WriteLine("2. Gestion des salariés");
            Console.WriteLine("3. Gestion des commandes");
            Console.WriteLine("4. Statistiques");
            Console.WriteLine("5. Autres fonctions");
            Console.WriteLine("6.Quitter");
            Console.WriteLine("===============");
            Console.WriteLine("Votre choix : ");
            int r = GoodValue(1, 6);
            switch (r)
            {
                case 1: ModuleClient(); break;
                case 2: ModuleSalarie(); break;
                case 3: ModuleCommande(); break;
                case 4: ModuleStatistique(); break;
                case 5: ModuleAutre(); break;
                case 6: Exit(); break;
            }
        }
        #region Module Client
        void ModuleClient()
        {
            Console.Clear();
            Console.WriteLine("Bienvenue dans le module client");
            Console.WriteLine("Choississez votre action");
            Console.WriteLine("================");
            Console.WriteLine("1. Ajouter un client");
            Console.WriteLine("2. Modifier un client");
            Console.WriteLine("3. Supprimer un client");
            Console.WriteLine("4. Afficher la liste des clients");
            Console.WriteLine("5. Retour");
            Console.WriteLine("===============");
            Console.WriteLine("Votre choix : ");
            int r = GoodValue(1, 5);
            Console.Clear();
                switch (r)
                {
                    case 1: ExceptionManager(AjouterClient); break;
                    case 2: ExceptionManager(ModifierClient); break;
                    case 3: ExceptionManager(SupprimerClient); break;
                    case 4: ExceptionManager(AfficherClient); break;
                    case 5: ExceptionManager(Affichage); break;
                }
        }
        void AjouterClient()
        {
                Console.WriteLine("Vous avez choisis de ajouter un nouveau client");
                Console.WriteLine("===============");
                Console.Write("nom : ");
                string nom = Console.ReadLine();
                Console.WriteLine();
                Console.Write("prenom : ");
                string prenom = Console.ReadLine();
                Console.WriteLine();
                Console.Write("date de Naissance (jour/mois/année): ");
                string date = Console.ReadLine();
                string[] dates = date.Split('/');
                DateTime birth = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]));
                Console.WriteLine();
                Console.Write("adresse : ");
                string adresse = Console.ReadLine();
                Console.WriteLine();
                Console.Write("adresse mail : ");
                string mail = Console.ReadLine();
                Console.WriteLine();
                Console.Write("numero de telephone : ");
                int tel = int.Parse(Console.ReadLine());
                Console.WriteLine();
                manager.AjouterClient(nom, prenom, birth, adresse, mail, tel);
                Console.WriteLine("Client Ajouté !");
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                Affichage();
        }
        void ModifierClient()
        {
                Console.WriteLine("Vous avez choisis de modifier un client");
                Console.WriteLine("===============");
                if (manager.Clients.Count > 0)
                {
                    Console.WriteLine("Voici la liste des clients :");
                    int i = 0;
                    foreach (Client c in manager.Clients)
                    {
                        Console.Write(i + ": ");
                        Console.WriteLine(c.Id + " : " + c.Nom + " " + c.Prenom);
                        i++;
                    }
                    Console.WriteLine("Quel client voulez vous modifier ? :");
                    int r = GoodValue(0, manager.Clients.Count);
                    Client cible = manager.Clients[r];
                    Console.WriteLine("Modification de {0}", cible.Id + "  " + cible.Nom + " " + cible.Prenom);
                    Console.WriteLine("Que souhaitez vous modifier ? \n 1 : nom, 2 : prenom, 3 : date de naissance, 4 : adresse, 5 : adresse mail, 6 : numero de telephone");
                    Console.WriteLine("  :  ");
                    int l = GoodValue(1, 6);
                    switch (l)
                    {
                        case 1: Console.WriteLine("Entrez le nouveau nom : "); cible.Nom = Console.ReadLine(); break;
                        case 2: Console.WriteLine("Entrez le nouveau prenom : "); cible.Prenom = Console.ReadLine(); break;
                        case 3:
                            {
                                Console.WriteLine("Entrez la nouvelle date de naissance  (jour/mois/année) : ");
                                string date = Console.ReadLine();
                                string[] dates = date.Split('/');
                                DateTime birth = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]));
                                cible.DateNaissance = birth;
                                break;
                            }
                        case 4: Console.WriteLine("Entrez la nouvelle adresse : "); cible.Adresse = Console.ReadLine(); break;
                        case 5: Console.WriteLine("Entrez la nouvelle adresse mail : "); cible.AdresseMail = Console.ReadLine(); break;
                        case 6: Console.WriteLine("Entrez le numéro de téléphone : "); cible.Telephone = int.Parse(Console.ReadLine()); break;
                    }

                }
                else

                {
                    Console.WriteLine("Pas de client dans la banque de donnée : Veuillez en rajouter avant de procéder à une modification");
                }
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                Affichage();    
        }
        void SupprimerClient()
        {
            Console.WriteLine("Vous avez choisis de supprimer un client");
            Console.WriteLine("===============");
            if (manager.Clients.Count > 0)
            {
                Console.WriteLine("Voici la liste des clients :");
                int i = 0;
                foreach (Client c in manager.Clients)
                {
                    Console.Write(i + ": ");
                    Console.WriteLine(c.Id + " : " + c.Nom + " " + c.Prenom);
                    i++;
                }
                Console.WriteLine("Quel client voulez vous supprimer ? :");
                int r = GoodValue(0, manager.Clients.Count);
                Client cible = manager.Clients[r];
                Console.WriteLine("Suppression de {0}", cible.Id + "  " + cible.Nom + " " + cible.Prenom);
                manager.Clients.Remove(cible);
                Console.WriteLine("CLient supprimé");
            }
            else
            {
                Console.WriteLine("Pas de client dans la banque de donnée : Veuillez en rajouter avant de procéder à une suppression");
            }
            Console.WriteLine("===============");
            Console.WriteLine("Appuyez sur une touche ...");
            Console.ReadKey();
            Console.Clear();
            Affichage();
        }
        void AfficherClient()
        {
            Console.WriteLine("Vous avez choisis d'afficher les clients");
            Console.WriteLine("===============");
            Console.WriteLine("Quel méthode de tri souhaitez-vous appliquer à la liste des clients ? \n 1 : par nom et prénom, 2 : par adresse, 3 : par prix total de commande, 4 : ne pas trier :");
            int l = GoodValue(1, 4);
            switch(l)
            {
                case 1: manager.TriClientParOrdreAlphabetique(); break;
                case 2: manager.TriClientParAdresse(); break;
                case 3: manager.TriClientParPrix(); break;
                case 4: break;    
            }
            bool boo = true;
            Console.WriteLine("Voulez vous afficher toutes les informations des client ? (O/N)");
            string s = Console.ReadLine();
            boo = (s == "O" || s == "o");
            if (manager.Clients.Count > 0)
            {
                foreach(Client c in manager.Clients)
                {
                    if(boo) Console.WriteLine(c.ToStringComplete());
                    else Console.WriteLine(c.ToString());
                }
            }
            else Console.WriteLine("Pas de client dans la banque de donnée : Veuillez en rajouter avant de procéder à un affichage");
            Console.WriteLine("===============");
            Console.WriteLine("Appuyez sur une touche ...");
            Console.ReadKey();
            Console.Clear();
            Affichage();
        }
        #endregion
        void ModuleSalarie() { }
        void ModuleCommande() { }
        void ModuleStatistique() { }
        void ModuleAutre() { }
        void Exit() { }
    }
    
}
