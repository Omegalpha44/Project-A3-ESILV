namespace Project_A3_ESILV
{
    internal class Commande : IPrix, IId
    {
        #region Champs
        int id;
        Client client;
        string depart;
        string arrivee;
        List<Arete> itineraire;
        DateTime dateLivraison;
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

        public Commande(int id, Client client, string depart, string arrivee, DateTime dateLivraison, Salarie chauffeur,int prix) // utilisé à des fins d'archivage dans un fichier
        {
            this.id = id;
            this.client = client;
            this.depart = depart;
            this.arrivee = arrivee;
            this.dateLivraison = dateLivraison;
            this.itineraire = new List<Arete>();
            this.prix = prix;
            this.vehicule = null;
            this.chauffeur = chauffeur;
        }

        public Commande(int id, Client client, string depart, string arrivee, DateTime dateLivraison, Salarie chauffeur, int prix,Vehicule v) // utilisé à des fins d'archivage dans un fichier
        {
            this.id = id;
            this.client = client;
            this.depart = depart;
            this.arrivee = arrivee;
            this.dateLivraison = dateLivraison;
            this.itineraire = new List<Arete>();
            this.prix = prix;
            this.vehicule = v;
            this.chauffeur = chauffeur;
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
        public Salarie Chauffeur
        {
            get { return chauffeur; }
            set { chauffeur = value; }
        }

        public Vehicule Vehicule
        {
            get { return vehicule; }
            set { vehicule = value; }
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return "Commande n° : " + id + ", Client : " + client.Nom + " " + client.Prenom + ", Route : " + depart + "-->" + arrivee + ", Date de livraison : " + dateLivraison.Date + ", chauffeur : "+ chauffeur.Nom + " " + chauffeur.Prenom+", prix : "+prix+"€";
        }

        public string DescriptionComplete()
        {
            return "Commande n°" + id + " : " + client.Nom + " " + client.Prenom + " a commandé un " + vehicule.Immatriculation + " pour aller de " + depart + " à " + arrivee + " pour un prix de " + prix + "€." + " Le chauffeur est " + chauffeur.Nom + " " + chauffeur.Prenom + " " + chauffeur.Nom;
        }

        public void AfficherItineraire() //Affichage des itinéraires dans la console
        {
            float km = 0;
            TimeSpan duree= TimeSpan.Zero;
            itineraire.ForEach(arete =>
            {
                Console.WriteLine("{0} (km : {1}, duree : {2})",arete.Depart,km,duree);
                Console.WriteLine("| |");
                for (int i = 0; i < arete.Distance / 50; i++) //Affichage proportionnel à la distance
                {
                    Console.WriteLine("| |");
                }
                km += arete.Distance;
                duree+=arete.Duree;
            });
            Console.WriteLine("{0} (km : {1}, duree : {2})", arrivee, km, duree);
        }

        public float getDistanceTotale() //récupère la distance de la livraison
        {
            float res = 0;
            foreach(Arete arete in itineraire)
            {
                res += arete.Distance;
            }
            return res;
        } 

        public TimeSpan getDureeTotale() //récupère la durée de la livraison
        {
            TimeSpan res = TimeSpan.Zero; ;
            foreach (Arete arete in itineraire)
            {
                res += arete.Duree;
            }
            return res;
        } 

        public void getPrix() // calcul et met à jour le champ prix de la commande
        {
            float res = 5; // montant minimal pour les livraisons au sein d'une même ville
            res += (float)getDureeTotale().TotalHours * chauffeur.TarifHoraire;
            string typeVehicule = vehicule.GetType().Name;
            switch (typeVehicule)
            {
                case "Voiture": res += getDistanceTotale() * 0.2f;
                    break;
                case "Camion_benne":
                    res += getDistanceTotale() * 0.3f+((Camion_benne)vehicule).Equipements.Count*0.05f;
                    break;
                case "Camion_citerne":
                    res += getDistanceTotale() * 0.4f+((Camion_frigorifique)vehicule).Capacite*0.02f;
                    break;
                case "Camion_frigorifique":
                    res += getDistanceTotale() * 0.2f+((Camion_frigorifique)vehicule).NbGroupeElectrogene*0.05f;
                    break;

            }
            this.Prix = (int)res;
            
        }
        #endregion
    }
}
