using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MTDClasses;

namespace MTDTests
{
    class BoneyardTests
    {
        BoneYard invalidBoneYard;
        BoneYard testBoneyard;
        BoneYard testBoneyard2;
        Domino d1;
        Domino d2;
        [SetUp]
        public void SetUpTests()
        {
            testBoneyard = new BoneYard(6);
            testBoneyard2 = new BoneYard(6);
            

        }
        /// <summary>
        /// tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            int count=testBoneyard.DominosRemaining();
            Assert.AreEqual(count, 28);
        }
        /// <summary>
        /// tests what happens if invalid data is passed. expecting to have an excaption thrown.
        /// </summary>
        [Test]
        public void InvalidConstructor()
        {
            Assert.Throws<ArgumentException>(() => invalidBoneYard=new BoneYard(-1));
            Assert.Throws<ArgumentException>(() => invalidBoneYard = new BoneYard(100));
        }
        /// <summary>
        /// tests the draw class.
        /// </summary>
        [Test]
        public void TestDraw()
        {
            d1 = testBoneyard[0];
            d2 = testBoneyard.Draw();
            Assert.AreEqual(d1, d2);
            
        }
        /// <summary>
        /// tests dominoesRemainings. Expecting the remaining bones to be 0 
        /// </summary>
        [Test]
        public void TestDominoesRemaining()
        {
            int count = testBoneyard.DominosRemaining();
            //clear the list then check use is empty to check. if empty returns true
            for(int i=0;i<count;i++)
            {
                testBoneyard.Draw();
            }
            Assert.AreEqual(0, testBoneyard.DominosRemaining());

        }
        /// <summary>
        /// similar to above. empties the boneyard then tests is empty. expecting return of true
        /// </summary>
        [Test]
        public void TestIsEmpty()
        {
            int count = testBoneyard.DominosRemaining();
            //clear the list then check use is empty to check. if empty returns true
            for (int i = 0; i < count; i++)
            {
                testBoneyard.Draw();
            }
            Assert.IsTrue(testBoneyard.IsEmpty());

        }
        /// <summary>
        /// tests Shuffle(). 1 list is shuffled the other is not. then chose 4 bones to compare. they should all be different. us
        /// </summary>
        [Test]
        public void TestShuffle()
        {
            bool flag = true;
            testBoneyard.Shuffle();
            if (testBoneyard[5].Equals(testBoneyard2[5]))
                flag = false;
            Assert.IsTrue(flag);
            if (testBoneyard[10] == testBoneyard2[10])
                flag = false;
            Assert.IsTrue(flag);
            if (testBoneyard[15] == testBoneyard2[15])
                flag = false;
            Assert.IsTrue(flag);
            if (testBoneyard[20] == testBoneyard2[20])
                flag = false;
            Assert.IsTrue(flag);
        }
       
        


    }
}
