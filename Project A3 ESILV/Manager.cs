namespace Project_A3_ESILV
{
    internal class Manager
    {
        #region Champs
        List<Salarie> salaries;
        List<Client> clients;
        List<Vehicule> vehicules;
        SalariesArbre salariesHierarchie;
        #endregion

        #region Constructeur
        public Manager(List<Salarie> salaries, List<Client> clients, List<Vehicule> vehicules)
        {
            this.salaries = salaries;
            this.clients = clients;
            this.vehicules = vehicules;
            this.salariesHierarchie = null;
        }
        public Manager()
        {
            this.salaries = new List<Salarie>();
            this.clients = new List<Client>();
            this.vehicules = new List<Vehicule>();
            this.salariesHierarchie = null;
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
        public SalariesArbre SalariesHierarchie
        {
            get { return salariesHierarchie; }
            set { salariesHierarchie = value; }
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
        #endregion

        #region Génération de commandes
        private Salarie ChooseDriver()
        {
            Salarie s = null;
            foreach (Salarie salarie in salaries)
            {
                if (salarie.Poste == "chauffeur" && salarie.IsFree && salarie.HasDrivenToday == false)
                {
                    s = salarie;
                    salarie.IsFree = false;
                    salarie.HasDrivenToday = true;
                    break;
                }
            }
            return s;
        }
        private Vehicule ChooseVehicle() // il faudra définir ici le type de véhicule que l'on souhaite utiliser pour faire le trajet
        {
            Vehicule v = null;
            foreach (Vehicule vehicule in vehicules)
            {
                if (vehicule.Disponible)
                {
                    v = vehicule;
                    vehicule.Disponible = false;
                    break;
                }
            }
            return v;
        }
        public Commande GenerationDeCommande(string depart, string arrive, Client client, int prix, int id) // Permet de générer une commande en indiquant selon les disponibilités des véhicules et des conducteurs si ils peuvent faire le trajet
        {
            Salarie s = ChooseDriver();
            Vehicule v = ChooseVehicle();
            Commande c = new Commande(client, arrive, depart, prix, v, s, id);
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
        public void TriClientParPrix() // tri les clients en fonction de la somme dépensée par le client
        {
            clients.Sort((x, y) => x.PrixCommandes().CompareTo(y.PrixCommandes()));
        }
        #endregion

        #region gestion sous la forme d'un arbre n-aire des salaries

        #endregion

        #endregion
    }
}
