using AssignmentApi.Models.Input;
using BusinessAPI;
using DataModel.API.ExchangeRate.Output;
using log4net;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace AssignmentApi.Controllers
{
    [Route("api/ExchangeRate")]
    public class ExchangeRateController : ApiController
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        [SwaggerOperation("GetExchangeRateHistory")]
        [Route("api/ExchangeRate/GetExchangeRateHistory")]
        public IHttpActionResult GetExchangeRateHistory([FromUri]ExchangeRateInput exchangeRateInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                ExchangeRateOutputModel exchangeRateStatisticsForDateRange = ExchangeRateBusiness.GetExchangeRateStatistics(exchangeRateInput.StartDate, exchangeRateInput.EndDate,
                    exchangeRateInput.BaseCurrency, exchangeRateInput.TargetCurrency);
                return Ok(exchangeRateStatisticsForDateRange);
            }
            catch (Exception ex)
            {
                log.Error("GetExchangeRateHistory failed to serve the results", ex);
                return BadRequest();
            }
        }


        [HttpGet]
        [SwaggerOperation("GetExchangeRateForMultipleDates")]
        [Route("api/ExchangeRate/GetExchangeRateForMultipleDates")]
        public IHttpActionResult GetExchangeRateForMultipleDates([FromUri]ExchangeRateSelectedDatesInput exchangeRateInput)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                ExchangeRateOutputModel exchangeRateStatisticsForDates = ExchangeRateBusiness.GetExchangeRateFoDateSetStatistics(exchangeRateInput.Dates, exchangeRateInput.BaseCurrency,
                    exchangeRateInput.TargetCurrency);
                return Ok(exchangeRateStatisticsForDates);
            }
            catch (Exception ex)
            {
                log.Error("GetExchangeRateForMultipleDates failed to serve the results", ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [SwaggerOperation("GetExchangeRateHistoryAsync")]
        [Route("api/ExchangeRate/GetExchangeRateHistoryAsync")]
        public async Task<IHttpActionResult> GetExchangeRateHistoryAsync([FromUri]ExchangeRateInput exchangeRateInput)
        {
            return await Task.FromResult(GetExchangeRateHistory(exchangeRateInput));
        }

        [HttpGet]
        [SwaggerOperation("GetExchangeRateForMultipleDatesAsync")]
        [Route("api/ExchangeRate/GetExchangeRateForMultipleDatesAsync")]
        public async Task<IHttpActionResult> GetExchangeRateForMultipleDatesAsync([FromUri]ExchangeRateSelectedDatesInput exchangeRateInput)
        {
            return await Task.FromResult(GetExchangeRateForMultipleDates(exchangeRateInput));
        }
    }
}
