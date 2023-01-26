namespace Project_A3_ESILV
{
    internal class Client : Personne, ICloneable
    {
        #region Champs
        List<Commande> commandes; // les commandes archivées
        #endregion

        #region Constructeur
        public Client(int id, string nom, string prenom, DateTime dateNaissance, string adresse, string adresseMail, int telephone) : base(id, nom, prenom, dateNaissance, adresse, adresseMail, telephone)
        {
            this.commandes = new List<Commande>();
        }
        
        public Client() : base()
        {
            this.commandes = new List<Commande>();
        }
        #endregion

        #region getter-setter
        public List<Commande> Commandes
        {
            get { return commandes; }
        }
        #endregion

        #region méthodes
        #region ajout et retrait de commande
        public void AjouteCommande(Commande command)
        {
            commandes.Add(command);
        }
        public void RetireCommand(Commande command)
        {
            commandes.Remove(command);
        }
        public void RetireCommand(int id)
        {
            foreach (Commande command in commandes)
            {
                if (command.Id == id)
                {
                    commandes.Remove(command);
                    break;
                }
            }
        }
        #endregion
        #region affichage
        public override string ToString()
        {
            string res = "Client : " + base.ToString();
            foreach (Commande com in commandes)
            {
                res += "\n" + com.ToString();
            }
            return res;
        }
        public override string ToStringComplete()
        {
            //renvoie une string contenant tout les paramètres protected de la classe Personne
            string res = "Client : " + base.ToStringComplete() + "ID : " + id;
            foreach (Commande com in commandes)
            {
                res += "\n" + com.ToString();
            }
            return res;

        }
        public void AfficherClient()
        {
            Console.WriteLine(this);
        }
        #endregion
        public int PrixCommandes() // renvoie la somme totale payé par la personne
        {
            int prix = 0;
            foreach (Commande command in commandes)
            {
                if (command != null)
                {
                    prix += command.Prix;
                }
            }
            return prix;
        }
        public object Clone() // possibilité de backup d'un client avant manipulation. Fonction non utilisé dans le programme principal, utilisé principalement pour des tests.
        {
            Client client = new Client();
            client.id = this.id;
            client.nom = this.nom;
            client.prenom = this.prenom;
            client.dateNaissance = this.dateNaissance;
            client.adresse = this.adresse;
            client.adresseMail = this.adresseMail;
            client.telephone = this.telephone;
            client.commandes = this.commandes;
            return client;
        }
        #endregion
    }

}
