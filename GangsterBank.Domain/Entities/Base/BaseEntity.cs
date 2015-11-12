namespace GangsterBank.Domain.Entities.Base
{
    #region

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class BaseEntity : IEntity
    {
        #region Public Properties

        [Key]
        public virtual int Id { get; set; }

        public virtual bool IsDeleted { get; set; }

        #endregion
    }
}