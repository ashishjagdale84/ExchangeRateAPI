using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Service
{
    public class ExchangeRateRatePerDateServiceResponseModel
    {
        public string date { get; set; }
        public Dictionary<string, double> rates { get; set; }
    }
}
