using DataModel.API.ExchangeRate;
using DataModel.API.ExchangeRate.Output;
using DataModel.Service;
using ServiceAccess.ExchangeRateService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI
{
    public static class ExchangeRateBusiness
    {
        public static ExchangeRateOutputModel GetExchangeRateStatistics(string startDate, string endDate, string baseCurrency, string targetCurrency)
        {
            int retryCounter = 0;
            retry:
            try
            {
                ExchangeRateHistoryServiceResponseModel exchangeRateHistory = ExchangeRateService.GetExchangeRateHistory(startDate, endDate, baseCurrency,
                    targetCurrency).Result;

                List<ExchangeRateDateModel> exchangeRateDateModelList = exchangeRateHistory.rates.Select(x => new ExchangeRateDateModel
                {
                    Date = x.Key,
                    Rate = x.Value.ContainsKey(targetCurrency.ToUpper()) ?
                    x.Value.Where(y => y.Key.Equals(targetCurrency, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value : 0
                }).OrderBy(z => z.Rate).ToList();

                return GetCurrencyStatisticsDataLogic(exchangeRateDateModelList);
            }
            catch (Exception exService)
            {
                retryCounter++;
                if (retryCounter <= 2)
                    goto retry;
                else
                {
                    throw exService;
                }
            }
        }

        public static ExchangeRateOutputModel GetExchangeRateFoDateSetStatistics(string commaSeparatedDates, string baseCurrency, string targetCurrency)
        {
            List<DateTime?> dateSetList = commaSeparatedDates.GetDateSetArray();
            List<ExchangeRateDateModel> exchangeRateDateModelList = new List<ExchangeRateDateModel>();
            Parallel.For(0, dateSetList.Count, new ParallelOptions { MaxDegreeOfParallelism = 50 }, (i, state) =>
              {
                  int retryCounter = 0;
                  retry:
                  try
                  {
                      ExchangeRateRatePerDateServiceResponseModel exchangeRateHistory = ExchangeRateService.GetExchangeRateForSelectedDate(dateSetList[i].Value.ToString("yyyy-MM-dd"), baseCurrency,
                            targetCurrency).Result;
                      exchangeRateDateModelList.Add(new ExchangeRateDateModel
                      {
                          Date = dateSetList[i].Value.ToString("yyyy-MM-dd"),
                          Rate = exchangeRateHistory.rates.Values.FirstOrDefault()
                      });
                  }
                  catch (Exception exService)
                  {
                      retryCounter++;
                      if (retryCounter <= 2)
                          goto retry;
                      else
                      {
                          state.Stop();
                          throw exService;
                      }
                  }
              });
            if (exchangeRateDateModelList.Count == dateSetList.Count)
            {
                exchangeRateDateModelList = exchangeRateDateModelList.OrderBy(x => x.Rate).ToList();

                return GetCurrencyStatisticsDataLogic(exchangeRateDateModelList);
            }
            else
            {
                return null;
            }
        }

        private static List<DateTime?> GetDateSetArray(this string value)
        {
            string[] datesString = value.ToString().Split(',');
            DateTime?[] datesDatetime = datesString.ToList().Select(x => x.ConvertToDate()).ToArray();
            if (datesDatetime.Any(x => !x.HasValue))
                return null;
            else
                return datesDatetime.ToList();
        }

        private static DateTime? ConvertToDate(this string dateString)
        {
            DateTime outputDate;
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out outputDate))
                return outputDate;
            else
                return null;
        }

        private static ExchangeRateOutputModel GetCurrencyStatisticsDataLogic(List<ExchangeRateDateModel> exchangeRateDateModelList)
        {
            return new ExchangeRateOutputModel
            {
                MinRate = exchangeRateDateModelList.FirstOrDefault().Rate,
                MinRateOnDate = exchangeRateDateModelList.FirstOrDefault().Date,
                MaxRateOnDate = exchangeRateDateModelList.LastOrDefault().Date,
                MaxRate = exchangeRateDateModelList.LastOrDefault().Rate,
                AverageRate = exchangeRateDateModelList.Average(x => x.Rate)
            };
        }
    }
}
