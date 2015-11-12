namespace GangsterBank.Web.Infrastructure.ModelBinders
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using GangsterBank.Models.Kendo;
    using GangsterBank.Web.Models.Kendo;

    public class AutoCompleteSourceRequestBinder : IModelBinder
    {
        #region Constants

        private const string IgnoreCaseParameterName = "filter[filters][0][ignoreCase]";

        private const string OperatorParameterName = "filter[filters][0][operator]";

        private const string ValueParameterName = "filter[filters][0][value]";

        #endregion

        #region Public Methods and Operators

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            string valueParam = request.Params.Get(ValueParameterName);
            string operatorParam = request.Params.Get(OperatorParameterName);
            string ignoreCaseParam = request.Params.Get(IgnoreCaseParameterName);

            var autoCompleteOperator =
                (AutoCompleteOperator)Enum.Parse(typeof(AutoCompleteOperator), operatorParam, true);
            bool ignoreCase = bool.Parse(ignoreCaseParam);

            return new AutoCompleteSourceRequest
                       {
                           Value = valueParam, 
                           Operator = autoCompleteOperator, 
                           IgnoreCase = ignoreCase
                       };
        }

        #endregion
    }
}