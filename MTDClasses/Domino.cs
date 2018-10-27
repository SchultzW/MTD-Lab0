using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    [Serializable()]
    public class Domino: IComparable
    {
        private int side1;
        private int side2;
        /// <summary>
        /// default constructor
        /// </summary>
        public Domino()
        {
        }
        /// <summary>
        /// constructor with 2 param
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public Domino(int p1, int p2)
        {
            this.Side1 = p1;
            this.Side2 = p2;
        }

        // don't use an auto implemented property because of the validation in the setter - p 390
        /// <summary>
        /// side1 setter and getter
        /// </summary>
        public int Side1
        {
            get
            {
                return side1;
            }
            set
            {
                if (value < 0 || value > 12)
                    throw new ArgumentException("The domino number is out of range.");
                side1 = value;
            }
            
        }

        /// <summary>
        /// side2 setter and getter
        /// </summary>
        public int Side2
        {
            get
            {
                return side2;
            }
            set
            {
                if (value < 0 || value > 12)
                    throw new ArgumentException("The domino number is out of range.");
                side2 = value;
            }

        }
        /// <summary>
        /// flips the values of the 2 sides
        /// </summary>
        public void Flip()
        {
            int temp = side1;
            side1 = side2;
            side2 = temp;
        }

        /// This is how I would have done this in 233N
        ///<summary>
        ///this adds the 2 sides and returns an int with the score
        ///</summary>

        public int Score
        {
            
            get
            {
                return side1 + side2;
            }
        }

        // because it's a read only property, I can use the "expression bodied syntax" or a lamdba expression - p 393
        //public int Score => side1 + side2;

        //ditto for the first version of this method and the next one
        /// <summary>
        /// checks if the domino is double sided or not
        /// </summary>
        /// <returns></returns>
        public bool IsDouble()
        {
            if (side1 == side2)
            {
                return true;
            }
            else
                return false;
        }

        // could you do this one using a lambda expression? //public bool IsDouble() => (side1 == side2) ? true : false;
        /// <summary>
        /// this has to do with creating a filename for the domino to match with a picture.
        /// </summary>
        public string Filename
        {
            get
            {
                return String.Format("d{0}{1}.png", side1, side2);
            }
        }

        
        /// <summary>
        /// returns the number on each side of a domino
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("Side 1: {0}  Side 2: {1}", side1, side2);
        }

        // could you overload the == and != operators?
        /// <summary>
        /// overloads the equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Domino d = (Domino)obj;
            if (this.side1 == d.side1 && this.side2 == d.side2)
                return true;
            else
                return false;
        }
        /// <summary>
        /// overrides hashcode for overriding equals
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public int CompareTo(object obj)
        {
            Domino d = (Domino) obj;
            return this.Score.CompareTo(d.Score);
        }
    }
}
