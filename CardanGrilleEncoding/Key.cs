using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardanGrilleEncoding
{
    /// <summary>
    /// Class, that provides all the required information for decoding the message.
    /// </summary>
    public class Key
    {
        #region Fields
        private GrilleDimensions _base;
        private int[,] _rotateIndexes;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a key instance based on the dimension of Cardan grille
        /// </summary>
        /// <param name="b"></param>
        public Key(GrilleDimensions b)
        {
            _base = b;
            _rotateIndexes = CardanGrille.GetRotatingIndexes(b);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns the int array[Const,2], where each row is a set of coordinates for corresponding element in Cardan grille
        /// </summary>
        public int[,] Indexes
        {
            get
            {
                return _rotateIndexes;
            }
        }

        /// <summary>
        /// Returns the element of enum for Cardan Grille dimensions
        /// </summary>
        public GrilleDimensions Base
        {
            get
            {
                return _base;
            }
        }
        #endregion
    }

}
