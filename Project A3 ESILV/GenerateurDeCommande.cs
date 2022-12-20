using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class GenerateurDeCommande
    {
        List<Salarie> salaries;
        List<Client> clients;
        List<Vehicule> vehicules;

        //constructeur
        public GenerateurDeCommande(List<Salarie> salaries, List<Client> clients, List<Vehicule> vehicules)
        {
            this.salaries = salaries;
            this.clients = clients;
            this.vehicules = vehicules;
        }

        //getter-setter
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

        //methodes
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
        public Command GenerationDeCommande(string depart, string arrive,Client client, int prix) // Permet de générer une commande en indiquant selon les disponibilités des véhicules et des conducteurs si ils peuvent faire le trajet
        {
            Salarie s = ChooseDriver();
            Vehicule v = ChooseVehicle();
            Command c = new Command(client, arrive, depart, prix, v, s);
            return c;
        }
    }
}
