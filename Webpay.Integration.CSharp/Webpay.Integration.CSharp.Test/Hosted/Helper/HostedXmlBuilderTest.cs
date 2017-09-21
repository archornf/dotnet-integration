﻿using NUnit.Framework;
using Webpay.Integration.CSharp.Config;
using Webpay.Integration.CSharp.Hosted.Helper;
using Webpay.Integration.CSharp.Order.Create;
using Webpay.Integration.CSharp.Order.Row;
using Webpay.Integration.CSharp.Test.Hosted.Payment;
using Webpay.Integration.CSharp.Util.Constant;
using Webpay.Integration.CSharp.Util.Testing;

namespace Webpay.Integration.CSharp.Test.Hosted.Helper
{
    [TestFixture]
    public class HostedXmlBuilderTest
    {
        private HostedXmlBuilder _xmlBuilder;
        private CreateOrderBuilder _order;
        private string _xml = "";

        [SetUp]
        public void SetUp()
        {
            _xmlBuilder = new HostedXmlBuilder();
        }

        [Test]
        public void TestBasicXml()
        {
            _order = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                     .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                     .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                     .SetCurrency(TestingTool.DefaultTestCurrency)
                                     .AddCustomerDetails(Item.IndividualCustomer()
                                                             .SetNationalIdNumber(TestingTool.DefaultTestIndividualNationalIdNumber))
                                     .AddOrderRow(Item.OrderRow()
                                                      .SetAmountExVat(4)
                                                      .SetVatPercent(25)
                                                      .SetQuantity(1));

            var payment = new FakeHostedPayment(_order);
            payment.SetReturnUrl("http://myurl.se")
                   .CalculateRequestValues();

            _xml = _xmlBuilder.GetXml(payment);

            const string expectedXml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><!--Message generated by Integration package C#-->" +
                "<payment><customerrefno>33</customerrefno><currency>SEK</currency><amount>500</amount>" +
                "<vat>100</vat><lang>en</lang><returnurl>http://myurl.se</returnurl><iscompany>false</iscompany>" +
                "<customer><ssn>194605092222</ssn><country>SE</country></customer><orderrows><row><sku /><name />" +
                "<description /><amount>500</amount><vat>100</vat><quantity>1</quantity></row></orderrows>" +
                "<excludepaymentMethods /><addinvoicefee>false</addinvoicefee></payment>";

            Assert.That(_xml, Is.EqualTo(expectedXml));
        }

        [Test]
        public void TestXmlWithIndividualCustomer()
        {
            _order = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                     .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                     .SetCurrency(TestingTool.DefaultTestCurrency)
                                     .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                     .AddOrderRow(Item.OrderRow()
                                                      .SetAmountExVat(4)
                                                      .SetVatPercent(25)
                                                      .SetQuantity(1))
                                     .AddCustomerDetails(Item.IndividualCustomer()
                                                             .SetName("Julius", "Caesar")
                                                             .SetInitials("JS")
                                                             .SetNationalIdNumber("666666")
                                                             .SetPhoneNumber("999999")
                                                             .SetEmail("test@svea.com")
                                                             .SetIpAddress("123.123.123.123")
                                                             .SetStreetAddress("Gatan", "23")
                                                             .SetCoAddress("c/o Eriksson")
                                                             .SetZipCode("9999")
                                                             .SetLocality("Stan"));

            var payment = new FakeHostedPayment(_order);
            payment.SetReturnUrl("http://myurl.se")
                   .CalculateRequestValues();

            _xml = _xmlBuilder.GetXml(payment);

            const string expectedString =
                "<customer><ssn>666666</ssn><firstname>Julius</firstname>" +
                "<lastname>Caesar</lastname><initials>JS</initials>" +
                "<phone>999999</phone><email>test@svea.com</email>" +
                "<address>Gatan</address><housenumber>23</housenumber>" +
                "<address2>c/o Eriksson</address2><zip>9999</zip>" +
                "<city>Stan</city><country>SE</country></customer>";

            Assert.That(_xml.Contains(expectedString), Is.True);
            Assert.That(_xml.Contains("<ipaddress>123.123.123.123</ipaddress>"), Is.True);
        }

        [Test]
        public void TestXmlWithCompanyCustomer()
        {
            _order = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                     .AddOrderRow(Item.OrderRow()
                                                      .SetAmountExVat(4)
                                                      .SetVatPercent(25)
                                                      .SetQuantity(1))
                                     .SetCurrency(TestingTool.DefaultTestCurrency)
                                     .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                     .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                     .AddCustomerDetails(TestingTool.CreateCompanyCustomer());

            var payment = new FakeHostedPayment(_order);
            payment.SetReturnUrl("http://myurl.se")
                   .CalculateRequestValues();

            _xml = _xmlBuilder.GetXml(payment);

            const string expectedString = 
                "<customer><ssn>194608142222</ssn><firstname>Tess, T Persson</firstname>" +
                "<phone>0811111111</phone><email>test@svea.com</email>" +
                "<address>Testgatan</address><housenumber>1</housenumber>" +
                "<address2>c/o Eriksson, Erik</address2><zip>99999</zip>" +
                "<city>Stan</city><country>SE</country></customer>";

            Assert.That(_xml.Contains(expectedString), Is.True);
            Assert.That(_xml.Contains("<ipaddress>123.123.123.123</ipaddress>"), Is.True);
        }

