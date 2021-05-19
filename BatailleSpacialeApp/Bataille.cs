using System;
using System.Collections.Generic;

namespace BatailleSpacialeApp
{
    class Bataille
    {
        public List<EmpireSoldat> empireSoldats = new List<EmpireSoldat>();
        public List<RebelleSoldat> rebelleSoldats = new List<RebelleSoldat>();
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("usage: {0} [nombre de soldats de l'empire] [nombre de soldats rebelles]", AppDomain.CurrentDomain.FriendlyName);
                Environment.Exit(1);
            }
            int nbSoldatEmpire;
            if (!int.TryParse(args[0], out nbSoldatEmpire))
            {
                Console.WriteLine("L'argument {0} n'est pas un entier", args[0]);
                Environment.Exit(1);
            }
            int nbSoldatRebelle;
            if (!int.TryParse(args[1], out nbSoldatRebelle))
            {
                Console.WriteLine("L'argument {0} n'est pas un entier", args[1]);
                Environment.Exit(1);
            }

            Bataille b = new Bataille();

            int prediction = b.InitBataille(nbSoldatEmpire, nbSoldatRebelle);
            int resultat = b.DeroulementBataille();
            b.FinPartie(prediction, resultat);
        }

        public int InitBataille(int nbSoldatEmpire, int nbSoldatRebelle)
        {
            //Initialisation de l'armée de l'Empire
            int[][] scoreEmpire = new int[2][]; 
            for(int i = 0; i < 2; i++)
            {
                scoreEmpire[i] = new int[nbSoldatEmpire];
            }

            for(int i = 0; i < nbSoldatEmpire; i++)
            {
                this.empireSoldats.Add(new EmpireSoldat(i));
                //On place ici l'index du soldat
                scoreEmpire[0][i] = i;
                //Ici son score
                scoreEmpire[1][i] = (this.empireSoldats[i].Vie + (this.empireSoldats[i].DegatsInf * 10));
            }

            //On tri le tableau pour obtenir les meilleurs scores
            Utilitaires.Sort(scoreEmpire, nbSoldatEmpire, 1);

            //On determine arbitrairement le pourcentage d'héros à 5%, sinon ce n'est plus vraiment des héros n'est-ce pas ?
            int nombreHerosEmpire = (int)Math.Round((double)(nbSoldatEmpire * (5.0 / 100)));
            //On en met au moins 1
            if (nombreHerosEmpire == 0)
                nombreHerosEmpire = 1;

            Utilitaires.WriteColor("Les "+ nombreHerosEmpire + " héros de l'Empire sont :\n","Empire", ConsoleColor.DarkRed);

            for(int i = nbSoldatEmpire-1; i >= (nbSoldatEmpire - nombreHerosEmpire); i--)
            {
                this.empireSoldats[scoreEmpire[0][i]].Hero = true;
                Console.WriteLine(this.empireSoldats[scoreEmpire[0][i]].ToString());
            }

            Console.WriteLine();


           //Initialisation de l'armée Rebelle (même principe)
           int[][] scoreRebelle = new int[2][];
            for (int i = 0; i < 2; i++)
            {
                scoreRebelle[i] = new int[nbSoldatRebelle];
            }

            for (int i = 0; i < nbSoldatRebelle; i++)
            {
                this.rebelleSoldats.Add(new RebelleSoldat(i));
                //On place ici l'index du soldat
                scoreRebelle[0][i] = i;
                //Ici son score
                scoreRebelle[1][i] = (this.rebelleSoldats[i].Vie + (this.rebelleSoldats[i].DegatsInf * 10));
            }

            //On tri le tableau pour obtenir les meilleurs scores
            Utilitaires.Sort(scoreRebelle, nbSoldatRebelle, 1);

            int nombreHerosRebelle = (int)Math.Round((double)(nbSoldatRebelle * (5.0 / 100)));

            //On en met au moins 1
            if (nombreHerosRebelle == 0)
                nombreHerosRebelle = 1;

            Utilitaires.WriteColor("Les "+ nombreHerosRebelle + " héros Rebelle sont :\n", "Rebelle", ConsoleColor.DarkGreen);

            for (int i = nbSoldatRebelle - 1; i >= (nbSoldatRebelle - nombreHerosRebelle); i--)
            {
                this.rebelleSoldats[scoreRebelle[0][i]].Hero = true;
                Console.WriteLine(this.rebelleSoldats[scoreRebelle[0][i]].ToString());
            }

            Console.WriteLine();

            int scoreTotalEmpire = 0;
            int scoreTotalRebelle = 0;

            for(int i = 0; i < nbSoldatEmpire; i++)
            {
                scoreTotalEmpire += scoreEmpire[1][i];
            }

            for (int i = 0; i < nbSoldatRebelle; i++)
            {
                scoreTotalRebelle += scoreRebelle[1][i];
            }

            int scoreTotalBataille = scoreTotalEmpire + scoreTotalRebelle;

            if (scoreTotalEmpire > scoreTotalRebelle)
            {
                Utilitaires.WriteColor("Le favori pour cette bataille est l'Empire, avec une probabilité de " + (((double)scoreTotalEmpire / scoreTotalBataille) * 100) + "%\n", "Empire", ConsoleColor.DarkRed);
                return 1;
            }
            else if(scoreTotalEmpire < scoreTotalRebelle)
            {
                Utilitaires.WriteColor("Le favori pour cette bataille est l'armée Rebelle, avec une probabilité de " + (((double)scoreTotalRebelle / scoreTotalBataille) * 100) + "%\n", "Rebelle" , ConsoleColor.DarkGreen);
                return 2;
            }
            Console.WriteLine("Il n'y a pas de favori dans cette bataille, que le meilleur gagne !");
            return 0;
        }

        public int DeroulementBataille()
        {
            int nbTour = 1;
            int resultat;
            while((resultat = CheckPartie()) == 0)
            {
                Console.WriteLine("Tour " + nbTour + " :");
                Soldat attaquant = ChoisirSoldat(0);
                Soldat cible;
                if(attaquant.PrefixeMatricule.Equals("Empire"))
                {
                    cible = ChoisirSoldat(2);
                }
                else
                {
                    cible = ChoisirSoldat(1);
                }
                attaquant.Attaque(cible);
                nbTour++;
                Console.WriteLine();
            }
            return resultat;
        }

        public void FinPartie(int prediction, int resultat)
        {
            if(resultat == 1)
            {
                Utilitaires.WriteColor("L'Empire a gagné la bataille !\n", "Empire", ConsoleColor.DarkRed);
                if (prediction == resultat)
                {
                    Utilitaires.WriteColor("Le favori est donc bel et bien sorti vainqueur !\n", "favori", ConsoleColor.DarkYellow);
                }
                else
                {
                    Utilitaires.WriteColor("Les prédictions se sont trompées, le favori à perdu !\n", "favori", ConsoleColor.DarkYellow);
                }
            }
            else
            {
                Utilitaires.WriteColor("L'armée Rebelle a gagné la bataille !\n", "Rebelle", ConsoleColor.DarkGreen);
                if (prediction == resultat)
                {
                    Utilitaires.WriteColor("Le favori est donc bel et bien sorti vainqueur !\n", "favori", ConsoleColor.DarkYellow);
                }
                else
                {
                    Utilitaires.WriteColor("Les prédictions se sont trompées, le favori à perdu !\n", "favori", ConsoleColor.DarkYellow);
                }
            }
        }

        public int CheckPartie()
        {
            bool empireEnVie = false;
            bool rebelleEnVie = false;

            for(int i = 0; i < this.empireSoldats.Count; i++)
            {
                if (this.empireSoldats[i].Vie > 0)
                {
                    empireEnVie = true;
                    break;
                }
            }

            for (int i = 0; i < this.rebelleSoldats.Count; i++)
            {
                if (this.rebelleSoldats[i].Vie > 0)
                {
                    rebelleEnVie = true;
                    break;
                }
            }

            if (empireEnVie && rebelleEnVie)
                return 0;
            if (empireEnVie)
                return 1;
            return 2;
        }

        public Soldat ChoisirSoldat(int camp)
        {
            int index;
            List<EmpireSoldat> empireSoldatsEnVie = new List<EmpireSoldat>();
            List<RebelleSoldat> rebelleSoldatsEnVie = new List<RebelleSoldat>();

            for (int i = 0; i < this.empireSoldats.Count; i++)
            {
                if (this.empireSoldats[i].Vie > 0)
                {
                    empireSoldatsEnVie.Add(this.empireSoldats[i]);
                }
            }

            for (int i = 0; i < this.rebelleSoldats.Count; i++)
            {
                if (this.rebelleSoldats[i].Vie > 0)
                {
                    rebelleSoldatsEnVie.Add(this.rebelleSoldats[i]);
                }
            }

            if (camp == 0)
            {
                int equipe = Utilitaires.RandomGen(1, 3);
                if(equipe == 1)
                {
                    index = Utilitaires.RandomGen(0, empireSoldatsEnVie.Count);
                    return empireSoldatsEnVie[index];
                }
                index = Utilitaires.RandomGen(0, rebelleSoldatsEnVie.Count);
                return rebelleSoldatsEnVie[index];
            }
            else if(camp == 1)
            {
                index = Utilitaires.RandomGen(0, empireSoldatsEnVie.Count);
                return empireSoldatsEnVie[index];
            }
            index = Utilitaires.RandomGen(0, rebelleSoldatsEnVie.Count);
            return rebelleSoldatsEnVie[index];
        }
    }
}
