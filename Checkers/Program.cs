using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            Joueur joueur1 = new Joueur(true);
            Joueur joueur2 = new Joueur(false);
            Plateau plateau = new Plateau(joueur1, joueur2);
            bool victoire = false;
            while (victoire == false)
            {
                joueur1.joue(plateau);
                /*victoire = true;
                for(int i = 0; i < 10; i++)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        if(plateau.tableau[i,j].Joueur == joueur2)
                        {
                            victoire = false;
                        }
                    }
                }
                if (victoire)
                    break;*/
                joueur2.joue(plateau);
                /*victoire = true;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (plateau.tableau[i, j].Joueur == joueur1)
                        {
                            victoire = false;
                        }
                    }
                }*/
            }
        }
    }
}
