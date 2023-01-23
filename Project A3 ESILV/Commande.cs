namespace Project_A3_ESILV
{
    internal class Commande : IPrix, IId
    {
        #region Champs
        int id;
        Client client;
        string arrivee;
        string depart;
        DateTime dateLivraison;
        List<Arete> itineraire;
        int prix;
        Vehicule vehicule;
        Salarie chauffeur;
        #endregion

        #region Constructeurs
        public Commande(int id, Client client, string depart, string arrivee, DateTime dateLivraison, Vehicule vehicule, Salarie chauffeur)
        {
            this.id = id;
            this.client = client;
            this.depart = depart;
            this.arrivee = arrivee;
            this.dateLivraison= dateLivraison;
            this.itineraire = new List<Arete>();
            this.prix = 0;
            this.vehicule = vehicule;
            this.chauffeur = chauffeur;

        }

        public Commande()
        {
            this.id = 0;
            this.client = new Client();
            this.depart = "";
            this.arrivee = "";
            this.dateLivraison = new DateTime();
            this.itineraire = new List<Arete>();
            this.prix = 0;
            this.vehicule = null;
            this.chauffeur = null;

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
        public DateTime DateLivraison 
        { 
            get { return dateLivraison; }
            set { dateLivraison = value;}
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
        public List<Arete> Itineraire
        { 
            get { return itineraire; }
            set { itineraire = value; }
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return "Commande n° : " + id + ", Client : " + client.Nom + " " + client.Prenom + ", Route : " + depart + "-->" + arrivee + ", Date de livraison : " + dateLivraison.Date;
        }

        public string DescriptionComplete()
        {
            return "Commande n°" + id + " : " + client.Nom + " " + client.Prenom + " a commandé un " + vehicule.Immatriculation + " pour aller de " + depart + " à " + arrivee + " pour un prix de " + prix + "€." + " Le chauffeur est " + chauffeur.Nom + " " + chauffeur.Prenom + " " + chauffeur.Nom;
        }

        public void AfficherItineraire()
        {
            float km = 0;
            itineraire.ForEach(arete =>
            {
                Console.WriteLine("{0} (km : {1})",arete.Depart,km);
                Console.WriteLine("| |");
                for (int i = 0; i < arete.Distance / 50; i++) //Affichage proportionnel à la distance
                {
                    Console.WriteLine("| |");
                }
                km += arete.Distance;
            });
            Console.WriteLine("{0} (km : {1})", arrivee, km);
        }
        #endregion
    }
}
