using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class Salarie : PersonneEnt,IPrix
    {

        DateTime dateEmbauche;
        string poste;
        int salaire;
        bool hasDrivenToday; // vérifie si le conducteur a conduit aujourd'hui (false si non, true si oui) NE S'APPLIQUE QUE SI CHAUFFEUR
        int livraison; // nombre de livraison effectuées par le conducteur NE S'APPLIQUE QUE SI CHAUFFEUR
        bool isFree; //false si le conducteur est occupé, true si il est libre NE S'APPLIQUE QUE SI CHAUFFEUR

        //constructeur
        public Salarie(int id, string nom, string prenom, DateTime dateNaissance, string adresse, string adresseMail, int telephone, DateTime dateEmbauche, string poste, int salaire) : base(id, nom, prenom, dateNaissance, adresse, adresseMail, telephone)
        {
            this.dateEmbauche = dateEmbauche;
            this.poste = poste;
            this.salaire = salaire;
            this.hasDrivenToday = false; 
            this.livraison = 0;
            this.isFree = true;
        }

        //getter-setter
        public DateTime DateEmbauche
        {
            get { return dateEmbauche; }
        }
        public string Poste
        {
            get { return poste; }
            set { poste = value; }
        }
        public int Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }
        public int Prix // reformulation pour passer dans l'interface IPrix
        {
            get { return salaire; }
            set { salaire = value; }
        }
        public bool HasDrivenToday // permet de modifier ce state dans une autre classe, le public peut être gênant niveau sécurité
        {
            get { if (poste == "chauffeur") return hasDrivenToday; else return false; }
            set { hasDrivenToday = value; }
        }
        public int Livraison // renvoie le nombre de livraison du salarié
        {
            get { if (poste == "chauffeur") return livraison; else return -1; }
            set { livraison = value; }
        }
        public bool IsFree // renvoie si le salarié est libre ou non
        {
            get { if (poste == "chauffeur") return isFree; else return false; }
            set { isFree = value; }
        }

        //méthodes
        public void AjoutLivraison()  // ajoute une livraison au conducteur
        {
            if(poste == "chauffeur")  livraison++;
        }

        public override string ToString()
        {
            return "Salarié : " + base.ToString() + " Date d'embauche : " + dateEmbauche + " Poste : " + poste + " Salaire : " + salaire;
        }
        public static  bool Equal(Salarie s1, Salarie s2)
        {
            return s1.Nom == s2.Nom && s2.Prenom == s2.Prenom;
        }
    }
}
