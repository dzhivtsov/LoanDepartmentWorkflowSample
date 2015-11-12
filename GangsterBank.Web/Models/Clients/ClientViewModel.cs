namespace GangsterBank.Web.Models.Clients
{
    using GangsterBank.Domain.Entities.Membership;

    public class ClientViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Id { get; set; }

        public string Roles { get; set; }
    }
}