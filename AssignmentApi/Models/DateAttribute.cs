﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssignmentApi.Models
{
    internal class DateAttribute : RegularExpressionAttribute
    {
        internal DateAttribute() :
            base(@"^(((\d{4}-((0[13578]-|1[02]-)(0[1-9]|[12]\d|3[01])|(0[13456789]-|1[012]-)(0[1-9]|[12]\d|30)|02-(0[1-9]|1\d|2[0-8])))|((([02468][048]|[13579] [26])00|\d{2}([13579][26]|0[48]|[2468] [048])))-02-29)){0,10}$")
        { }
    }
}
