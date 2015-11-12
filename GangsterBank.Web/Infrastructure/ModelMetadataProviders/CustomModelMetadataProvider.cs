namespace GangsterBank.Web.Infrastructure.ModelMetadataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        #region Constants

        private const string EnumTemplateHint = "EnumDropdown";

        #endregion

        #region Methods

        protected override ModelMetadata CreateMetadata(
            IEnumerable<Attribute> attributes, 
            Type containerType, 
            Func<object> modelAccessor, 
            Type modelType, 
            string propertyName)
        {
            ModelMetadata result = base.CreateMetadata(
                attributes, 
                containerType, 
                modelAccessor, 
                modelType, 
                propertyName);
            SetEnumTemplateHint(result);
            return result;
        }

        private static void SetEnumTemplateHint(ModelMetadata result)
        {
            if (result.TemplateHint == null
                && typeof(Enum).IsAssignableFrom(Nullable.GetUnderlyingType(result.ModelType) ?? result.ModelType))
            {
                result.TemplateHint = EnumTemplateHint;
            }
        }

        #endregion
    }
}