using Stage;

using Webpay.Integration.CSharp.Config;
using Webpay.Integration.CSharp;

namespace SveaWebpayIntegration
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Start");
            //MyConfigTest confTest = new MyConfigTest();
            IConfigurationProvider confTest = new MyConfigTest();
            OrderTest orderTest = new OrderTest(confTest);
            orderTest.TestOrder();
        }
    }
}