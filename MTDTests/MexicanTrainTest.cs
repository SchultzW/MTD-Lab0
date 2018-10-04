using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTDClasses;
using NUnit.Framework;

namespace MTDTests
{
    [TestFixture]
    class MexicanTrainTest
    {
        
        MexicanTrain mT;
        MexicanTrain mT1;
        Domino d;
        Domino d1;
        Domino d3;
        Hand h;
        Domino d4;

        [SetUp]
        public void SetUpAllTests()
        {
            mT = new MexicanTrain();
            mT1 = new MexicanTrain(12);
            d = new Domino(10, 10);
            d1 = new Domino(5, 5);
            d3 = new Domino(12, 12);
            d4 = new Domino(12, 0);

        }
        [Test]
        public void TestConstructors()
        {
            Assert.IsTrue(mT.EngineValue.Equals(12));
            Assert.IsTrue(mT1.EngineValue.Equals(12));
        }
        [Test]
        public void TestIsPlayable()
        {
            Assert.IsTrue(mT.IsPlayable(h, d3, out bool mustFlip));

        }
    }
}
