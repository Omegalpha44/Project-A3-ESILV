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
    }
}
