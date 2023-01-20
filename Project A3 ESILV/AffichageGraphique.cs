using System.Runtime.ExceptionServices;

namespace Project_A3_ESILV
{
    public delegate void Ui();
    internal class AffichageGraphique
    {

        public Manager manager;
        public LireFichier fileExplorer; // explorateur de fichier permettant de modifier la sauvegarde des salariés
        public LireFichier fileExplorerClient; // explorateur de fichier permettant de modifier la sauvegarde des clients
        bool display = false;
        public AffichageGraphique(string path,string path2, Manager manager)
        {
            this.manager = manager;
            this.fileExplorer = new LireFichier(manager, path);
            this.fileExplorerClient = new LireFichier(manager, path2);
        }
        #region AffichageGLobale
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
            fileExplorerClient.ReadFileClient();
            fileExplorer.ReadFile();
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
            Console.Write("numero de sécurité sociale : ");
            int secu = int.Parse(Console.ReadLine());
            Client c = new Client(secu, nom, prenom, birth, adresse, mail, tel);
            manager.AjouterClient(c);
            fileExplorerClient.Add(c);
            Console.WriteLine("Client Ajouté !");
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
            Console.WriteLine("================");
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
            string nom = Console.ReadLine();
            Console.WriteLine("Entrez le prénom du salarié : ");
            string prenom = Console.ReadLine();
            Console.WriteLine("Entrez la date de naissance du salarié (jour/mois/année) : ");
            string date = Console.ReadLine();
            string[] dates = date.Split('/');
            DateTime birth = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]));
            Console.WriteLine("Entrez l'adresse du salarié : ");
            string adresse = Console.ReadLine();
            Console.WriteLine("Entrez l'adresse mail du salarié : ");
            string mail = Console.ReadLine();
            Console.WriteLine("Entrez le numéro de téléphone du salarié : ");
            int tel = int.Parse(Console.ReadLine());
            Console.WriteLine("Entrez le salaire du salarié : ");
            int salaire = int.Parse(Console.ReadLine());
            Console.WriteLine("Entrez le poste du salarié : ");
            string poste = Console.ReadLine();
            Console.WriteLine("Entrez la date d'embauche du salarié : (jour/mois/année) ");
            string date2 = Console.ReadLine();
            string[] dates2 = date2.Split('/');
            Console.WriteLine("Entrez le numéro de sécurité social : ");
            int numSecu = int.Parse(Console.ReadLine());
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
                Console.WriteLine("Entrez le nom de l'empoloyeur : ");
                string nomEmployeur = Console.ReadLine();
                Console.WriteLine("Entrez le prénom de l'employeur : ");
                string prenomEmployeur = Console.ReadLine();
                manager.SalariesHierarchie.AjouterSalarie(sal, nomEmployeur, prenomEmployeur);
                fileExplorer.Add(sal, nomEmployeur, prenomEmployeur);
            }
            Console.WriteLine("Salarie ajouté");
            Console.WriteLine("===============");
            Console.WriteLine("Appuyez sur une touche ...");
            Console.ReadKey();
            Console.Clear();
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
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                ModuleSalarie();
            }
            else
            {
                Console.WriteLine("Salarie introuvable");
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                ModuleSalarie();
            }
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
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                ModuleSalarie();
            }
            else
            {
                Console.WriteLine("Salarie introuvable");
                Console.WriteLine("===============");
                Console.WriteLine("Appuyez sur une touche ...");
                Console.ReadKey();
                Console.Clear();
                ModuleSalarie();
            }
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
            void ModuleCommande() { }
            void ModuleStatistique() { }
            void ModuleAutre() { }
            void Exit() { }
        }
    
    } 
