using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credit_Card_Fraud_Detection_Project
{
    public class PaymentModel
    {
        public string cardnumber { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public string cvc { get; set; }
        public int value { get; set; }

    }
}
