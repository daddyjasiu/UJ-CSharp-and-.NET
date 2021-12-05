using System;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace Zadanie5
{
    [TestFixture]
    public class StringAdderTests
    {
        [Test]
        public void TestRegularUseCaseTwoStrings()
        {
            //Arrange:
            StringAdder stringAdder = new();
            
            //Act:
            var result = stringAdder.Add("ala", "kot");
            
            //Assert:
            Assert.AreEqual("alakot", result);
        }
        
        [Test]
        public void TestFirstStringNull()
        {
            //Arrange:
            StringAdder stringAdder = new();
            
            //Act:
            var result = stringAdder.Add(null, "kot");
            
            //Assert:
            Assert.AreEqual(null, result);
        }
        
        [Test]
        public void TestSecondStringNull()
        {
            //Arrange:
            StringAdder stringAdder = new();
            
            //Act:
            var result = stringAdder.Add("ala", null);
            
            //Assert:   
            Assert.AreEqual(null, result);
        }
        
        [Test]
        public void TestBothStringsNull()
        {
            //Arrange:
            StringAdder stringAdder = new();
            
            //Act:
            var result = stringAdder.Add(null, null);
            
            //Assert:
            Assert.AreEqual(null, result);
        }
        
        [Test]
        public void TestEmptyStrings()
        {
            //Arrange:
            StringAdder stringAdder = new();
            
            //Act:
            var result = stringAdder.Add("", "");
            
            //Assert:
            Assert.AreEqual("", result);
        }
        
        [Test]
        public void TestBetterAdderException()
        {
            //Arrange:
            BetterStringAdder stringAdder = new();

            //Assert:
            Assert.Catch<ArgumentNullException>(() => stringAdder.Add(null, "kot"));
        }
    }
    
    [TestFixture]
    class AnagramCheckerTests
    {
        [Test]
        public void TestOneWordEmpty()
        {
            //Arrange
            AnagramChecker anagramChecker = new();
                
            //Assert:
            Assert.Catch<ArgumentNullException>(() => anagramChecker.IsAnagram(null, "kot"));
        }
        [Test]
        public void TestDifferentLengths()
        {
            //Arrange
            AnagramChecker anagramChecker = new();
            
            //Act
            var result = anagramChecker.IsAnagram("kotek", "tok");
            
            //Assert:
            Assert.AreEqual(false, result);
        }
        [Test]
        public void TestVariousWords()
        {
            //Arrange
            AnagramChecker anagramChecker = new();
            
            //Act
            var result1 = anagramChecker.IsAnagram("kot", "tok");
            var result2 = anagramChecker.IsAnagram("elvis", "lives");
            var result3 = anagramChecker.IsAnagram("silent", "listen");
            var result4 = anagramChecker.IsAnagram("pilka", "klipy");
            var result5 = anagramChecker.IsAnagram("masno", "maslo");
            
            //Assert:
            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(true, result3);
            Assert.AreEqual(false, result4);
            Assert.AreEqual(false, result5);
        }
        [Test]
        public void TestUppercase()
        {
            //Arrange
            AnagramChecker anagramChecker = new();
            
            //Act
            var result = anagramChecker.IsAnagram("kot", "ToK");

            //Assert:
            Assert.AreEqual(true, result);
        }
        //DODANE PO BLEDACH
        [Test]
        public void TestAlphanumeric()
        {
            //Arrange
            AnagramChecker anagramChecker = new();
            
            //Act
            var result = anagramChecker.IsAnagram("kot", "123t456o789k");

            //Assert:
            Assert.AreEqual(true, result);
        }
    }

    [TestFixture]
    class PESELDiscountCheckerTests
    {
        [Test]
        public void TestCorrectAgeGroup()
        {
            //Arrange
            DiscountFromPeselComputer discountFromPeselComputer = new();

            //Act
            var result = discountFromPeselComputer.HasDiscount("00211501170");

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestWrongAgeGroup()
        {
            //Arrange
            DiscountFromPeselComputer discountFromPeselComputer = new();

            //Act
            var result = discountFromPeselComputer.HasDiscount("40211501170");

            //Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TestEmptyPESEL()
        {
            //Arrange
            DiscountFromPeselComputer discountFromPeselComputer = new();

            //Assert
            Assert.Catch<ArgumentException>(() => discountFromPeselComputer.HasDiscount(""));
        }

        [Test]
        public void TestNullPESEL()
        {
            //Arrange
            DiscountFromPeselComputer discountFromPeselComputer = new();

            //Assert
            Assert.Catch<ArgumentNullException>(() => discountFromPeselComputer.HasDiscount(null));
        }
        [Test]
        public void TestMyPESELDiscountCorrectAgeGroup()
        {
            //Arrange
            MyPESELDiscount myPeselDiscount = new();

            //Act
            var result = myPeselDiscount.HasDiscount("00211501170");
            
            //Assert
            Assert.AreEqual(true, result);
        }
        [Test]
        public void TestMyPESELDiscountWrongAgeGroup()
        {
            //Arrange
            MyPESELDiscount myPeselDiscount = new();

            //Act
            var result = myPeselDiscount.HasDiscount("07030343312");
            
            //Assert
            Assert.AreEqual(false, result);
        }
        [Test]
        public void TestMyPESELDiscountEmptyPESEL()
        {
            //Arrange
            MyPESELDiscount myPeselDiscount = new();

            //Assert
            Assert.Catch<ArgumentNullException>(() => myPeselDiscount.HasDiscount(""));
        }
        [Test]
        public void TestMyPESELDiscountNullPESEL()
        {
            //Arrange
            MyPESELDiscount myPeselDiscount = new();

            //Assert
            Assert.Catch<ArgumentNullException>(() => myPeselDiscount.HasDiscount(null));
        }
    }
}