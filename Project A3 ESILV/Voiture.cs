namespace Project_A3_ESILV
{
    internal class Voiture : Vehicule
    {
        #region Champs
        int nbPlaces;
        #endregion

        #region Constructeur
        public Voiture(string immatriculation, int nbPlaces) : base(immatriculation)
        {
            this.nbPlaces = nbPlaces;
        }
        #endregion

        #region getter-setter
        public int NbPlaces
        {
            get { return nbPlaces; }
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            return base.ToString() + " NbPlaces : " + nbPlaces;
        }
        #endregion
    }
}
