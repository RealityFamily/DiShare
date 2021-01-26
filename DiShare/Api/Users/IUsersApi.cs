

using DiShare.Api.Base;
using DiShare.Api.Users.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.Users
{
  public interface IUsersApi
  {
    Task<Response<string>> LoginAsync(
      string email,
      string password,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<UserResponse>> RegisterAsync(
      string email,
      string name,
      string password,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<string>> ResetPasswordAsync(
      string email,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<string>> ChangePasswordAsync(
      string token,
      string email,
      string oldPassword,
      string password,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<string>> ConfirmEmailAsync(
      string email,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<bool>> CheckIsEmailRegisteredAsync(
      string email,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<string>> RefreshTokenAsync(
      string token,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<bool>> CheckIsEmailConfirmedAsync(
      string email,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<bool>> CheckHasMarathonGroupsAsync(
      string token,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<string>> ActivatePromoCodeAsync(
      string email,
      string promoCode,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
