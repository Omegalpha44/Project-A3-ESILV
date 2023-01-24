﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Project_A3_ESILV
{
    internal class LireFichier // permet de lire un fichier contenant les employés de TransConnect
    {
        #region Champs
        public Manager manager;
        string path;
        static string sep = ";";
        #endregion

        #region Constructeurs
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
        #endregion

        #region Propriétés
        public Manager Manager { get; }
        #endregion

        #region Methodes

        #region BDD Clients / Salariés
        // pos = 0 : nom
        // pos = 1 : prenom
        // pos = 2 : date de naissance
        // pos = 3 : adresse
        // pos = 4 : adresse mail
        // pos = 5 : téléphone
        // pos = 6 : date d'embauche (salarie only)
        // pos = 7 : salaire (salarie only)
        // pos = 8 : poste (salarie only)
        // pos = 9 : Employeur : (salarie only)
        // pos = 10 : Nom employeur (salarie only)
        // pos = 11 : Prenom employeur (salarie only)
        // pos = 12 : id (salarie only)

        #region Modification et lecture du fichier de sauvegarde
        public void ReadFile()
        {
            //lis un fichier texte et en tire les informations pour créer des objets de type Salarie
            //le fichier doit être de la forme suivante:
            // nom, prenom, date de naissance, adresse, adresse mail, téléphone, date d'embauche, salaire, poste, Employeur :, Nom, Prenom, id
            //exemple:
            // Dupont, Jean, 01/01/1990, 1 rue de la paix, dupont@gmail.com, 0719203, 01/01/2002, 10000, chauffeur, id

            //on ouvre le fichier
            if (!File.Exists(path))
            {
                //on créé le fichier s'il n'existe pas
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine("nom" + sep + "prenom" + sep + "date de naissance" + sep + "adresse" + sep + "adresse mail" + sep + "téléphone" + sep + "date d'embauche" + sep + "salaire" + sep + "poste" + sep + "Employeur :" + sep + "Nom" + sep + "Prenom" + sep + "id");
                writer.Close();
            }
            manager.Salaries.Clear();
            manager.SalariesHierarchie = null;
            StreamReader sr = new StreamReader(path);
            string line = sr.ReadLine();
            line = sr.ReadLine();

            //on lit le fichier ligne par ligne
            while (line != null && line != ";;;;;;;;;;;;") // le deuxième cas correspond à une ligne fantôme, créé par excel
            {
                //on découpe la ligne en fonction des virgules
                string[] mots = line.Split(sep);
                mots[0] = mots[0].ToUpper();
                mots[1] = mots[1].ToUpper();
                mots[10] = mots[10].ToUpper();
                mots[11] = mots[11].ToUpper();
                mots[8] = mots[8].ToUpper();
                //on crée un objet de type Salarie avec les informations de la ligne
                string[] birth = mots[2].Split("/");
                DateTime X = new DateTime(int.Parse(birth[2]), int.Parse(birth[1]), int.Parse(birth[0]));
                string[] hire = mots[6].Split("/");
                DateTime Y = new DateTime(int.Parse(hire[2]), int.Parse(hire[1]), int.Parse(hire[0]));
                Salarie salarie = new Salarie(int.Parse(mots[12]), mots[0], mots[1], X, mots[3], mots[4], int.Parse(mots[5]), Y, mots[8], int.Parse(mots[7]));
                //on ajoute le salarié à la liste des salariés
                manager.Salaries.Add(salarie);
                if (mots[10] == "" || mots[10] == "TRANSCONNECT")
                {
                    if(manager.SalariesHierarchie != null) manager.SalariesHierarchie.AjouterBoss(salarie);
                    else manager.SalariesHierarchie = new SalariesArbre(salarie);
                }
                else
                {
                    manager.SalariesHierarchie.AjouterSalarie(salarie, mots[10], mots[11]);
                }
                //on lit la ligne suivante
                line = sr.ReadLine();
            }
            sr.Close();
        }
        public void ReadFileClient()
        {
            if (!File.Exists(path))
            {
                //on créé le fichier si il n'existe pas
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine("nom" + sep + "prenom" + sep + "date de naissance" + sep + "adresse" + sep + "adresse mail" + sep + "téléphone" + sep + "id");
                writer.Close();
            }
            manager.Clients.Clear();
            TextReader sr = new StreamReader(path);
            string line = sr.ReadLine();
            line = sr.ReadLine();
            while (line != null)
                {
                string[] mots = line.Split(sep);
                string[] birth = mots[2].Split("/");
                DateTime X = new DateTime(int.Parse(birth[2]), int.Parse(birth[1]), int.Parse(birth[0]));
                Client client = new Client(int.Parse(mots[6]),mots[0], mots[1], X, mots[3], mots[4], int.Parse(mots[5]));
                manager.Clients.Add(client);
                line = sr.ReadLine();
            }
            sr.Close();
        }
        public void Add(Salarie s, string nom,string prenom)
        {
            TextWriter sr = new StreamWriter(path,true);
            string birth = s.DateNaissance.Day.ToString() + "/" + s.DateNaissance.Month.ToString() + "/" + s.DateNaissance.Year.ToString();
            string embauche = s.DateEmbauche.Day.ToString() + "/" + s.DateEmbauche.Month.ToString() + "/" + s.DateEmbauche.Year.ToString();
            sr.Write(s.Nom + sep + s.Prenom + sep + birth + sep + s.Adresse + sep + s.AdresseMail + sep + s.Telephone + sep + embauche + sep + s.Salaire + sep + s.Poste + sep + "Employeur :" + sep + nom + sep + prenom + sep + s.Id+"\n");
            sr.Close();
        }
        public void Add(Client c)
        {
            TextWriter sr = new StreamWriter(path, true);
            string birth = c.DateNaissance.Day.ToString() + "/" + c.DateNaissance.Month.ToString() + "/" + c.DateNaissance.Year.ToString();
            sr.Write(c.Nom + sep + c.Prenom + sep + birth + sep + c.Adresse + sep + c.AdresseMail + sep + c.Telephone + sep + c.Id + "\n");
            sr.Close();
        }
        public void Remove(Salarie s)
        {
            TextReader tr = new StreamReader(path);
            TextWriter temp = new StreamWriter("temp.txt");
            bool haveWeRemovedThePdg = false;
            string prenom ="";
            string nom="";
            string bossNom = "";
            string bossPrenom = "";
            string line = tr.ReadLine();
            while (line != null)
            {
                string[] mots = line.Split(sep);
                if (int.Parse(mots[12]) == s.Id && mots[10].ToUpper() == "TRANSCONNECT")
                {
                    haveWeRemovedThePdg = true;
                    prenom = s.Prenom;
                    nom = s.Nom;

                }
                else if (int.Parse(mots[12]) == s.Id)
                {
                    prenom = s.Prenom;
                    nom = s.Nom;
                    bossNom = mots[10];
                    bossPrenom = mots[11];
                }
                if (int.Parse(mots[12]) != s.Id)
                {
                   if (haveWeRemovedThePdg && mots[10]==nom && mots[11] == prenom)
                    {
                        mots[11] = "";
                        mots[10] = "TransConnect";
                        haveWeRemovedThePdg = false;
                        bossNom = mots[0];
                        bossPrenom = mots[1];
                    }
                   else if (mots[10] == nom && mots[11] == prenom)
                    {
                        mots[10] = bossNom;
                        mots[11] = bossPrenom;
                    }
                   temp.Write(mots[0] + sep + mots[1] + sep + mots[2] + sep + mots[3] + sep + mots[4] + sep + mots[5] + sep + mots[6] + sep + mots[7] + sep + mots[8] + sep + mots[9] + sep + mots[10] + sep + mots[11] + sep + mots[12] + "\n");
                }
                line = tr.ReadLine();
            }
            tr.Close();
            temp.Close();
            File.Delete(path);
            File.Move("temp.txt", path);
        }
        public void Remove(Client c)
        {
            TextReader tr = new StreamReader(path);
            TextWriter temp = new StreamWriter("temp.txt");
            string line = tr.ReadLine();
            while (line != null)
            {
                string[] mots = line.Split(sep);
                if (int.Parse(mots[6]) != c.Id)
                {
                    temp.Write(mots[0] + sep + mots[1] + sep + mots[2] + sep + mots[3] + sep + mots[4] + sep + mots[5] + sep + mots[6] + "\n");
                }
                line = tr.ReadLine();
            }
            tr.Close();
            temp.Close();
            File.Delete(path);
            File.Move("temp.txt", path);
        }
        public void Modify(Salarie s, int pos, string modification)
        {
            // modifie un salarie en changeant l'information situé à la case pos par modification
            TextReader tr = new StreamReader(path);
            TextWriter temp = new StreamWriter("temp.txt",false);
            string line = tr.ReadLine();
            string nom = "";
            string prenom = "";
            while (line != null)
            {
                string[] mots = line.Split(sep);
                if (int.Parse(mots[12]) == s.Id)
                {
                    nom = mots[0];
                    prenom = mots[1];
                    switch (pos)
                    {
                        case 0:
                            mots[0] = modification;
                            break;
                        case 1:
                            mots[1] = modification;
                            break;
                        case 2:
                            mots[2] = modification;
                            break;
                        case 3:
                            mots[3] = modification;
                            break;
                        case 4:
                            mots[4] = modification;
                            break;
                        case 5:
                            mots[5] = modification;
                            break;
                        case 6:
                            mots[6] = modification;
                            break;
                        case 7:
                            mots[7] = modification;
                            break;
                        case 8:
                            mots[8] = modification;
                            break;
                        case 9:
                            mots[9] = modification;
                            break;
                        case 10:
                            mots[10] = modification;
                            break;
                        case 11:
                            mots[11] = modification;
                            break;
                        case 12:
                            mots[12] = modification;
                            break;
                    }
                    temp.Write(mots[0] + sep + mots[1] + sep + mots[2] + sep + mots[3] + sep + mots[4] + sep + mots[5] + sep + mots[6] + sep + mots[7] + sep + mots[8] + sep + mots[9] + sep + mots[10] + sep + mots[11] + sep + mots[12]+"\n");
                }
                else
                {
                    if (pos  == 0 )
                    {
                        if (mots[10] == nom)
                        {
                            mots[10] = modification;
                        }
                    }
                    if(pos == 1)
                    {
                        if (mots[11] == prenom)
                        {
                            mots[11] = modification;
                        }
                    }
                    temp.Write(mots[0] + sep + mots[1] + sep + mots[2] + sep + mots[3] + sep + mots[4] + sep + mots[5] + sep + mots[6] + sep + mots[7] + sep + mots[8] + sep + mots[9] + sep + mots[10] + sep + mots[11] + sep + mots[12] + "\n");
                }
                line = tr.ReadLine();
            }
            tr.Close();
            temp.Close();
            File.Delete(path);
            File.Move("temp.txt", path);
        }
        public void Modify(Client c, int pos, string modification)
        {
            // modifie un client en changeant l'information situé à la case pos par modification
            TextReader tr = new StreamReader(path);
            TextWriter temp = new StreamWriter("temp.txt", false);
            string line = tr.ReadLine();
            while (line != null)
            {
                string[] mots = line.Split(sep);
                if (int.Parse(mots[6]) == c.Id)
                {
                    switch (pos)
                    {
                        case 0:
                            mots[0] = modification;
                            break;
                        case 1:
                            mots[1] = modification;
                            break;
                        case 2:
                            mots[2] = modification;
                            break;
                        case 3:
                            mots[3] = modification;
                            break;
                        case 4:
                            mots[4] = modification;
                            break;
                        case 5:
                            mots[5] = modification;
                            break;
                        case 6:
                            mots[6] = modification;
                            break;
                    }
                    temp.Write(mots[0] + sep + mots[1] + sep + mots[2] + sep + mots[3] + sep + mots[4] + sep + mots[5] + sep + mots[6] + "\n");
                }
                else
                {
                    temp.Write(mots[0] + sep + mots[1] + sep + mots[2] + sep + mots[3] + sep + mots[4] + sep + mots[5] + sep + mots[6] + "\n");
                }
                line = tr.ReadLine();
            }
            tr.Close();
            temp.Close();
            File.Delete(path);
            File.Move("temp.txt", path);
        }
        #endregion
        #endregion
        #region BDD Distances
        // pos = 0 : Départ
        // pos = 1 : Arrivée
        // pos = 2 : Distance
        // pos = 3 : Durée


        public void InitialisationGraphe()
        {
            // Initialise le graphe en fonction des données de distances.csv
            // Il faut relancer InitialisationGraphe si le fichier distances.csv est modifié
            // Le fichier doit être de la forme suivante:
            // Départ, Arrivée, Distance, Durée
            //exemple:
            // Paris Rouen 133 1h45

            //on ouvre le fichier
            if (!File.Exists(path))
            {
                //on créé le fichier s'il n'existe pas
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine("depart" + sep + "arrivee" + sep + "distance" + sep + "duree");
                writer.Close();
            }
            manager.Graphe.Sommets.Clear();
            manager.Graphe.Aretes.Clear();
            StreamReader sr = new StreamReader(path);
            string line = sr.ReadLine();
            line = sr.ReadLine();

            //on lit le fichier ligne par ligne
            while (line != null)
            {
                //on découpe la ligne en fonction des virgules
                string[] mots = line.Split(sep);
                //on crée un objet de type Arete avec les informations de la ligne
                Arete arete = new Arete(mots[0], mots[1], float.Parse(mots[2])); //départ, arrivée, distance
                //on ajoute l'arête à la liste des arêtes du graphe
                if(!manager.Graphe.Aretes.Contains(arete)) manager.Graphe.Aretes.Add(arete);
                if(!manager.Graphe.Sommets.Contains(arete.Depart)) manager.Graphe.Sommets.Add(arete.Depart);
                if (!manager.Graphe.Sommets.Contains(arete.Arrivee)) manager.Graphe.Sommets.Add(arete.Arrivee);
                //on lit la ligne suivante
                line = sr.ReadLine();
            }
            sr.Close();
        }
        #endregion
        #region utilitaire de transformation du fichier de sauvegarde
        static string alpha = "abcdefghijklmnopqrstuvwxyz"; // les permutations dépendront toute de l'alphabet
        public static void FromTxt2Csv(string path1, string path2)
        {// transforme le fichier txt en une sauvegarde csv
            string spe2 = ";";
            TextReader tr = new StreamReader(path1);
            TextWriter tw = new StreamWriter(path2, false);
            string line = tr.ReadLine();
            while (line != null)
            {
                string[] mots = line.Split(sep);
                tw.Write(mots[0] + spe2 + mots[1] + spe2 + mots[2] + spe2 + mots[3] + spe2 + mots[4] + spe2 + mots[5] + spe2 + mots[6] + spe2 + mots[7] + spe2 + mots[8] + spe2 + mots[9] + spe2 + mots[10] + spe2 + mots[11] + spe2 + mots[12] + "\n");
                line = tr.ReadLine();
            }
            tr.Close();
            tw.Close();
        }
        // chiffrage par substitution du fichier
        public static void ChiffrageSub(string path, string path2, string permu)
        { // chiffre le fichier situé au path dans le path2
            TextReader tr = new StreamReader(path);
            TextWriter tw = new StreamWriter(path2, false);
            string ligne = tr.ReadLine();
            while (ligne != null)
            {
                tw.WriteLine(Chiffrer(ligne, permu));
                ligne = tr.ReadLine();
            }
            tr.Close();
            tw.Close();
        }
        static string Chiffrer(string line, string permu)
        {
            string res = "";
            foreach (char c in line)
            {
                if (alpha.Contains(c)) // si il s'agit d'une minuscule
                {
                    res += permu[alpha.IndexOf(c)];
                }
                else if (alpha.ToUpper().Contains(c)) // gérer le cas si il s'agit d'une majuscule
                {
                    res += permu.ToUpper()[alpha.ToUpper().IndexOf(c)];
                }
                else
                {
                    res += c;
                }
            }
            return res;
        }
        static string Dechiffrer(string line, string permu)
        {
            string res = "";
            foreach (char c in line)
            {
                if (permu.Contains(c))
                {
                    res += alpha[permu.IndexOf(c)];
                }
                else if (permu.ToUpper().Contains(c))
                {
                    res += alpha.ToUpper()[permu.ToUpper().IndexOf(c)];
                }
                else
                {
                    res += c;
                }
            }
            return res;
        }
        public static void DechiffrageSub(string path, string path2, string permu)
        { // déchiffre le fichier situé au path dans le path2
            TextReader tr = new StreamReader(path);
            TextWriter tw = new StreamWriter(path2, false);
            string ligne = tr.ReadLine();
            while (ligne != null)
            {
                tw.WriteLine(Dechiffrer(ligne, permu));
                ligne = tr.ReadLine();
            }
            tr.Close();
            tw.Close();
        }
        #endregion
        #endregion
    }
}
