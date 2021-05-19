using System;
using System.Collections.Generic;
using System.Text;

namespace BatailleSpacialeApp
{
    public class Soldat
    {
        public int Vie { get; set; }
        public int DegatsInf { get; set; }
        public string Matricule { get; set; }
        public bool Hero { get; set; }
        public string PrefixeMatricule { get; set; }

        public Soldat(int numeroSoldat)
        {
            this.Vie = Utilitaires.RandomGen(1000, 2001);
            this.DegatsInf = Utilitaires.RandomGen(100, 501);
            this.PrefixeMatricule = "Soldat";
            this.Matricule = this.PrefixeMatricule + "_" + numeroSoldat;
            this.Hero = false;
        }

        public virtual void Attaque(Soldat cible)
        {
            cible.Vie = cible.Vie - ( this.DegatsInf * (Utilitaires.RandomGen(0,100) / 100 ));
            if (cible.Vie <= 0 && cible.Hero == true)
            {
                Utilitaires.WriteColor("Un héro des Soldats vient de mourir !\n","Soldats", ConsoleColor.Gray);
            }
        }
        public override string ToString()
        {
            return this.Matricule + " Vie : " + this.Vie + " Dégats Infligés : " + this.DegatsInf;
        }
    }

    public class EmpireSoldat : Soldat
    {

        public EmpireSoldat(int numeroSoldat) : base( numeroSoldat)
        {
            this.PrefixeMatricule = "Empire";
            this.Matricule = this.PrefixeMatricule + "_" + numeroSoldat;
        }

        public override void Attaque(Soldat cible)
        {
            int degatAttaque = this.DegatsInf * (Utilitaires.RandomGen(0, 101) / 100);
            cible.Vie = cible.Vie - degatAttaque;

            Utilitaires.WriteColor(this.Matricule + "vient d'attaquer ", this.Matricule, ConsoleColor.DarkRed);
            Utilitaires.WriteColor(cible.Matricule + ". ", cible.Matricule, ConsoleColor.DarkGreen);

            Utilitaires.WriteColor("Traitor !\n", "Traitor", ConsoleColor.DarkRed);

            if (cible.Vie <= 0)
            {
                Utilitaires.WriteColor(cible.Matricule + " est mort !\n", cible.Matricule, ConsoleColor.DarkGreen);
                if (cible.Hero == true)
                    Utilitaires.WriteColor("Un héro Rebelle vient de mourir !\n", "Soldats", ConsoleColor.DarkGreen);
            }
            else
            {
                Console.WriteLine("Il lui reste " + cible.Vie + " points de vie.");
            }
        }


    }

    public class RebelleSoldat : Soldat
    {

        public RebelleSoldat(int numeroSoldat) : base(numeroSoldat)
        {
            this.PrefixeMatricule = "Rebelle";
            this.Matricule = this.PrefixeMatricule + "_" + numeroSoldat;
        }

        public override void Attaque(Soldat cible)
        {
            int degatAttaque = this.DegatsInf * (Utilitaires.RandomGen(0, 101) / 100);
            cible.Vie = cible.Vie - degatAttaque;

            Utilitaires.WriteColor(this.Matricule + "vient d'attaquer ", this.Matricule, ConsoleColor.DarkGreen);
            Utilitaires.WriteColor(cible.Matricule + ". ", cible.Matricule, ConsoleColor.DarkRed);

            //Sur le sujet il est noté princesses, je me permet de corriger
            Utilitaires.WriteColor("Pour la princesse Organa !\n", "Organa", ConsoleColor.DarkGreen);

            if (cible.Vie <= 0)
            {
                Utilitaires.WriteColor(cible.Matricule + " vient de mourir !\n", cible.Matricule, ConsoleColor.DarkRed);
                if (cible.Hero == true)
                    Utilitaires.WriteColor("Un héro de l'Empire vient de mourir !\n", "Soldats", ConsoleColor.DarkRed);
            }
            else
            {
                Console.WriteLine("Il lui reste " + cible.Vie + " points de vie.");
            }
        }



    }


}
