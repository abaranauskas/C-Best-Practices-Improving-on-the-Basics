using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void SayHelloTest()
        {
            //arrange
            var currentProduct = new Product();
            currentProduct.ProductId = 1;
            currentProduct.ProductName = "Sony";
            currentProduct.Description = "Best tv ever";
            currentProduct.ProductVendor.CompanyName = "ABC Corp";
            var expected = "Hello Sony 1 Best tv ever. Available on: ";

            //act
            var actual = currentProduct.SayHello();

            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void SayHello_ParameterizedCtor()
        {
            //arrange
            var currentProduct = new Product(1, "Sony", "Best tv ever");
            var expected = "Hello Sony 1 Best tv ever. Available on: ";

            //act
            var actual = currentProduct.SayHello();

            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void SayHello_ObjectInitializer()
        {
            //arrange
            var currentProduct = new Product()
            {
                ProductId = 1,
                ProductName = "Sony",
                Description = "Best tv ever"
            };

            var expected = "Hello Sony 1 Best tv ever. Available on: ";

            //act
            var actual = currentProduct.SayHello();

            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void Product_Null()
        {
            //arrange
            Product currentProduct = null;
            var companyName = currentProduct?.ProductVendor?.CompanyName;
            // "?? null conditional operator if not null then dot

            string expected = null;

            //act
            var actual = companyName;

            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void ConvertMetersToInches()
        {
            //arrange
            var expected = 78.74;

            //act
            var actual = 2 * Product.InchesPerMeter;

            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void MinimumPriceTest_Default()
        {
            //arrange
            var currentproduct = new Product();
            //kadangi readonly field ne static reikia sukurti object
            var expected = 0.96m;

            //act
            var actual = currentproduct.MinimumPrice;

            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void MinimumPriceTest_Bulk()
        {
            //arrange
            var currentproduct = new Product(1, "Bulk tools", "irankiai");
            //kadangi readonly field ne static reikia sukurti object
            var expected = 9.99m;

            //act
            var actual = currentproduct.MinimumPrice;

            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void ProductName_Format()
        {
            //arrange
            var currentproduct = new Product();
            currentproduct.ProductName = "   Steel Hummer  ";
            var expected = "Steel Hummer";
            //act
            var actual = currentproduct.ProductName;

            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void ProductName_TooShort()
        {
            //arrange
            var currentproduct = new Product();
            currentproduct.ProductName = "as";
            string expected = null;
            var expectedMessage = "name is too short";
            //act
            var actual = currentproduct.ProductName;
            var actualMessage = currentproduct.ValidationMessage;

            //assert
            Assert.AreEqual(actual, expected);
            Assert.AreEqual(actualMessage, expectedMessage);
        }

        [TestMethod()]
        public void ProductName_TooLong()
        {
            //arrange
            var currentproduct = new Product();
            currentproduct.ProductName = "ajhgfkasjhdkfjhaslkdjflaskjdflsakjdlakdj";
            string expected = null;
            var expectedMessage = "name is too long";
            //act
            var actual = currentproduct.ProductName;
            var actualMessage = currentproduct.ValidationMessage;

            //assert
            Assert.AreEqual(actual, expected);
            Assert.AreEqual(actualMessage, expectedMessage);
        }

        [TestMethod()]
        public void ProductName_CorrectLength()
        {
            //arrange
            var currentproduct = new Product();
            currentproduct.ProductName = "Kirvis";
            string expected = "Kirvis";
            string expectedMessage = null;
            //act
            var actual = currentproduct.ProductName;
            var actualMessage = currentproduct.ValidationMessage;

            //assert
            Assert.AreEqual(actual, expected);
            Assert.AreEqual(actualMessage, expectedMessage);
        }

        [TestMethod()]
        public void CategoryName_DefaulValue()
        {
            //arrange
            var currentproduct = new Product();

            string expected = "Tools";
            //act
            var actual = currentproduct.Category;

            //assert
            Assert.AreEqual(actual, expected);

        }

        [TestMethod()]
        public void CategoryName_NewValue()
        {
            //arrange
            var currentproduct = new Product();
            currentproduct.Category = "Drinks";

            string expected = "Drinks";
            //act
            var actual = currentproduct.Category;

            //assert
            Assert.AreEqual(actual, expected);

        }

        [TestMethod()]
        public void SequenceNumber_DefaulValue()
        {
            //arrange
            var currentproduct = new Product();

            var expected = 1;
            //act
            var actual = currentproduct.SequenceNumber;

            //assert
            Assert.AreEqual(actual, expected);

        }

        [TestMethod()]
        public void SequenceNumber_NewValue()
        {
            //arrange
            var currentproduct = new Product();
            currentproduct.SequenceNumber = 25245;

            var expected = 25245;
            //act
            var actual = currentproduct.SequenceNumber;

            //assert
            Assert.AreEqual(actual, expected);

        }

        [TestMethod()]
        public void ProductCode_Default()
        {
            //arrange
            var currentproduct = new Product();

            var expected = "Tools-0001";
            //act
            var actual = currentproduct.ProductCode;

            //assert
            Assert.AreEqual(actual, expected);

        }

        [TestMethod()]
        public void ProductCode_NewValue()
        {
            //arrange
            var currentproduct = new Product();
            currentproduct.Category = "Drinks";
            currentproduct.SequenceNumber = 2545;

            var expected = "Drinks-2545";
            //act
            var actual = currentproduct.ProductCode;

            //assert
            Assert.AreEqual(actual, expected);

        }

        [TestMethod()]
        public void CalculateSuggestedPriceTest()
        {
            Product product = new Product(2, "Philips", "cool tv");
            product.Cost = 50;

            var expected = 55;

            //act 
            var actual = product.CalculateSuggestedPrice(10);

            //assert
            Assert.AreEqual(actual, expected);
        }
    }
}