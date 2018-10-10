using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTDClasses;

namespace MTDClasses
{
   
    /// <summary>
    /// Represents a hand of dominos //list=handofdominoes
    /// </summary>
    public class Hand:IEnumerable<Domino>
    {
        public delegate void EmptyHandler(Hand h);
        public event EmptyHandler AlmostEmpty;
        private List<Domino> handOfDominos=new List<Domino>();
        /// <summary>
        /// The list of dominos in the hand
        /// </summary>
        public void HandleEmpty(Hand h) { }
        /// <summary>
        /// Creates an empty hand
        /// </summary>
        public Hand()
        {
            List<Domino> handOfDominos = new List<Domino>();
        }

        /// <summary>
        /// Creates a hand of dominos from the boneyard.
        /// The number of dominos is based on the number of players
        /// 2–4 players: 10 dominoes each
        /// 5–6 players: 9 dominoes each
        /// 7–8 players: 7 dominoes each
        /// create a hand of dominoes from the boneyard based on number of players switch statement with loop?
        /// </summary>
        /// <param name="by"></param>
        /// <param name="numPlayers"></param>
        public Hand(BoneYard by, int numPlayers)
        {
            List<Domino> handOfDominos = new List<Domino>();
            
            if(numPlayers==2|numPlayers==3|numPlayers==4)
            {
                for (int i = 0; i <= 10; i++)
                {
                    handOfDominos.Add(by.Draw());
                }

            }
           else if(numPlayers==5|numPlayers==6)
            {
                for (int i = 0; 0 < 9; i++)
                {
                    handOfDominos.Add(by.Draw());
                }
            }
            else
            {
                for (int i = 0; 0 < 9; i++)
                {
                    handOfDominos.Add(by.Draw());
                }
            }
            AlmostEmpty = new EmptyHandler(HandleEmpty);
                
        }

        public void Add(Domino d)
        {
            handOfDominos.Add(d);
        }


        public int Count => handOfDominos.Count;
       
