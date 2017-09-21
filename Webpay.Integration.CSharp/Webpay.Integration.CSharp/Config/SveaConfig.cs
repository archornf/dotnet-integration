﻿using System.Reflection.Emit;

namespace Webpay.Integration.CSharp.Config
{
    public class SveaConfig
    {
        private const string HostedTestAdminBaseUrl = "https://webpaypaymentgatewaytest.svea.com/webpay/rest";
        private const string HostedProdAdminBaseUrl = "https://webpaypaymentgateway.svea.com/webpay/rest";
        private const string SwpTestUrl = "https://webpaypaymentgatewaytest.svea.com/webpay/payment";
        private const string SwpProdUrl = "https://webpaypaymentgateway.svea.com/webpay/payment";
        private const string SwpTestWsUrl = "https://webpaywsstage.svea.com/SveaWebPay.asmx?WSDL";
        private const string SwpProdWsUrl = "https://webpayws.svea.com/SveaWebPay.asmx?WSDL";
        private const string SwpTestAdminWsUrl = "https://webpayadminservicestage.svea.com/AdminService.svc" + "/secure"; // make sure to include "/secure" part
        private const string SwpProdAdminWsUrl = "https://webpayadminservice.svea.com/AdminService.svc" + "/secure"; // make sure to include "/secure" part

        public static string GetProdWebserviceUrl()
        {
            return SwpProdWsUrl;
        }

        public static string GetProdAdminServiceUrl()
        {
            return SwpProdAdminWsUrl;
        }

        public static string GetProdPayPageUrl()
        {
            return SwpProdUrl;
        }

        public static string GetTestHostedAdminUrl()
        {
            return HostedTestAdminBaseUrl;
        }

        public static string GetProdHostedAdminUrl()
        {
            return HostedProdAdminBaseUrl;
        }

        public static string GetTestWebserviceUrl()
        {
            return SwpTestWsUrl;
        }

        public static string GetTestAdminServiceUrl()
        {
            return SwpTestAdminWsUrl;
        }

        public static string GetTestPayPageUrl()
        {
            return SwpTestUrl;
        }

        public static IConfigurationProvider GetDefaultConfig()
        {
            return new SveaTestConfigurationProvider();
        }
    }
}