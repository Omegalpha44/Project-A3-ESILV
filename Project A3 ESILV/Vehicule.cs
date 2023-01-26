namespace Project_A3_ESILV
{
    internal class Vehicule
    {
        #region Champs
        // les attributs ne sont pas définis dans le sujet, je prends l'exemple donc de Uber en ne définissant que l'immatriculation du véhicule pour le reconnaitre. Cette classe est abstracte
        string immatriculation;
        List<DateTime> planning;
        #endregion

        #region Constructeur
        public Vehicule(string immatriculation)
        {
            this.immatriculation = immatriculation;
            this.planning= new List<DateTime>();
        }

        public Vehicule()
        {
            this.immatriculation = "";
            this.planning = new List<DateTime>();
        }
        #endregion

        #region getter-setter
        public string Immatriculation
        {
            get { return immatriculation; }
        }
        public List<DateTime> Planning
        {
            get { return planning; }
            set { planning = value; }
        }
        #endregion

        #region Méthodes
        public override string ToString()
        {
            string res = "Vehicule : " + immatriculation;
            if(planning.Count != 0) 
            {
                res += "Planning : ";
                foreach (DateTime dt in planning) res += dt.Date + " ";
            }
            
            return res;
        }

        public bool EstDisponible(DateTime dateLivraison)
        {
            return !this.Planning.Contains(dateLivraison);
        }
        #endregion
    }
}
