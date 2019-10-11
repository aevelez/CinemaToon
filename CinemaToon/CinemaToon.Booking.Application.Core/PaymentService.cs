using CinemaToon.Entities.Payment;
using CinemaToon.Utilities.Abstractions.Interfaces;
using System;

namespace CinemaToon.Booking.Application.Core
{
    public class PaymentService : IPaymentService
    {
        private readonly IAPIClient _apiClient;
        private readonly PaymentSettings _paymentSettings;

        public PaymentService(IAPIClient aPIClient, PaymentSettings paymentSettings)
        {
            _apiClient = aPIClient;
            _paymentSettings = paymentSettings;
        }

        public string Pay(string user, double total)
        {
            return _apiClient.Get(new Uri($"{_paymentSettings.Url}/{user}/{total}"));
        }
    }
}
