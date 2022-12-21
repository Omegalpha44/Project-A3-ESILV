using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Client : PersonneEnt
    {
        List<Command> commandes;

        //constructeur 
        public Client(int id, string nom, string prenom, DateTime dateNaissance, string adresse, string adresseMail, int telephone) : base(id, nom, prenom, dateNaissance, adresse, adresseMail, telephone)
        {
            this.commandes = new List<Command>();
        }

        //getter-setter
        public List<Command> Commandes
        {
            get { return commandes; }
        }

        //méthodes
        #region ajout et retrait de commande
        public void AjouteCommande(Command command)
        {
            commandes.Add(command);
        }
        public void RetireCommand(Command command)
        {
            commandes.Remove(command);
        }
        #endregion
        public int PrixCommandes() // renvoie la somme totale payé par la personne
        {
            int prix = 0;
            foreach (Command command in commandes)
            {
                if (command != null)
                {
                    prix += command.Prix;
                }
            }
            return prix;
        }
    }
    
}
