namespace GangsterBank.Web.Infrastructure.Helpers.HtmlHelperExtensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    using GangsterBank.Core.Extensions;
    using GangsterBank.Domain.Exceptions;

    using Kendo.Mvc.UI;

    public static class EnumHtmlHelperExtensions
    {
        #region Public Methods and Operators

        public static IHtmlString EnumDropDownListFor<TModel, TEnum>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TEnum>> expression)
        {
            Contract.Requires<ArgumentNullException>(htmlHelper.IsNotNull());
            Contract.Requires<ArgumentNullException>(expression.IsNotNull());

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;
            IEnumerable<Enum> enumValues = Enum.GetValues(enumType).Cast<Enum>();
            IEnumerable<SelectListItem> items = GetSelectListItems(enumValues, metadata);
            
            return htmlHelper.DropDownListFor(expression, items, string.Empty, null);
        }

        #endregion

        #region Methods

        private static DisplayAttribute GetDisplayAttribute(Enum enumValue)
        {
            DisplayAttribute displayAttribute =
                enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .Single()
                    .GetCustomAttributes(false)
                    .OfType<DisplayAttribute>()
                    .SingleOrDefault();
            if (displayAttribute.IsNull())
            {
                throw new NotFoundException();
            }

            return displayAttribute;
        }

        private static string GetDisplayName(Enum enumValue)
        {
            DisplayAttribute displayAttribute;
            try
            {
                displayAttribute = GetDisplayAttribute(enumValue);
            }
            catch (NotFoundException)
            {
                return enumValue.ToString();
            }

            return displayAttribute.GetName();
        }

        private static IEnumerable<SelectListItem> GetSelectListItems(
            IEnumerable<Enum> enumValues, 
            ModelMetadata metadata)
        {
            IEnumerable<SelectListItem> items = from enumValue in enumValues
                                                select
                                                    new SelectListItem
                                                        {
                                                            Text = GetDisplayName(enumValue), 
                                                            Value = enumValue.ToInvariantCultureString(), 
                                                            Selected = enumValue.Equals(metadata.Model)
                                                        };
            return items;
        }

        #endregion
    }
}