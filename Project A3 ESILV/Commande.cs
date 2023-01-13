namespace Project_A3_ESILV
{
    internal class Commande : IPrix, IId
    {
        #region Champs
        Client client;
        string arrivee;
        string depart;
        int prix;
        Vehicule vehicule;
        Salarie chauffeur;
        int id;
        #endregion

        #region Constructeurs
        public Commande(Client client, string arrivee, string depart, int prix, Vehicule vehicule, Salarie chauffeur, int id)
        {
            this.client = client;
            this.arrivee = arrivee;
            this.depart = depart;
            this.prix = prix;
            this.vehicule = vehicule;
            this.chauffeur = chauffeur;
            this.id = id;

        }
        #endregion

        #region Propriétés
        public Client Client
        {
            get { return client; }
        }
        public string Arrivee
        {
            get { return arrivee; }
            set { arrivee = value; }
        }
        public string Depart
        {
            get { return depart; }
            set { depart = value; }
        }
        public int Prix
        {
            get { return prix; }
            set { prix = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return "Commande n°" + id + " : " + client.Nom + " " + client.Prenom + " a commandé un " + vehicule.Immatriculation + " pour aller de " + depart + " à " + arrivee + " pour un prix de " + prix + "€." + " Le chauffeur est " + chauffeur.Nom + " " + chauffeur.Prenom + " " + chauffeur.Nom;
        }
        #endregion
    }
}
