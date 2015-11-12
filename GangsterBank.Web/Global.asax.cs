namespace GangsterBank.Web
{
    #region

    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Autofac;
    using Autofac.Integration.Mvc;

    using GangsterBank.Models.Kendo;
    using GangsterBank.Web.Infrastructure.ModelBinders;
    using GangsterBank.Web.Infrastructure.ModelMetadataProviders;
    using GangsterBank.Web.Infrastructure.ValidatonAttributes;

    #endregion

    public class MvcApplication : HttpApplication
    {
        #region Methods

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelMetadataProviders.Current = new CustomModelMetadataProvider();
            ModelBinders.Binders.Add(typeof(AutoCompleteSourceRequest), new AutoCompleteSourceRequestBinder());
            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(PositiveAttribute),
                typeof(RangeAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(BeforeTodayAttribute),
                typeof(RangeAttributeAdapter));
            InitializeDependencyResolver();
        }

        private static void InitializeDependencyResolver()
        {
            var builder = new ContainerBuilder();
            ContainerConfig.RegisterTypes(builder);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #endregion
    }
}