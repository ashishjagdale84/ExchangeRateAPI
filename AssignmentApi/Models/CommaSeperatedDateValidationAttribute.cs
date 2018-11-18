using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApi.Models
{
    internal class CommaSeperatedDateValidationAttribute : ValidationAttribute
    {
        public CommaSeperatedDateValidationAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            string[] datesString = value.ToString().Split(',');
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
