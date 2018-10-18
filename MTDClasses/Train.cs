using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTDClasses
{
    /// <summary>
    /// Represents a generic Train for MTD
    /// </summary>
    public abstract class Train
    {

        private List<Domino> dominos;
        private int engineValue;

      
        public Train()
        {
            //set engineine value to 12 and create list
            this.EngineValue = 12;
            dominos = new List<Domino>();

        }

        public Train(int engValue)
        {
            //set engine value to int engValue and create empty list
            this.EngineValue = engValue;
            dominos = new List<Domino>();

        }

        //public int Count => dominos.Count();
        public int Count
        {
            get
            {
                return dominos.Count;
            }


        }

        /// <summary>
        /// The first domino value that must be played on a train
        /// </summary>
        public int EngineValue
        {
            get
            {
                return engineValue;
            }
            set
            {
                //add validation
                engineValue = value;
                
            }
        }

        /// <summary>
        /// returns true if the count is empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (dominos.Count.Equals(0))
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// looks at the list of dominos and returns the last domino that was played.
        /// </summary>
        //public Domino LastDomino => dominos[Count - 1];
        public Domino LastDomino
        {
            get
            {
                try
                {
                    return dominos[Count - 1];
                }
                catch
                {
                    int val= engineValue;
                    Domino d = new Domino(val, val);
                    return d;
                }
            }

        }

        /// <summary>
        /// Side2 of the last domino in the train.  It's the value of the next domino that can be played.
        /// which side of the domino is playedable? (2|4)(4|5) 5 is the playable value. 
        /// </summary>
        public int PlayableValue => LastDomino.Side2;
        //{
        //    get
        //    {
        //        LastDomino.Side2
        //    }
        //}

        public void Add(Domino d)
        {
            //adds a bone to the trian(dominoes list)
            dominos.Add(d);
        }

        /// <summary>
        /// returns a domino at some index of the list
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Domino this[int index]
        {
            get
            {
                return dominos[index];
            }
        }

        /// <summary>
        /// Determines whether a hand can play a specific domino on this train and if the domino must be flipped.
        /// Because the rules for playing are different for Mexican and Player trains, this method is abstract.
        /// </summary>
        public abstract bool IsPlayable(Hand h, Domino d, out bool mustFlip);

        //needs hand class and works like below

        /// <summary>
        /// A helper method that determines whether a specific domino can be played on this train.
        /// It can be called in the Mexican and Player train class implementations of the abstract method
        /// //pass domino returns bool if domino is playable. out mustFLip true=you must flip domino to play it. false no flip
        //mustflip=true;
        //return true; play domino but it must be flipped over
        //mustflip=false;
        //return true; play domino no flip
        //mustflip=false;
        //return false;  domino is not playable
        /// </summary>
        protected bool IsPlayable(Domino d, out bool mustFlip)
        {

            if (d.Side1.Equals(PlayableValue))
            {

                mustFlip = false;
                return true;
            }
            else if (d.Side2.Equals(PlayableValue))
            {
                mustFlip = true;
                return true;
            }
            else
            {
                mustFlip = false;
                return false;
            }
                


        }
        /// <summary>
        /// // assumes the domino has already been removed from the hand
        /// hand h: what hand we are playing from? domino d has been removed form hand list.
        /// </summary>
        /// <param name="h"></param>
        /// <param name="d"></param>

        public void Play(Hand h, Domino d)
        {
           //call hand 
           
           bool mustFlip;
           bool flag = IsPlayable(h, d, out mustFlip);
           if(flag==false)
            {
                throw new Exception("The Domino cannot be played.");
            }
           else if(mustFlip==true && flag==true)
            {
                //dominos.Add(d);
                Add(d);
            }
            else
            {
                d.Flip();
                // better to use the method i created. dominos.Add(d);
                Add(d);
            }
        }

        public override string ToString()
        {
            string message = "";
            foreach (Domino d in dominos)
            {
                message += d.ToString() + " /n";
            }
            return message;
        }
        
    }
}