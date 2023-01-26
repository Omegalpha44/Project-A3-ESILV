using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Project_A3_ESILV
{
    public delegate void Ui();
    internal class AffichageGraphique
    {
        #region Champs
        public Manager manager;
        public LireFichier fileExplorer; // explorateur de fichier permettant de modifier la sauvegarde des salariés
        public LireFichier fileExplorerClient; // explorateur de fichier permettant de modifier la sauvegarde des clients
        public LireFichier fileExplorerDistances; // explorateur de fichier permettant de lire distances.csv
        public LireFichier fileExplorerCommandes; // explorateur de fichier permettant de lire commandes.csv
        bool display = false; // gère l'affichage de l'organigramme
        bool main = true; // Gère si l'animation doit continuer
        #endregion

        #region Constructeurs
        public AffichageGraphique(string pathSalaries,string pathClients, string pathDistances, string pathCommandes, Manager manager)
        {
            this.manager = manager;
            this.fileExplorer = new LireFichier(manager, pathSalaries);
            this.fileExplorerClient = new LireFichier(manager, pathClients);
            this.fileExplorerDistances = new LireFichier(manager, pathDistances);
            this.fileExplorerCommandes = new LireFichier(manager, pathCommandes);
        }
        #endregion

        #region Méthodes

        #region AffichageGLobale
        void ExceptionManager(Ui func) // permet, si une erreur est détecté, de relancer la méthode
        {
            try { func(); }
            catch (Exception e)
            {
                main = false;
                Console.WriteLine(e.Message);
                Console.WriteLine("Une erreur est survenue, veuillez vérifier vos réponses");
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                ExceptionManager(Affichage);
            }
        }
        int GoodValue(int a, int b) // vérifie la valeur fournie en entrée
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
        public void MainAffichage() // affichage central
        {
            bool error = false;
            try
            {
                if (File.Exists("key.txt"))
                {
                    TextReader tr = new StreamReader("key.txt");
                    string permu = tr.ReadLine();
                    tr.Close();
                    if (permu != null)
                    {
                        LireFichier.DechiffrageSub("employeeCipher.csv", "employee.csv", permu); // déchiffrement
                        LireFichier.DechiffrageSub("clientCipher.csv", "client.csv", permu); // chiffrement
                    }
                }
                fileExplorerClient.ReadFileClient();
                fileExplorer.ReadFile();
                fileExplorerCommandes.ReadFileCommandeArchived();
            }
            catch(Exception e)
            {
                error = true;
                Console.WriteLine(e.Message);
                Console.WriteLine("Une erreur est survenue dans la lecture des fichiers csv de sauvegarde, veuillez vérifier que vos entrées respectent le bon format");
                Console.WriteLine("================");
                Console.WriteLine("Veuillez appuyer sur une touche ...");
                Console.ReadKey();
                Console.Clear();

            }
            
            if(!error) ExceptionManager(Affichage);
        }
        void TransconnectStylised() // effet stylisé en haut de la page d'accueil
        {
            string DecallageTexte(string text)
            {
                string res = "";
                for(int i =1;i<text.Length;i++)
                {
                    res += text[i];
                }
                res +=text[0];
                return res;
            }
            string text = "Bienvenue chez TransConnect  ";
            for (int i = 0; i < text.Length; i++)
            {
                if (main)
                {
                    (int left,int position) = Console.GetCursorPosition();
                    Console.SetCursorPosition(0, 0);
                    Console.Write(text);
                    Console.SetCursorPosition(left, position);
                    text = DecallageTexte(text);
                    Thread.Sleep(100);
                }
                
            }
            if(main) TransconnectStylised();
        }
        void Affichage()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Choississez votre module");
            Console.WriteLine("================");
            Console.WriteLine("1. Gestion des clients");
            Console.WriteLine("2. Gestion des salariés");
            Console.WriteLine("3. Gestion des commandes");
            Console.WriteLine("4. Statistiques");
            Console.WriteLine("5. Autres fonctions");
            Console.WriteLine("6. Quitter");
            Console.WriteLine("===============");
            Console.WriteLine("Votre choix : ");
            Task Style = new Task(() => TransconnectStylised());
            main = true;
            Style.Start();
            int r = GoodValue(1, 6);
            main = false;
            
            Style.Wait();
            //end task Style

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
        #endregion

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
            int GoodSecu()
            {
                Console.Write("numero d'identification : ");
                int secu = int.Parse(Console.ReadLine());
                if (manager.Clients.FindAll(x => x.Id == secu).Count != 0)
                {
                    Console.WriteLine("Ce numéro d'identification existe déjà.");
                    Console.WriteLine("===============");
                    Console.WriteLine("Voulez vous ressaisir le numéro d'identification du client ? (o/n)");
                    string l = Console.ReadLine();
                    if (l == "o" || l == "O" || l == "0")
                    {
                        return GoodSecu();
                    }
                    else
                    {
                        return -1;
                    }
                }
                else return secu;
            }
            int secu = GoodSecu();
            if (secu != -1)
            {
                Client c = new Client(secu, nom, prenom, birth, adresse, mail, tel);
                manager.AjouterClient(c);
                fileExplorerClient.Add(c);
                Console.WriteLine("Client Ajouté !");
            }
            else Console.WriteLine("Client non ajouté");
            Console.WriteLine("===============");
            Console.WriteLine("Appuyez sur une touche ...");
            Console.ReadKey();
            Console.Clear();
            ModuleClient();
            
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
                Console.WriteLine("Que souhaitez vous modifier ? \n 1 : nom, 2 : adresse, 3 : adresse mail, 4 : numero de telephone");
                Console.WriteLine("  :  ");
                int l = GoodValue(1, 6);
                switch (l)
                {
                    case 1: Console.WriteLine("Entrez le nouveau nom : "); cible.Nom = Console.ReadLine();fileExplorerClient.Modify(cible,0,cible.Nom) ; break;
                    case 2: Console.WriteLine("Entrez la nouvelle adresse : "); cible.Adresse = Console.ReadLine();fileExplorerClient.Modify(cible,3,cible.Adresse) ; break;
                    case 3: Console.WriteLine("Entrez la nouvelle adresse mail : "); cible.AdresseMail = Console.ReadLine();fileExplorerClient.Modify(cible, 4, cible.AdresseMail); break;
                    case 4: Console.WriteLine("Entrez le numéro de téléphone : "); cible.Telephone = int.Parse(Console.ReadLine());fileExplorerClient.Modify(cible, 5, cible.Telephone.ToString()); break;
                }

            }
            else

            {
                Console.WriteLine("Pas de client dans la banque de donnée : Veuillez en rajouter avant de procéder à une modification");
            }
            Console.WriteLine("Modification faite !");
            Console.WriteLine("===============");
            Console.WriteLine("Appuyez sur une touche ...");
            Console.ReadKey();
            Console.Clear();
            ModuleClient();
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
                fileExplorerClient.Remove(cible);
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
            ModuleClient();
        }
        void AfficherClient()
        {
            Console.WriteLine("Vous avez choisis d'afficher les clients");
            Console.WriteLine("===============");
            Console.WriteLine("Quel méthode de tri souhaitez-vous appliquer à la liste des clients ? \n 1 : par nom et prénom, 2 : par adresse, 3 : par prix total de commande, 4 : ne pas trier :");
            int l = GoodValue(1, 4);
            switch (l)
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
                foreach (Client c in manager.Clients)
                {
                    if (boo) Console.WriteLine(c.ToStringComplete());
                    else Console.WriteLine(c.ToString());
                }
            }
            else Console.WriteLine("Pas de client dans la banque de donnée : Veuillez en rajouter avant de procéder à un affichage");
            Console.WriteLine("===============");
            Console.WriteLine("Appuyez sur une touche ...");
            Console.ReadKey();
            Console.Clear();
            ModuleClient();
        }
        #endregion
        #region Module Salarié
        void ModuleSalarie()
        {
            Console.Clear();
            AfficherOrganigramme();
            Console.WriteLine("Bienvenue dans le module salarié");
            Console.WriteLine("Choississez votre action");
            Console.WriteLine("===============");
            Console.WriteLine("1. Ajouter un salarié");
            Console.WriteLine("2. Modifier un salarié");
            Console.WriteLine("3. Supprimer un salarié");
            Console.WriteLine("4. Afficher l'organigramme des salariés");
            Console.WriteLine("5. Afficher/Desactiver en permanence l'organigramme");
            Console.WriteLine("6. Retour");
            Console.WriteLine("===============");
            Console.WriteLine("Votre choix : ");
            int r = GoodValue(1, 6);
            Console.Clear();
            switch (r)
            {
                case 1: AjouterSalarie(); break;
                case 2: ModifierSalarie(); break;
                case 3: SupprimerSalarie(); break;
                case 4: AfficherSalarie(); break;
                case 5: display = (display == false) ;ModuleSalarie() ; break;
                case 6: Affichage(); break;
            }
        }
        void AfficherOrganigramme(bool foo=false)
        {
            foo = display;
            if (foo)
            {
                if(manager.SalariesHierarchie != null) manager.SalariesHierarchie.Affichage2();
                Console.WriteLine("===============");
            }
        }
        void AjouterSalarie()
        {
            AfficherOrganigramme();
            Console.WriteLine("Vous avez choisis d'ajouter un salarié");
            Console.WriteLine("===============");
            Console.WriteLine("Entrez le nom du salarié : ");
            string nom = Console.ReadLine().ToUpper();
            Console.WriteLine("Entrez le prénom du salarié : ");
            string prenom = Console.ReadLine().ToUpper();
            Console.WriteLine("Entrez la date de naissance du salarié (jour/mois/année) : ");
            string date = Console.ReadLine();
            string[] dates = date.Split('/');
            DateTime birth = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]));
            Console.WriteLine("Entrez l'adresse du salarié : ");
            string adresse = Console.ReadLine().ToUpper();
            Console.WriteLine("Entrez l'adresse mail du salarié : ");
            string mail = Console.ReadLine().ToUpper();
            Console.WriteLine("Entrez le numéro de téléphone du salarié : ");
            int tel = int.Parse(Console.ReadLine());
            Console.WriteLine("Entrez le salaire du salarié : ");
            int salaire = int.Parse(Console.ReadLine());
            Console.WriteLine("Entrez le poste du salarié : ");
            string poste = Console.ReadLine().ToUpper();
            Console.WriteLine("Entrez la date d'embauche du salarié : (jour/mois/année) ");
            string date2 = Console.ReadLine();
            string[] dates2 = date2.Split('/');
            int GoodSecu()
            {
                Console.WriteLine("Entrez le numéro de sécurité social : ");
                int secu = int.Parse(Console.ReadLine());
                if (manager.Salaries.FindAll(x => x.Id == secu).Count != 0)
                {
                    Console.WriteLine("Ce numéro d'identification sociale existe déjà.");
                    Console.WriteLine("===============");
                    Console.WriteLine("Voulez vous ressaisir le numéro sociale du salarié ? (o/n)");
                    string l = Console.ReadLine();
                    if (l == "o" || l == "O" || l == "0")
                    {
                        return GoodSecu();
                    }
                    else
                    {
                        return -1;
                    }
                }
                else return secu;
            }
            int numSecu = GoodSecu();
            if (numSecu != -1)
            {
                DateTime embauche = new DateTime(int.Parse(dates2[2]), int.Parse(dates2[1]), int.Parse(dates2[0]));
                //création du salarie
                Salarie sal = new Salarie(numSecu, nom, prenom, birth, adresse, mail, tel, embauche, poste, salaire);
                // ajout du salarie dans la liste des salariés et dans l'arbre n-aire
                manager.Salaries.Add(sal);
                if (manager.SalariesHierarchie == null)
                {
                    manager.SalariesHierarchie = new SalariesArbre(sal);
                    fileExplorer.Add(sal, "TransConnect", "");
                }
                else
                {
                    Console.WriteLine("Entrez le nom de l'employeur : ");
                    string nomEmployeur = Console.ReadLine().ToUpper();
                    Console.WriteLine("Entrez le prénom de l'employeur : ");
                    string prenomEmployeur = Console.ReadLine().ToUpper();
                    manager.SalariesHierarchie.AjouterSalarie(sal, nomEmployeur, prenomEmployeur);
                    fileExplorer.Add(sal, nomEmployeur, prenomEmployeur);
                }
                Console.WriteLine("Salarie ajouté");
            }
            else Console.WriteLine("Le salarie n'a pas été ajouté");
            FooterMenu();
            ModuleSalarie();
        }
        void ModifierSalarie()
        {
            AfficherOrganigramme();
            Console.WriteLine("Vous avez choisis de modifier un salarié");
            Console.WriteLine("===============");
            if (manager.SalariesHierarchie == null)
            {
                Console.WriteLine("Aucun salarié n'est listé dans la base de données");
                FooterMenu();
                ModuleSalarie();
            }
            Console.WriteLine("Entrez le nom du salarié : ");
            string nom = Console.ReadLine();
            Console.WriteLine("Entrez le prénom du salarié : ");
            string prenom = Console.ReadLine();
            Salarie sal = manager.Salaries.Find(x => x.Nom.ToUpper() == nom.ToUpper() && x.Prenom.ToUpper() == prenom.ToUpper());
            if (sal != null)
            {
                Console.WriteLine("Veuillez indiquer l'attribut à modifier (1 : nom, 2 : adresse, 3 : mail, 4 : télépone, 5 : poste, 6 : salaire) : ");
                int r = GoodValue(1, 6);
                switch (r)
                {
                    case 1: Console.WriteLine("Entrez le nouveau nom : ");
                        {
                            string newNom = Console.ReadLine();
                            sal.Nom = newNom;
                            fileExplorer.Modify(sal, 0, newNom);
                            Console.WriteLine("Nom modifié");
                            break;
                        }
                    case 2: Console.WriteLine("Entrez la nouvelle adresse : "); sal.Adresse = Console.ReadLine();fileExplorer.Modify(sal,3,sal.Adresse) ; break;
                    case 3: Console.WriteLine("Entrez le nouveau mail : "); sal.AdresseMail = Console.ReadLine();fileExplorer.Modify(sal, 4, sal.AdresseMail); break;
                    case 4: Console.WriteLine("Entrez le nouveau numéro de téléphone : "); sal.Telephone = int.Parse(Console.ReadLine());fileExplorer.Modify(sal, 5, sal.Telephone.ToString()); break;
                    case 5: Console.WriteLine("Entrez le nouveau poste : "); sal.Poste = Console.ReadLine();fileExplorer.Modify(sal, 8, sal.Poste); break;
                    case 6: Console.WriteLine("Entrez le nouveau salaire : "); sal.Salaire = int.Parse(Console.ReadLine());fileExplorer.Modify(sal, 7, sal.Salaire.ToString()); break;
                }
                fileExplorer.ReadFile();
                Console.WriteLine("Modification terminée ...");
            }
            else Console.WriteLine("Salarie introuvable");
            FooterMenu();
            ModuleSalarie();
        }
        void SupprimerSalarie() 
        {
            AfficherOrganigramme();
            Console.WriteLine("Vous avez choisis de supprimer un salarié");
            Console.WriteLine("===============");
            if (manager.SalariesHierarchie == null)
            {
                Console.WriteLine("Aucun salarié n'est listé dans la base de données");
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                ModuleSalarie();
            }
            Console.WriteLine("Entrez le nom du salarié : ");
            string nom = Console.ReadLine();
            Console.WriteLine("Entrez le prénom du salarié : ");
            string prenom = Console.ReadLine();
            Salarie sal = manager.Salaries.Find(x => x.Nom.ToUpper() == nom.ToUpper() && x.Prenom.ToUpper() == prenom.ToUpper());
            if (sal != null)
            {
                fileExplorer.Remove(sal);
                fileExplorer.ReadFile();
                Console.WriteLine("Salarie supprimé");
            }
            else Console.WriteLine("Salarie introuvable");    
            FooterMenu();
            ModuleSalarie();
        }
        void AfficherSalarie()
            {
                Console.WriteLine("Vous avez choisis d'afficher les salariés de Transconnect");
                Console.WriteLine("===============");
            if (manager.SalariesHierarchie == null)
            {
                Console.WriteLine("Aucun salarié n'est listé dans la base de données");
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                ModuleSalarie();
            }
                Console.WriteLine("Comment souhaitez-vous afficher les salariés ? \n 1 : arbre n-aire, 2 : liste d'adjacence, 3 : énumération des salariés par ordre d'ajout, 4 : énumération des salariés par relation employé/employeur");
                int l = GoodValue(1, 4);
                switch (l)
                {
                    case 1: manager.SalariesHierarchie.Affichage2(); break;
                    case 2:
                        {
                            string[,] graphe = manager.SalariesHierarchie.GrapheAdjacence();
                            for (int i = 0; i < graphe.GetLength(0); i++)
                            {
                                for (int j = 0; j < graphe.GetLength(1); j++)
                                {
                                    Console.Write(graphe[i, j] + " ");
                                }
                                Console.WriteLine();
                            }
                            break;
                        }
                    case 3: manager.Salaries.ForEach(x => Console.WriteLine(x)); break;
                    case 4: manager.SalariesHierarchie.AfficherHierarchie(); break;
                }
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                ModuleSalarie();
            }
        #endregion
        #region Module Commandes

        void ModuleCommande()
        {
            Console.Clear();
            Console.WriteLine("Bienvenue dans le module commande");
            Console.WriteLine("Choississez votre action");
            Console.WriteLine("================");
            Console.WriteLine("1. Ajouter une commande");
            Console.WriteLine("2. Modifier une commande");
            Console.WriteLine("3. Supprimer une commande");
            Console.WriteLine("4. Afficher une commande");
            Console.WriteLine("5. Valider la livraison d'une commande"); //La commande de la BDD commandes est alors archivées dans la liste de commandes propre au client
            Console.WriteLine("6. Retour");
            Console.WriteLine("===============");
            Console.WriteLine("Votre choix : ");
            int r = GoodValue(1, 7);
            Console.Clear();
            switch (r)
            {
                case 1: AjouterCommande(); break;
                case 2: ModifierCommande(); break;
                case 3: SupprimerCommande(); break;
                case 4: AfficherCommande(); break;
                case 5: ArchiverCommande(); break;
                case 6: Affichage(); break;
            }
        }

        void AjouterCommande()
        {
            Console.WriteLine("Vous avez choisi d'ajouter une nouvelle commande");
            Console.WriteLine("===============");

            Console.Write("n° de commande (idCommande) : ");
            int idCommande = int.Parse(Console.ReadLine());
            while (manager.Commandes.Exists(x => x.Id == idCommande)) //évite d'avoir 2 commandes avec même id
            {
                Console.WriteLine("Une commande avec cet id existe déjà dans la BDD, veuillez en saisir un nouveau");
                Console.Write("n° de commande (idCommande) : ");
                idCommande = int.Parse(Console.ReadLine());
            } 

            
            Console.WriteLine();
            Console.Write("n° de client (idClient) : ");
            int idClient = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("ville de départ : ");
            string depart = Console.ReadLine();
            Console.WriteLine();
            Console.Write("ville d'arrivée : ");
            string arrivee = Console.ReadLine();
            Console.WriteLine();
            Console.Write("date de livraison (jour/mois/année): ");
            string date = Console.ReadLine();
            string[] dates = date.Split('/');
            DateTime dateLivraison = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]));
            Console.WriteLine();

            // On vérifie l'existence du client à livrer dans la BDD
            Client clientCommande = manager.Clients.Find(x => x.Id == idClient);
            //exception à ajouter 
            if (clientCommande == null) //le client n'a pas été trouvé dans la BDD
            {
                Console.WriteLine("Votre client n°{0} n'a pas été trouvé dans la BDD, veuillez d'abord l'ajouter via le Module Client", idClient);
            }
            else //le client à été trouvé
            {
                Console.WriteLine("Veuillez sélectionner un type de véhicule");
                Console.WriteLine("1 = Voiture\n2 = Camion benne\n3 = Camion citerne\n4 = Camion frigorifique");
                Console.Write("Votre choix : ");
                int typeVehicule = GoodValue(1, 4);
                //On génère une commande et le prix suivant le parcours déterminé par Dijkstra
                Commande offreCommande = manager.GenerationDeCommande(idCommande, clientCommande, depart, arrivee,typeVehicule, dateLivraison);
                if(offreCommande!= null && offreCommande.Itineraire.Count!=0) // la commande et son itinéraire ont été correctement générés
                {
                    Console.WriteLine("Commande générée : ");
                    Console.WriteLine("===============");
                    Console.WriteLine(offreCommande);
                    Console.WriteLine("Itinéraire le plus court proposé : ");
                    offreCommande.AfficherItineraire();
                    Console.WriteLine("===============");
                    Console.WriteLine("Cette commande vous convient-elle ? (1=oui 2=non) : ");
                    int r = GoodValue(1, 2);
                    switch (r)
                    {
                        case 1:
                            manager.AjouterCommande(offreCommande);
                            Console.WriteLine("Commande ajoutée avec succès");
                            break;
                        case 2:
                            Console.WriteLine("Commande annulée");
                            break;
                    }
                }
            }
            FooterMenu();
            ModuleCommande();
        }
        void ModifierCommande()
        {
            Console.WriteLine("Vous avez choisi de modifier une commande");
            Console.WriteLine("===============");
            AffichageListe<Commande>(manager.Commandes, "commande");
            if (manager.Commandes.Count > 0)
            {
                Console.WriteLine("Quelle commande voulez vous modifier ? (n° de commande):");
                int r = int.Parse(Console.ReadLine());
                if (manager.Commandes.Exists(x => x.Id == r))
                {
                    Commande cible = manager.Commandes.Find(x => x.Id == r);
                    Console.WriteLine("Modification de {0}", cible.Id + "  " + cible.Depart + " " + cible.Arrivee + " " + cible.DateLivraison);
                    Console.WriteLine("Que souhaitez vous modifier ? \n 1 : Départ, 2 : Arrivée, 3 : Date de livraison");
                    Console.WriteLine("  :  ");
                    int l = GoodValue(1, 3);
                    switch (l)
                    {
                        case 1: Console.WriteLine("Entrez le nouveau départ : "); cible.Depart = Console.ReadLine(); break;
                        case 2: Console.WriteLine("Entrez la nouvelle arrivée : "); cible.Arrivee = Console.ReadLine(); break;
                        case 3:
                            {
                                Console.WriteLine("Entrez la nouvelle date de livraison  (jour/mois/année) : ");
                                string date = Console.ReadLine();
                                string[] dates = date.Split('/');
                                DateTime dateLivraison = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]));
                                cible.DateLivraison = dateLivraison;
                                break;
                            }
                    }

                    List<Arete> nouvelItineraire = manager.Graphe.Dijkstra(cible.Depart, cible.Arrivee);
                    if (nouvelItineraire.Count == 0) 
                    {
                        Console.WriteLine("Impossible de changer l'itinéraire, nouvel itinéraire incompatible");
                    }
                    else
                    {
                        Commande temp = new Commande();
                        temp.Itineraire = nouvelItineraire;
                        temp.AfficherItineraire();
                        Console.WriteLine("===============");
                        Console.WriteLine("Ce nouvel itinéraire vous convient-il ? (1=oui 2=non) : ");
                    }
                }
            }
            FooterMenu();
            ModuleClient();
        }
        void SupprimerCommande()
        {
            Console.WriteLine("Vous avez choisis de supprimer une commande");
            Console.WriteLine("===============");
            AffichageListe<Commande>(manager.Commandes, "commande");
            if (manager.Commandes.Count > 0)
            {
                Console.WriteLine("Quelle commande voulez vous supprimer ? (n° de commande):");
                int r = int.Parse(Console.ReadLine());
                if (manager.Commandes.Exists(x => x.Id == r))
                {
                    Commande cible = manager.Commandes.Find(x => x.Id == r);
                    Console.WriteLine("Suppression de {0}", cible.Id + "  " + cible.Depart + " " + cible.Arrivee + " " + cible.DateLivraison);
                    manager.Commandes.Remove(cible);
                    Console.WriteLine("Commande supprimée");
                }
                else
                {
                    Console.WriteLine("La commande n'a pas été trouvée dans la BDD");
                }
                    
            }
            FooterMenu();
            ModuleCommande();
        }
        void AfficherCommande() 
        {
            Console.WriteLine("Vous avez choisis d'afficher une commande");
            Console.WriteLine("===============");
            AffichageListe<Commande>(manager.Commandes, "commande");
            if (manager.Commandes.Count > 0)
            {
                Console.WriteLine("Quelle commande voulez vous afficher ? (n° de commande):");
                int r = int.Parse(Console.ReadLine());
                if (manager.Commandes.Exists(x => x.Id == r))
                {
                    Commande cible = manager.Commandes.Find(x => x.Id == r);
                    Console.WriteLine("Itinéraire pour la commande : " + cible);
                    cible.AfficherItineraire();
                }
                else
                {
                    Console.WriteLine("La commande n'a pas été trouvée dans la BDD");
                }

            }
            FooterMenu();
            ModuleCommande();
        }
        void ArchiverCommande()
        {
            Console.WriteLine("Vous avez choisis d'archiver une commande");
            Console.WriteLine("===============");
            AffichageListe<Commande>(manager.Commandes, "commande");
            if (manager.Commandes.Count > 0)
            {
                Console.WriteLine("Quelle commande voulez vous archiver ? (n° de commande):");
                int r = int.Parse(Console.ReadLine());
                if (manager.Commandes.Exists(x => x.Id == r))
                {
                    Commande cible = manager.Commandes.Find(x => x.Id == r);
                    Console.WriteLine("Archivage de {0}", cible.Id + "  " + cible.Depart + " " + cible.Arrivee + " " + cible.DateLivraison);

                    //on déplace la commande dans les archives du client
                    cible.Client.AjouteCommande(cible);
                    fileExplorerCommandes.Add(cible); // on ajoute la commande aux archives
                    Console.WriteLine("Commande archivée dans le dossier client de : "+cible.Client.Nom+" "+cible.Client.Prenom);
                    //on supprime la commande de la BDD
                    manager.Commandes.Remove(cible);
                }
                else
                {
                    Console.WriteLine("La commande n'a pas été trouvée dans la BDD");
                }

            }
            FooterMenu(); ;
            ModuleCommande();
        }

        #endregion
        #region Module Vehicules

        void ModuleVehicule()
        {
            Console.Clear();
            Console.WriteLine("Bienvenue dans le module véhicule");
            Console.WriteLine("Choississez votre action");
            Console.WriteLine("================");
            Console.WriteLine("1. Ajouter un véhicule");
            Console.WriteLine("2. Supprimer un véhicule");
            Console.WriteLine("3. Afficher un véhicule");
            Console.WriteLine("4. Retour");
            Console.WriteLine("===============");
            Console.WriteLine("Votre choix : ");
            int r = GoodValue(1, 4);
            Console.Clear();
            switch (r)
            {
                case 1: AjouterVehicule(); break;
                case 2: SupprimerVehicule(); break;
                case 3: AfficherVehicule(); break;
                case 4: Affichage(); break;
            }
        }

        List<string> ChoixMatieres()
        {
            List<string> matieres = new List<string>();
            Console.WriteLine("Quelles sont les matières transportées? (1=ajouter matière, 2=terminé)");
            int m = GoodValue(1, 2);
            while (m != 2)
            {
                Console.Write("Indiquez le nom d'une matière à transporter : ");
                matieres.Add(Console.ReadLine());
                Console.WriteLine("Quelles sont les matières transportées? (1=ajouter matière, 2=terminé)");
                m = GoodValue(1, 2);
            }
            return matieres;
        }

        List<string> ChoixEquipements()
        {
            List<string> equipements = new List<string>();
            Console.WriteLine("Quelles sont les équipements du véhicules? (1=ajouter équipement, 2=terminé)");
            int m = GoodValue(1, 2);
            while (m != 2)
            {
                Console.Write("Indiquez le nom d'un équipement à ajouter au véhicule : ");
                equipements.Add(Console.ReadLine());
                Console.WriteLine("Quelles sont les équipements du véhicules? (1=ajouter équipement, 2=terminé)");
                m = GoodValue(1, 2);
            }
            return equipements;
        }

        void AjouterVehicule()
        {
            Console.WriteLine("Vous avez choisi d'ajouter un nouveau véhicule");
            Console.WriteLine("===============");

            Console.Write("Immatriculation (idVehicule) : ");
            string idVehicule = Console.ReadLine();
            while (manager.Vehicules.Exists(x => x.Immatriculation == idVehicule)) //évite d'avoir 2 commandes avec même id
            {
                Console.WriteLine("Un véhicule avec cet immatriculation existe déjà dans la BDD, veuillez en saisir un nouveau");
                Console.Write("Immatriculation (idVéhicule) : ");
                idVehicule = Console.ReadLine();
            }
            Console.WriteLine();

            List<string> matieres = new List<string>();
            List<string> equipements = new List<string>();
            int capacite = 0;
            Vehicule v=null;

            Console.WriteLine("Quel type de véhicule souhaitez-vous ajouter : (1=voiture, 2=camion citerne, 3=camion benne, 4 = camion frigorifique");
            int r = GoodValue(1, 4);
            switch (r)
            {
                case 1: //voiture
                    Console.Write("Nombre de places : ");
                    int nbPlaces = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    v = new Voiture(idVehicule, nbPlaces);
                    break;
                case 2: //camion citerne
                    Console.Write("Type de cuve : (1=liquide, 2=gaz) ");
                    int l= GoodValue(1, 2);
                    string typeCuve="";
                    Console.WriteLine();
                    switch (l)
                    {
                        case 1: typeCuve = "liquide"; break;
                        case 2: typeCuve = "gaz"; break;
                    }
                    matieres = ChoixMatieres();
                    Console.Write("Capacité du véhicule (en L ou kg) : ");
                    capacite = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    v = new Camion_citerne(idVehicule, capacite, matieres, typeCuve);
                    break;

                case 3: //camion benne
                    matieres= ChoixMatieres();
                    equipements = ChoixEquipements();
                    Console.Write("Capacité du véhicule (en L ou kg) : ");
                    capacite = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    v = new Camion_benne(idVehicule, capacite, matieres, equipements);
                    break;
                case 4://camion frigorifique
                    matieres = ChoixMatieres();
                    Console.Write("Nombre de groupes électrogène du véhicule : ");
                    int nbGroupeElectrogene = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.Write("Capacité du véhicule (en L ou kg) : ");
                    capacite = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    v = new Camion_frigorifique(idVehicule, capacite, matieres, nbGroupeElectrogene);
                    break;
            }

            manager.Vehicules.Add(v);
            Console.WriteLine("Véhicule ajouté");
            FooterMenu();
            ModuleVehicule();
        }
        void SupprimerVehicule()
        {
            Console.WriteLine("Vous avez choisis de supprimer un véhicule");
            Console.WriteLine("===============");
            AffichageListe<Vehicule>(manager.Vehicules, "vehicule");
            if (manager.Vehicules.Count > 0)
            {
                Console.WriteLine("Quel véhicule voulez vous supprimer ? (immatriculation):");
                string idVehicule = Console.ReadLine();
                if (manager.Vehicules.Exists(x => x.Immatriculation == idVehicule))
                {
                    Vehicule cible = manager.Vehicules.Find(x => x.Immatriculation == idVehicule);
                    Console.WriteLine("Suppression de {0}", cible);
                    manager.Vehicules.Remove(cible);
                    Console.WriteLine("Vehicule supprimée");
                }
                else Console.WriteLine("Le véhicule n'a pas été trouvée dans la BDD");
            }
            FooterMenu();
            ModuleVehicule();
        }
        void AfficherVehicule()
        {
            Console.WriteLine("Vous avez choisi d'afficher un véhicule");
            Console.WriteLine("===============");
            AffichageListe<Vehicule>(manager.Vehicules, "vehicule");
            if (manager.Vehicules.Count > 0)
            {
         
                Console.WriteLine("Quelle véhicule voulez-vous afficher ? (immatriculation):");
                string idVehicule = Console.ReadLine();
                if (manager.Vehicules.Exists(x => x.Immatriculation == idVehicule))
                {
                    Vehicule cible = manager.Vehicules.Find(x => x.Immatriculation == idVehicule);
                    Console.WriteLine(cible);
                }
                else Console.WriteLine("La commande n'a pas été trouvée dans la BDD");

            }
            FooterMenu();
            ModuleVehicule();
        }

        #endregion
        #region Module Statistiques

        
        void ModuleStatistique() 
        {
            Console.Clear();
            Console.WriteLine("Vous avez choisi le module Statistiques :");
            Console.WriteLine("===============");
            Console.WriteLine("Disclaimer : Les commandes à venir étant susceptibles d'être modifiées, seules les commandes archivées = effectuées sont utilisées pour ce module");
            Console.WriteLine("Choisissez votre action :");
            Console.WriteLine("===============");
            Console.WriteLine("1 : Afficher nb livraisons / chauffeur");
            Console.WriteLine("2 : Afficher commandes / période de temps");
            Console.WriteLine("3 : Afficher moyenne des prix des commandes");
            Console.WriteLine("4 : Afficher moyenne des prix / client");
            Console.WriteLine("5 : Afficher liste commandes pour un client");
            Console.WriteLine("6 : Retour");
            Console.WriteLine("===============");
            Console.WriteLine("Votre choix : ");
            int r = GoodValue(1, 6);
            Console.Clear();
            List<Commande> commandes = new List<Commande>();
            float moy = 0;
            int nbCommandesArchivees = 0;
            int nbCommandesBDD=0;
            switch (r)
            {
                case 1:
                    Console.WriteLine("Nombre de livraisons / chauffeurs (parmi les livraisons archivées) :");
                    Console.WriteLine("===============");
                    List<Salarie> listeChauffeurs = manager.Salaries.FindAll(x => x.IsDriver());
                    listeChauffeurs.ForEach(x=> Console.WriteLine(x.Nom+" "+x.Prenom+" : "+NbLivraisons(x)+" livraisons effectuées"));
                    FooterMenu();
                    ModuleStatistique();
                    break;
                case 2:
                    Console.WriteLine("Commandes / période de temps (parmi les livraisons archivées) :");
                    Console.WriteLine("Veuillez saisir une date de début (jour/mois/année)");
                    DateTime dateDebut = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Veuillez saisir une date de fin (jour/mois/année)");
                    DateTime dateFin = DateTime.Parse(Console.ReadLine());
                    foreach (Client c in manager.Clients)
                    {
                        foreach(Commande commande in c.Commandes)
                        {
                            if(commande.DateLivraison<=dateFin && commande.DateLivraison >= dateDebut)
                            {
                                commandes.Add(commande);
                            }
                        }
                        //commandes.Concat<Commande>(c.Commandes.FindAll(x => x.DateLivraison >= dateDebut && x.DateLivraison <= dateFin));

                    }
                    Console.WriteLine("Commandes entre {0} et {1} (archivées) :", dateDebut.Date,dateFin.Date);
                    Console.WriteLine("===============");
                    commandes.ForEach(x => Console.WriteLine(x));
                    FooterMenu();
                    ModuleStatistique();
                    break;
                case 3:
                    Console.WriteLine("Moyenne des prix des commandes");
                    Console.WriteLine("===============");
                    foreach (Client client in manager.Clients)
                    {
                        foreach (Commande commande in client.Commandes) 
                        {
                            moy += commande.Prix;
                            nbCommandesArchivees++;
                        }
                    }
                    moy /= (float)nbCommandesArchivees;
                    Console.WriteLine("Moyenne des prix des commandes archivées : "+moy);
                    moy = 0;
                    foreach(Commande commande in manager.Commandes)
                    {
                        moy += commande.Prix;
                        nbCommandesBDD++;
                    }
                    moy /= (float)nbCommandesBDD;
                    Console.WriteLine("Moyenne des prix des commandes à venir (=dans la BDD) : " + moy);
                    FooterMenu();
                    ModuleStatistique();
                    break;
                case 4:
                    Console.WriteLine("Moyenne des prix des commandes / client : ");
                    Console.WriteLine("===============");
                    foreach (Client client in manager.Clients)
                    {
                        moy = 0;
                        foreach (Commande commande in client.Commandes)
                        {
                            moy += commande.Prix;
                        }
                        moy/=(float) client.Commandes.Count;
                        Console.WriteLine(client.Nom+" "+client.Prenom+" : "+moy+"€ / commande");
                    }
                    FooterMenu();
                    ModuleStatistique();
                    break;
                case 5:
                    Console.WriteLine("Liste des commandes pour un client : ");
                    Console.WriteLine("===============");
                    Console.Write("Identifiant client :");
                    int idClient=int.Parse(Console.ReadLine());
                    if (manager.Clients.Exists(x => x.Id == idClient))
                    {
                        Client cible = manager.Clients.Find(x => x.Id == idClient);
                        if (cible.Commandes.Count != 0)
                        {
                            Console.WriteLine("Liste des commandes de " + cible.Nom + " " + cible.Prenom + " :");
                            cible.Commandes.ForEach(x => Console.WriteLine(x));
                        }
                        else Console.WriteLine("Le client ne possède pas de commandes archivées");
                    }
                    else Console.WriteLine("Le client n'a pas été trouvé dans la BDD");
                    FooterMenu();
                    ModuleStatistique();
                    break;
                case 6: Affichage(); break;
                  
            }
            
        }
        #endregion
        #region Module Autre
        void ModuleAutre() 
        {
            Console.Clear();
            Console.WriteLine("Vous avez choisis le module Autre :");
            Console.WriteLine("Choisissez votre action :");
            Console.WriteLine("===============");
            Console.WriteLine("1 : Chiffer les données de sauvegarde");
            Console.WriteLine("2 : Déchiffer les données de sauvegarde");
            Console.WriteLine("3 : Module Véhicule");
            Console.WriteLine("4 : Promouvoir un Salarié");
            Console.WriteLine("5 : Retour");
            Console.WriteLine("===============");
            Console.WriteLine("Votre choix : ");
            int r = GoodValue(1, 5);
            Console.Clear();

            switch (r)
            {
                case 1: // chiffrement des données des dataset
                    {
                        Console.Clear();
                        Console.WriteLine("===============");
                        if(File.Exists("key.txt"))
                        {
                            Console.WriteLine("Les fichiers de sauvegardes sont déjà chiffrés. Un second chiffrage est impossible. Veuillez les déchiffrer avant de procéder");
                        }
                        else { 
                        Console.WriteLine("Vous vous apprêtez à chiffrer les données de sauvegarde");
                        Console.WriteLine("Une fois cela fait, il ne sera plus possible de modifier à même le fichier les données des clients et des salariés");
                        Console.WriteLine("êtes vous sûr de vouloir procéder au chiffrage ? (O/N) :");
                        string s = Console.ReadLine();
                            if (s.ToUpper() == "O" || s == "0")
                            {
                                string permu = GoodPermu();
                                if (permu != "error")
                                {
                                    Console.WriteLine("Permutation valide");
                                    Console.WriteLine("Veuillez noter quelques part votre permutation. Si celle ci est perdue, nous ne serons pas reponsable de pertes de données.");
                                    Console.WriteLine("Chiffrage en cours. Veuillez ne pas modifier les set de donnée ...");
                                    LireFichier.ChiffrageSub("client.csv", "clientCipher.csv", permu);
                                    LireFichier.ChiffrageSub("employee.csv", "employeeCipher.csv", permu);

                                    Console.WriteLine("Chiffrage terminée avec succès");
                                    TextWriter tw = new StreamWriter("key.txt", false); // on suppose que le fichier est protégé par le système d'exploitation (W11 pro uniquement)
                                    tw.Write(permu);
                                    tw.Close();
                                    File.Encrypt("key.txt"); //UNIQUEMENT SUR WINDOWS PRO
                                }
                                else
                                {
                                    Console.WriteLine("Une erreur s'est produite, veuillez réessayer ultérieurement ...");
                                }
                            }
                        }
                        FooterMenu();
                        ModuleAutre();
                        break;
                    }
                case 2: // déchiffrement des données des dataset
                    {
                        if(File.Exists("key.txt"))
                        {
                            Console.WriteLine("Êtes vous sûr de vouloir déchiffrer les données de sauvegarde ? (O/N) :");
                            string l = Console.ReadLine();
                            if (l == "O" || l == "o")
                            {
                                TextReader tr = new StreamReader("key.txt");
                                string permu = tr.ReadLine();
                                tr.Close();
                                if (permu != null)
                                {
                                    LireFichier.DechiffrageSub("employeeCipher.csv", "employee.csv", permu);
                                    LireFichier.DechiffrageSub("clientCipher.csv", "client.csv", permu);
                                    File.Delete("key.txt");
                                    File.Delete("employeeCipher.csv");
                                    File.Delete("clientCipher.csv");
                                }
                                Console.WriteLine("Déchiffrage fini");
                            }
                            else Console.WriteLine("les données n'ont pas été déchiffré.");
                        }
                        else
                        {
                            Console.WriteLine("Les données ne sont pas chiffrés. Veuillez les chiffrer avant de procéder à un quelconques déchiffrement");
                        }
                        FooterMenu();
                        ModuleAutre();
                        break;
                    }
                case 3:
                    ModuleVehicule();
                    break;
                case 4:PromotionSalarie(); break;
                case 5:Affichage();break;
            }
        }
        
        public void PromotionSalarie()
        {
            Console.WriteLine("Vous avez choisis de promouvoir un salarié :");
            Console.WriteLine("===============");
            display = true;
            AfficherOrganigramme();
            display = false;
            Console.WriteLine("===============");
            Console.WriteLine("Veuillez entrer le nom du salarié :");
            string nom = Console.ReadLine().ToUpper();
            Console.WriteLine("Veuillez entrer le prénom du salarié :");
            string prenom = Console.ReadLine().ToUpper();
            Console.WriteLine("Veuillez entrer le nom du nouvel l'employeur (ne rien indiquer si le salarié est promu PDG) :");
            string nomEmployeur = Console.ReadLine().ToUpper();
            Console.WriteLine("Veuillez entrer le prénom du nouvel l'employeur (ne rien indiquer si le salarié est promu PDG):");
            string prenomEmployeur = Console.ReadLine().ToUpper();
            Console.WriteLine("Veuillez entrer le nom de son nouvel employé direct :");
            string nomEmployeDirect = Console.ReadLine().ToUpper();
            Console.WriteLine("Veuillez entrer le prénom de son nouvel employé direct :");
            string prenomEmployeDirect = Console.ReadLine().ToUpper();
            Console.WriteLine("Veuillez entrer son nouveau poste (par exemple : PDG ):");
            string newPoste = Console.ReadLine().ToUpper();
            Salarie s = manager.Salaries.Find(x => x.Nom.ToUpper() == nom && x.Prenom.ToUpper() == prenom);
            Salarie employeur = manager.Salaries.Find(x => x.Nom.ToUpper() == nomEmployeur && x.Prenom.ToUpper() == prenomEmployeur);
            Salarie employee = manager.Salaries.Find(x => x.Nom.ToUpper() == nomEmployeDirect && x.Prenom.ToUpper() == prenomEmployeDirect);
            string boss = "";
            string[] couple = { "", "" };
            if (employeur != null && employee != null)
            {
                couple = manager.salariesHierarchie.CoupleSalarieEmployeur(nomEmployeDirect, prenomEmployeDirect, employee.Poste);
                boss = nomEmployeur + " " + prenomEmployeur + " " + employeur.Poste;
                Console.WriteLine(boss);
                Console.WriteLine(couple[0] + " " + couple[1]);
            }
            if (s != null && employee != null && boss == couple[0] && !Salarie.Equal(s, employee))
            {
                if( employeur != null) // le salarié n'est pas promu PDG
                {
                    if (!Salarie.Equal(employeur, s))
                    {
                        fileExplorer.Promote(s, employeur, employee, newPoste);
                        fileExplorer.ReadFile();
                        Console.WriteLine("salarié promu !");
                    }
                    else Console.WriteLine("l'employeur est le salarié promu !");
                }
                else // le salarié est promu PDG
                {
                    couple = manager.salariesHierarchie.CoupleSalarieEmployeur(nomEmployeDirect, prenomEmployeDirect, employee.Poste);
                    if (couple[0] == "PDG")
                    {
                        fileExplorer.Promote(s, employeur, employee, newPoste);
                        fileExplorer.ReadFile();
                        Console.WriteLine("salarié promu !");
                    }
                    else
                    {
                        Console.WriteLine("l'employeur n'a pas été trouvé et l'employé directe n'est pas l'ancien PDG. Veuillez vérifier les données fournis");
                    }
                    
                }
            }
            else
            {
                Console.WriteLine("les relations indiqués n'existent pas, veuillez vérifier l'orthographe des noms et prénoms");
            }
            FooterMenu();
            ModuleAutre();
        }
        #endregion

        #region Méthodes utilitaires
        static void AffichageListe<T>(List<T> list, string label)
        {
            if (list.Count > 0)
            {
                Console.WriteLine("Voici la liste des {0} :", label + "s");
                foreach (T obj in list)
                {
                    Console.WriteLine(obj);
                }
            }
            else Console.WriteLine("Pas de commande dans la banque de donnée : Veuillez en rajouter avant de procéder à une suppression");
        }
        static void FooterMenu()
        {
            Console.WriteLine("===============");
            Console.WriteLine("Appuyez sur une touche ...");
            Console.ReadKey();
            Console.Clear();
        }
        string GoodPermu()
        {
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            Console.WriteLine("Veuillez entrer une permutation à appliquer au jeu de donnée. Elle doit contenir toutes les lettres de l'alphabet et ne pas avoir de répétition.");
            Console.WriteLine("Exemple : " + alpha);
            Console.WriteLine("Votre permutation : ");
            string permu = Console.ReadLine();
            permu = permu.ToLower();
            bool foo = true;
            if (permu.Length != 26)
            {
                Console.WriteLine("La permutation ne dispose pas d'une taille suffisante. Veuillez vérifier la taille fournie :");
                Console.WriteLine("Voulez vous retenter ? (0/N):");
                string s = Console.ReadLine();
                if (s.ToUpper() == "O" || s == "0")
                {
                    return GoodPermu();
                }
                else
                {
                    return "error";
                }
                    

            }
            else
            {
                foreach (char c in alpha)
                {
                    if (!permu.Contains(c))
                    {
                        foo = false;
                    }
                }
                if(!foo)
                {
                    Console.WriteLine("Vous n'avez pas fourni toutes les lettres de l'alphabet dans votre permutation.");
                    Console.WriteLine("Voulez vous retenter ? (0/N):");
                    string s = Console.ReadLine();
                    if (s.ToUpper() == "O" || s == "0")
                    {
                        return GoodPermu();
                    }
                    else
                    {
                        return "error";
                    }
                }
                else
                {
                    return permu;
                }
            }
            

        }
        void Exit() 
        {
            if(File.Exists("key.txt"))
            {
                TextReader tr = new StreamReader("key.txt");
                string permu = tr.ReadLine();
                tr.Close();
                if (permu != null)
                {
                    LireFichier.ChiffrageSub("client.csv", "clientCipher.csv", permu);
                    LireFichier.ChiffrageSub("employee.csv", "employeeCipher.csv", permu);
                }
                File.Delete("employee.csv");
                File.Delete("client.csv");
            }
        }
        int NbLivraisons(Salarie chauffeur)
        {
            int i = 0;
            foreach(Client client in manager.Clients)
            {
                foreach(Commande commande in client.Commandes)
                {
                    if(Salarie.Equal(commande.Chauffeur,chauffeur))
                    {
                        i++;
                    }
                }
            }
            return i;
        }
        #endregion

        #endregion
    }
    
} 
