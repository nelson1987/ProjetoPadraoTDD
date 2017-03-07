using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebForDocs.Biblioteca
{
    public class DataAnnotationCustom
    {
    }

    public class ValidarData : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt;

            return DateTime.TryParse((string)value, out dt);
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = "Data Inválida"; // default message
            }

            return base.FormatErrorMessage(name);
        }
    }
}