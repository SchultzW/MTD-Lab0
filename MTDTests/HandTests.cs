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
    class HandTests
    {
        Hand h;
        Hand hEmpt;
        BoneYard by;
        BoneYard byEmpt;
        Domino dDouble;
        Domino d;
        PlayerTrain pT;
        [SetUp]
        public void SetUpTest()
        {
            d = new Domino(1, 2);
            dDouble = new Domino(12, 12);
            by = new BoneYard(12);
            h = new Hand(by, 2);
            pT = new PlayerTrain(h);
              
        }
        /// <summary>
        /// tests the add method
        /// </summary>
        [Test]
        public void TestAdd()
        {
            h.Add(d);
            Assert.IsTrue(h.Count.Equals(1));
        }
        /// <summary>
        /// tests the is empty method. removes everything from the hand
        /// </summary>
        [Test]
        public void TestIsEmpty()
        {
            for(int i=0;i<h.Count;i++)
            {
                h.RemoveAt(0);
            }
            Assert.IsTrue(h.IsEmpty);
        }
        /// <summary>
        /// tests that the score adds up
        /// </summary>
        [Test]
        public void TestScore()
        {
            bool flag = false;
            h.Add(dDouble);
            int score = h.Score;
            if (score > 5)
                flag = true;

            Assert.IsTrue(flag);
        }
        [Test]
        public void TestHasDomino()
        {
            h.Add(d);
            int value = d.Side1;
            Assert.IsTrue(h.HasDomino(value));
        }
        [Test]
        public void TestHasDouble()
        {
            h.Add(dDouble);
            int value = dDouble.Side1;
            Assert.IsTrue(h.HasDomino(value));
        }
        [Test]
        public void TestIndexOfDomino()
        {
            h.Add(d);
            int value = h.IndexOfDomino(d.Side1);
            Assert.IsTrue(value.Equals(0));
        }
        [Test]
        public void TestIndexOfDouble()
        {
            h.Add(dDouble);
            int value = h.IndexOfDomino(dDouble.Side1);
            Assert.IsTrue(value.Equals(0));
        }
        public void TestIndexOfHighest()
        {
            h.Add(dDouble);
            int value = h.IndexOfDomino(dDouble.Side1);
            Assert.IsTrue(value.Equals(0));
        }
        public void TestRemoveAt()
        {

            h.Add(dDouble);
            h.RemoveAt(0);
            Assert.IsFalse(h[0].Equals(dDouble));
            
        }
        public void TestGetDomino()
        {
            h.Add(dDouble);
            Domino dom=h.GetDomino(12);
            Assert.IsTrue(dom.Equals(d));
        }
        public void TestGetDouble()
        {
            h.Add(dDouble);
            Domino dom = h.GetDoubleDomino(12);
            Assert.IsTrue(dom.Equals(d));
        }
        public void TestDraw()
        {
            Domino dom = by[0];
            h.Draw(by);
            Assert.IsTrue(dom.Equals(h[0]));
        }
        public void TestPlay1()
        {
            h.Add(d);
            h.Play(d, pT);
            Assert.IsTrue(pT[0].Equals(d));

        }
        public void TestPlay2()
        {
            h.Add(d);
            h.Play(pT);
            Assert.IsTrue(pT[0].Equals(d));
        }
}
}
