namespace GangsterBank.Domain.Entities.Credits
{
    public enum LoanRequestStatus
    {
        Requested = 1, 

        ProfilePrerequisitesAreVerified = 2, 

        ApprovedByAllApprovers = 3, 

        Declined = 4,

        Approved = 5
    }
}