        [Test]
        public void TestXmlCancelUrl()
        {
            _order = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                     .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                     .SetCurrency(TestingTool.DefaultTestCurrency)
                                     .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                     .AddOrderRow(TestingTool.CreateMiniOrderRow())
                                     .AddCustomerDetails(Item.CompanyCustomer());

            var payment = new FakeHostedPayment(_order);
            payment.SetCancelUrl("http://www.cancel.com")
                   .SetReturnUrl("http://myurl.se")
                   .CalculateRequestValues();

            _xml = _xmlBuilder.GetXml(payment);

            Assert.That(_xml.Contains("<cancelurl>http://www.cancel.com</cancelurl>"), Is.True);
        }

        [Test]
        public void TestXmlCallbackUrl()
        {
            _order = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                     .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                     .SetCurrency(TestingTool.DefaultTestCurrency)
                                     .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                     .AddOrderRow(TestingTool.CreateMiniOrderRow())
                                     .AddCustomerDetails(Item.CompanyCustomer());

            var payment = new FakeHostedPayment(_order);
            payment.SetCallbackUrl("http://www.callback.nu")
                   .SetCancelUrl("http://www.cancel.com")
                   .SetReturnUrl("http://myurl.se")
                   .CalculateRequestValues();

            _xml = _xmlBuilder.GetXml(payment);

            Assert.That(_xml.Contains("<callbackurl>http://www.callback.nu</callbackurl>"), Is.True);
        }

        [Test]
        public void TestOrderRowXml()
        {
            _order = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                     .AddOrderRow(Item.OrderRow()
                                                      .SetArticleNumber("0")
                                                      .SetName("Product")
                                                      .SetDescription("Good product")
                                                      .SetAmountExVat(4)
                                                      .SetVatPercent(25)
                                                      .SetQuantity(1)
                                                      .SetUnit("kg"))
                                     .AddCustomerDetails(Item.CompanyCustomer())
                                     .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                     .SetCurrency(TestingTool.DefaultTestCurrency)
                                     .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber);

            var payment = new FakeHostedPayment(_order);
            payment.SetPayPageLanguageCode(LanguageCode.sv)
                   .SetReturnUrl("http://myurl.se")
                   .CalculateRequestValues();

            _xml = _xmlBuilder.GetXml(payment);

            const string expectedString =
                "<orderrows><row><sku>0</sku><name>Product</name>" +
                "<description>Good product</description><amount>500</amount>" +
                "<vat>100</vat><quantity>1</quantity><unit>kg</unit></row></orderrows>";

            Assert.That(_xml.Contains(expectedString), Is.True);
        }

        [Test]
        public void TestDirectPaymentSpecificXml()
        {
            _xml = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                   .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                   .SetCurrency(TestingTool.DefaultTestCurrency)
                                   .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                   .AddOrderRow(TestingTool.CreateMiniOrderRow())
                                   .AddCustomerDetails(Item.CompanyCustomer())
                                   .UsePayPageDirectBankOnly()
                                   .SetReturnUrl(
                                       "https://webpaypaymentgatewaytest.svea.com/Webpayconnection/admin/merchantresponSetest.xhtm")
                                   .GetPaymentForm()
                                   .GetXmlMessage();

            const string expectedString = 
                "<excludepaymentMethods><exclude>BANKAXESS</exclude>" +
                "<exclude>PAYPAL</exclude><exclude>KORTCERT</exclude>" +
                "<exclude>SVEACARDPAY</exclude><exclude>SKRILL</exclude>" +
                "<exclude>SVEAINVOICESE</exclude><exclude>SVEAINVOICEEU_SE</exclude>" +
                "<exclude>SVEASPLITSE</exclude><exclude>SVEASPLITEU_SE</exclude>" +
                "<exclude>SVEAINVOICEEU_DE</exclude><exclude>SVEASPLITEU_DE</exclude>" +
                "<exclude>SVEAINVOICEEU_DK</exclude><exclude>SVEASPLITEU_DK</exclude>" +
                "<exclude>SVEAINVOICEEU_FI</exclude><exclude>SVEASPLITEU_FI</exclude>" +
                "<exclude>SVEAINVOICEEU_NL</exclude><exclude>SVEASPLITEU_NL</exclude>" +
                "<exclude>SVEAINVOICEEU_NO</exclude><exclude>SVEASPLITEU_NO</exclude>" +
                "</excludepaymentMethods>";

            Assert.That(_xml.Contains(expectedString), Is.True);
        }

