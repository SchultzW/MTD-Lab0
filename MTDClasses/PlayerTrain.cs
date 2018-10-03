using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    public class PlayerTrain:Train
    {
        //private Hand hand;
        private bool isOpen;
        private Hand Hand;
        public PlayerTrain(Hand h): base()
        {
            //playTrain has a hand that is dealing with. 
            this.Hand = h;
            this.EngineValue = 12;
            List<Domino> playerTrain = new List<Domino>();

        }

        /// <summary>
        /// This is the most appropriate constructor for the class.
        /// </summary>
        /// <param name="h">Represents the Hand object to which the train belongs</param>
        /// <param name="engineValue">Represents the first playable value on the train</param>
        public PlayerTrain(Hand h, int engineValue) : base (engineValue)
        {

            this.Hand = h;
            this.EngineValue = engineValue;
            List<Domino> playerTrain = new List<Domino>();
        }

        /// <summary>
        /// Returns whether or not the train is open.  An open train
        /// can be played upon by any player.
        /// </summary>
        public bool IsOpen
        {
        
            get
            {
                return isOpen;
            }
           
        }

        /// <summary>
        /// Open the train
        /// </summary>
        public void Open()
        {
            //true other players can play on it.
            isOpen = true;
        }

        /// <summary>
        /// Close the train
        /// </summary>
        public void Close()
        {
            isOpen = false;
            //close the train so others cannot play on it. true() other players cnanot play0
        }

        /// <summary>
        /// Can the domino d be played by the hand h on this train?
        /// If it can be played, must it be flipped to do so?
        /// must test isOpen if its true or fasle. player train is only playable to another play if it is open
        /// </summary>
        /// <param name="d"></param>
        /// <param name="mustFlip"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public override bool IsPlayable(Hand h, Domino d, out bool mustFlip)
        {

            if (d.Side1.Equals(base.PlayableValue))
            {

                mustFlip = false;
                return true;
            }
            else if (d.Side2.Equals(base.PlayableValue))
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
        
    }
}
