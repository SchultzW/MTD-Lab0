using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    public class BoneYard
    {
        private List<Domino> boneYardList;

        public BoneYard(int maxDots)
        {
            //creates lists of dominos
            boneYardList = new List<Domino>();
            for (int i = 0; i < maxDots; i++)
            {
                for (int j = 0; j < maxDots; j++)
                {
                    Domino d = new Domino(i, j);
                    boneYardList.Add(d);
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>

        public void Shuffle()
        {
            //shuffles the boneyard up
            int count = boneYardList.Count;
            Domino temp;
            Random random = new Random();
            //int randomDom = random.Next(0, count);

            for(int i=0; i<200;i++)
            {
                int randomDom = random.Next(0, count);
                temp = boneYardList[randomDom];
                boneYardList.RemoveAt(randomDom);
                boneYardList.Add(temp);

            }

        }

        public bool IsEmpty()
        {
            //checks if boneyard is empty
            if (boneYardList.Count == 0)
                return true;
            else
                return false;
        }

        public int DominosRemaining()
        {
            //returns the # of bones left
                int count = 0;
                count = boneYardList.Count();
                return count;
        }
    

        public Domino Draw()
        {
            Domino d = boneYardList[0];
            boneYardList.RemoveAt(0);
            return d;

        }

        public Domino this[int index]
        {
            get
            {
                return boneYardList[index];

            }
            set
            {
                boneYardList[index] = value;
            }

        }

        public override string ToString()
        {
            string message = "";
            foreach(Domino d in boneYardList)
            {
                message += d.ToString() + " /n";
            }
            return message;
        }
    }
}

