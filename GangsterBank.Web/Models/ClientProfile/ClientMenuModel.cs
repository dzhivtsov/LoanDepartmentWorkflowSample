using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GangsterBank.Web.Models.ClientProfile
{
    public class ClientMenuModel
    {
        public int ClientId { get; set; }
        public string BasicDetailsUrl { get; set; }
        public string PassportUrl { get; set; }
        public string ContactsUrl { get; set; }
        public string EmploymentUrl { get; set; }
        public string ObligationsUrl { get; set; }
        public string PropertyUrl { get; set; }
        public string CreditsUrl { get; set; }
        public bool IsClient { get; set; }
    }
}