namespace GangsterBank.Web.Models.Credit
{
    using System.Web.Mvc;

    public class DeclineRequestViewModel
    {
        public int Id { get; set; }

        [AllowHtml]
        public string Text { get; set; }

        public int ClientId { get; set; }
    }
}