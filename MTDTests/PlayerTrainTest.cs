using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MTDClasses;

namespace MTDTests
{
    [TestFixture]
    class PlayerTrainTest
    {
        PlayerTrain pT;
        PlayerTrain pT1;
        Domino d;
        Domino d1;
        Domino d3;
        Hand h;
        Domino d4;

        [SetUp]
        public void SetUpAllTests()
        {
            pT = new PlayerTrain(h,12);
            pT1 = new PlayerTrain(h);
            d = new Domino(10, 10);
            d1 = new Domino(5, 5);
            d3 = new Domino(12, 12);
            d4 = new Domino(12, 0);

        }

        /// <summary>
        /// Tests if the deafult constructor and overload work. Expect both to =12
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            Assert.IsTrue(pT.EngineValue.Equals(12));
            Assert.IsTrue(pT1.EngineValue.Equals(12));

        }
        /// <summary>
        /// testing isempty. it should return true as pT is an empty list.
        /// </summary>
        [Test]
        public void IsEmptyTest()
        {
            bool flag = pT.IsEmpty;
            Assert.IsTrue(pT.IsEmpty);
        }
        /// <summary>
        /// LastDom checks the last domino added to the list.
        /// </summary>
        [Test]
        public void TestLastDomino()
        {
            pT.Add(d);
            Assert.IsTrue(pT.LastDomino.Equals(d));
            
        }
        /// <summary>
        /// test to make sure it works with an invalid bone
        /// </summary>
        [Test]
        public void TestInvalidLastDomino()
        {
            pT.Add(d);
            Assert.IsFalse(pT.LastDomino.Equals(d1));


        }
        [Test]
        public void TestOpen()
        {
            pT.Open();
            Assert.IsTrue(pT.IsOpen);

        }
        [Test]
        public void TestClose()
        {
            pT.Close();
            Assert.IsFalse(pT.IsOpen);
        }
        [Test]
        public void TestIsPlayable()
        {
           Assert.IsTrue(pT.IsPlayable(h, d3, out bool mustFlip));

        }
        [Test]
        public void TestMustFlip()
        {
            bool flag;
            flag = pT.IsPlayable(h, d4, out bool mustFlip);
            flag = mustFlip;
            Assert.IsFalse(flag);
        }


    }

   
}
