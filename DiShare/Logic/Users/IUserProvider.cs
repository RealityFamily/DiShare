

using DiShare.Api.Users.Responses;
using DiShare.Infrastructure;
using DiShare.Logic.Users.Models;
using System.Threading.Tasks;

namespace DiShare.Logic.Users
{
  public interface IUserProvider
  {
    Task<TryResult<bool>> HasUserRegisteredAsync();

    Task<TryResult<UserInfo>> GetUserInfoAsync();

    Task<TryResult<UserInfo>> UpdateUserInfoAsync();

    Task<TryResult<string>> SendResetPasswordRequestAsync(string email);

    Task<TryResult<string>> SendEmailConfirmationRequestAsync(string email);

    Task<TryResult<bool>> CheckIsEmailRegisteredAsync(string email);

    Task<TryResult<bool>> CheckIsEmailConfirmedAsync(string email);

    Task<TryResult<UserResponse>> RegisterUserAsync(
      string email,
      string name,
      string password);

    Task<TryResult<bool>> LoginUserAsync(string email, string password);

    Task<TryResult> ChangePasswordAsync(string oldPassword, string password);

    Task LogoutAsync();

    Task<TryResult<bool>> ActivatePromoCodeAsync(string email, string promoCode);
  }
}
