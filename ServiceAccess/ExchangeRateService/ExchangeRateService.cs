using DataModel.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAccess.ExchangeRateService
{
    public static class ExchangeRateService
    {
        public async static Task<ExchangeRateHistoryServiceResponseModel> GetExchangeRateHistory(string startat, string endat, string baseCurrency, string symbol)
        {
            // call to app.config to get url           
            string url = string.Format(ConfigurationManager.AppSettings["ExchangeRateServiceURL"] +
                "history?start_at={0}&end_at={1}&base={2}&symbols={3}", startat, endat, baseCurrency.ToUpper(), symbol.ToUpper());

            var _json = await RestRequestHelper.Get(url);
            return JsonConvert.DeserializeObject<ExchangeRateHistoryServiceResponseModel>(_json);
        }

        public async static Task<ExchangeRateRatePerDateServiceResponseModel> GetExchangeRateForSelectedDate(string date, string baseCurrency, string symbol)
        {
            // https://api.exchangeratesapi.io/2018-05-03?base=SEK&symbols=NOK
            // call to app.config to get url           
            string url = string.Format(ConfigurationManager.AppSettings["ExchangeRateServiceURL"] +
                "{0}?base={1}&symbols={2}", date, baseCurrency.ToUpper(), symbol.ToUpper());

            var _json = await RestRequestHelper.Get(url);
            return JsonConvert.DeserializeObject<ExchangeRateRatePerDateServiceResponseModel>(_json);
        }
    }
}
