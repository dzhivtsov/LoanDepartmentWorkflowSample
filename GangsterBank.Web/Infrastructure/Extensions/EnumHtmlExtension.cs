using System;
using System.Linq;

namespace GangsterBank.Web.Infrastructure.Extensions
{
    using System.Web.Mvc;

    public static class EnumHtmlExtension
    {

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            var values = from Enum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };

            return new SelectList(values, "Id", "Name");
        }
    }
}