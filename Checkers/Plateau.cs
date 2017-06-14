using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Plateau
    {
        public Pion[,] tableau;
        public Joueur joueur1;
        public Joueur joueur2;
        static bool diagodame;

        //Construit un plateau et place les pièces de chaque joueur dessus
        public Plateau(Joueur pjoueur1, Joueur pjoueur2)
        {
            diagodame = false;
            tableau = new Pion[10,10];
            this.joueur1 = pjoueur1;
            this.joueur2 = pjoueur2;
            int[] coor = new int[2];
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    tableau[i,j] = null;
                }
                for (int j = 0; j < 4; j++)
                {
                    if ((i + j) % 2 != 1)
                    {
                        coor[0] = i;
                        coor[1] = j;
                        tableau[i,j] = new Pion(joueur1,coor);
                    }
                }
                for (int j = 9; j > 5; j--)
                {
                    if ((i + j) % 2 != 1)
                    {
                        coor[0] = i;
                        coor[1] = j;
                        tableau[i,j] = new Pion(joueur2,coor);
                    }
                }
            }
        }
      
        //trouve les déplacements possibles de la dame et rempli sa liste de cases pouvant être atteintes  
        public List<int[]> moveDame(Dame pion)
        {
            diagodame = true;
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < 10; i++)
            {
                List<int[]> test = new List<int[]>();
                test = movePion(pion,i);
                foreach (int[] casee in test)
                {
                    list.Add(casee);
                }
            }
            diagodame = false;
            pion.Cases = list;
            tableau[pion.Coor[0], pion.Coor[1]].Cases = pion.Cases;
            return pion.Cases;
        }

        //trouve les déplacements possibles du pion et rempli sa liste de cases pouvant être atteintes
        public List<int[]> movePion(Pion pion,int i=1)
        {
            int x=0;
            int y=0;
            int[] test = new int[2];
            bool joueur = false;
            List<int[]> list = new List<int[]>();
            for (int compteur = 0; compteur < 4; compteur++)
            {
                switch (compteur)
                {
                    case 0:
                        x = i;
                        y = i;
                        joueur = joueur1.color;
                        break;
                    case 1:
                        x = -i;
                        y = i;
                        joueur = joueur1.color;
                        break;
                    case 2:
                        x = -i;
                        y = -i;
                        joueur = joueur2.color;
                        break;
                    case 3:
                        x = i;
                        y = -i;
                        joueur = joueur2.color;
                        break;
                }
                test = verif_case(pion.Coor, x, y);
                if (test != null)
                {
                    if (tableau[pion.Coor[0], pion.Coor[1]] is Dame || tableau[pion.Coor[0], pion.Coor[1]].Joueur.color == joueur)
                    {

                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee[0] == test[0] && casee[1] == test[1])
                                valid = false;
                        }
                        if (valid)
                            list.Add(test);
                    }
                }
            }
            pion.Cases = list;
            list = mangeable(pion);
            return list;
        }

        //verifie si la case donnée est vide et renvoie alors cette case
        public int[] verif_case(int[] caseA,int x,int y)
        {
            if (0 <= caseA[0] + x && caseA[0] + x <= 9 && 0 <= caseA[1] + y && caseA[1] + y <= 9)
                if (tableau[caseA[0] + x, caseA[1] + y] == null)
                {
                    int[] caseAdj = new int[2];
                    caseAdj[0] = caseA[0] + x;
                    caseAdj[1] = caseA[1] + y;
                    return caseAdj;
                }
            return null;
        }

        //vérifie les cases mangeables par le pion et rempli sa liste de cases pouvant être atteintes
        public List<int[]> mangeable(Pion pion)
        {
            List<int[]> list = new List<int[]>();
            list = pion.Cases;
            int[] caseA = pion.Coor;
            if (2 <= pion.Coor[0] && pion.Coor[0] <= 7 && 2 <= pion.Coor[1] && pion.Coor[1] <= 7)
            {
                if (tableau[caseA[0] - 1, caseA[1] - 1] != null && pion.Joueur.color != tableau[caseA[0] - 1, caseA[1] - 1].Joueur.color && tableau[caseA[0] - 2, caseA[1] - 2] == null)
                {
                    int[] caseAdj = new int[2];
                    caseAdj[0] = caseA[0] - 2;
                    caseAdj[1] = caseA[1] - 2;
                    bool valid = true;
                    foreach(int[] casee in list)
                    {
                        if (casee == caseAdj)
                            valid= false;
                    }
                    if(valid)
                        list.Add(caseAdj);
                }
                if (tableau[caseA[0] + 1, caseA[1] - 1] != null && pion.Joueur.color != tableau[caseA[0] + 1, caseA[1] - 1].Joueur.color && tableau[caseA[0] + 2, caseA[1] - 2] == null)
                {
                    int[] caseAdj = new int[2];
                    caseAdj[0] = caseA[0] + 2;
                    caseAdj[1] = caseA[1] - 2;
                    list.Add(caseAdj);
                    bool valid = true;
                    foreach (int[] casee in list)
                    {
                        if (casee == caseAdj)
                            valid = false;
                    }
                    if (valid)
                        list.Add(caseAdj);
                }
                if (tableau[caseA[0] + 1, caseA[1] + 1] != null && pion.Joueur.color != tableau[caseA[0] + 1, caseA[1] + 1].Joueur.color && tableau[caseA[0] + 2, caseA[1] + 2] == null)
                {
                    int[] caseAdj = new int[2];
                    caseAdj[0] = caseA[0] + 2;
                    caseAdj[1] = caseA[1] + 2;
                    list.Add(caseAdj);
                    bool valid = true;
                    foreach (int[] casee in list)
                    {
                        if (casee == caseAdj)
                            valid = false;
                    }
                    if (valid)
                        list.Add(caseAdj);
                }
                if (tableau[caseA[0] - 1, caseA[1] + 1] != null && pion.Joueur.color != tableau[caseA[0] - 1, caseA[1] + 1].Joueur.color && tableau[caseA[0] - 2, caseA[1] + 2] == null)
                {
                    int[] caseAdj = new int[2];
                    caseAdj[0] = caseA[0] - 2;
                    caseAdj[1] = caseA[1] + 2;
                    list.Add(caseAdj);
                    bool valid = true;
                    foreach (int[] casee in list)
                    {
                        if (casee == caseAdj)
                            valid = false;
                    }
                    if (valid)
                        list.Add(caseAdj);
                }
            }
            else if (pion.Coor[0] == 0 || pion.Coor[0] == 1)
            {
                if (pion.Coor[1] == 0 || pion.Coor[1] == 1)
                {
                    if (tableau[caseA[0] + 1, caseA[1] + 1] != null && pion.Joueur.color != tableau[caseA[0] + 1, caseA[1] + 1].Joueur.color && tableau[caseA[0] + 2, caseA[1] + 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] + 2;
                        caseAdj[1] = caseA[1] + 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
                else if (pion.Coor[1] == 9 || pion.Coor[1] == 8)
                {
                    if (tableau[caseA[0] + 1, caseA[1] - 1] != null && pion.Joueur.color != tableau[caseA[0] + 1, caseA[1] - 1].Joueur.color && tableau[caseA[0] + 2, caseA[1] - 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] + 2;
                        caseAdj[1] = caseA[1] - 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
                else
                {
                    if (tableau[caseA[0] + 1, caseA[1] - 1] != null && pion.Joueur.color != tableau[caseA[0] + 1, caseA[1] - 1].Joueur.color && tableau[caseA[0] + 2, caseA[1] - 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] + 2;
                        caseAdj[1] = caseA[1] - 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                    if (tableau[caseA[0] + 1, caseA[1] + 1] != null && pion.Joueur.color != tableau[caseA[0] + 1, caseA[1] + 1].Joueur.color && tableau[caseA[0] + 2, caseA[1] + 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] + 2;
                        caseAdj[1] = caseA[1] + 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
            }
            else if (pion.Coor[0] == 9 || pion.Coor[0] == 8)
            {
                if (pion.Coor[1] == 0 || pion.Coor[1] == 1)
                {
                    if (tableau[caseA[0] - 1, caseA[1] + 1] != null && pion.Joueur.color != tableau[caseA[0] - 1, caseA[1] + 1].Joueur.color && tableau[caseA[0] - 2, caseA[1] + 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] - 2;
                        caseAdj[1] = caseA[1] + 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
                else if (pion.Coor[1] == 9 || pion.Coor[1] == 8)
                {
                    if (tableau[caseA[0] - 1, caseA[1] - 1] != null && pion.Joueur.color != tableau[caseA[0] - 1, caseA[1] - 1].Joueur.color && tableau[caseA[0] - 2, caseA[1] - 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] - 2;
                        caseAdj[1] = caseA[1] - 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
                else
                {
                    if (tableau[caseA[0] - 1, caseA[1] - 1] != null && pion.Joueur.color != tableau[caseA[0] - 1, caseA[1] - 1].Joueur.color && tableau[caseA[0] - 2, caseA[1] - 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] - 2;
                        caseAdj[1] = caseA[1] - 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                    if (tableau[caseA[0] - 1, caseA[1] + 1] != null && pion.Joueur.color != tableau[caseA[0] - 1, caseA[1] + 1].Joueur.color && tableau[caseA[0] - 2, caseA[1] + 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] - 2;
                        caseAdj[1] = caseA[1] + 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
            }
            else if (pion.Coor[1] == 0 || pion.Coor[1] == 1)
            {
                if (pion.Coor[0] == 0 || pion.Coor[0] == 1)
                {
                    if (tableau[caseA[0] + 1, caseA[1] + 1] != null && pion.Joueur.color != tableau[caseA[0] + 1, caseA[1] + 1].Joueur.color && tableau[caseA[0] + 2, caseA[1] + 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] + 2;
                        caseAdj[1] = caseA[1] + 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
                else if (pion.Coor[0] == 9 || pion.Coor[0] == 8)
                {
                    if (tableau[caseA[0] - 1, caseA[1] + 1] != null && pion.Joueur.color != tableau[caseA[0] - 1, caseA[1] + 1].Joueur.color && tableau[caseA[0] - 2, caseA[1] + 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] - 2;
                        caseAdj[1] = caseA[1] + 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
                else
                {
                    if (tableau[caseA[0] + 1, caseA[1] + 1] != null && pion.Joueur.color != tableau[caseA[0] + 1, caseA[1] + 1].Joueur.color && tableau[caseA[0] + 2, caseA[1] + 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] + 2;
                        caseAdj[1] = caseA[1] + 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                    if (tableau[caseA[0] - 1, caseA[1] + 1] != null && pion.Joueur.color != tableau[caseA[0] - 1, caseA[1] + 1].Joueur.color && tableau[caseA[0] - 2, caseA[1] + 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] - 2;
                        caseAdj[1] = caseA[1] + 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
            }
            else if (pion.Coor[1] == 9 || pion.Coor[1] == 8)
            {
                if (pion.Coor[0] == 0 || pion.Coor[0] == 1)
                {
                    if (tableau[caseA[0] + 1, caseA[1] - 1] != null && pion.Joueur.color != tableau[caseA[0] + 1, caseA[1] - 1].Joueur.color && tableau[caseA[0] + 2, caseA[1] - 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] + 2;
                        caseAdj[1] = caseA[1] - 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
                else if (pion.Coor[0] == 9 || pion.Coor[0] == 8)
                {
                    if (tableau[caseA[0] - 1, caseA[1] - 1] != null && pion.Joueur.color != tableau[caseA[0] - 1, caseA[1] - 1].Joueur.color && tableau[caseA[0] - 2, caseA[1] - 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] - 2;
                        caseAdj[1] = caseA[1] - 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
                else
                {
                    if (tableau[caseA[0] - 1, caseA[1] - 1] != null && pion.Joueur.color != tableau[caseA[0] - 1, caseA[1] - 1].Joueur.color && tableau[caseA[0] - 2, caseA[1] - 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] - 2;
                        caseAdj[1] = caseA[1] - 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                    if (tableau[caseA[0] + 1, caseA[1] - 1] != null && pion.Joueur.color != tableau[caseA[0] + 1, caseA[1] - 1].Joueur.color && tableau[caseA[0] + 2, caseA[1] - 2] == null)
                    {
                        int[] caseAdj = new int[2];
                        caseAdj[0] = caseA[0] + 2;
                        caseAdj[1] = caseA[1] - 2;
                        list.Add(caseAdj);
                        bool valid = true;
                        foreach (int[] casee in list)
                        {
                            if (casee == caseAdj)
                                valid = false;
                        }
                        if (valid)
                            list.Add(caseAdj);
                    }
                }
            }
            return list;
        }

        //bouge le pion par rapport a la case choisie par le joueur et vérifie si il passe par un ennemis en même temps
        public void move(Pion pion,int[] caseJ,Joueur joueur)
        {
            tableau[pion.Coor[0], pion.Coor[1]].reset();
            Console.WriteLine("casej=" + caseJ[0] + "   " + caseJ[1]);
            if (!(tableau[pion.Coor[0], pion.Coor[1]] is Dame))
            {
                if (Math.Abs(pion.Coor[0] - caseJ[0]) == 2 || Math.Abs(pion.Coor[1] - caseJ[1]) == 2)
                {
                    Console.WriteLine("je mange");
                    int[] coor = new int[2];
                    coor[0] = caseJ[0] + ((pion.Coor[0] - caseJ[0]) / 2);
                    coor[1] = caseJ[1] + ((pion.Coor[1] - caseJ[1]) / 2);
                    tableau[pion.Coor[0], pion.Coor[1]] = null;
                    tableau[caseJ[0], caseJ[1]] = new Pion(joueur, caseJ);
                    pion.Coor = caseJ;
                    mange(pion, coor, joueur);
                }
                else
                {
                    tableau[caseJ[0], caseJ[1]] = new Pion(joueur, caseJ);
                    tableau[pion.Coor[0], pion.Coor[1]] = null;
                    meta(tableau[caseJ[0], caseJ[1]]);
                }
            }
            else
            {
                for (int x = 1; x < Math.Abs(pion.Coor[0] - caseJ[0]); x++)
                {
                    int abscisse = pion.Coor[0] - ((pion.Coor[0] - caseJ[0]) / Math.Abs(pion.Coor[0] - caseJ[0]) * x);
                    int ordonne = pion.Coor[1] - ((pion.Coor[1] - caseJ[1]) / Math.Abs(pion.Coor[1] - caseJ[1]) * x);
                    if (tableau[abscisse,ordonne] != null)
                    {
                        Console.WriteLine("je mange");
                        int[] coor = new int[2];
                        coor[0] = abscisse;
                        coor[1] = ordonne;
                        tableau[pion.Coor[0], pion.Coor[1]] = null;
                        tableau[caseJ[0], caseJ[1]] = new Dame(joueur, caseJ);
                        pion.Coor = caseJ;
                        mange(pion, coor, joueur);
                    }
                    else
                    {
                        tableau[caseJ[0], caseJ[1]] = new Dame(joueur, caseJ);
                        tableau[pion.Coor[0], pion.Coor[1]] = null;
                        break;
                    }
                }
            }
        }

        //mange le pion ennemi en le supprimant et permet au pion de remanger si des ennemis sont mangeables
        public void mange(Pion pion,int[] coor,Joueur joueur)
        {
            pion.reset();
            Console.WriteLine("coor:" + coor[0] + coor[1]);
            tableau[coor[0], coor[1]] = null;
            meta(pion);
            List<int[]> list = new List<int[]>();
            list = mangeable(pion);
            pion.Cases = list;
            mouvposs(pion);
            bool coord = false;
            int[] coorJ = new int[2];
            if (pion.Cases.Count != 0)
            {
                Console.WriteLine("Déplacements possibles:");
                foreach (int[] casee in pion.Cases)
                {
                    Console.WriteLine("[" + casee[0] + "," + casee[1] + "]");
                }
                Console.WriteLine("choisissez votre déplacement");
                while (coord == false)
                {
                    coorJ[0] = Console.Read() - 48;
                    Console.ReadLine();
                    coorJ[1] = Console.Read() - 48;
                    Console.ReadLine();
                    foreach (int[] choixposs in pion.Cases)
                        if (coorJ[0] == choixposs[0] && coorJ[1] == choixposs[1])
                            coord = true;
                    if (coord == false)
                        Console.WriteLine("you can't");
                }
                move(pion, coorJ, joueur);
            }

        }

        //vérifie si un pion peut se trasformer en dame et dans ce cas le transforme en dame
        public void meta(Pion pion)
        {
            int[] coor = new int[2];
            if (pion.Joueur.color == joueur2.color)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (pion.Coor[0] == i && pion.Coor[1] == 0)
                    {
                        coor[0] = i;
                        coor[1] = 0;
                        tableau[i, 0] = null;
                        tableau[i,0] = new Dame(joueur2,coor);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if (pion.Coor[0] == i && pion.Coor[1] == 9)
                    {
                        coor[0] = i;
                        coor[1] = 9;
                        tableau[i, 9] = null;
                        tableau[i,9] = new Dame(joueur1,coor);
                    }
                }
            }
        }

        //affiche la grille avec les pions
        public void affiche()
        {
            Console.Clear();
            Console.Write("     ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("  " + i + "  ");
            }
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("  " + i + "  ");
                for (int j = 0; j < 10; j++)
                {
                    if (tableau[i, j] == null)
                    {
                        Console.Write("     ");
                    }
                    else if (tableau[i, j].Joueur == joueur1)
                    {
                        if (tableau[i, j] is Dame)
                            Console.Write(" |B| ");
                        else
                            Console.Write("  B  ");
                    }
                    else if (tableau[i, j].Joueur == joueur2)
                    {
                        if (tableau[i, j] is Dame)
                            Console.Write(" |W| ");
                        else
                            Console.Write("  W  ");
                    }
                }
                Console.WriteLine();
            }
        }

        //affiche la grille avec les pions et les mouvements du pion choisi
        public void mouvposs(Pion pion)
        {
            Console.Clear();
            Console.Write("     ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("  " + i + "  ");
            }
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("  " + i + "  ");
                for (int j = 0; j < 10; j++)
                {   
                    if (tableau[i, j] == null)
                    {
                        bool exist = false;
                        foreach (int[] item in pion.Cases)
                        {
                            if (item[0] == i && item[1] == j)
                            {
                                Console.Write(" O O ");
                                exist = true;
                            }
                        }
                        if (!exist)
                        {
                            Console.Write("     ");
                        }
                    }
                    else if (tableau[i, j].Joueur == joueur1)
                    {
                        if(tableau[i, j] is Dame)
                            Console.Write(" |B| ");
                        else
                            Console.Write("  B  ");
                    }
                    else if (tableau[i, j].Joueur == joueur2)
                    {
                        if (tableau[i, j] is Dame)
                            Console.Write(" |W| ");
                        else
                            Console.Write("  W  ");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
