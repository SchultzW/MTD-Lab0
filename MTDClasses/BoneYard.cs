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
        /// <summary>
        /// creates a new boardyard list and populates it.
        /// </summary>
        /// <param name="maxDots"></param>
        public BoneYard(int maxDots)
        {
            if (maxDots < 6 || maxDots > 12)
                throw new ArgumentException("Invalid Max Dots");
            else
            {
                Domino blank = new Domino(0, 0);
                boneYardList = new List<Domino>();
                boneYardList.Add(blank);
                for (int i = 0; i <= maxDots; i++)
                {
                    for (int j = 0; j <= maxDots; j++)
                    {
                        bool flag = false;
                        Domino d = new Domino(i, j);
                        Domino dFlip = new Domino(j, i);
                        foreach(Domino d2Check in boneYardList)
                        {

                            if (d.Equals(d2Check) || dFlip.Equals(d2Check))
                            {
                                flag = true;
                                break;
                            }
                            else
                                flag = false;
                           
                        }
                        if(flag==false)
                        {
                            boneYardList.Add(d);
                        }
                    }
                }
                
            }
        }
        /// <summary>
        /// shuffles the dominos in the boneyard list.
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
        /// <summary>
        /// tests if the boneyard is empty and returns true or false.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            //checks if boneyard is empty
            if (boneYardList.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// checks how many bones are in the bone yard and returns an int
        /// </summary>
        /// <returns></returns>
        public int DominosRemaining()
        {
            //returns the # of bones left
                int count = 0;
                count = boneYardList.Count();
                return count;
        }
    
        /// <summary>
        /// draws a bone from the bottom of the boneyard
        /// </summary>
        /// <returns></returns>
        public Domino Draw()
        {
            Domino d = boneYardList[0];
            boneYardList.RemoveAt(0);
            return d;

        }
        /// <summary>
        /// creates indexer for boneyard class
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
        /// <summary>
        /// returns a string of all the bones in the boneyard list.
        /// </summary>
        /// <returns></returns>
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

