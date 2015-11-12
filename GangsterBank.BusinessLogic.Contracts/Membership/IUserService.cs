namespace GangsterBank.BusinessLogic.Contracts.Membership
{
    #region

    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using GangsterBank.Domain.Entities.Membership;

    using Microsoft.AspNet.Identity;

    #endregion

    public interface IUserService
    {
        #region Public Properties

        ClaimsIdentityFactory<User, int> ClaimsIdentityFactory { get; set; }
        
        IPasswordHasher PasswordHasher { get; set; }

        ITokenProvider PasswordResetTokens { get; set; }
        
        IIdentityValidator<string> PasswordValidator { get; set; }
        
        bool SupportsUserClaim { get; }
        
        bool SupportsUserEmail { get; }
        
        bool SupportsUserLogin { get; }
        
        bool SupportsUserPassword { get; }
        
        bool SupportsUserRole { get; }
        
        bool SupportsUserSecurityStamp { get; }
        
        ITokenProvider UserConfirmationTokens { get; set; }
        
        IIdentityValidator<User> UserValidator { get; set; }
        
        IQueryable<User> Users { get; }

        #endregion

        #region Public Methods and Operators

        Task<IdentityResult> AddClaimAsync(int userId, Claim claim);

        Task<IdentityResult> AddLoginAsync(int userId, UserLoginInfo login);

        Task<IdentityResult> AddPasswordAsync(int userId, string password);

        Task<IdentityResult> AddToRoleAsync(int userId, string role);

        Task<IdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

        Task<IdentityResult> ConfirmUserAsync(int userId, string token);

        Task<IdentityResult> CreateAsync(User user);

        Task<IdentityResult> CreateAsync(User user, string password);

        Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType);

        Task<IdentityResult> DeleteAsync(User user);

        void Dispose();

        Task<User> FindAsync(string userName, string password);

        Task<User> FindAsync(UserLoginInfo login);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindByIdAsync(int userId);

        Task<User> FindByNameAsync(string userName);

        Task<IList<Claim>> GetClaimsAsync(int userId);

        Task<string> GetConfirmationTokenAsync(int userId);

        Task<string> GetEmailAsync(int userId);

        Task<IList<UserLoginInfo>> GetLoginsAsync(int userId);

        Task<string> GetPasswordResetTokenAsync(int userId);

        Task<IList<string>> GetRolesAsync(int userId);

        Task<string> GetSecurityStampAsync(int userId);

        Task<bool> HasPasswordAsync(int userId);

        Task<bool> IsConfirmedAsync(int userId);

        Task<bool> IsInRoleAsync(int userId, string role);

        Task<IdentityResult> RemoveClaimAsync(int userId, Claim claim);

        Task<IdentityResult> RemoveFromRoleAsync(int userId, string role);

        Task<IdentityResult> RemoveLoginAsync(int userId, UserLoginInfo login);

        Task<IdentityResult> RemovePasswordAsync(int userId);

        Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword);

        Task<IdentityResult> SetConfirmedAsync(int userId, bool confirmed);

        Task<IdentityResult> SetEmailAsync(int userId, string email);

        Task<IdentityResult> UpdateAsync(User user);

        Task<IdentityResult> UpdateSecurityStampAsync(int userId);

        IEnumerable<IdentityRoleEntity> GetRoles();

        #endregion

        IEnumerable<IdentityRoleEntity> GetRoles(IEnumerable<Role> roles);
    }
}