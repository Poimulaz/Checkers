using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Pion
    {
        protected Joueur joueur;
        protected int[] coor;
        protected List<int[]> cases;

        public Pion(Joueur pjoueur, int[] pcoor)
        {
            cases = new List<int[]>();
            this.joueur = pjoueur;
            this.coor = pcoor;
        }

        public Joueur Joueur
        {
            get
            {
                return joueur;
            }

            set
            {
                joueur = value;
            }
        }

        public List<int[]> Cases
        {
            get
            {
                return cases;
            }

            set
            {
                cases = value;
            }
        }

        //réinitialise la listes des cases atteignables(après un mouvement de la pièce)
        public void reset()
        {
            if (cases.Count != 0)
            {
                cases.Clear();
                cases = new List<int[]>();
            }
        }

         public int[] Coor
        {
            get
            {
                return coor;
            }

            set
            {
                coor = value;
            }
        }
    }
}
