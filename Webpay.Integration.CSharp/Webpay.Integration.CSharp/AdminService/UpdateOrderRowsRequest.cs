﻿using System.Linq;
using System.ServiceModel;
using Webpay.Integration.CSharp.AdminWS;
using Webpay.Integration.CSharp.Order.Handle;
using Webpay.Integration.CSharp.Util.Constant;

namespace Webpay.Integration.CSharp.AdminService
{
    public class UpdateOrderRowsRequest : WebpayAdminRequest
    {
        private readonly UpdateOrderRowsBuilder _builder;

        public UpdateOrderRowsRequest(UpdateOrderRowsBuilder builder)
        {
            _builder = builder;
        }

        public Webpay.Integration.CSharp.AdminWS.UpdateOrderRowsResponse DoRequest()
        {
            var auth = new AdminWS.Authentication()
            {
                Password = _builder.GetConfig().GetPassword(_builder.OrderType,_builder.GetCountryCode()),
                Username = _builder.GetConfig().GetUsername(_builder.OrderType,_builder.GetCountryCode())                
            };

            var request = new AdminWS.UpdateOrderRowsRequest()
            {
                Authentication = auth,
                SveaOrderId = _builder.Id,
                OrderType = ConvertPaymentTypeToOrderType(_builder.OrderType),
                ClientId = _builder.GetConfig().GetClientNumber(_builder.OrderType, _builder.GetCountryCode()),
                UpdatedOrderRows = _builder.NumberedOrderRows.Select(ConvertNumberedOrderRowBuilderToAdminWSNumberedOrderRow).ToArray()
            };

            // make request to correct endpoint, return response object
            var endpoint = _builder.GetConfig().GetEndPoint(PaymentType.ADMIN_TYPE);
            //var adminWS = new AdminServiceClient("WcfAdminSoapService", endpoint);
            var wsBinding = new WSHttpBinding();
            wsBinding.Name = "WcfAdminSoapService";
            wsBinding.Security.Mode = SecurityMode.Transport;
            var adminWS = new AdminServiceClient(wsBinding,new EndpointAddress(endpoint));
            var response = adminWS.UpdateOrderRows(request);

            return response;
        }
    }
}