namespace GangsterBank.Domain.Workflow
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using GangsterBank.Domain.Entities.Credits;
    using GangsterBank.Domain.Entities.Membership;

    #endregion

    public class LoanProductCreationWorkflowConfiguration
    {
        #region Fields

        private readonly Dictionary<LoanProductStatus, IEnumerable<LoanProductStatus>> prerequisiteStatuses =
            new Dictionary<LoanProductStatus, IEnumerable<LoanProductStatus>>
                {
                    {
                        LoanProductStatus.Draft, 
                        new[]
                            {
                                LoanProductStatus
                                    .ReadyForReview, 
                                LoanProductStatus.Active
                            }
                    }, 
                    {
                        LoanProductStatus.ReadyForReview, 
                        new[]
                            {
                                LoanProductStatus
                                    .ReadyForReview
                            }
                    }, 
                    {
                        LoanProductStatus.Active, 
                        new[]
                            {
                                LoanProductStatus
                                    .ReadyForReview, 
                                LoanProductStatus.Archived
                            }
                    }, 
                    {
                        LoanProductStatus.Archived, 
                        new[] { LoanProductStatus.Active }
                    }
                };

        private readonly Dictionary<LoanProductStatus, IEnumerable<Role>> requiredRoles =
            new Dictionary<LoanProductStatus, IEnumerable<Role>>
                {
                    {
                        LoanProductStatus.Draft, 
                        new[]
                            {
                                Role.LendingDepartmentSpecialist, 
                                Role.LendingDepartmentHead, 
                                Role.Administrator
                            }
                    }, 
                    {
                        LoanProductStatus.ReadyForReview, 
                        new[]
                            {
                                Role.LendingDepartmentSpecialist, 
                                Role.LendingDepartmentHead, 
                                Role.Administrator
                            }
                    }, 
                    {
                        LoanProductStatus.Active, 
                        new[]
                            {
                                Role.LendingDepartmentHead, 
                                Role.Administrator
                            }
                    }, 
                    {
                        LoanProductStatus.Archived, 
                        new[]
                            {
                                Role.LendingDepartmentHead, 
                                Role.Administrator
                            }
                    }
                };

        #endregion

        #region Public Methods and Operators

        public bool CanHaveStatus(LoanProduct loanProduct, LoanProductStatus status)
        {
            Contract.Requires<ArgumentNullException>(loanProduct != null);
            if (!this.prerequisiteStatuses.ContainsKey(status))
            {
                throw new ArgumentOutOfRangeException("status");
            }

            return this.prerequisiteStatuses[status].Contains(loanProduct.Status);
        }

        public bool IsAllowedToChangeStatus(LoanProductStatus status, params Role[] roles)
        {
            if (!this.prerequisiteStatuses.ContainsKey(status))
            {
                throw new ArgumentOutOfRangeException("status");
            }

            return roles.Any(role => this.requiredRoles[status].Contains(role));
        }

        #endregion
    }
}