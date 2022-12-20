using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Command : IPrix
    {
        Client client;
        string arrivee;
        string depart;
        int prix;
        Vehicule vehicule;
        Salarie chauffeur;
        
        //constructeur
        public Command(Client client, string arrivee, string depart, int prix, Vehicule vehicule, Salarie chauffeur)
        {
            this.client = client;
            this.arrivee = arrivee;
            this.depart = depart;
            this.prix = prix;
            this.vehicule = vehicule;
            this.chauffeur = chauffeur;
        }

        //getter-setter
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
        
    }
}
