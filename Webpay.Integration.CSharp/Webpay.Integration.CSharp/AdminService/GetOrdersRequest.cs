﻿using System.ServiceModel;
using System.Runtime.InteropServices;
using Webpay.Integration.CSharp.AdminWS;
using Webpay.Integration.CSharp.Exception;
using Webpay.Integration.CSharp.Order.Handle;
using Webpay.Integration.CSharp.Util.Constant;
using OrderType = Webpay.Integration.CSharp.AdminWS.OrderType;

namespace Webpay.Integration.CSharp.AdminService
{
    public class GetOrdersRequest : WebpayAdminRequest
    {
        private readonly QueryOrderBuilder _builder;

        public GetOrdersRequest(QueryOrderBuilder builder)
        {
            _builder = builder;
        }

        public Webpay.Integration.CSharp.AdminWS.GetOrdersResponse DoRequest()
        {
            var auth = new Webpay.Integration.CSharp.AdminWS.Authentication()
            {
                Password = _builder.GetConfig().GetPassword(_builder.OrderType,_builder.GetCountryCode()),
                Username = _builder.GetConfig().GetUsername(_builder.OrderType,_builder.GetCountryCode())                
            };

            var request = new Webpay.Integration.CSharp.AdminWS.GetOrdersRequest()
            {
                Authentication = auth,
                OrdersToRetrieve = new[]
                {
                    new GetOrderInformation()
                    {
                        SveaOrderId = _builder.Id,
                        OrderType = ConvertPaymentTypeToOrderType(_builder.OrderType),
                        ClientId = _builder.GetConfig().GetClientNumber(_builder.OrderType, _builder.GetCountryCode())
                    }
                }
            };

            // make request to correct endpoint, return response object
            var endpoint = _builder.GetConfig().GetEndPoint(PaymentType.ADMIN_TYPE);
            //var adminWS = new AdminServiceClient("WcfAdminSoapService", endpoint);
            var wsBinding = new WSHttpBinding();
            wsBinding.Name = "WcfAdminSoapService";
            wsBinding.Security.Mode = SecurityMode.Transport;
            var adminWS = new AdminServiceClient(wsBinding,new EndpointAddress(endpoint));
            var response = adminWS.GetOrders(request);

            return response;
        }
    }
}