using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardanGrilleEncoding
{
    /// <summary>
    /// Enum, that represents the available dimensions for Cardan Grille encoding
    /// </summary>
    public enum GrilleDimensions
    {
        Four = 4,
        Five,
        Six
    }

    /// <summary>
    /// This static class contains the auxiliary methods for operating with Cardan Grille
    /// </summary>
    public static class CardanGrille
    {
        #region Properties
        /// <summary>
        /// Returns a concrete matrix 4*4, in which each element is at unique position while rotating it right
        /// </summary>
        private static int[,] Base4
        {
            get
            {
                return new int[4, 4] {
                    { 1, 2, 3, 1 },
                    { 3, 4, 4, 2 },
                    { 2, 4, 4, 3 },
                    { 1, 3, 2, 1 } };
            }
        }

        /// <summary>
        /// Returns a concrete matrix 5*5, in which each element is at unique position while rotating it right
        /// </summary>
        private static int[,] Base5
        {
            get
            {
                return new int[5, 5] {
                    { 1, 2, 3, 4, 1 },
                    { 4, 5, 6, 5, 2 },
                    { 3, 6, 7, 6, 3 },
                    { 2, 5, 6, 5, 4 },
                    { 1, 4, 3, 2, 1 } };
            }
        }

        /// <summary>
        /// Returns a concrete matrix 6*6, in which each element is at unique position while rotating it right
        /// </summary>
        private static int[,] Base6
        {
            get
            {
                return new int[6, 6]
                {
                    {1,2,3,4,5,1 },
                    {5,6,7,8,6,2 },
                    {4,8,9,9,7,3 },
                    {3,7,9,9,8,4 },
                    {2,6,8,7,6,5 },
                    {1,5,4,3,2,1 }
                };
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Calls the corresponding Base property, depending on the argument
        /// </summary>
        /// <param name="b">Element of GrilleDimensions enum</param>
        /// <returns>An int[,] array</returns>
        public static int[,] GetGrille(GrilleDimensions b)
        {
            switch (b)
            {
                case GrilleDimensions.Four:
                    return Base4;
                case GrilleDimensions.Five:
                    return Base5;
                case GrilleDimensions.Six:
                    return Base6;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns the const, which determines the amount of encoded symbols during the iteration, depending on the dimension of the grille
        /// </summary>
        /// <param name="b">Element of GrilleDimensions enum</param>
        /// <returns>An int value from 4 to 9</returns>
        public static int Const(GrilleDimensions b)
        {
            switch (b)
            {
                case GrilleDimensions.Four:
                    return 4;
                case GrilleDimensions.Five:
                    return 7;
                case GrilleDimensions.Six:
                    return 9;
                default:
                    return 0;
            }                    
        }

        /// <summary>
        /// Returns the int array[Const,2], where each row is a set of coordinates for corresponding element in Cardan grille
        /// </summary>
        /// <param name="b">Element of GrilleDimensions enum</param>
        /// <returns>An int[Const,2] array</returns>
        public static int[,] GetRotatingIndexes(GrilleDimensions b)
        {
            int max = Const(b);
            int[,] indexes = new int[max,2];
            int[,] matrix = GetGrille(b);

            for (int i = 0; i < max; i++)
            {
                int x, y;                
                while (true)
                {
                    Random r = new Random();
                    x = r.Next(0, (int)b -1);
                    y = r.Next(0, (int)b - 1);
                    if (matrix[x,y] == i + 1)
                    {
                        indexes[i, 0] = x;
                        indexes[i, 1] = y;
                        break;
                    }
                }              
            }

            return indexes;
        }

        /// <summary>
        /// Returns the new matrix, which is created by rotating the original one 90 degrees right
        /// </summary>
        /// <param name="matrix">Char[,] marix to rotate</param>
        /// <returns>A rotated char[,] array</returns>
        public static char[,] RotateRight(this char[,] matrix)
        {
            int max = matrix.GetLength(0);
            char[,] arr = new char[max,max];

            for(int i = 0; i<max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    arr[j, max - i - 1] = matrix[i, j];
                }
            }
            return arr;
        }

        /// <summary>
        /// Creates a Key for an appropriate grille
        /// </summary>
        /// <param name="b">Element of GrilleDimensions enum</param>
        /// <returns>An object of Key type</returns>
        public static Key CreateKey(GrilleDimensions b)
        {
            return new Key(b);
        }
        #endregion
    }
}
