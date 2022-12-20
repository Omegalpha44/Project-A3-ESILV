using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    abstract internal class PersonneEnt
    {
        int id;
        string nom;
        string prenom;
        DateTime dateNaissance;
        string adresse;
        string adresseMail;
        int telephone;

        //constructeur
        public PersonneEnt(int id, string nom, string prenom, DateTime dateNaissance, string adresse, string adresseMail, int telephone)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.adresse = adresse;
            this.adresseMail = adresseMail;
            this.telephone = telephone;
        }

        //getter-setter
        public int Id
        {
            get { return id; }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public string Prenom
        {
            get { return prenom; }
        }
        public DateTime DateNaissance
        {
            get { return dateNaissance; }
        }
        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }
        public string AdresseMail
        {
            get { return adresseMail; }
            set { adresseMail = value; }
        }
        public int Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        
        //méthodes
    }
}
