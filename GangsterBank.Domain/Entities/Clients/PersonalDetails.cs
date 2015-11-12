namespace GangsterBank.Domain.Entities.Clients
{
    using System;

    using GangsterBank.Domain.Entities.Base;

    public class PersonalDetails : BaseEntity
    {
        #region Public Properties

        public virtual DateTime? BirthDate { get; set; }
        
        public virtual Contacts Contacts { get; set; }
        
        public virtual EmploymentData EmploymentData { get; set; }
        
        public virtual Gender? Gender { get; set; }
        
        public virtual PassportData PassportData { get; set; }

        #endregion
    }
}