

using DiShare.Api.Abstract;
using DiShare.Api.Base;
using DiShare.Api.Users.Requests;
using DiShare.Api.Users.Responses;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace DiShare.Api.Users
{
  public class UsersApi : ApiBase, IUsersApi
  {
    public UsersApi(TinyRestClient client)
      : base(client)
    {
    }

    public Task<Response<bool>> CheckHasMarathonGroupsAsync(
      string token,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.GetRequest("api/v1/Users/HasMarathonGroups").AddHeader("X-API-KEY", this.ApiKey).AddHeader("Authorization", "Bearer " + token).ExecuteAsync<Response<bool>>(cancellationToken);
    }

    public Task<Response<bool>> CheckIsEmailConfirmedAsync(
      string email,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.GetRequest("api/v1/Users/IsEmailConfirmed").AddQueryParameter(nameof (email), email).AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<bool>>(cancellationToken);
    }

    public Task<Response<bool>> CheckIsEmailRegisteredAsync(
      string email,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.GetRequest("api/v1/Users/IsEmailRegistered").AddQueryParameter(nameof (email), email).AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<bool>>(cancellationToken);
    }

    public Task<Response<string>> RefreshTokenAsync(
      string token,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.PostRequest("api/v1/Users/RefreshToken").AddHeader("X-API-KEY", this.ApiKey).AddHeader("Authorization", "Bearer " + token).ExecuteAsync<Response<string>>(cancellationToken);
    }

    public Task<Response<string>> ChangePasswordAsync(
      string token,
      string email,
      string oldPassword,
      string password,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.PostRequest("api/v1/users/password").AddContent<ChangePasswordRequest>(new ChangePasswordRequest(email, oldPassword, password)).AddHeader("X-API-KEY", this.ApiKey).AddHeader("Authorization", "Bearer " + token).ExecuteAsync<Response<string>>(cancellationToken);
    }

    public Task<Response<string>> ConfirmEmailAsync(
      string email,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.PostRequest("api/v1/Users/ConfirmEmail").AddFormParameter(nameof (email), email).AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<string>>(cancellationToken);
    }

    public Task<Response<string>> LoginAsync(
      string email,
      string password,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.PostRequest("api/v1/users/login").AddContent<LoginRequest>(new LoginRequest(email, password)).AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<string>>(cancellationToken);
    }

    public Task<Response<UserResponse>> RegisterAsync(
      string email,
      string name,
      string password,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.PostRequest("api/v1/Users/Register").AddContent<RegisterRequest>(new RegisterRequest(email, name, password)).AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<UserResponse>>(cancellationToken);
    }

    public Task<Response<string>> ResetPasswordAsync(
      string email,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.PostRequest("api/v1/Users/ResetPassword").AddFormParameter(nameof (email), email).AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<string>>(cancellationToken);
    }

    public Task<Response<string>> ActivatePromoCodeAsync(
      string email,
      string promoCode,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.PostRequest("api/v1/Users/ActivatePromoCode").AddContent(new
      {
        Email = email,
        PromoCode = promoCode
      }).AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<string>>(cancellationToken);
    }
  }
}
