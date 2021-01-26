

using DiShare.Api;
using DiShare.Api.Update;
using DiShare.Api.Update.Responses;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Network;
using DiShare.Logic.ErrorHandler.Models;
using DiShare.Logic.ExceptionHandler;
using DiShare.Logic.Updater.Builders;
using DiShare.Logic.Updater.Models;
using System;
using System.Threading.Tasks;

namespace DiShare.Logic.Updater.Checkers
{
  public class UpdateChecker : IUpdateChecker
  {
    private readonly INetworkChecker networkChecker;
    private readonly IUpdateApi updateApiClient;
    private readonly IUpdateInfoBuilder updateInfoBuilder;
    private readonly IExceptionHandler exceptionHandler;
    private readonly string baseApiUrl;

    public UpdateChecker(
      INetworkChecker networkChecker,
      IUpdateApi updateApiClient,
      IUpdateInfoBuilder updateInfoBuilder,
      ILibraryApiClient libraryApiClient,
      IExceptionHandler exceptionHandler)
    {
      this.networkChecker = networkChecker;
      this.updateApiClient = updateApiClient;
      this.updateInfoBuilder = updateInfoBuilder;
      this.exceptionHandler = exceptionHandler;
      this.baseApiUrl = libraryApiClient.BaseAddress;
    }

    public async Task<TryResult<UpdateInfo>> GetUpdateInfoAsync()
    {
        if (!this.networkChecker.CheckIsConnected())
            return (TryResult<UpdateInfo>) (UpdateInfo) null;
        try
        {
            UpdateResponse update = await this.updateApiClient.GetUpdateInfoAsync().ConfigureAwait(false);
            return update != null ? (TryResult<UpdateInfo>) this.updateInfoBuilder.Build(update, this.baseApiUrl) : (TryResult<UpdateInfo>) (UpdateInfo) null;
        }
        catch (Exception ex)
        {
            this.exceptionHandler.HandleException(ex, notificationMode: NotificationMode.Suppress);
            return (TryResult<UpdateInfo>) (UpdateInfo) null;
        }
    }
  }
}
