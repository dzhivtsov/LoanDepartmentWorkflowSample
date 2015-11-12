using GangsterBank.Web.Infrastructure.Contexts;

namespace GangsterBank.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Web.Mvc;

    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.BusinessLogic.Contracts.Info;
    using GangsterBank.Core.Extensions;
    using GangsterBank.Models.Kendo;

    public class GeoInfoController : BaseController
    {
        #region Fields

        private readonly ICitiesService citiesService;

        private readonly ICountriesService countriesService;

        #endregion

        #region Constructors and Destructors

        public GeoInfoController(ICitiesService citiesService,
            IUserContext userContext,
            ICountriesService countriesService)
            : base(userContext)
        {
            Contract.Requires<ArgumentNullException>(citiesService.IsNotNull());
            Contract.Requires<ArgumentNullException>(countriesService.IsNotNull());

            this.citiesService = citiesService;
            this.countriesService = countriesService;
        }

        #endregion

        #region Public Methods and Operators

        public JsonResult Cities(AutoCompleteSourceRequest request)
        {
            Contract.Requires<ArgumentNullException>(request.IsNotNull());
            IEnumerable<string> cityNames = this.citiesService.SearchCityNames(request.Value);
            return this.Json(cityNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Countries(AutoCompleteSourceRequest request)
        {
            Contract.Requires<ArgumentNullException>(request.IsNotNull());
            IEnumerable<string> countryNames = this.countriesService.SearchCountryNames(request.Value);
            return this.Json(countryNames, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}