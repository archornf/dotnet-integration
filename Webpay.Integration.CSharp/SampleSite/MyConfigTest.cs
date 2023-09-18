﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Webpay.Integration.CSharp.Config;
using Webpay.Integration.CSharp.Util.Constant;

namespace SampleSite
{
    internal class MyConfigTest : Webpay.Integration.CSharp.Config.IConfigurationProvider
    {

        private const string myUserName = "";
        private const string myPassword = "";
        private const int myClientNumber = 00000;
        private const string myMerchantId = "123";
        private const string mySecretWord = "asd";

        internal MyConfigTest() { }

        /// <summary>
        /// Get the return value from your database or likewise
        /// </summary>
        /// <param name="type"> eg. HOSTED, INVOICE or PAYMENTPLAN</param>
        /// <param name="country">country code</param>
        /// <returns>user name string</returns>
        public string GetUsername(PaymentType type, CountryCode country)
        {
            return myUserName;
        }

        /// <summary>
        /// Get the return value from your database or likewise
        /// </summary>
        /// <param name="type"> eg. HOSTED, INVOICE or PAYMENTPLAN</param>
        /// <param name="country">country code</param>
        /// <returns>password string</returns>
        public string GetPassword(PaymentType type, CountryCode country)
        {
            return myPassword;
        }

        /// <summary>
        /// Get the return value from your database or likewise
        /// </summary>
        /// <param name="type"> eg. HOSTED, INVOICE or PAYMENTPLAN</param>
        /// <param name="country">country code</param>
        /// <returns>client number int</returns>
        public int GetClientNumber(PaymentType type, CountryCode country)
        {
            return myClientNumber;
        }

        /// <summary>
        /// Get the return value from your database or likewise
        /// </summary>
        /// <param name="type"> eg. HOSTED, INVOICE or PAYMENTPLAN</param>
        /// <param name="country">country code</param>
        /// <returns>merchant id string</returns>
        public string GetMerchantId(PaymentType type, CountryCode country)
        {
            return myMerchantId;
        }

        /// <summary>
        /// Get the return value from your database or likewise
        /// </summary>
        /// <param name="type"> eg. HOSTED, INVOICE or PAYMENTPLAN</param>
        /// <param name="country">country code</param>
        /// <returns>secret word string</returns>
        public string GetSecretWord(PaymentType type, CountryCode country)
        {
            return mySecretWord;
        }

        /// <summary>
        /// Constants for the end point url found in the class SveaConfig
        /// </summary>
        /// <param name="type"> eg. HOSTED, INVOICE or PAYMENTPLAN</param>
        /// <returns>end point url</returns>
        public string GetEndPoint(PaymentType type)
        {
            return type == PaymentType.HOSTED ? SveaConfig.GetTestPayPageUrl() : SveaConfig.GetTestWebserviceUrl();
        }
    }
}
