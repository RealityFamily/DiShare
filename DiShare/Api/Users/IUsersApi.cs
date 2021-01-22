// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Users.IUsersApi
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

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
