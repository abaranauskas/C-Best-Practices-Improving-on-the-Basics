using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory.
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37;
        //Konstantos by defaul static todel nesukurus opjecto is kitus galima
        //pasiekti taip Product.InchesPerMeter;

        public readonly decimal MinimumPrice;

        public Product()
        {
            //this.ProductVendor = new Vendor();
            Console.WriteLine("default ctor");
            this.MinimumPrice = 0.96m;
            this.Category = "Tools";
        }

        public Product(int productId, string productName, string description) 
            : this ()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;

            if (ProductName.StartsWith("Bulk"))
            {
                MinimumPrice = 9.99m;
            }   

            Console.WriteLine("overloaded ctor");
        }

        private DateTime? availabilityDate;
        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        public decimal Cost { get; set; }

        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private string productName;
        public string ProductName
        {
            get
            {
                var formatedName = productName?.Trim();
                return formatedName;
            }
            set
            {
                if (value.Length<3)
                {
                    ValidationMessage = "name is too short";
                }
                else if (value.Length>20)
                {
                    ValidationMessage = "name is too long";
                }
                else
                {
                    productName = value;
                }
            }                
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private Vendor productVendor;
        public Vendor ProductVendor
        {
            get {
                if (productVendor==null)
                {
                    productVendor = new Vendor(); //lazy loading method
                }  //sukuriamas objectas tik tada kai jo reikia
                return productVendor;
            }
                
            set { productVendor = value; }
        }

        internal string Category {  get; set; }
        public int SequenceNumber { get; set; } = 1;

        //public string ProductCode => String.Format("{0}-{1:0000}",Category,SequenceNumber);
        public string ProductCode => $"{Category}-{SequenceNumber:0000}";

        public string ValidationMessage { get; private set; }

        public decimal CalculateSuggestedPrice(decimal markupPercent)=>       
                                         Cost + (Cost * markupPercent / 100);
       

        public string SayHello()
        {
            //this.MinimumPrice = 7.77m; //meta errora nes readonly 
            //ir MinPrice galima nustatyti tiek dekraruojant arba ctor

            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("Message from product");

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New product",
                this.ProductName, "sales@abc.com");

            var result = LoggingService.LogAction("Say hello");

            return $"Hello {ProductName} {ProductId} {Description}. Available on: {AvailabilityDate?.ToShortDateString()}";
        }

        public override string ToString()
        {
            return $"{ProductName}({productId})";
        }

    }
}
