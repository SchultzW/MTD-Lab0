using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    [Serializable()]
    public class Domino
    {
        private int side1;
        private int side2;

        public Domino()
        {
        }

        public Domino(int p1, int p2)
        {
            this.side1 = p1;
            this.side2 = p2;
        }

        // don't use an auto implemented property because of the validation in the setter - p 390
        public int Side1
        {
            get
            {
                return Side1;
            }
            set
            {
                if (value < 0 || value > 12)
                    throw new ArgumentException("The domino number is out of range.");
                side1 = value;
            }
            
        }


        public int Side2
        {
            get
            {
                return Side2;
            }
            set
            {
                if (value < 0 || value > 12)
                    throw new ArgumentException("The domino number is out of range.");
                side2 = value;
            }

        }
        //flips values of side1 and side2
        public void Flip()
        {
            int temp = side1;
            side1 = side2;
            side2 = temp;
        }

        /// This is how I would have done this in 233N
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
        public string Filename
        {
            get
            {
                return String.Format("d{0}{1}.png", side1, side2);
            }
        }

        

        public override string ToString()
        {
            return String.Format("Side 1: {0}  Side 2: {1}", side1, side2);
        }

        // could you overload the == and != operators?
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

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
