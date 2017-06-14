using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Joueur
    {
        public bool color;

        public Joueur(bool color)
        {
            this.color = color;
        }
        
        //joue le tour d'un joueur
        public void joue(Plateau plateau)
        {
            plateau.affiche();
            if (this.color)
                Console.WriteLine("C'est au tour de B de jouer");
            else
                Console.WriteLine("C'est au tour de W de jouer");
            int[] coorP = new int[2];
            coorP[0] = 0;
            coorP[1] = 0;
            bool coor = false;
            while (coor == false)
            {
                coorP[0] = Console.Read() - 48;
                Console.ReadLine();
                coorP[1] = Console.Read() - 48;
                Console.ReadLine();
                Console.WriteLine("i=" + coorP[0] + " j=" + coorP[1]);
                if (0 <= coorP[0] && coorP[0] <= 10 && 0 <= coorP[1] && coorP[1] <= 10)
                    if (plateau.tableau[coorP[0], coorP[1]] != null)
                        if (plateau.tableau[coorP[0], coorP[1]].Joueur == this)
                        {
                            if (plateau.tableau[coorP[0], coorP[1]] is Dame)
                            {
                                List<int[]> mouvements = new List<int[]>();
                                Dame pion1 = new Dame(this, coorP);
                                mouvements = plateau.moveDame(pion1);
                                pion1.Cases = mouvements;
                                if (pion1.Cases.Count != 0)
                                    coor = true;
                                else Console.WriteLine("no move for this pion");
                            }
                            else
                            {
                                Pion pion1 = new Pion(this, coorP);
                                plateau.movePion(pion1);
                                if (pion1.Cases.Count != 0)
                                    coor = true;
                                else Console.WriteLine("no move for this pion");
                            }
                        }
                        else Console.WriteLine("not YOUR pion");
                    else Console.WriteLine("not a pion");
                else Console.WriteLine("Not in the grille");
            }
            coor = false;
            Pion pion;

            if (plateau.tableau[coorP[0], coorP[1]] is Dame)
            {
                pion = new Dame(this, coorP);
                Dame pion2 = new Dame(this,coorP);
                plateau.moveDame(pion2);
                pion.Cases = pion2.Cases;
            }
            else
            {
                pion = new Pion(this, coorP);
                plateau.movePion(pion);
            }
            plateau.mouvposs(pion);
            if (this.color)
                Console.WriteLine("C'est au tour de B de jouer");
            else
                Console.WriteLine("C'est au tour de W de jouer");
            Console.WriteLine("Déplacements possibles:");
            foreach (int[] casee in pion.Cases)
            {
                Console.WriteLine("[" + casee[0] + "," + casee[1] + "]");
            }
            Console.WriteLine("choisissez votre déplacement");
            int[] coorJ = new int[2];
            while (coor == false)
            {
                coorJ[0] = Console.Read() - 48;
                Console.ReadLine();
                coorJ[1] = Console.Read() - 48;
                Console.ReadLine();
                foreach (int[] choixposs in pion.Cases)
                    if (coorJ[0] == choixposs[0] && coorJ[1] == choixposs[1])
                        coor = true;
                if (coor == false)
                    Console.WriteLine("you can't");
            }

            plateau.move(pion, coorJ,this);
            plateau.affiche();
        }
    }
}
