using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace AssignmentApi.Models.Input
{
    [Validator(typeof(ExchangeRateSelectedDatesInputValidator))]
    public class ExchangeRateSelectedDatesInput
    {
        public string BaseCurrency { get; set; }

        public string TargetCurrency { get; set; }

        public string Dates { get; set; }
    }


    public class ExchangeRateSelectedDatesInputValidator : AbstractValidator<ExchangeRateSelectedDatesInput>
    {
        public ExchangeRateSelectedDatesInputValidator()
        {
            RuleFor(x => x.BaseCurrency).NotEmpty().WithMessage("Base currency is required.").Length(3).WithMessage("Base currency length should be 3.");

            RuleFor(x => x.TargetCurrency).NotEmpty().WithMessage("Target currency is required.").Length(3).WithMessage("Target currency length should be 3.");

            RuleFor(x => x.Dates).NotEmpty().WithMessage("Comma separated dates in YYYY-MM-DD format is required.").
                Must(CommaSeparatedDateFormatValidator).WithMessage("Comma separated dates in YYYY-MM-DD format expected.");
        }

        public bool CommaSeparatedDateFormatValidator(string dates)
        {
            string[] datesString = dates.ToString().Split(',');
            DateTime?[] datesDatetime = datesString.ToList().Select(x => ConvertToDate(x)).ToArray();
            if (datesDatetime.Any(x => !x.HasValue))
                return false;
            else
                return true;
        }

        private DateTime? ConvertToDate(string dateString)
        {
            DateTime outputDate;
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out outputDate))
                return outputDate;
            else
                return null;
        }

    }
}
