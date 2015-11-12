using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GangsterBank.Web.Infrastructure.Validators
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class ImageOnlyAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            try
            {
                var image = (HttpPostedFileBase)value;

                if ((image.ContentType != "image/gif") && (image.ContentType != "image/jpeg")
                    && (image.ContentType != "image/png") && (image.ContentType != "image/svg+xml")
                    && (image.ContentType != "image/vnd.microsoft.icon") && (image.ContentType != "image/vnd.wap.wbmp")
                    && (image.ContentType != "image/tiff")) return false;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }

    }
}