using Stage;

using Webpay.Integration.CSharp.Config;
using Webpay.Integration.CSharp;
using Webpay.Integration.CSharp.Order.Row;
using Webpay.Integration.CSharp.Util.Testing;
using Webpay.Integration.CSharp.WebpayWS;

namespace SveaWebpayIntegration
{
    public class Program
    {
        //public static void Main()
        public static void MainOld()
        {
            Console.WriteLine("Start");
            //MyConfigTest confTest = new MyConfigTest();
            IConfigurationProvider confTest = new MyConfigTest();
            OrderTest orderTest = new OrderTest(confTest);
            orderTest.TestOrder();
        }
    }
}