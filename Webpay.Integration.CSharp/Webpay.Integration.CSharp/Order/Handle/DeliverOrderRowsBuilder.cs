﻿using System;
using System.Collections.Generic;
using Webpay.Integration.CSharp.Config;
using Webpay.Integration.CSharp.Order.Row;
using Webpay.Integration.CSharp.Util.Constant;

namespace Webpay.Integration.CSharp.Order.Handle
{
    public class DeliverOrderRowsBuilder : Builder<DeliverOrderRowsBuilder>
    {
        internal long Id { get; private set; }
        internal PaymentType OrderType { get; set; }
        internal DateTime? CaptureDate { get; private set; }
        internal DistributionType DistributionType { get; private set; }
        internal List<long> RowIndexesToDeliver { get; private set; }
        internal List<NumberedOrderRowBuilder> NumberedOrderRows { get; private set; }
        internal string CallerReferenceId { get; private set; }

        public DeliverOrderRowsBuilder(IConfigurationProvider config) : base(config)
        {
            this.CaptureDate = null;
            this.RowIndexesToDeliver = new List<long>();
            this.NumberedOrderRows = new List<NumberedOrderRowBuilder>();
        }

        public DeliverOrderRowsBuilder SetOrderId(long orderId)
        {
            Id = orderId;
            return this;
        }

        public DeliverOrderRowsBuilder SetCallerReferenceId(long CallerReferenceId)
        {
            CallerReferenceId = CallerReferenceId;
            return this;
        }

        public DeliverOrderRowsBuilder SetTransactionId(long orderId)
        {
            return SetOrderId(orderId);
        }

        public override DeliverOrderRowsBuilder SetCountryCode(CountryCode countryCode)
        {
            _countryCode = countryCode;
            return this;
        }

        public DeliverOrderRowsBuilder SetInvoiceDistributionType(DistributionType distributionType)
        {
            DistributionType = distributionType;
            return this;
        }

        public DeliverOrderRowsBuilder SetRowToDeliver( long rowIndexToDeliver )
        {
            RowIndexesToDeliver.Add(rowIndexToDeliver);
            return this;
        }

        public DeliverOrderRowsBuilder AddNumberedOrderRows(IList<NumberedOrderRowBuilder> numberedOrderRows)
        {
            NumberedOrderRows.AddRange(numberedOrderRows);
            return this;
        }

        public AdminService.DeliverOrderRowsRequest DeliverInvoiceOrderRows()
        {
            OrderType = PaymentType.INVOICE;
            return new AdminService.DeliverOrderRowsRequest(this);
        }

        public AdminService.ConfirmTransactionRequest DeliverCardOrderRows()
        {
            return new AdminService.ConfirmTransactionRequest(this);
        }
    }
}