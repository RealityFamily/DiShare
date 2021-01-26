

using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Logic.RegistrySettingsProvider
{
  public interface IRegistrySettingsProvider
  {
    Task<bool> IsFirstStartAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> GetTopmostEnabledAsync(CancellationToken cancellationToken = default (CancellationToken));

    bool GetTopmostEnabled();

    Task SetTopmostEnabledAsync(bool topmostEnabled, CancellationToken cancellationToken = default (CancellationToken));

    Task<string> GetLibraryPathAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetLibraryPathAsync(string path, CancellationToken cancellationToken = default (CancellationToken));

    Task<string> GetEmailAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetEmailAsync(string email, CancellationToken cancellationToken = default (CancellationToken));

    Task<string> GetTokenAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetTokenAsync(string token, CancellationToken cancellationToken = default (CancellationToken));

    Task<string> GetSelectedCategoryAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetSelectedCategoryAsync(string categoryId, CancellationToken cancellationToken = default (CancellationToken));

    Task SetHardwareIdAsync(string hardwareId, CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> GetFirstRunLoginDialogShowedAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetFirstRunLoginDialogShowedAsync(
      bool isFirstRunLoginDialogShowed,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> IsLoggingAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> IsAnalyticsEnabledAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> IsUpdatesEnabledAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> IsShowMaxBadVersionWarningDisabledAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetShowMaxBadVersionWarningDisabledAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> IsEmailConfirmedAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetEmailConfirmedAsync(bool confirmed, CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> IsFirstDetectedBadMaxVersionAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> GetDontShowOnboardingDialogAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetDontShowOnboardingDialogAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> GetDontShowThanksDialogAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetDontShowThanksDialogAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetThanksDialogShowedCountAsync(uint count, CancellationToken cancellationToken = default (CancellationToken));

    Task<uint> GetThanksDialogShowedCountAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task<bool> IsLicenseAcceptedAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetLicenseAcceptedAsync(bool accepted, CancellationToken cancellationToken = default (CancellationToken));

    Task<string> GetSelectedTabAsync(CancellationToken cancellationToken = default (CancellationToken));

    Task SetSelectedTabAsync(string categoryId, CancellationToken cancellationToken = default (CancellationToken));

    Task<string> GetTabLastCategoryAsync(string tabName, CancellationToken cancellationToken = default (CancellationToken));

    Task SetTabLastCategoryAsync(
      string tabName,
      string categoryId,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
