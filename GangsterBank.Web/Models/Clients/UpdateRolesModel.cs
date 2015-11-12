using System.Collections.Generic;

namespace GangsterBank.Web.Models.Clients
{
    using GangsterBank.Domain.Entities.Membership;

    public class UpdateRolesModel
    {
        public IEnumerable<Role> Roles { get; set; }

        public int ClientId { get; set; }
    }
}