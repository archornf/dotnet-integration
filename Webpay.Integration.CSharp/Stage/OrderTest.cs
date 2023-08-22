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
        private readonly Webpay.Integration.CSharp.Config.IConfigurationProvider _conf;
        internal OrderTest(Webpay.Integration.CSharp.Config.IConfigurationProvider conf)
        {
            _conf = conf;
        }

        public void TestOrder()
        {
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
            Console.WriteLine(response.ResultCode); // 0
            Console.WriteLine(response.Accepted); // True
        }
    }
}
