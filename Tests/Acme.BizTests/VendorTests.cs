using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorTests
    {
        [TestMethod()]
        public void SendWelcomeEmail_ValidCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "ABC Corp";
            var expected = "Message sent: Hello ABC Corp";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_EmptyCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "";
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_NullCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = null;
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PlaceOrderTest()
        {
            //arrange
            var vendor = new Vendor()
            {
                VendorId = 1,
                CompanyName = "ABC Inc",
                Email = "info@abc.com"
            };

            var product = new Product()
            {
                ProductId = 22,
                Category = "Tools",
                SequenceNumber = 245
            };

            var expected = new OperationResult(true,
                "Order from Acme inc\r\nProduct: Tools-0245\r\nQuantity: 3" +
                "\r\nInstructions: Standard delivery");

            //Act
            var actual = vendor.PlaceOrder(product, 3);

            //assert
            Assert.AreEqual(actual.Success, expected.Success);
            Assert.AreEqual(actual.Message, expected.Message);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlaceOrder_product_Null()
        {
            //arrange
            var vendor = new Vendor()
            {
                VendorId = 1,
                CompanyName = "ABC Inc",
                Email = "info@abc.com"
            };

            Product product = null;

            //Act
            var actual = vendor.PlaceOrder(product, quantity: 3);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PlaceOrder_Quantity_Zero()
        {
            //arrange
            var vendor = new Vendor()
            {
                VendorId = 1,
                CompanyName = "ABC Inc",
                Email = "info@abc.com"
            };

            Product product = new Product();

            //Act
            var actual = vendor.PlaceOrder(product, 0);
        }

        [TestMethod()]
        public void PlaceOrderTes_3Params()
        {
            //arrange
            var vendor = new Vendor()
            {
                VendorId = 1,
                CompanyName = "ABC Inc",
                Email = "info@abc.com"
            };

            var product = new Product()
            {
                ProductId = 22,
                Category = "Tools",
                SequenceNumber = 245
            };

            var expected = new OperationResult(true,
                "Order from Acme inc\r\nProduct: Tools-0245\r\nQuantity: 3" +
                "\r\nDeliver By: 2020-10-25" +
                "\r\nInstructions: Standard delivery");

            //Act
            var actual = vendor.PlaceOrder(product, 3,
                new DateTimeOffset(2020, 10, 25, 0, 0, 0, new TimeSpan(-7, 0, 0)));

            //assert
            Assert.AreEqual(actual.Success, expected.Success);
            Assert.AreEqual(actual.Message, expected.Message);
        }

        [TestMethod()]
        public void PlaceOrder_WithAddress()
        {
            //arrage
            var vendor = new Vendor();
            var product = new Product(22, "Dell", "27inches");
            var expected = new OperationResult(success: true,
                                message: "Test with Address");

            //act
            var number = 12;
            var actual = vendor.PlaceOrder(product, quantity: number,
                            includeAddress: true, sendCopy: false);


            //asssert
            Assert.AreEqual(actual.Success, expected.Success);
            Assert.AreEqual(actual.Message, expected.Message);
        }

        [TestMethod()]
        public void PlaceOrder_WithAddress_Enums()
        {
            //arrage
            var vendor = new Vendor();
            var product = new Product(22, "Dell", "27inches");
            var expected = new OperationResult(success: true,
                                message: "Test with Address");

            //act
            var number = 12;
            var actual = vendor.PlaceOrder(product, number,
                                Vendor.IncludeAddress.Yes,
                                Vendor.SendCopy.No);


            //asssert
            Assert.AreEqual(actual.Success, expected.Success);
            Assert.AreEqual(actual.Message, expected.Message);
        }

        [TestMethod()]
        public void PlaceOrderTest_NoDeliveryDate()
        {
            //arrange
            var vendor = new Vendor();

            var product = new Product()
            {
                ProductId = 22,
                Category = "Tools",
                SequenceNumber = 245
            };

            var expected = new OperationResult(true,
                "Order from Acme inc\r\nProduct: Tools-0245\r\nQuantity: 3" +
                "\r\nInstructions: ring the upper bell");

            //Act
            var actual = vendor.PlaceOrder(product, 3, instructions: "ring the upper bell");
            //kadangi instruction 3 parametras o antro nenaudojam
            //reikia tikliai tada nurodyti

            //assert
            Assert.AreEqual(actual.Success, expected.Success);
            Assert.AreEqual(actual.Message, expected.Message);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            //assert
            var vendor = new Vendor()
            {
                VendorId = 1,
                CompanyName = "Bite"
            };

            var expected = "Vendor: Bite";
            //act
            var actual = vendor.ToString();

            //asser
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void PrepareDirectionsTest()
        {
            //arange
            var vendor = new Vendor();
            var expected = @"Insert \r\n to define new line";

            //act
            var actual = vendor.PrepareDirections();
            Console.WriteLine(actual);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}