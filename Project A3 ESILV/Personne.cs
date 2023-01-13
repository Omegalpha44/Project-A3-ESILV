namespace Project_A3_ESILV
{
    abstract internal class Personne : IId
    {
        #region Champs
        protected int id;
        protected string nom;
        protected string prenom;
        protected DateTime dateNaissance;
        protected string adresse;
        protected string adresseMail;
        protected int telephone;
        #endregion

        #region constructeur
        public Personne(int id, string nom, string prenom, DateTime dateNaissance, string adresse, string adresseMail, int telephone)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.adresse = adresse;
            this.adresseMail = adresseMail;
            this.telephone = telephone;
        }
        #endregion

        #region getter-setter
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
            set { prenom = value; }
        }
        public DateTime DateNaissance
        {
            get { return dateNaissance; }
            set { dateNaissance = value; }
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
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return nom + " " + prenom;

        }
        public virtual string ToStringComplete()
        {
            return "Nom : " + nom + "\nPrenom : " + prenom + "\nDate de naissance : " + dateNaissance + "\nAdresse : " + adresse + "\nAdresse mail : " + adresseMail + "\nTelephone : " + telephone + "\n";
        }
        #endregion
    }
}
