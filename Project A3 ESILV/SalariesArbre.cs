namespace Project_A3_ESILV
{
    internal class SalariesArbre
    {
        #region Champs
        Salarie s;
        SalariesArbre frere;
        SalariesArbre fils;
        #endregion

        #region Constructeurs
        public SalariesArbre(Salarie s)
        {
            this.s = s;
            frere = null;
            fils = null;
        }
        #endregion

        #region Propriétés

        public Salarie S { get; }

        #endregion

        #region Méthodes

        #region gestion de l'arbre
        public void CreerArbre(List<Salarie> salaries, SalariesArbre boss = null) // création rapide d'un arbre n-aire L'ORDRE N'EST PAS RESPECTE
        {
            if (salaries.Count > 1)
            {
                if (boss == null)
                {
                    fils = new SalariesArbre(salaries[0]);
                    salaries.RemoveAt(0);
                    if (fils != null) fils.CreerArbre(salaries, this);
                }
                else
                {
                    frere = new SalariesArbre(salaries[0]);
                    fils = new SalariesArbre(salaries[1]);
                    salaries.RemoveAt(0);
                    salaries.RemoveAt(0);
                    if (fils != null)
                        fils.CreerArbre(salaries, this);
                }
            }
            if (salaries.Count == 1)
            {
                frere = new SalariesArbre(salaries[0]);
                salaries.RemoveAt(0);
            }
        }
        public void AfficherArbre() // obsolète, préférez AfficherHierarchie
        {
            if (fils != null)
            {
                Console.WriteLine("Fils : " + fils.s.Nom);
                fils.AfficherArbre();
            }
            if (frere != null)
            {
                Console.WriteLine("Frere : " + frere.s.Nom);
                frere.AfficherArbre();
            }
        }
        public int NombreFreres()
        {
            if (frere != null) return 1 + frere.NombreFreres();
            else return 1;
        }
        #endregion

        #region ToString
        public override string ToString() // renvoie sous forme de string les information des salaries de l'arbres n-aire
        {
            string self = s.ToString();
            if (frere != null) self += "\n " + frere.ToString();
            if (fils != null) self += "\n " + fils.ToString();
            return self;
        }
        #endregion

        #region permet de déterminer les employées ou le manager de la personne indiqué
        bool IsInSalariesList(string prenom, string nom, string poste) // vérifie si l'employée est dans la liste des frères
        {
            poste = poste.ToUpper();
            prenom = prenom.ToUpper();
            nom = nom.ToUpper();
            if (s.Prenom.ToUpper() == prenom && s.Nom.ToUpper() == nom && s.Poste.ToUpper() == poste)
            {
                return true;
            }
            else
            {
                if (frere != null)
                    return frere.IsInSalariesList(prenom, nom, poste);
                else
                    return false;
            }
        }
        public void AfficherManager(string prenom, string nom, string poste, Salarie boss = null)
        {
            if (IsInSalariesList(prenom, nom, poste)) // si l'employee est dans la liste des frères
            {
                if (boss == null) Console.WriteLine("PDG");
                else Console.WriteLine(boss);
            }
            else
            {
                if (fils != null) fils.AfficherManager(prenom, nom, poste, s);
                if (frere != null) frere.AfficherManager(prenom, nom, poste, boss);
            }
        }
        public void AfficherEmployees(string prenom, string nom, string poste)
        {
            nom = nom.ToUpper();
            prenom = prenom.ToUpper();
            poste = poste.ToUpper();
            
            if (s.Nom.ToUpper() == nom && s.Prenom.ToUpper() == prenom && s.Poste.ToUpper() == poste)
            {
                Console.WriteLine("les employées de cette personne sont : ");
                Console.WriteLine(fils);
            }
            else
            {
                if (frere != null) frere.AfficherEmployees(prenom, nom, poste);
                if (fils != null) fils.AfficherEmployees(prenom, nom, poste);
            }
        }
        public void AfficherHierarchie(Salarie manager = null) // affiche la hiérarchie de l'entreprise
        {
            if (manager == null)
            {
                if (s != null) Console.WriteLine("PDG :" + s.Nom + " " + s.Prenom);
            }
            else if (s != null) Console.WriteLine("employée de " + manager.Nom + ": " + s.Nom + " " + s.Prenom);
            if (frere != null) frere.AfficherHierarchie(manager);
            if (fils != null) fils.AfficherHierarchie(s);
        }
        #endregion

        #region Ajout et retrait d'un salarié en fonction de différent paramètres
        public void AjouterBoss(Salarie sal)
        {
            Salarie temp = s;
            s = sal;
            AjouterSalarie(temp, sal.Nom, sal.Prenom);
        }
        public void AjouterSalarie(Salarie sal, string managerNom, string managerPrenom) // permet de rajouter un salarié dans l'ordre hiérarchique. NE FONCTIONNE PAS POUR LE PDG
        {
            if (s.Nom == managerNom && s.Prenom == managerPrenom)
            {
                if (fils == null) fils = new SalariesArbre(sal);
                else
                {
                    SalariesArbre temp = fils;
                    while (temp.frere != null) temp = temp.frere;
                    temp.frere = new SalariesArbre(sal);
                }
            }
            else
            {
                if (frere != null) frere.AjouterSalarie(sal, managerNom, managerPrenom);
                if (fils != null) fils.AjouterSalarie(sal, managerNom, managerPrenom);
                
            }
        }
        void AjouterFrere(SalariesArbre test) // permet d'ajouter un frère en bout de liste
        {
            if (frere != null) frere.AjouterFrere(test);
            else frere = test;
        }
        void RegulerFrere(Salarie test) // permet de réguler une liste chainée quand un élément est nullé
        {
            if (s.Nom == test.Nom && s.Prenom == test.Prenom && s.Poste == test.Poste)
            {
                if (frere != null)
                {
                    if (frere.s == null)
                    {
                        frere = frere.frere;
                        frere.frere.s = null;
                        frere.RegulerFrere(test);
                    }
                    else
                    {
                        frere.RegulerFrere(test);
                    }
                }
                else
                {
                    s = null;
                }
            }
        }
        public void RetirerSalarie(string prenom, string nom, string poste, SalariesArbre boss = null) // permet de retirer un employée de la hiérarchie. Si il disposait d'employé, ils sont redirigés vers le patron dudit employée
        {

            if (s.Nom == prenom && s.Nom == nom && s.Poste == poste)
            {
                if (boss != null)
                {
                    boss.AjouterFrere(fils); // boss fils ne peut pas être nul car il est le manager de l'employée
                    boss.fils.RegulerFrere(s); // on enlève un élément d'une liste chainée

                }
                else
                {
                    if (fils != null)
                    {
                        SalariesArbre temps = fils;
                        s = fils.s;
                        frere = null;
                        fils = fils.fils;
                        fils.AjouterFrere(temps.frere);
                        fils.RegulerFrere(temps.s);
                    }
                    else // double PDG sans employé
                    {
                        s = null;
                    }

                }
            }
            else if (boss == null && IsInSalariesList(prenom, nom, poste)) frere = null; // gère le cas d'un double PDG qui n'aurait pas d'employé, AKA un bras droit
            else
            {
                if (frere != null) frere.RetirerSalarie(prenom, nom, poste);
                if (fils != null) fils.RetirerSalarie(prenom, nom, poste);
            }
        }
        #endregion

        #region affichage graphique de la hiérarchie
        public string[] CoupleSalarieEmployeur(string nom, string prenom, string poste, Salarie boss = null) // renvoie un couple contenant le patron et l'employée
        {
            if (IsInSalariesList(prenom, nom, poste)) // si l'employee est dans la liste des frères
            {
                if (boss == null) return new string[2] { "PDG", nom + " " + prenom + " " + poste };
                else return new string[2] { boss.Nom + " " + boss.Prenom + " " + boss.Poste, nom + " " + prenom + " " + poste };
            }
            else
            {
                if (fils != null) return fils.CoupleSalarieEmployeur(nom, prenom, poste, s);
                if (frere != null) return frere.CoupleSalarieEmployeur(nom, prenom, poste, boss);
                else return new string[2] { "erreur", "erreur" };
            }
        }
        public List<Salarie> ListeDesSalaries()
        {
            List<Salarie> temp = new List<Salarie>();
            if (s != null) temp.Add(s);
            if (frere != null) temp.AddRange(frere.ListeDesSalaries());
            if (fils != null) temp.AddRange(fils.ListeDesSalaries());
            return temp;
        }
        public string[,] GrapheAdjacence() // renvoie le graphe d'adjacence de l'arbre n-aire
        {
            // erreur lié à la fonction CoupleSalarieEmployeur ne renvoyant pas le père de l'employé quand call dans la fonction principale
            List<Salarie> temp = ListeDesSalaries();
            List<String> name = new List<string>(); // liste contenant les noms sous la forme nom prenom poste
            foreach (Salarie sal in temp)
            {
                name.Add(sal.Nom + " " + sal.Prenom + " " + sal.Poste);
            }

            string[,] graphe = new string[temp.Count, 2];
            for (int i = 0; i < name.Count; i++) // initialisation du graphes d'adjacence
            {
                graphe[i, 0] = name[i];
                graphe[i, 1] = ": ";
            }
            
            for (int i = 0; i < temp.Count; i++) // remplissage du graphe d'adjacence
            {
                string[] aux = CoupleSalarieEmployeur(temp[i].Nom, temp[i].Prenom, temp[i].Poste);

                if (aux[0] != "PDG" && aux[0] != "erreur")
                {
                    int index = name.IndexOf(aux[0]);
                    graphe[index, 1] += " " + aux[1] + ", ";
                }
            }
            return graphe;
        }

        //public void Affichage(int profondeur = 0, int profondeurPere = 0, List<Tuple<int, int>> UnfinishedBranch = null)
        //{
        //    if (UnfinishedBranch == null) UnfinishedBranch = new List<Tuple<int, int>>();
        //    if (s != null)
        //    {
        //        if (profondeurPere == 0 && profondeur == 0)
        //        {
        //            Console.WriteLine(s.Nom + " " + s.Prenom + " " + s.Poste);
        //            if (fils != null)
        //            {
        //                if (frere != null) UnfinishedBranch.Add(new Tuple<int, int>(profondeur, profondeurPere));
        //                fils.Affichage(profondeur + 2, profondeur + 1, UnfinishedBranch);
        //            }
        //            if (frere != null) frere.Affichage(profondeur, profondeurPere, UnfinishedBranch);

        //        }
        //        else
        //        {
        //            string temp = "";
        //            for (int i = 0; i < profondeurPere; i++)
        //            {
        //                if (UnfinishedBranch.Contains(new Tuple<int, int>(profondeurPere, i))) temp += "│   ";
        //                else temp += "    ";
        //            }
        //            if (profondeurPere != 0) temp += "├── ";
        //            Console.WriteLine(temp + s.Nom + " " + s.Prenom + " " + s.Poste);
        //            if (fils != null)
        //            {
        //                if (frere != null) UnfinishedBranch.Add(new Tuple<int, int>(profondeur, profondeurPere));
        //                fils.Affichage(profondeur + 1, profondeur, UnfinishedBranch);
        //            }
        //            if (frere != null) frere.Affichage(profondeur, profondeurPere, UnfinishedBranch);
        //        }

        //    }

        //}
        public void Affichage2(int profondeur = 0, int profondeurPere = 0, List<int> barreNonFini = null, int pass = 0)
        {
            if (barreNonFini == null) barreNonFini = new List<int>();
            if (s != null)
            {
                if (profondeurPere == 0 && profondeur == 0) // il s'agit nécessairement du PDG
                {
                    Console.WriteLine(s.Nom + " " + s.Prenom + " " + s.Poste);
                    if(fils!= null)pass = fils.NombreFreres();
                    if (fils != null)
                    {
                        if (frere != null) barreNonFini.Add(profondeurPere);
                        fils.Affichage2(2, 1, barreNonFini,pass);
                    }
                    if (frere != null) frere.Affichage2(profondeur, profondeurPere, barreNonFini,pass);

                }
                else
                {
                    
                    string temp = "";
                    for (int i = 0; i < profondeurPere; i++)
                    {
                        if (profondeurPere == 1) pass-=1;
                        if (barreNonFini.Contains(i) || (i ==1  && pass>0)) temp += "│   ";
                        else temp += "    ";
                    }
                    if (profondeurPere != 0)
                    {
                        if (frere != null) temp += "├── ";
                        else
                        {
                            temp += "└── ";
                            barreNonFini.Remove(profondeurPere);
                        }
                        Console.WriteLine(temp + s.Nom + " " + s.Prenom + " " + s.Poste);
                        if (fils != null)
                        {
                            if (frere != null) barreNonFini.Add(profondeur);
                            fils.Affichage2(profondeur + 1, profondeur, barreNonFini,pass);
                        }
                        if (frere != null) frere.Affichage2(profondeur, profondeurPere, barreNonFini,pass);
                    }

                }
            }
            
        }
        #endregion
        #endregion
    }
}
