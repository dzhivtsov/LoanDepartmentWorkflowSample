namespace GangsterBank.Domain.Entities.Base
{
    public interface IEntity
    {
        int Id { get; set; }

        bool IsDeleted { get; set; }
    }
}