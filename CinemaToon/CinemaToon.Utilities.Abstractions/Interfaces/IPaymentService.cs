using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaToon.Utilities.Abstractions.Interfaces
{
    public interface IPaymentService
    {
        string Pay(string user, double total);
    }
}
