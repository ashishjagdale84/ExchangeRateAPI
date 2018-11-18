using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Service
{
    public class ExchangeRateHistoryServiceResponseModel
    {
        public string start_at { get; set; }
        public string end_at { get; set; }
        public Dictionary<string, Dictionary<string, double>> rates { get; set; }
    }
}
