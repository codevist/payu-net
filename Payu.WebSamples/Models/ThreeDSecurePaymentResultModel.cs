using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payu.WebSamples.Models
{
    public class ThreeDSecurePaymentResultModel
    {
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string RefNo { get; set; }
        public string Status { get; set; }
    }
}