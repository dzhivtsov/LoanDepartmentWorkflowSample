namespace GangsterBank.Domain.Entities.Credits
{
    public enum LoanProductStatus
    {
        // на доработку
        Draft = 1,

        // сразу после создания
        ReadyForReview = 2,

        // рабочий
        Active = 3,

        // архив
        Archived = 4
    }
}