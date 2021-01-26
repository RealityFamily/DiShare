
using DiShare.Api.Base;
using DiShare.Api.Users;
using DiShare.Api.Users.Responses;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Network;
using DiShare.Infrastructure.Threading;
using DiShare.Logic.ApiValidationErrorParser;
using DiShare.Logic.Exceptions;
using DiShare.Logic.RegistrySettingsProvider;
using DiShare.Logic.Users.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace DiShare.Logic.Users
{
  public class UserProvider : IUserProvider
  {
    private readonly IUsersApi _usersApi;
    private readonly IRegistrySettingsProvider _registrySettingsProvider;
    private readonly IApiValidationErrorParser _apiValidationErrorParser;
    private readonly INetworkChecker _networkChecker;
    private readonly AsyncLock _asyncLock;
    private UserInfo _userInfo;

    public UserProvider(
      IUsersApi usersApi,
      IRegistrySettingsProvider registrySettingsProvider,
      IApiValidationErrorParser apiValidationErrorParser,
      INetworkChecker networkChecker)
    {
      this._registrySettingsProvider = registrySettingsProvider;
      this._apiValidationErrorParser = apiValidationErrorParser;
      this._networkChecker = networkChecker;
      this._usersApi = usersApi;
      this._asyncLock = new AsyncLock();
    }

    public async Task<TryResult<UserInfo>> GetUserInfoAsync()
    {
      int num;
      Exception ex;
      try
      {
        if (this._userInfo == null)
        {
          using (await this._asyncLock.LockAsync())
          {
            if (this._userInfo == null)
            {
              string email = await this._registrySettingsProvider.GetEmailAsync().ConfigureAwait(false);
              string token = await this._registrySettingsProvider.GetTokenAsync().ConfigureAwait(false);
              if (email.IsNullOrEmpty() || token.IsNullOrEmpty())
                return new TryResult<UserInfo>(new UserInfo(false));
              bool? registered = new bool?();
              bool? emailConfirmed = new bool?();
              bool? hasMarathonGroups = new bool?();
              if (this._networkChecker.CheckIsConnected())
              {
                try
                {
                  Response<string> response = await this._usersApi.RefreshTokenAsync(token).ConfigureAwait(false);
                  registered = new bool?(response.Status == ResponseStatus.OK);
                  if (response.Status != ResponseStatus.OK || response.Result.IsNullOrWhiteSpace())
                    throw new UnauthorizedUserProviderException(response.Error);
                  token = response.Result;
                }
                catch (Exception ex1)
                {
                  await this.ClearUserInfo();
                  return new TryResult<UserInfo>(ex1);
                }
                if (registered.Value)
                {
                  ConfiguredTaskAwaitable configuredTaskAwaitable = this._registrySettingsProvider.SetTokenAsync(token).ConfigureAwait(false);
                  await configuredTaskAwaitable;
                  UserInfo checkResult = await this.UpdateUserInfoDetailsAsync(email, token).ConfigureAwait(false);
                  emailConfirmed = checkResult.IsEmailConfirmed;
                  configuredTaskAwaitable = this._registrySettingsProvider.SetEmailConfirmedAsync(emailConfirmed.GetValueOrDefault()).ConfigureAwait(false);
                  await configuredTaskAwaitable;
                  hasMarathonGroups = new bool?(checkResult.HasMarathonGroups.GetValueOrDefault());
                  checkResult = (UserInfo) null;
                }
                else
                {
                  await this.ClearUserInfo();
                  email = (string) null;
                  token = (string) null;
                  await this._registrySettingsProvider.SetEmailAsync((string) null).ConfigureAwait(false);
                  return new TryResult<UserInfo>(new UserInfo(false));
                }
              }
              else if (await this._registrySettingsProvider.IsEmailConfirmedAsync().ConfigureAwait(false))
              {
                registered = new bool?(true);
                emailConfirmed = new bool?(true);
              }
              string name = email;
              if (email.Contains("@"))
                name = email.Split('@')[0];
              this._userInfo = new UserInfo(email, name, token, registered, emailConfirmed, hasMarathonGroups);
              email = (string) null;
              token = (string) null;
              registered = new bool?();
              emailConfirmed = new bool?();
              hasMarathonGroups = new bool?();
            }
          }
        }
        return new TryResult<UserInfo>(this._userInfo);
      }
      catch (Exception ex1)
      {
        num = 1;
        ex = ex1;
      }
      if (num != 1)
      {
          TryResult<UserInfo> tryResult = null;
        return tryResult;
      }
      
      await this.ClearUserInfo();
      return new TryResult<UserInfo>(ex);
    }

    private async Task<UserInfo> UpdateUserInfoDetailsAsync(string email, string token)
    {
      try
      {
        Response<bool> response1 = await this._usersApi.CheckIsEmailConfirmedAsync(email).ConfigureAwait(false);
        bool emailConfirmed = response1.Result;
        await this._registrySettingsProvider.SetEmailConfirmedAsync(response1.Result).ConfigureAwait(false);
        bool hasMarathonGroups = false;
        try
        {
          Response<bool> response2 = await this._usersApi.CheckHasMarathonGroupsAsync(token).ConfigureAwait(false);
          hasMarathonGroups = response2.Status == ResponseStatus.OK && response2.Result;
        }
        catch (HttpException ex)
        {
          if (ex.StatusCode != HttpStatusCode.Forbidden)
          {
            if (ex.StatusCode != HttpStatusCode.Unauthorized)
              throw;
          }
        }
        return new UserInfo(email, this.EmailToName(email), token, new bool?(true), new bool?(emailConfirmed), new bool?(hasMarathonGroups));
      }
      catch (HttpException ex)
      {
        if (ex.StatusCode == HttpStatusCode.Forbidden || ex.StatusCode == HttpStatusCode.Unauthorized)
          return new UserInfo(email, this.EmailToName(email), token, new bool?(false), new bool?(false), new bool?(false));
        throw;
      }
    }

    private string EmailToName(string email)
    {
      if (email.IsNullOrEmpty())
        return (string) null;
      string str = email;
      if (email.Contains("@"))
        str = email.Split('@')[0];
      return str;
    }

    public Task<TryResult<UserInfo>> UpdateUserInfoAsync()
    {
      this._userInfo = (UserInfo) null;
      return this.GetUserInfoAsync();
    }

    public async Task<TryResult<string>> SendResetPasswordRequestAsync(string email)
    {
      try
      {
        return (TryResult<string>) (await this._usersApi.ResetPasswordAsync(email).ConfigureAwait(false)).Result;
      }
      catch (HttpException ex)
      {
        if (ex.StatusCode != HttpStatusCode.NotFound)
          return new TryResult<string>((Exception) ex);
        Response<string> response;
        try
        {
          response = JsonConvert.DeserializeObject<Response<string>>(ex.Content);
        }
        catch
        {
          return new TryResult<string>((Exception) ex);
        }
        return new TryResult<string>((Exception) new UserNotFoundUserProviderException(response.Error, (Exception) ex));
      }
      catch (Exception ex)
      {
        return new TryResult<string>(ex);
      }
    }

    public async Task<TryResult<string>> SendEmailConfirmationRequestAsync(string email)
    {
      try
      {
        return (TryResult<string>) (await this._usersApi.ConfirmEmailAsync(email).ConfigureAwait(false)).Result;
      }
      catch (Exception ex)
      {
        return new TryResult<string>(ex);
      }
    }

    public async Task<TryResult<bool>> CheckIsEmailRegisteredAsync(string email)
    {
      try
      {
        return (TryResult<bool>) (await this._usersApi.CheckIsEmailRegisteredAsync(email).ConfigureAwait(false)).Result;
      }
      catch (Exception ex)
      {
        return new TryResult<bool>(ex);
      }
    }

    public async Task<TryResult<bool>> CheckIsEmailConfirmedAsync(string email)
    {
      try
      {
        return (TryResult<bool>) (await this._usersApi.CheckIsEmailConfirmedAsync(email).ConfigureAwait(false)).Result;
      }
      catch (Exception ex)
      {
        return new TryResult<bool>(ex);
      }
    }

    public async Task<TryResult<UserResponse>> RegisterUserAsync(
      string email,
      string name,
      string password)
    {
      using (await this._asyncLock.LockAsync())
      {
        //int num1 = 0 ;
        //int num2 = num1 - 1;
        try
        {
          Response<UserResponse> result = await this._usersApi.RegisterAsync(email, name, password).ConfigureAwait(false);
          if (result.Error.IsNullOrEmpty() && result.Status == ResponseStatus.OK)
          {
            await this._registrySettingsProvider.SetEmailAsync(email).ConfigureAwait(false);
            await this._registrySettingsProvider.SetTokenAsync(result.Result.AccessToken).ConfigureAwait(false);
            this._userInfo = await this.UpdateUserInfoDetailsAsync(email, result.Result.AccessToken).ConfigureAwait(false);
          }
          return (TryResult<UserResponse>) result.Result;
        }
        catch (HttpException ex)
        {
          Response<string> response;
          try
          {
            response = JsonConvert.DeserializeObject<Response<string>>(ex.Content);
            if (response.Error.IsNullOrEmpty())
            {
              TryResult<string> validationResponse = this._apiValidationErrorParser.ParseValidationResponse(ex.Content);
              return !validationResponse.IsFaulted ? new TryResult<UserResponse>((Exception) new UserProviderException(validationResponse.Value, (Exception) ex)) : new TryResult<UserResponse>((Exception) new UserProviderException(response.Error, (Exception) ex));
            }
          }
          catch
          {
            return new TryResult<UserResponse>((Exception) ex);
          }
          return new TryResult<UserResponse>((Exception) new UserProviderException(response.Error, (Exception) ex));
        }
        catch (Exception ex)
        {
          return new TryResult<UserResponse>(ex);
        }
      }
    }

    public async Task<TryResult<bool>> LoginUserAsync(string email, string password)
    {
      try
      {
        Response<string> result = await this._usersApi.LoginAsync(email, password).ConfigureAwait(false);
        if (!result.Error.IsNullOrEmpty() || result.Status != ResponseStatus.OK || result.Result.IsNullOrEmpty())
          return result.Error.IsNullOrEmpty() ? new TryResult<bool>(false) : new TryResult<bool>((Exception) new UserProviderException(result.Error));
        await this._registrySettingsProvider.SetEmailAsync(email).ConfigureAwait(false);
        await this._registrySettingsProvider.SetTokenAsync(result.Result).ConfigureAwait(false);
        return new TryResult<bool>(true);
      }
      catch (HttpException ex)
      {
        Response<string> response;
        try
        {
          response = JsonConvert.DeserializeObject<Response<string>>(ex.Content);
          if (response.Error.IsNullOrEmpty())
          {
            TryResult<string> validationResponse = this._apiValidationErrorParser.ParseValidationResponse(ex.Content);
            return !validationResponse.IsFaulted ? new TryResult<bool>((Exception) new UserProviderException(validationResponse.Value, (Exception) ex)) : new TryResult<bool>((Exception) new UserProviderException(response.Error, (Exception) ex));
          }
        }
        catch
        {
          return new TryResult<bool>((Exception) ex);
        }
        return new TryResult<bool>((Exception) new UserProviderException(response.Error, (Exception) ex));
      }
      catch (Exception ex)
      {
        return new TryResult<bool>(ex);
      }
    }

    public async Task<TryResult> ChangePasswordAsync(
      string oldPassword,
      string password)
    {
      UserInfo userInfo = this._userInfo;
      bool? nullable1;
      bool? nullable2;
      if (userInfo == null)
      {
        nullable2 = new bool?();
      }
      else
      {
        nullable1 = userInfo.IsRegistered;
        nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
      }
      nullable1 = nullable2;
      if (nullable1.GetValueOrDefault())
        return new TryResult((Exception) new UserProviderException("You must be registered"));
      try
      {
        Response<string> response = await this._usersApi.ChangePasswordAsync(this._userInfo?.Token, this._userInfo?.Email, oldPassword, password).ConfigureAwait(false);
        if (response.Status == ResponseStatus.Error)
          return new TryResult((Exception) new UserProviderException(response.Error));
        TryResult<bool> tryResult = await this.LoginUserAsync(this._userInfo?.Email, password).ConfigureAwait(false);
        return !tryResult.IsFaulted ? (tryResult.Value ? new TryResult() : new TryResult((Exception) new UserProviderException("Can't login with new password, please try again or use reset password feature"))) : new TryResult(tryResult.Exception);
      }
      catch (Exception ex)
      {
        return new TryResult(ex);
      }
    }

    public async Task<TryResult<bool>> ActivatePromoCodeAsync(
      string email,
      string promoCode)
    {
      try
      {
        Response<string> response = await this._usersApi.ActivatePromoCodeAsync(email, promoCode).ConfigureAwait(false);
        return !response.Error.IsNullOrEmpty() || response.Status != ResponseStatus.OK || response.Result.IsNullOrEmpty() ? (response.Error.IsNullOrEmpty() ? new TryResult<bool>(false) : new TryResult<bool>((Exception) new UserProviderException(response.Error))) : new TryResult<bool>(true);
      }
      catch (HttpException ex)
      {
        Response<string> response;
        try
        {
          response = JsonConvert.DeserializeObject<Response<string>>(ex.Content);
          if (response.Error.IsNullOrEmpty())
          {
            TryResult<string> validationResponse = this._apiValidationErrorParser.ParseValidationResponse(ex.Content);
            return !validationResponse.IsFaulted ? new TryResult<bool>((Exception) new UserProviderException(validationResponse.Value, (Exception) ex)) : new TryResult<bool>((Exception) new UserProviderException(response.Error, (Exception) ex));
          }
        }
        catch
        {
          return new TryResult<bool>((Exception) ex);
        }
        return new TryResult<bool>((Exception) new UserProviderException(response.Error, (Exception) ex));
      }
      catch (Exception ex)
      {
        return new TryResult<bool>(ex);
      }
    }

    public async Task<TryResult<bool>> HasUserRegisteredAsync()
    {
      TryResult<UserInfo> tryResult = await this.GetUserInfoAsync().ConfigureAwait(false);
      return !tryResult.IsFaulted ? new TryResult<bool>(((bool?) this._userInfo?.IsRegistered).GetValueOrDefault()) : new TryResult<bool>(tryResult.Exception);
    }

    public async Task LogoutAsync()
    {
      ConfiguredTaskAwaitable configuredTaskAwaitable = this._registrySettingsProvider.SetEmailAsync((string) null).ConfigureAwait(false);
      await configuredTaskAwaitable;
      configuredTaskAwaitable = this._registrySettingsProvider.SetTokenAsync((string) null).ConfigureAwait(false);
      await configuredTaskAwaitable;
      this._userInfo = (UserInfo) null;
    }

    private async Task ClearUserInfo()
    {
      ConfiguredTaskAwaitable configuredTaskAwaitable = this._registrySettingsProvider.SetTokenAsync((string) null).ConfigureAwait(false);
      await configuredTaskAwaitable;
      configuredTaskAwaitable = this._registrySettingsProvider.SetEmailConfirmedAsync(false).ConfigureAwait(false);
      await configuredTaskAwaitable;
    }
  }
}
