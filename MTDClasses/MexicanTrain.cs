using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDClasses
{
    public class MexicanTrain:Train
    {

            /*over ride or extend is playable method
             * 
             * is playable is abstract so it must be implemented here in this class >_<
             * implement with the Hand h
             * 2 constructors, defeault and int engineValue
             * 
             * 
             */
        public MexicanTrain():base()
        {
            EngineValue = 12;
            List<Domino> playerTrain = new List<Domino>();
        }
        public MexicanTrain(int engineValue): base(engineValue)
        {
            this.EngineValue = engineValue;
            List<Domino> playerTrain = new List<Domino>();
        }
        public override bool IsPlayable(Hand h, Domino d,out bool mustFlip)
        {

            //is defined in base class dont need to do it this way.
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
            //return 
        }
    }
}
