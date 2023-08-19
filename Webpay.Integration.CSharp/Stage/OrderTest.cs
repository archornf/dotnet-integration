using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Webpay.Integration.CSharp.Config;
using Webpay.Integration.CSharp;
using Webpay.Integration.CSharp.Order.Create;
using Webpay.Integration.CSharp.Order.Row;
using Webpay.Integration.CSharp.Util.Constant;
using Webpay.Integration.CSharp.Util.Testing;
using Webpay.Integration.CSharp.WebpayWS;

namespace Stage
{
    internal class OrderTest
    {
        private readonly IConfigurationProvider _conf;
        internal OrderTest(IConfigurationProvider conf)
        {
            _conf = conf;
        }

        internal void TestOrder()
        {
            //Create a class including test authorization
            //IConfigurationProvider confTest = new MyConfigTest();
            //Create a class including production authorization
            //MyConfigProd confProd = new MyConfigProd();

            //Create your CreateOrder object with selected and continue building your order. See next steps.
            //var conf = new ConfigurationProviderTestData();
            CreateOrderEuResponse response = WebpayConnection.CreateOrder(_conf)
                                                             .AddOrderRow(TestingTool.CreateExVatBasedOrderRow())
                                                             .AddOrderRow(TestingTool.CreateExVatBasedOrderRow())
                                                             .AddCustomerDetails(Item.IndividualCustomer()
                                                                                     .SetNationalIdNumber(TestingTool.DefaultTestIndividualNationalIdNumber)
                                                                                     .SetIpAddress("123.123.123"))
                                                             .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                                             .SetOrderDate(TestingTool.DefaultTestDate)
                                                             .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                                             .SetCurrency(TestingTool.DefaultTestCurrency)
                                                             .UseInvoicePayment()
                                                             .DoRequest();

            Console.WriteLine(response.ResultCode);
            Console.WriteLine(response.Accepted);

            //private CreateOrderEuResponse response = WebpayConnection.CreateOrder(confTest)		//See Configuration chapt.3
            ////For all products and other items
            //.AddOrderRow(Item.OrderRow())
            ////If shipping fee
            //.AddFee(Item.ShippingFee())
            ////If invoice with invoice fee
            //.AddFee(Item.InvoiceFee()...)
            ////If discount or coupon with fixed amount
            //.AddDiscount(Item.FixedDiscount()...)
            ////If discount or coupon with percent discount
            //.AddDiscount(Item.RelativeDiscount()...)
            ////Individual customer values
            //.AddCustomerDetails(Item.IndividualCustomer()...)
            ////Company customer values
            //.AddCustomerDetails(Item.CompanyCustomer()...)
            ////Other values
            //.SetCountryCode(CountryCode.SE)
            //.SetOrderDate(new DateTime(2012, 12, 12))
            //.SetCustomerReference("ref33")
            //.SetClientOrderNumber("33")
            //.SetCurrency(Currency.SEK)
            ////Continue as an invoice payment
            //.UseInvoicePayment()...
            //.DoRequest();
        }
    }
}
