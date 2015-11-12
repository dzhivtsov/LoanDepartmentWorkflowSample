namespace GangsterBank.Web.Infrastructure.MetadataAttributes
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Web.Mvc;

    using GangsterBank.Core.Extensions;

    public class AutoCompleteAttribute : Attribute, IMetadataAware
    {
        #region Constants

        public const string AutoCompleteActionKey = "AutoCompleteAction";

        public const string AutoCompleteControllerKey = "AutoCompleteController";

        private const string ControllerNamingConventionEnding = "Controller";

        private const string AutoCompleteTemplateName = "Autocomplete";

        #endregion

        #region Fields

        private readonly string actionName;

        private readonly string controllerName;

        #endregion

        #region Constructors and Destructors

        public AutoCompleteAttribute(Type controllerType, string actionName)
        {
            Contract.Requires<ArgumentNullException>(controllerType.IsNotNull());
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(actionName));
            this.controllerName = GetControllerName(controllerType);
            this.actionName = actionName;
        }

        #endregion

        #region Public Methods and Operators

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues[AutoCompleteControllerKey] = this.controllerName;
            metadata.AdditionalValues[AutoCompleteActionKey] = this.actionName;
            metadata.TemplateHint = AutoCompleteTemplateName;
        }

        #endregion

        #region Methods

        private static string GetControllerName(Type controllerType)
        {
            string controllerTypeName = controllerType.Name;
            if (!controllerTypeName.EndsWith(ControllerNamingConventionEnding))
            {
                return controllerTypeName;
            }

            int controllerNameLength = controllerTypeName.Length - ControllerNamingConventionEnding.Length;
            return controllerTypeName.Substring(0, controllerNameLength);
        }

        #endregion
    }
}