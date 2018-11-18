using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using AssignmentApi.Controllers;
using AssignmentApi.Models.Input;
using DataModel.API.ExchangeRate.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssignmentApi.Tests
{
    [TestClass]
    public class ExchangeRateControllerTest
    {
        [TestMethod]
        public void GetExchangeRateHistory_ValidResult()
        {
            ExchangeRateInput exchangeRateInput = GetExchangeRateHistoryInputModel();

            var exchangeRateController = new ExchangeRateController();
            var result = exchangeRateController.GetExchangeRateHistory(exchangeRateInput);
            var contentResult = ((OkNegotiatedContentResult<ExchangeRateOutputModel>)(result));

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);

        }

        [TestMethod]
        public void GetExchangeRateHistory_BadResultWithEmptyEndDate()
        {
            ExchangeRateInput exchangeRateInput = GetExchangeRateHistoryInputModel();
            exchangeRateInput.EndDate = string.Empty;
            var exchangeRateController = new ExchangeRateController();
            var result = exchangeRateController.GetExchangeRateHistory(exchangeRateInput);
            var contentResult = result as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetExchangeRateForMultipleDates_ValidResult()
        {
            ExchangeRateSelectedDatesInput exchangeRateInput = GetExchangeRateForMultipleDatesInputModel();

            var exchangeRateController = new ExchangeRateController();
            var result = exchangeRateController.GetExchangeRateForMultipleDates(exchangeRateInput);
            var contentResult = ((OkNegotiatedContentResult<ExchangeRateOutputModel>)(result));

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);

        }

        [TestMethod]
        public void GetExchangeRateForMultipleDates_BadResultWithEmptyEndDate()
        {
            ExchangeRateSelectedDatesInput exchangeRateInput = GetExchangeRateForMultipleDatesInputModel();
            exchangeRateInput.Dates = string.Empty;
            var exchangeRateController = new ExchangeRateController();
            var result = exchangeRateController.GetExchangeRateForMultipleDates(exchangeRateInput);
            var contentResult = result as BadRequestResult;

            Assert.IsNotNull(result);
        }

        private ExchangeRateSelectedDatesInput GetExchangeRateForMultipleDatesInputModel()
        {
            return new ExchangeRateSelectedDatesInput
            {
                BaseCurrency = "SEK",
                TargetCurrency = "NOK",
                Dates = "2018-02-01,2018-02-15,2018-03-01"
            };
        }

        private ExchangeRateInput GetExchangeRateHistoryInputModel()
        {
            ExchangeRateInput exchangeRateInput = new ExchangeRateInput
            {
                BaseCurrency = "SEK",
                TargetCurrency = "NOK",
                StartDate = "2018-07-01",
                EndDate = "2018-09-01"
            };

            return exchangeRateInput;
        }
    }
}
