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
        [Test]
        public void TestConstructor()
        {
            int count=testBoneyard.DominosRemaining();
            Assert.AreEqual(count, 28);
        }
        [Test]
        public void InvalidConstructor()
        {
            Assert.Throws<ArgumentException>(() => invalidBoneYard=new BoneYard(-1));
            Assert.Throws<ArgumentException>(() => invalidBoneYard = new BoneYard(100));
        }
        [Test]
        public void TestDraw()
        {
            d1 = testBoneyard[0];
            d2 = testBoneyard.Draw();
            Assert.AreEqual(d1, d2);
            
        }
        [Test]
        public void TestIsEmpty()
        {
            int count = testBoneyard.DominosRemaining();
            //clear the list then check use is empty to check. if empty returns true
            for(int i=0;i<count;i++)
            {
                testBoneyard.Draw();
            }
            Assert.AreEqual(0, testBoneyard.DominosRemaining());

        }
        [Test]
        public void TestShuffle()
        {
            bool flag = true;
            testBoneyard.Shuffle();
            if (testBoneyard[5].Equals(testBoneyard2[5]))
                flag = false;
            if (testBoneyard[10] == testBoneyard2[10])
                flag = false;
            if (testBoneyard[15] == testBoneyard2[15])
                flag = false;
            if (testBoneyard[20] == testBoneyard2[20])
                flag = false;
            Assert.IsTrue(flag);
        }
       
        


    }
}