        [Test]
        public void TestCardPaymentSpecificXml()
        {
            _xml = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                   .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                   .SetCurrency(TestingTool.DefaultTestCurrency)
                                   .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                   .AddOrderRow(TestingTool.CreateMiniOrderRow())
                                   .AddCustomerDetails(Item.CompanyCustomer())
                                   .UsePayPageCardOnly()
                                   .SetReturnUrl(
                                       "https://webpaypaymentgatewaytest.svea.com/Webpayconnection/admin/merchantresponSetest.xhtm")
                                   .GetPaymentForm()
                                   .GetXmlMessage();

            const string expectedString = 
                    "<excludepaymentMethods><exclude>PAYPAL</exclude>" +
                    "<exclude>DBNORDEASE</exclude><exclude>DBSEBSE</exclude>" +
                    "<exclude>DBSEBFTGSE</exclude><exclude>DBSHBSE</exclude>" +
                    "<exclude>DBSWEDBANKSE</exclude><exclude>BANKAXESS</exclude>" +
                    "<exclude>SVEAINVOICESE</exclude>" +
                    "<exclude>SVEAINVOICEEU_SE</exclude>" +
                    "<exclude>SVEASPLITSE</exclude><exclude>SVEASPLITEU_SE</exclude>" +
                    "<exclude>SVEAINVOICEEU_DE</exclude>" +
                    "<exclude>SVEASPLITEU_DE</exclude>" +
                    "<exclude>SVEAINVOICEEU_DK</exclude>" +
                    "<exclude>SVEASPLITEU_DK</exclude>" +
                    "<exclude>SVEAINVOICEEU_FI</exclude><exclude>SVEASPLITEU_FI</exclude>" +
                    "<exclude>SVEAINVOICEEU_NL</exclude><exclude>SVEASPLITEU_NL</exclude>" +
                    "<exclude>SVEAINVOICEEU_NO</exclude><exclude>SVEASPLITEU_NO</exclude>" +
                    "</excludepaymentMethods>";

            Assert.That(_xml.Contains(expectedString), Is.True);
        }

        [Test]
        public void TestPayPagePaymentSpecificXmlNullPaymentMethod()
        {
            _xml = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                   .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                   .SetCurrency(TestingTool.DefaultTestCurrency)
                                   .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                   .AddOrderRow(TestingTool.CreateMiniOrderRow())
                                   .AddCustomerDetails(Item.CompanyCustomer())
                                   .UsePayPage()
                                   .SetReturnUrl(
                                       "https://webpaypaymentgatewaytest.svea.com/Webpayconnection/admin/merchantresponSetest.xhtm")
                                   .GetPaymentForm()
                                   .GetXmlMessage();

            Assert.That(_xml.Contains("<excludepaymentMethods />"), Is.True);
        }

        [Test]
        public void TestPayPagePaymentSetLanguageCode()
        {
            _xml = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                   .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                   .SetCurrency(TestingTool.DefaultTestCurrency)
                                   .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                   .AddOrderRow(TestingTool.CreateMiniOrderRow())
                                   .AddCustomerDetails(Item.CompanyCustomer())
                                   .UsePayPage()
                                   .SetPayPageLanguageCode(LanguageCode.sv)
                                   .SetReturnUrl(
                                       "https://webpaypaymentgatewaytest.svea.com/Webpayconnection/admin/merchantresponSetest.xhtm")
                                   .GetPaymentForm()
                                   .GetXmlMessage();

            Assert.That(_xml.Contains("<lang>sv</lang>"), Is.True);
        }

        [Test]
        public void TestPayPagePaymentPayPal()
        {
            _xml = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                   .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                   .SetCurrency(TestingTool.DefaultTestCurrency)
                                   .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                   .AddOrderRow(TestingTool.CreateMiniOrderRow())
                                   .AddCustomerDetails(Item.CompanyCustomer())
                                   .UsePayPage()
                                   .SetReturnUrl(
                                       "https://webpaypaymentgatewaytest.svea.com/Webpayconnection/admin/merchantresponSetest.xhtm")
                                   .SetPaymentMethod(PaymentMethod.PAYPAL)
                                   .GetPaymentForm()
                                   .GetXmlMessage();

            Assert.That(_xml.Contains("<paymentmethod>PAYPAL</paymentmethod>"), Is.True);
        }

        [Test]
        public void TestPayPagePaymentSpecificXml()
        {
            _xml = WebpayConnection.CreateOrder(SveaConfig.GetDefaultConfig())
                                   .SetCountryCode(TestingTool.DefaultTestCountryCode)
                                   .SetCurrency(TestingTool.DefaultTestCurrency)
                                   .SetClientOrderNumber(TestingTool.DefaultTestClientOrderNumber)
                                   .AddOrderRow(TestingTool.CreateMiniOrderRow())
                                   .AddCustomerDetails(Item.CompanyCustomer())
                                   .UsePayPage()
                                   .SetReturnUrl(
                                       "https://webpaypaymentgatewaytest.svea.com/Webpayconnection/admin/merchantresponSetest.xhtm")
                                   .SetPaymentMethod(PaymentMethod.INVOICE)
                                   .GetPaymentForm()
                                   .GetXmlMessage();

            Assert.That(_xml.Contains("<paymentmethod>SVEAINVOICEEU_SE</paymentmethod>"), Is.True);
        }
    }
}