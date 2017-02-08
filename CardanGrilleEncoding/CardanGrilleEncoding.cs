using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardanGrilleEncoding
{
    /// <summary>
    /// A static class that provides methods for encoding and decoding messages with CardanGrille method
    /// </summary>
    public static class CardanGrilleEncoding
    {
        #region Field
        private static char _placeholder = '#';
        #endregion

        #region Property
        /// <summary>
        /// Provides an ability to set the currently used placeholder
        /// </summary>
        public static char Placeholder
        {
            private get
            {
                return _placeholder;
            }
            set
            {
                _placeholder = value;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Encodes the provided text using Cardan Grille algorithm with the mentioned Grille and out key
        /// </summary>
        /// <param name="text">Message to encode</param>
        /// <param name="b">An element of GrilleDimensions array</param>
        /// <param name="k">An out Key parameter</param>
        /// <returns>String value of encoded text</returns>
        public static string Encode(string text, GrilleDimensions b, out Key k)
        {
            if (String.IsNullOrEmpty(text))
                throw new ArgumentOutOfRangeException("text");

            text = text.Trim(' ');
            while (text.Length % (int)b != 0)
                String.Concat(text, Placeholder);
            k = new Key(b);
            return Encoding(text, k);
        }

        /// <summary>
        /// Encodes the provided text using Cardan Grille algorithm with the 4*4 Grille and your out key parameter
        /// </summary>
        /// <param name="text">Message to encode</param>
        /// <param name="k">An out Key parameter</param>
        /// <returns>String value of encoded text</returns>
        public static string Encode (string text, out Key k)
        {
            return Encode(text, GrilleDimensions.Four, out k);
        }

        /// <summary>
        /// Encodes the provided text using Cardan Grille algorithm with your Key properties
        /// </summary>
        /// <param name="text">Message to encode</param>
        /// <param name="k">A created Key parameter</param>
        /// <returns>String value of encoded text</returns>
        public static string Encode(string text, Key k)
        {
            if (String.IsNullOrEmpty(text))
                throw new ArgumentOutOfRangeException("text");

            text = text.Trim(' ');
            while (text.Length % Math.Pow((int)k.Base,2)  != 0)
                text = String.Concat(text, Placeholder);
            return Encoding(text, k);
        }

        /// <summary>
        /// Decodes the provided text using Cardan Grille algorithm and your Key, received after Encoding
        /// </summary>
        /// <param name="text">String text to decode</param>
        /// <param name="k">Key</param>
        /// <returns>String value of decoded text</returns>
        public static string Decode(string text, Key k)
        {
            if (String.IsNullOrEmpty(text))
                throw new ArgumentOutOfRangeException("text");

            if (text.Length % Math.Pow((int)k.Base, 2) != 0)
                throw new ArgumentException("text");

            return Decoding(text, k).Trim(Placeholder);
            
        }
        #endregion

        #region Private Methods
        private static string Encoding(string text, Key k)
        {
            StringBuilder result = new StringBuilder();

            while (!String.IsNullOrEmpty(text))
            {
                char[,] target = new char[(int)k.Base, (int)k.Base];
                for (int n = 0; n < 4; n++)
                {           
                    for (int i = 0; i < CardanGrille.Const(k.Base); i++)
                    {
                        target[k.Indexes[i, 0], k.Indexes[i, 1]] = text.First();
                        text = text.Remove(0, 1);
                    }
                    target = target.RotateRight();
                }

                foreach (var item in target)
                    result.Append(item);
            }

            return result.ToString();                     
        }

        private static string Decoding(string text, Key k)
        {
            char[,] original = new char[(int)k.Base, (int)k.Base];
            StringBuilder result = new StringBuilder();

            while (!String.IsNullOrEmpty(text))
            {
                original = new char[(int)k.Base, (int)k.Base];

                for (int i = 0; i < (int)k.Base; i++)
                {
                    for (int j = 0; j < (int)k.Base; j++)
                    {
                        original[i, j] = text.First();
                        text = text.Remove(0, 1);
                    }
                }

                for (int n = 0; n < 4; n++)
                {
                    for (int i = 0; i < CardanGrille.Const(k.Base); i++)
                    {
                        result.Append(original[k.Indexes[i, 0], k.Indexes[i, 1]]);
                    }
                    original = original.RotateRight();
                }
            }              

            return result.ToString();
        }
        #endregion
    }
}
