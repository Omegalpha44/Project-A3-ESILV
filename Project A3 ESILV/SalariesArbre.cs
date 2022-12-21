using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class SalariesArbre
    {
        Salarie s;
        SalariesArbre frere;
        SalariesArbre fils;

        public SalariesArbre(Salarie s)
        {
            this.s = s;
            frere = null;
            fils = null;
        }
        public Salarie S { get; }
        #region gestion de l'arbre
        public void CreerArbre(List<Salarie> salaries,SalariesArbre boss = null) // création rapide d'un arbre n-aire L'ORDRE N'EST PAS RESPECTE
        {
            if(salaries.Count>1)
            {
                if(boss == null)
                {
                    fils = new SalariesArbre(salaries[0]);
                    salaries.RemoveAt(0);
                    if (fils != null) fils.CreerArbre(salaries,this);
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
        public void AfficherArbre()
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
        #endregion
        #region ToString
        public override string ToString() // renvoie sous forme de string les information des salaries de l'arbres n-aire
        {
            string self = s.ToString();
            if(frere!=null) self += "\n " + frere.ToString();
            if(fils != null) self += "\n " + fils.ToString();
            return self;
        }
        #endregion
        #region permet de déterminer les employées ou le manager de la personne indiqué
        bool IsInSalariesList(string prenom,string nom,string poste) // vérifie si l'employée est dans la liste des frères
        {
            if (s.Prenom == prenom && s.Nom == nom && s.Poste == poste)
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
        public void AfficherManager(string prenom,string nom,string poste,Salarie boss = null)
        {
            if(IsInSalariesList(prenom,nom,poste)) // si l'employee est dans la liste des frères
            {
                if (boss == null) Console.WriteLine("PDG");
                else Console.WriteLine(boss);
            }
            else
            {
                if (fils != null) this.fils.AfficherManager(prenom, nom, poste, s);
                if (frere != null) this.frere.AfficherManager(prenom, nom, poste, boss);
            }
        }
        public void AfficherEmployees(string prenom,string nom,string poste)
        {
            if(s.Nom == nom && s.Prenom == prenom && s.Poste == poste)
            {
                Console.WriteLine("les employées de cette personne sont : ");
                Console.WriteLine(fils);
            }
            else
            {
                if (frere != null) frere.AfficherEmployees(prenom,nom,poste);
                if (fils != null) fils.AfficherEmployees(prenom, nom, poste);
            }
        }
        public void AfficherHierarchie(Salarie manager = null) // affiche la hiérarchie de l'entreprise
        {
            if (manager == null)
            {
                if(s!=null) Console.WriteLine("PDG :" + s.Nom);
            }
            else if(s!=null) Console.WriteLine("employée de " + manager.Nom + ": " + s.Nom);
            if (frere != null) frere.AfficherHierarchie(manager);
            if (fils != null) fils.AfficherHierarchie(s);
        }
        #endregion
        #region Ajout et retrait d'un salarié en fonctiond de différent paramètres
        public void AjouterSalarie(Salarie sal, string managerNom,string managerPrenom) // permet de rajouter un salarié dans l'ordre hiérarchique. NE FONCTIONNE PAS POUR LE PDG
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
                else Console.WriteLine("Le manager n'existe pas");
            }
        }
        void AjouterFrere(SalariesArbre test) // permet d'ajouter un frère en bout de liste
        {
            if (frere != null) frere.AjouterFrere(test);
            else frere = test;
        }
        void RegulerFrere(Salarie test) // permet de réguler une liste chainée quand un élément est nullé
        {
            if(s.Nom == test.Nom && s.Prenom == test.Prenom && s.Poste == test.Poste)
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
        public void RetirerSalarie(string prenom,string nom,string poste,SalariesArbre boss = null) // permet de retirer un employée de la hiérarchie. Si il disposait d'employé, ils sont redirigés vers le patron dudit employée
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
                        this.s = fils.s;
                        this.frere = null;
                        this.fils = fils.fils;
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
    }
}
