using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.CustomDataAnnotations
{
    public class DateValidator : ValidationAttribute
    {
        public DateValidator() { }
        // Return true for valid data, otherwise return false
        public override bool IsValid(object value)
        {
            var date = (DateTime)value;
            if (date > DateTime.Now.Date.AddYears(-1) && date <= DateTime.Now.Date.AddYears(10))
            {
                return true;
            }
            return false;
        }
    }
}