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
        Domino d1;
        Domino d2;
        [SetUp]
        public void SetUpTests()
        {
            testBoneyard = new BoneYard(6);
            

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
            //clear the list then check use is empty to check. if empty returns true
        }
        


    }
}