        /// <summary>
        /// when count==0 is true;
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (handOfDominos.Count == 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Sum of the score of each domino in the hand
        /// </summary>
        public int Score
        {
            
            get
            {

                int score = 0;
                for(int i=0;i<handOfDominos.Count;i++)
                {
                    score += handOfDominos[i].Score;
                }
                return score;
            }

        }

        /// <summary>
        /// Does the hand contain a domino with value in side 1 or side 2?
        /// </summary>
        /// <param name="value">The number of dots on one side of the domino that you're looking for</param>
        public bool HasDomino(int value)
        {
            bool flag = false;
            for(int i=0;i<handOfDominos.Count;i++)
            {
                Domino d = handOfDominos[i];
                if(d.Side1.Equals(value)||d.Side2.Equals(value))
                {

                    flag = true;
                    break;
                }
                else
                {
                    flag= false;
                }
            }
            return flag;


        }

        /// <summary>
        ///  DOes the hand contain a double of a certain value?
        /// </summary>
        /// <param name="value">The number of (double) dots that you're looking for</param>
        public bool HasDoubleDomino(int value)
        {
            bool flag = false;
            for (int i = 0; i < handOfDominos.Count; i++)
            {
                Domino d = handOfDominos[i];
                if (d.Side1.Equals(value) && d.Side2.Equals(value))
                {

                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// The index of a domino with a value in the hand
        /// </summary>
        /// <param name="value">The number of dots on one side of the domino that you're looking for</param>
        /// <returns>-1 if the domino doesn't exist in the hand</returns>
        public int IndexOfDomino(int value)
        {
            Domino d;
            int index=0;
            for(int i=0;i<handOfDominos.Count;i++)
            {
                d = handOfDominos[i];
                if (d.Side1.Equals(value) || d.Side2.Equals(value))
                    index = i;
                break;
            }
            return index;
        }
        /// <summary>
        /// index of domino that matches both side1 and side2. may not use this method depends how the rest 
        /// of the project is
        /// </summary>
        /// <param name="side1Value"></param>
        /// <param name="side2Value"></param>
        /// <returns></returns>
        public int IndexOfDomino(int side1Value, int side2Value)
        {
            Domino d;
            int index = 0;
            for (int i = 0; i < handOfDominos.Count; i++)
            {
                d = handOfDominos[i];
                if (d.Side1.Equals(side1Value) && d.Side2.Equals(side2Value))
                    index = i;
                break;
            }
            return index;
        }

        /// <summary>
        /// The index of the do
        /// </summary>
        /// <param name="value">The number of (double) dots that you're looking for</param>
        /// <returns>-1 if the domino doesn't exist in the hand</returns>
        public int IndexOfDoubleDomino(int value)
        {
            Domino d;
            int index = 0;
            for (int i = 0; i < handOfDominos.Count; i++)
            {
                d = handOfDominos[i];
                if (d.Side1.Equals(value) && d.Side2.Equals(value))
                    index = i;
                break;
            }
            return index;
        }

        /// <summary>
        /// The index of the highest double domino in the hand
        /// </summary>
        /// <returns>-1 if there isn't a double in the hand</returns>
        public int IndexOfHighDouble()
        {
            Domino d;
            int index = 0;
            for(int i=12;i<6;i--)
            {
                for(int j=0;i<handOfDominos.Count;j++)
                {
                    d = handOfDominos[j];
                    if (d.Side1.Equals(i) && d.Side2.Equals(i))
                        index = j;
                    break;
                }
            }
            return index;
        }

        public Domino this[int index]
        {
            get
            {
                return handOfDominos[index];
            }
            
        }

        public void RemoveAt(int index)
        {
            handOfDominos.RemoveAt(index);
        }

        /// <summary>
        /// Finds a domino with a certain number of dots in the hand.
        /// If it can find the domino, it removes it from the hand and returns it.
        /// Otherwise it returns null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Domino GetDomino(int value)
        {
            Domino d;
            int index=IndexOfDomino(value);
            d = handOfDominos[index];
            RemoveAt(index);
            return d;
            
        }

        /// <summary>
        /// Finds a domino with a certain number of double dots in the hand.
        /// If it can find the domino, it removes it from the hand and returns it.
        /// Otherwise it returns null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Domino GetDoubleDomino(int value)
        {
            Domino d;
            int index = IndexOfDoubleDomino(value);
            d = handOfDominos[index];
            RemoveAt(index);
            return d;
        }

        /// <summary>
        /// Draws a domino from the boneyard and adds it to the hand
        /// call draw method from boneyard
        /// </summary>
        /// <param name="by"></param>
        public void Draw(BoneYard by)
        {
            handOfDominos.Add(by.Draw());
        }

        /// <summary>
        /// Plays the domino at the index on the train.
        /// Flips the domino if necessary before playing.
        /// Removes the domino from the hand.
        /// Throws an exception if the domino at the index
        /// is not playable.
        /// use IsPLayable from train class.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="t"></param>
        private void Play(int index, Train t)
        {
            
            bool mustFlip,playable;
            Domino d = handOfDominos[index];
            playable=t.IsPlayable(this, d, out mustFlip);
            if (playable == false)
                throw new Exception("The domino is not playable.");
            if (mustFlip == true)
                d.Flip();
            this.RemoveAt(index);
            t.Add(d);
            
        }

        /// <summary>
        /// Plays the domino from the hand on the train.
        /// Flips the domino if necessary before playing.
        /// Removes the domino from the hand.
        /// Throws an exception if the domino is not in the hand
        /// or is not playable.
        /// </summary>
        public void Play(Domino d, Train t)
        {
            //recieve a domino and train, it plays the domino from the hand to the train at the last position. 
            //can pass mexican train or player train as it is a generic train(superclass)
            bool mustFlip, playable;
            Domino d1;
            int index=0;
            playable = t.IsPlayable(this, d, out mustFlip);
            if (playable == false)
                throw new Exception("The domino is not playable.");
            if (mustFlip == true)
                d.Flip();
            for(int i=0;i<this.Count;i++)
            {
                d1 = this[i];
                if(d.Equals(d1))
                {
                    index = i;
                    break;
                }
            }
            this.RemoveAt(index);
            t.Add(d);
        }

        /// <summary>
        /// Plays the first playable domino in the hand on the train
        /// Removes the domino from the hand.
        /// Returns the domino.
        /// Throws an exception if no dominos in the hand are playable.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Domino Play(Train t)
        {
            /*
             * int playablevalue=t.playablevalue;
             * int index= indexofdomino(playablevalue)
             * inf index !=1)
             * {
             * domino playable =this[index];
             * play(index t)
             * return playable
             * }
             */
            bool mustFlip, playable;
            Domino d1;
            for(int i=0;i<this.Count;i++)
            {
                Domino d = this[i];
                playable=t.IsPlayable(this, d, out mustFlip);
                if(playable==true)
                {
                    if (mustFlip == true)
                        d.Flip();
                    this.RemoveAt(i);
                    return d;
                }
                
            }
            throw new Exception("Cannot play");
        }

        public override string ToString()
        {
            string message = "";
            foreach (Domino d in handOfDominos)
            {
                message += d.ToString() + " /n";
            }
            return message;
        }

        public IEnumerator<Domino> GetEnumerator()
        {
            foreach(Domino item in handOfDominos)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
