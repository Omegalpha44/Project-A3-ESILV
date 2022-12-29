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
        #region Champs
        List<Command> commandes;
        #endregion

        #region Constructeur
        public Client(int id, string nom, string prenom, DateTime dateNaissance, string adresse, string adresseMail, int telephone) : base(id, nom, prenom, dateNaissance, adresse, adresseMail, telephone)
        {
            this.commandes = new List<Command>();
        }
        #endregion

        #region getter-setter
        public List<Command> Commandes
        {
            get { return commandes; }
        }
        #endregion

        #region méthodes
        #region ajout et retrait de commande
        public void AjouteCommande(Command command)
        {
            commandes.Add(command);
        }
        public void RetireCommand(Command command)
        {
            commandes.Remove(command);
        }
        public void RetireCommand(int id)
        {
            foreach (Command command in commandes)
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
            foreach(Command com in commandes)
            {
                res += "\n"+com.ToString();
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
            foreach (Command command in commandes)
            {
                if (command != null)
                {
                    prix += command.Prix;
                }
            }
            return prix;
        }
        #endregion
    }

}
