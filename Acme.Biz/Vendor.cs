using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor 
    {
        public enum IncludeAddress { Yes, No}
        public enum SendCopy { Yes, No}

        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Sends order to vendor.
        /// </summary>
        /// <param name="product">Product to order</param>
        /// <param name="quantity">Quantity of the product to order</param>
        ///// <returns></returns>
        //public OperationResult PlaceOrder(Product product, int quantity)
        //{
        //    return PlaceOrder(product, quantity, null, null);
        //}

        //public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy)
        //{

        //    return PlaceOrder(product, quantity, deliverBy, null);
        //}
        //nereikia nes padrem optional parametes priskirdami  drfsul vslue

        public OperationResult PlaceOrder(Product product, int quantity, 
                    DateTimeOffset? deliverBy=null, string instructions="Standard delivery")
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now) throw new ArgumentOutOfRangeException(nameof(deliverBy));


            var success = false;

            var orderTextBuilder = new StringBuilder("Order from Acme inc" + System.Environment.NewLine +
                            $"Product: {product.ProductCode}" + System.Environment.NewLine +
                            $"Quantity: {quantity}");

            if (deliverBy.HasValue)
            {
                orderTextBuilder.Append(System.Environment.NewLine +
                             $"Deliver By: {deliverBy.Value.ToString("d")}");
            }

            if (!string.IsNullOrWhiteSpace(instructions))
            {
                orderTextBuilder.Append(System.Environment.NewLine +
                             $"Instructions: {instructions}");
            }

            var orderText = orderTextBuilder.ToString();
            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New order", orderText, Email);

            if (confirmation.StartsWith("Message sent:"))
            {
                success = true;
            }

            var operationResult = new OperationResult(success, orderText);

            return operationResult;
        }

        /// <summary>
        /// Send product order to  vendor.
        /// </summary>
        /// <param name="product">Produvt to order</param>
        /// <param name="quantity">Quantity of product to order</param>
        /// <param name="includeAddress">True to include shipping address</param>
        /// <param name="sendCopy">true to send copy of the email to vendor</param>
        /// <returns>Suuccess flasg and order text</returns>
        public OperationResult PlaceOrder(Product product, int quantity, 
                                    bool includeAddress, bool sendCopy)
        {
            var orderText = "Test";
            if (includeAddress) orderText += " with Address";
            if (sendCopy) orderText += " with Copy";

            var operationResult = new OperationResult(true, orderText);
                       
            return operationResult;
        }


        public OperationResult PlaceOrder(Product product, int quantity,
                                    IncludeAddress includeAddress, SendCopy sendCopy)
        {
            var orderText = "Test";
            if (includeAddress==IncludeAddress.Yes) orderText += " with Address";
            if (sendCopy==SendCopy.Yes) orderText += " with Copy";

            var operationResult = new OperationResult(true, orderText);

            return operationResult;
        }


        public override string ToString()
        {
            var vendorInfo = $"Vendor: {this.CompanyName}";
            //string vendorInfo = null;
            string result;
           // if (!string.IsNullOrWhiteSpace(vendorInfo))
           // {
                result = vendorInfo?.ToLower();
                result = vendorInfo?.ToUpper();
                result = vendorInfo?.Replace("Vendor", "Supplier");

                var length = vendorInfo?.Length;
                var index = vendorInfo?.IndexOf(":");

                bool? begins = vendorInfo?.StartsWith("Vendor");
            //}
            return vendorInfo;
        }

        public string PrepareDirections()
        {
            var directions = @"Insert \r\n to define new line";
            var directions2 = $@"Insert \r\n to define {directions} new line";
            return directions;
        }

        public string PrepareDirectionsOnTwoLines()
        {
            var directions = "First do this" + Environment.NewLine +
                "Then do that!";

            var directions2 = "First do this\r\nThen do that!";

            var directions3 = @"First do this
Then do that!";
            var directions4 = $"First do this{Environment.NewLine}Then do that!";
            //cia virsui ekvivalentai


            return directions;
        }



        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message, 
                                                        this.Email);
            return confirmation;
        }
    }
}
