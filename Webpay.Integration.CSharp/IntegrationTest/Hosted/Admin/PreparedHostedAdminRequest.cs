using Webpay.Integration.CSharp.Config;
using Webpay.Integration.CSharp.Hosted.Helper;
using Webpay.Integration.CSharp.Util.Constant;

namespace Webpay.Integration.CSharp.IntegrationTest.Hosted.Admin
{
    public class PreparedHostedAdminRequest
    {
        public readonly string Xml;
        public readonly CountryCode CountryCode;
        public readonly string MerchantId;
        public readonly IConfigurationProvider ConfigurationProvider;
        public readonly string ServicePath;

        public PreparedHostedAdminRequest(string xml, CountryCode countryCode, string merchantId, IConfigurationProvider configurationProvider, string servicePath)
        {
            Xml = xml;
            CountryCode = countryCode;
            MerchantId = merchantId;
            ConfigurationProvider = configurationProvider;
            ServicePath = servicePath;
        }

        public HostedAdminResponse DoRequest()
        {
            return HostedAdminRequest.HostedAdminCall(GetEndPointBase(), Request());
        }

        private string GetEndPointBase()
        {
            var endPoint = ConfigurationProvider.GetEndPoint(PaymentType.HOSTED);
            var baseUrl = endPoint.Replace("/payment", "");

            var targetAddress = baseUrl + "/rest" + ServicePath;
            return targetAddress;
        }

        public HostedAdminRequest Request()
        {
            return new HostedAdminRequest(Xml, ConfigurationProvider.GetSecretWord(PaymentType.HOSTED, CountryCode), MerchantId);
        }
    }
}