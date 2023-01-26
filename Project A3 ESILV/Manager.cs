namespace Project_A3_ESILV
{
    internal class Manager
    {
        #region Champs
        List<Salarie> salaries;
        List<Client> clients;
        List<Vehicule> vehicules;
        public SalariesArbre salariesHierarchie;
        List<Commande> commandes;
        Graphe graphe;
        #endregion

        #region Constructeur
        public Manager(List<Salarie> salaries, List<Client> clients, List<Vehicule> vehicules, List<Commande> commandes, Graphe graphe)
        {
            this.salaries = salaries;
            this.clients = clients;
            this.vehicules = vehicules;
            this.commandes = commandes;
            this.salariesHierarchie = null;
            this.graphe = graphe;
        }
        public Manager()
        {
            this.salaries = new List<Salarie>();
            this.clients = new List<Client>();
            this.vehicules = new List<Vehicule>();
            this.commandes = new List<Commande>();
            this.salariesHierarchie = null;
            this.graphe = new Graphe();
        }
        #endregion

        #region getter-setter
        public List<Salarie> Salaries
        {
            get { return salaries; }
        }
        public List<Client> Clients
        {
            get { return clients; }
        }
        public List<Vehicule> Vehicules
        {
            get { return vehicules; }
        }
        public List<Commande> Commandes
        {
            get { return commandes; }
        }
        public SalariesArbre SalariesHierarchie
        {
            get { return salariesHierarchie; }
            set { salariesHierarchie = value; }
        }
        public Graphe Graphe 
        { 
            get { return graphe; } 
            set { graphe = value; }
        }
        #endregion

        #region Methodes

        #region Ajout et retrait d'éléments dans les listes de la classe
        public void AjouterClient(Client client)
        {
            clients.Add(client);
        }
        public void AjouterClient(Client[] clients)
        {
            foreach (Client client in clients)
            {
                this.clients.Add(client);
            }
        }
        public void AjouterClient(string nom, string prenom, DateTime dateNaissance, string adresse, string adresseMail, int telephone)
        {
            clients.Add(new Client(clients.Count, nom, prenom, dateNaissance, adresse, adresseMail, telephone)); // on génère un nouveau client. Son id est celui qui lui est défini dans la liste
        }
        public void AjouterClient(string nom, string prenom, DateTime dateNaissance, string adresse, string adresseMail, int telephone, int id)
        {
            clients.Add(new Client(id, nom, prenom, dateNaissance, adresse, adresseMail, telephone)); // on génère un nouveau client. Son id est celui qui lui est défini dans la liste
        }
        public void AjouterSalarie(Salarie salarie)
        {
            salaries.Add(salarie);
        }
        public void AjouterClient(Salarie[] salaries)
        {
            foreach (Salarie salarie in salaries)
            {
                this.salaries.Add(salarie);
            }
        }
        public void AjouterVehicule(Vehicule vehicule)
        {
            vehicules.Add(vehicule);
        }
        public void AjouterVehicule(Vehicule[] vehicules)
        {
            foreach (Vehicule vehicule in vehicules)
            {
                this.vehicules.Add(vehicule);
            }
        }
        public void RetirerClient(Client client)
        {
            clients.Remove(client);
        }
        public void RetirerClient(Client[] clients)
        {
            foreach (Client client in clients)
            {
                this.clients.Remove(client);
            }
        }
        public void RetirerClient(string nom, string prenom)
        {
            foreach (Client client in clients)
            {
                if (client.Nom == nom && client.Prenom == prenom)
                {
                    clients.Remove(client);
                }
            }
        }
        public void RetirerSalarie(Salarie salarie)
        {
            salaries.Remove(salarie);
        }
        public void RetirerSalarie(Salarie[] salaries)
        {
            foreach (Salarie salarie in salaries)
            {
                this.salaries.Remove(salarie);
            }
        }
        public void RetirerVehicule(Vehicule vehicule)
        {
            vehicules.Remove(vehicule);
        }
        public void RetirerVehicule(Vehicule[] vehicules)
        {
            foreach (Vehicule vehicule in vehicules)
            {
                this.vehicules.Remove(vehicule);
            }
        }
        public void AjouterCommande(Commande commande)
        {
            commandes.Add(commande);
        }
        public void AjouterCommande(Commande[] commandes)
        {
            foreach (Commande commande in commandes)
            {
                this.commandes.Add(commande);
            }
        }
        public void RetirerCommande(Commande commande)
        {
            commandes.Remove(commande);
        }
        public void RetirerCommande(Commande[] commandes)
        {
            foreach (Commande commande in commandes)
            {
                this.commandes.Remove(commande);
            }
        }
        public void RetirerCommande(int idCommande)
        {
            foreach (Commande commande in commandes)
            {
                if (commande.Id == idCommande)
                {
                    commandes.Remove(commande);
                }
            }
        }

        #endregion

        #region Génération de commandes
        private Salarie ChooseDriver(DateTime dateLivraison)
        { // renvoie un salarié de poste chauffeur qui est disponible à la date de livraison
            Salarie s = null;
            s=salaries.Find(s => s.IsDriver() && s.EstDisponible(dateLivraison));
            if (s != null)
            {
                s.Planning.Add(dateLivraison);
            }
            else
            {
                Console.WriteLine("Aucun Chauffeur approprié n'a été trouvé.");
                Console.WriteLine("Veuillez en ajouter dans la BDD (Module Salarié)");
            }
            return s;
        }
        private Vehicule ChooseVehicle(int typeVehicule, DateTime dateLivraison, float distanceTotale) // il faudra définir ici le type de véhicule que l'on souhaite utiliser pour faire le trajet
        { // renvoie un véhicule disponible à la date de livraison et qui peut faire le trajet
            Vehicule v = null;
            string labelTypeVehicule = "";
            switch (typeVehicule)
            {
                case 1: labelTypeVehicule += "Voiture"; break;
                case 2: labelTypeVehicule += "Camion_benne"; break;
                case 3: labelTypeVehicule += "Camion_citerne"; break;
                case 4: labelTypeVehicule += "Camion_frigorifique"; break;
                default: break;
            }

            if (typeVehicule == 4) // Camion frigorifique --> prend en compte l'autonomie frigorifique
            {
                v = vehicules.Find(v => v.EstDisponible(dateLivraison) && v.GetType().Name == labelTypeVehicule && ((Camion_frigorifique)v).Autonomie >= distanceTotale);
            }
            else v = vehicules.Find(v => v.EstDisponible(dateLivraison) && v.GetType().Name == labelTypeVehicule);

            if (v != null)
            {
                v.Planning.Add(dateLivraison);
            }
            else
            {
                Console.WriteLine("Aucun véhicule approprié n'a été trouvé.");
                Console.WriteLine("Veuillez en ajouter dans la BDD (Module Autre)");
            }
           
            return v;
        }

        public Commande GenerationDeCommande(int id, Client client, string depart, string arrivee, int typeVehicule, DateTime dateLivraison) // Permet de générer une commande en indiquant selon les disponibilités des véhicules et des conducteurs si ils peuvent faire le trajet
        { // génère la commande en se basant sur les données qui lui sont fournis
            Commande c = new Commande(id, client, depart, arrivee, dateLivraison, new Vehicule(), new Salarie());
            c.Itineraire = graphe.Dijkstra(depart, arrivee);

            Salarie s = ChooseDriver(dateLivraison);
            Vehicule v = ChooseVehicle(typeVehicule, dateLivraison, c.getDistanceTotale());

            if (s == null || v == null) c = null; // on annule la commande si pas de chauffeur ou vehicule
            else
            {
                c.Chauffeur = s;
                c.Vehicule = v;
                c.getPrix();
            }
            
            return c;
        }
        #endregion

        #region tri des clients
        public void TriClientParOrdreAlphabetique() // tri les clients par ordre alphabétique
        {
            clients.Sort((x, y) =>
            {
                if (x.Nom == y.Nom)
                {
                    return x.Prenom.CompareTo(y.Prenom);
                }
                else
                {
                    return x.Nom.CompareTo(y.Nom);
                }
            });
        }
        public void TriClientParAdresse() // tri les clients par ville
        {
            clients.Sort((x, y) => x.Adresse.CompareTo(y.Adresse));
        }
        public void TriClientParPrix() // tri les clients en fonction de la somme dépensée par le client. UNIQUEMENT LES COMMANDES ARCHIVEES
        {
            clients.Sort((x, y) => x.PrixCommandes().CompareTo(y.PrixCommandes()));
        }
        #endregion
        #endregion
    }
}
