using System.ComponentModel.DataAnnotations;

namespace DataModel.API.ExchangeRate.Output
{
    public class ExchangeRateOutputModel
    {
        public string MinRateOnDate { get; set; }
        public string MaxRateOnDate { get; set; }
        public double MinRate { get; set; }
        public double MaxRate { get; set; }
        public double AverageRate { get; set; }
    }
}
