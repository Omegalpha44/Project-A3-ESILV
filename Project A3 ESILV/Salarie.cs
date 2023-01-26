namespace Project_A3_ESILV
{
    internal class Salarie : Personne, IPrix
    {
        #region Champs
        DateTime dateEmbauche;
        string poste;
        int salaire;
        bool hasDrivenToday;//useless
        int nbLivraisons; // nombre de livraisons effectuées par le conducteur NE S'APPLIQUE QUE SI CHAUFFEUR
        List<DateTime> planning; //liste des dates où le conducteur n'est plus libre A RENOMMER
        #endregion

        #region Constructeur
        public Salarie(int id, string nom, string prenom, DateTime dateNaissance, string adresse, string adresseMail, int telephone, DateTime dateEmbauche, string poste, int salaire) : base(id, nom, prenom, dateNaissance, adresse, adresseMail, telephone)
        {
            this.dateEmbauche = dateEmbauche;
            this.poste = poste;
            this.salaire = salaire;
            this.hasDrivenToday = false; // useless
            this.nbLivraisons = 0; 
            this.planning = new List<DateTime>();
        }
        #endregion

        #region getter-setter
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
        bool IsDriver()
        {
            return poste.ToUpper() == "CHAUFFEUR";
        }
        public bool HasDrivenToday // permet de modifier ce state dans une autre classe, le public peut être gênant niveau sécurité
        {
            get { if (IsDriver()) return hasDrivenToday; else return false; }
            set { hasDrivenToday = value; }
        }
        public int NbLivraisons // renvoie le nombre de livraisons du salarié
        {
            get { if (IsDriver()) return nbLivraisons; else return -1; }
            set { nbLivraisons = value; }
        }
        public List<DateTime> Planning // renvoie si le salarié est libre ou non
        {
            get { if (IsDriver()) return planning; else return null; }
            set { planning = value; }
        }
        public float TarifHoraire
        {
            get { if (IsDriver()) return (float)salaire / 140f; else return -1; } // 140 correspond au nombre d'heure de travail dans un mois en considérant que le chauffeur conduit 35h par semaine
        }
        
        #endregion

        #region méthodes
        public void AjoutLivraison()  // ajoute une livraison au conducteur
        {
            if (IsDriver()) nbLivraisons++;
        }
        public override string ToString()
        {
            return "Salarié : " + base.ToString() + " Date d'embauche : " + dateEmbauche + " Poste : " + poste + " Salaire : " + salaire +  " Sécurité sociale : " + id;
        }
        public override string ToStringComplete()
        {
            return "Salarié : " + base.ToStringComplete() + " Date d'embauche : " + dateEmbauche + " Poste : " + poste + " Salaire : " + salaire + "Sécurité sociale : " + base.Id;
        }
        public static bool Equal(Salarie s1, Salarie s2)
        {
            return s1.Nom == s2.Nom && s2.Prenom == s2.Prenom;
        }
        #endregion
    }
}
