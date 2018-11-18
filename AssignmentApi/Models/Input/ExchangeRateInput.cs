using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AssignmentApi.Models.Input
{
    [Validator(typeof(ExchangeRateInputValidator))]
    public class ExchangeRateInput
    {
        public string BaseCurrency { get; set; }

        public string TargetCurrency { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }

    public class ExchangeRateInputValidator : AbstractValidator<ExchangeRateInput>
    {
        public ExchangeRateInputValidator()
        {
            RuleFor(x => x.BaseCurrency).NotEmpty().WithMessage("Base currency is required.").Length(3).WithMessage("Base currency length should be 3.");

            RuleFor(x => x.TargetCurrency).NotEmpty().WithMessage("Target currency is required.").Length(3).WithMessage("Target currency length should be 3.");

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date in YYYY-MM-DD format is required.").
                Must(DateFormatValidator).WithMessage("Start date in YYYY-MM-DD format is expected.");

            RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date in YYYY-MM-DD format is required.").
                Must(DateFormatValidator).WithMessage("End date in YYYY-MM-DD format is expected.");

            RuleFor(z => z).Must(DateComparer).WithMessage("End date should be greater than start date.");
        }

        private bool DateComparer(ExchangeRateInput exchangeRateInput)
        {
            DateTime startDate;
            DateTime.TryParseExact(exchangeRateInput.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

            DateTime endDate;
            DateTime.TryParseExact(exchangeRateInput.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);

            return startDate < endDate;
        }

        private bool DateFormatValidator(string dateString)
        {
            DateTime outputDate;
            return DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out outputDate);
        }
    }
}
