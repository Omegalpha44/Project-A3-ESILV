using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A3_ESILV
{
    internal class LireFichier // permet de lire un fichier contenant les employés de TransConnect
    {
        Manager manager;
        string path;
        string pathClient;
        public LireFichier(Manager manager,string path)
        {
            this.manager = manager;
            this.path = path;
        }
        public LireFichier(string path)
        {
            this.manager = new Manager();
            this.path = path;
        }

        public Manager Manager { get; }
        public void ReadFile()
        {
            //lis un fichier texte et en tire les informations pour créer des objets de type Salarie
            //le fichier doit être de la forme suivante:
            // nom, prenom, date de naissance, adresse, adresse mail, téléphone, date d'embauche, salaire, poste, Employeur :, Nom, Prenom
            //exemple:
            // Dupont, Jean, 01/01/1990, 1 rue de la paix, dupont@gmail.com, 0719203, 01/01/2002, 10000, chauffeur

            //on ouvre le fichier
            StreamReader sr = new StreamReader(path);
            string line = sr.ReadLine();

            //on lit le fichier ligne par ligne
            while (line != null)
            {
                //on découpe la ligne en fonction des virgules
                string[] mots = line.Split(", ");
                //on crée un objet de type Salarie avec les informations de la ligne
                string[] birth = mots[2].Split("/");
                DateTime X = new DateTime(int.Parse(birth[2]), int.Parse(birth[1]), int.Parse(birth[0]));
                string[] hire = mots[6].Split("/");
                DateTime Y = new DateTime(int.Parse(hire[2]), int.Parse(hire[1]), int.Parse(hire[0]));
                Salarie salarie = new Salarie(manager.Salaries.Count, mots[0], mots[1], X, mots[3], mots[4], int.Parse(mots[5]), Y, mots[8], int.Parse(mots[7]));
                Console.WriteLine(salarie);
                //on ajoute le salarié à la liste des salariés
                manager.Salaries.Add(salarie);
                if (mots[10] == "" || mots[10] == "TransConnect")
                {
                    if(manager.SalariesHierarchie != null) manager.SalariesHierarchie.AjouterBoss(salarie);
                    else manager.SalariesHierarchie = new SalariesArbre(salarie);
                }
                else
                {
                    Console.WriteLine(mots[10] + " " + mots[11]);
                    manager.SalariesHierarchie.AjouterSalarie(salarie, mots[10], mots[11]);
                }
                //on lit la ligne suivante
                line = sr.ReadLine();
            }
            manager.SalariesHierarchie.Affichage2();
            sr.Close();
        }
        public void Add(Salarie s, string nom,string prenom)
        {
            TextWriter sr = new StreamWriter(path,true);
            string sep = ", ";
            string birth = s.DateNaissance.Day.ToString() + "/" + s.DateNaissance.Month.ToString() + "/" + s.DateNaissance.Year.ToString();
            string embauche = s.DateEmbauche.Day.ToString() + "/" + s.DateEmbauche.Month.ToString() + "/" + s.DateEmbauche.Year.ToString();
            sr.Write("\n"+s.Nom + sep + s.Prenom + sep + birth + sep + s.Adresse + sep + s.AdresseMail + sep + s.Telephone + sep + embauche + sep + s.Salaire + sep + s.Poste + sep + "Employeur :" + sep + nom + sep + prenom);
            sr.Close();
        }
        public void Add(Client c)
        {

        }
        public void Remove(Salarie s)
        {

        }
        public void Remove(Client c)
        {

        }
    }
}
