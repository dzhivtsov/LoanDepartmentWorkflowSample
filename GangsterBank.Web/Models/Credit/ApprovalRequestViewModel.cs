namespace GangsterBank.Web.Models.Credit
{
    public class ApprovalRequestViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LoanProductName { get; set; }

        public decimal Amount { get; set; }

        public int ClientId { get; set; }
    }
}