namespace GangsterBank.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using GangsterBank.BusinessLogic.Contracts.Clients;
    using GangsterBank.BusinessLogic.Contracts.Credits;
    using GangsterBank.BusinessLogic.Contracts.Membership;
    using GangsterBank.BusinessLogic.Contracts.Tasks.Daily;
    using GangsterBank.Domain.Entities.Membership;
    using GangsterBank.SchedulerService;
    using GangsterBank.Web.Infrastructure.ActionFilters;
    using GangsterBank.Web.Models.Clients;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using WebGrease.Css.Extensions;

    [GangsterBankAuthorize(Role.Administrator)]
    public class AdministratorController : BaseController
    {
        private readonly IClientsService clientsService;

        private readonly IUserService userService;

        private readonly IDailyTaskManager dailyTaskManager;

        public AdministratorController(
            IUserContext userContext,
            IClientsService clientsService,
            IUserService userService, 
            IDailyTaskManager dailyTaskManager)
            : base(userContext)
        {
            this.clientsService = clientsService;
            this.userService = userService;
            this.dailyTaskManager = dailyTaskManager;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Users()
        {
            return this.View();
        }

        public ActionResult UsersData([DataSourceRequest] DataSourceRequest request)
        {
            var roles = this.userService.GetRoles();
            var data = this.clientsService.GetAllClients().Where(x => x.Id != _userContext.User.Id).Select(x => new ClientViewModel
                                                                      {
                                                                          FirstName = x.FirstName,
                                                                          LastName = x.LastName,
                                                                          Id = x.Id,
                                                                          Roles = string.Join(", ", x.Roles.Select(r => roles.First(ro => ro.Id == r.RoleId).Name))
                                                                      });
            return this.Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditRoles(int clientId)
        {
            var roles = this.userService.GetRoles().ToList();
            var client = this.clientsService.GetClient(clientId);
            var model = new UpdateRolesModel
                       {
                           ClientId = client.Id,
                           Roles = client.Roles.Select(x => (Role)Enum.Parse(typeof(Role), roles.First(r => r.Id == x.RoleId).Name, true))
                       };
            return this.View(model);
        }

        [HttpPost]
        public ActionResult EditRoles(UpdateRolesModel model)
        {
            var roles = this.userService.GetRoles();
            var client = this.clientsService.GetClient(model.ClientId);
            client.Roles.Clear();
            model.Roles.ForEach(
                r =>
                    {
                        var roleId = roles.First(x => x.Name == Enum.GetName(typeof(Role), r)).Id;
                        client.Roles.Add(new IdentityUserRoleEntity
                                             {
                                                 RoleId = roleId,
                                                 UserId = client.Id
                                             });
                    });
            this.clientsService.CreateOrUpdate(client);
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RunService()
        {
            this.dailyTaskManager.Execute(new OperationalDayContext(DateTime.Today));
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}