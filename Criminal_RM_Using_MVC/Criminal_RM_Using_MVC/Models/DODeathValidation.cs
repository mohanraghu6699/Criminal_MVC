﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Criminal_RM_Using_MVC.Models
{
    public class DODeathValidation:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime CurrentDate = DateTime.Now;
            DateTime dobvalue = Convert.ToDateTime(value);
            string message = "";

            if (Convert.ToDateTime(value) > CurrentDate)
            {

                message = "DODeath should not be greater than current Date";
                return new ValidationResult(message);
            }
            return ValidationResult.Success;
        }
    }
}