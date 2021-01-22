// Decompiled with JetBrains decompiler
// Type: Logic.RegistrySettingsProvider.RegistrySettingsProvider
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Threading;
using DiShare.OS.Registry;
using System.Threading;
using System.Threading.Tasks;
using DiShare.OS;

namespace DiShare.Logic.RegistrySettingsProvider
{
  public class RegistrySettingsProvider : IRegistrySettingsProvider
  {
    private readonly IRegistryProvider registryProvider;
    private readonly AsyncLock asyncLock;
    private static string RegistrySettingsSubKey = "3D Hamster"; // :TODO Change for DiShare registry data
    private static readonly string RegistrySettingsKey = RegistryRootTokens.HKEY_CURRENT_USER + "\\Software\\" + Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsSubKey;
    private bool? isTopmostEnabled;
    private readonly string IsTopmostEnabledValueName = "IsTopmostEnabled";
    private readonly bool IsTopmostEnabledDefaultValue;
    private bool? isFirstStart;
    private readonly string IsFirstStartValueName = "IsFirstStart";
    private bool? isFirstDetectedBadMaxVersion;
    private readonly string IsFirstDetectedBadMaxVersionValueName = "IsFirstDetectedBadMaxVersion";
    private string libraryPath;
    private readonly string LibraryPathValueName = "LibraryPath";
    private readonly string LibraryPathDefaultValue;
    private string token;
    private readonly string TokenValueName = "Token";
    private string email;
    private readonly string EmailValueName = "Email";
    private readonly string EmailDefaultValue;
    private string category;
    private readonly string CategoryValueName = "Category";
    private readonly string CategoryDefaultValue;
    private readonly string HardwareIdValueName = "HardwareId";
    private bool? isFirstRunLoginDialogShowed;
    private readonly string IsFirstRunLoginDialogShowedValueName = "IsFirstRunLoginDialogShowed";
    private readonly bool IsFirstRunLoginDialogShowedDefaultValue;
    private bool? isLogging;
    private readonly string IsLoggingValueName = "IsLogging";
    private readonly bool IsLoggingDefaultValue;
    private bool? isAnalyticsEnabled;
    private readonly string IsAnalyticsEnabledValueName = "IsAnalyticsEnabled";
    private readonly bool IsAnalyticsEnabledDefaultValue = false; // changed from true
    private bool? isUpdatesEnabled;
    private readonly string IsUpdatesEnabledValueName = "IsUpdatesEnabled";
    private readonly bool IsUpdatesEnabledDefaultValue = false; // changed from true
    private bool? isShowMaxBadVersionWarningDisabled;
    private readonly string isShowMaxBadVersionWarningDisabledValueName = "IsShowMaxBadVersionWarningDisabled";
    private readonly bool isShowMaxBadVersionWarningDisabledDefaultValue;
    private bool? dontShowOnboardingDialog;
    private readonly string dontShowOnboardingDialogValueName = "DontShowOnboardingDialog";
    private readonly bool dontShowOnboardingDialogDefaultValue;
    private bool? dontShowThanksDialog;
    private readonly string dontShowThanksDialogValueName = "DontShowThanksDialog";
    private readonly bool dontShowThanksDialogDefaultValue;
    private bool? isEmailConfirmed;
    private bool isEmailConfirmedDefaultValue;
    private string isEmailConfirmedValueName = "IsEmailConfirmed";
    private uint? thanksDialogShowedCount;
    private string thanksDialogShowedCountValueName = "ThanksDialogShowedCount";
    private bool? isLicenseAccepted;
    private string isLicenseAcceptedValueName = "IsLicenseAccepted";
    private string _selectedTab;
    private readonly string _selectedTabValueName = "SelectedTab";
    private readonly string _selectedTabDefaultValue;

    public RegistrySettingsProvider(IRegistryProvider registryProvider)
    {
      this.registryProvider = registryProvider;
      this.asyncLock = new AsyncLock();
    }

    public async Task<bool> GetTopmostEnabledAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this.isTopmostEnabled.HasValue)
      {
        using (await this.asyncLock.LockAsync())
        {
          if (!this.isTopmostEnabled.HasValue)
          {
            TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsTopmostEnabledValueName);
            if (tryGetResult.IsFound)
            {
              this.isTopmostEnabled = new bool?(string.Compare(tryGetResult.Value, "true", true) == 0);
            }
            else
            {
              this.isTopmostEnabled = new bool?(this.IsTopmostEnabledDefaultValue);
              IRegistryProvider registryProvider = this.registryProvider;
              string registrySettingsKey = Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey;
              string enabledValueName = this.IsTopmostEnabledValueName;
              bool? isTopmostEnabled = this.isTopmostEnabled;
              string str = (isTopmostEnabled.HasValue ? isTopmostEnabled.GetValueOrDefault() : this.IsTopmostEnabledDefaultValue).ToString();
              registryProvider.TrySetValue<string>(registrySettingsKey, enabledValueName, str);
            }
          }
        }
      }
      bool? isTopmostEnabled1 = this.isTopmostEnabled;
      return isTopmostEnabled1.HasValue ? isTopmostEnabled1.GetValueOrDefault() : this.IsTopmostEnabledDefaultValue;
    }

    public bool GetTopmostEnabled()
    {
      if (this.isTopmostEnabled.HasValue)
        return this.isTopmostEnabled ?? this.IsTopmostEnabledDefaultValue;
      TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsTopmostEnabledValueName);
      return !tryGetResult.IsFound ? this.IsTopmostEnabledDefaultValue : tryGetResult.Value.IsEqualsIgnoreCase("true");
    }

    public async Task SetTopmostEnabledAsync(
      bool isTopmostEnabled,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
      {
        bool? isTopmostEnabled1 = this.isTopmostEnabled;
        bool flag = isTopmostEnabled;
        if (isTopmostEnabled1.GetValueOrDefault() == flag & isTopmostEnabled1.HasValue)
          return;
        this.isTopmostEnabled = new bool?(isTopmostEnabled);
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsTopmostEnabledValueName, isTopmostEnabled.ToString());
      }
    }

    public async Task<bool> GetFirstRunLoginDialogShowedAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this.isFirstRunLoginDialogShowed.HasValue)
      {
        using (await this.asyncLock.LockAsync())
        {
          if (!this.isFirstRunLoginDialogShowed.HasValue)
          {
            TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsFirstRunLoginDialogShowedValueName);
            this.isFirstRunLoginDialogShowed = !tryGetResult.IsFound ? new bool?(this.IsFirstRunLoginDialogShowedDefaultValue) : new bool?(string.Compare(tryGetResult.Value, "true", true) == 0);
          }
        }
      }
      bool? loginDialogShowed = this.isFirstRunLoginDialogShowed;
      return loginDialogShowed.HasValue ? loginDialogShowed.GetValueOrDefault() : this.IsFirstRunLoginDialogShowedDefaultValue;
    }

    public async Task SetFirstRunLoginDialogShowedAsync(
      bool isFirstRunLoginDialogShowed,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
      {
        bool? loginDialogShowed = this.isFirstRunLoginDialogShowed;
        bool flag = isFirstRunLoginDialogShowed;
        if (loginDialogShowed.GetValueOrDefault() == flag & loginDialogShowed.HasValue)
          return;
        this.isFirstRunLoginDialogShowed = new bool?(isFirstRunLoginDialogShowed);
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsFirstRunLoginDialogShowedValueName, isFirstRunLoginDialogShowed.ToString());
      }
    }

    public async Task<string> GetLibraryPathAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.libraryPath == null)
      {
        using (await this.asyncLock.LockAsync())
        {
          if (this.libraryPath != null)
            return this.libraryPath;
          TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.LibraryPathValueName);
          this.libraryPath = tryGetResult.IsFound ? tryGetResult.Value : this.LibraryPathDefaultValue;
        }
      }
      return this.libraryPath;
    }

    public async Task SetLibraryPathAsync(string path, CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
      {
        if (this.libraryPath == path)
          return;
        this.libraryPath = path;
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.LibraryPathValueName, path);
      }
    }

    public async Task<string> GetEmailAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.email != null)
        return this.email;
      using (await this.asyncLock.LockAsync())
      {
        if (this.email != null)
          return this.email;
        TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.EmailValueName);
        this.email = tryGetResult.IsFound ? tryGetResult.Value : this.EmailDefaultValue;
      }
      return this.email;
    }

    public async Task SetEmailAsync(string email, CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
      {
        if (this.email == email)
          return;
        this.email = email;
        if (email.IsNullOrEmpty())
          this.registryProvider.TryDeleteValueHKCU(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.EmailValueName, false);
        else
          this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.EmailValueName, email);
      }
    }

    public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.token != null)
        return this.token;
      using (await this.asyncLock.LockAsync())
      {
        if (this.token != null)
          return this.token;
        TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.TokenValueName);
        this.token = tryGetResult.IsFound ? tryGetResult.Value : (string) null;
      }
      return this.token;
    }

    public async Task SetTokenAsync(string token, CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
      {
        if (this.token == token)
          return;
        this.token = token;
        if (token.IsNullOrEmpty())
          this.registryProvider.TryDeleteValueHKCU(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.TokenValueName, false);
        else
          this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.TokenValueName, token);
      }
    }

    public async Task<string> GetSelectedCategoryAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.category != null)
        return this.category;
      using (await this.asyncLock.LockAsync())
      {
        if (this.category != null)
          return this.category;
        TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.CategoryValueName);
        this.category = tryGetResult.IsFound ? tryGetResult.Value : this.CategoryDefaultValue;
      }
      return this.category;
    }

    public async Task SetSelectedCategoryAsync(
      string categoryId,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
      {
        if (this.category == categoryId)
          return;
        this.category = categoryId;
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.CategoryValueName, this.category);
      }
    }

    public async Task SetHardwareIdAsync(string hardwareId, CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.HardwareIdValueName, hardwareId);
    }

    public async Task<bool> IsFirstStartAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.isFirstStart.HasValue)
      {
        bool? isFirstStart = this.isFirstStart;
        return !isFirstStart.HasValue || isFirstStart.GetValueOrDefault();
      }
      using (await this.asyncLock.LockAsync())
      {
        if (this.isFirstStart.HasValue)
        {
          bool? isFirstStart = this.isFirstStart;
          return !isFirstStart.HasValue || isFirstStart.GetValueOrDefault();
        }
        if (this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsFirstStartValueName).IsFound)
        {
          this.isFirstStart = new bool?(false);
        }
        else
        {
          this.isFirstStart = new bool?(true);
          this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsFirstStartValueName, "false");
        }
      }
      bool? isFirstStart1 = this.isFirstStart;
      return !isFirstStart1.HasValue || isFirstStart1.GetValueOrDefault();
    }

    public async Task<bool> IsFirstDetectedBadMaxVersionAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.isFirstDetectedBadMaxVersion.HasValue)
      {
        bool? detectedBadMaxVersion = this.isFirstDetectedBadMaxVersion;
        return !detectedBadMaxVersion.HasValue || detectedBadMaxVersion.GetValueOrDefault();
      }
      using (await this.asyncLock.LockAsync())
      {
        if (this.isFirstDetectedBadMaxVersion.HasValue)
        {
          bool? detectedBadMaxVersion = this.isFirstDetectedBadMaxVersion;
          return !detectedBadMaxVersion.HasValue || detectedBadMaxVersion.GetValueOrDefault();
        }
        if (this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsFirstDetectedBadMaxVersionValueName).IsFound)
        {
          this.isFirstDetectedBadMaxVersion = new bool?(false);
        }
        else
        {
          this.isFirstDetectedBadMaxVersion = new bool?(true);
          this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsFirstDetectedBadMaxVersionValueName, "false");
        }
      }
      bool? detectedBadMaxVersion1 = this.isFirstDetectedBadMaxVersion;
      return !detectedBadMaxVersion1.HasValue || detectedBadMaxVersion1.GetValueOrDefault();
    }

    public async Task<bool> IsLoggingAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.isLogging.HasValue)
      {
        bool? isLogging = this.isLogging;
        return isLogging.HasValue ? isLogging.GetValueOrDefault() : this.IsLoggingDefaultValue;
      }
      using (await this.asyncLock.LockAsync())
      {
        if (this.isLogging.HasValue)
        {
          bool? isLogging = this.isLogging;
          return isLogging.HasValue ? isLogging.GetValueOrDefault() : this.IsLoggingDefaultValue;
        }
        if (this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsLoggingValueName).IsFound)
          this.isLogging = new bool?(true);
      }
      bool? isLogging1 = this.isLogging;
      return isLogging1.HasValue ? isLogging1.GetValueOrDefault() : this.IsLoggingDefaultValue;
    }

    public async Task<bool> IsAnalyticsEnabledAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.isAnalyticsEnabled.HasValue)
      {
        bool? analyticsEnabled = this.isAnalyticsEnabled;
        return analyticsEnabled.HasValue ? analyticsEnabled.GetValueOrDefault() : this.IsAnalyticsEnabledDefaultValue;
      }
      using (await this.asyncLock.LockAsync())
      {
        if (this.isAnalyticsEnabled.HasValue)
        {
          bool? analyticsEnabled = this.isAnalyticsEnabled;
          return analyticsEnabled.HasValue ? analyticsEnabled.GetValueOrDefault() : this.IsAnalyticsEnabledDefaultValue;
        }
        TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsAnalyticsEnabledValueName);
        this.isAnalyticsEnabled = new bool?(tryGetResult.IsFound ? tryGetResult.Value.IsEqualsIgnoreCase("true") : this.IsAnalyticsEnabledDefaultValue);
      }
      bool? analyticsEnabled1 = this.isAnalyticsEnabled;
      return analyticsEnabled1.HasValue ? analyticsEnabled1.GetValueOrDefault() : this.IsAnalyticsEnabledDefaultValue;
    }

    public async Task<bool> IsUpdatesEnabledAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.isUpdatesEnabled.HasValue)
      {
        bool? isUpdatesEnabled = this.isUpdatesEnabled;
        return isUpdatesEnabled.HasValue ? isUpdatesEnabled.GetValueOrDefault() : this.IsUpdatesEnabledDefaultValue;
      }
      using (await this.asyncLock.LockAsync())
      {
        if (this.isUpdatesEnabled.HasValue)
        {
          bool? isUpdatesEnabled = this.isUpdatesEnabled;
          return isUpdatesEnabled.HasValue ? isUpdatesEnabled.GetValueOrDefault() : this.IsUpdatesEnabledDefaultValue;
        }
        TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.IsUpdatesEnabledValueName);
        this.isUpdatesEnabled = new bool?(tryGetResult.IsFound ? tryGetResult.Value.IsEqualsIgnoreCase("true") : this.IsUpdatesEnabledDefaultValue);
      }
      bool? isUpdatesEnabled1 = this.isUpdatesEnabled;
      return isUpdatesEnabled1.HasValue ? isUpdatesEnabled1.GetValueOrDefault() : this.IsUpdatesEnabledDefaultValue;
    }

    public async Task<bool> IsShowMaxBadVersionWarningDisabledAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this.isShowMaxBadVersionWarningDisabled.HasValue)
      {
        using (await this.asyncLock.LockAsync())
        {
          if (this.isShowMaxBadVersionWarningDisabled.HasValue)
          {
            bool? versionWarningDisabled = this.isShowMaxBadVersionWarningDisabled;
            return versionWarningDisabled.HasValue ? versionWarningDisabled.GetValueOrDefault() : this.isShowMaxBadVersionWarningDisabledDefaultValue;
          }
          TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.isShowMaxBadVersionWarningDisabledValueName);
          this.isShowMaxBadVersionWarningDisabled = new bool?(tryGetResult.IsFound ? tryGetResult.Value.IsEqualsIgnoreCase("true") : this.isShowMaxBadVersionWarningDisabledDefaultValue);
        }
      }
      bool? versionWarningDisabled1 = this.isShowMaxBadVersionWarningDisabled;
      return versionWarningDisabled1.HasValue ? versionWarningDisabled1.GetValueOrDefault() : this.isShowMaxBadVersionWarningDisabledDefaultValue;
    }

    public async Task SetShowMaxBadVersionWarningDisabledAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      bool? versionWarningDisabled = this.isShowMaxBadVersionWarningDisabled;
      bool flag1 = true;
      if (versionWarningDisabled.GetValueOrDefault() == flag1 & versionWarningDisabled.HasValue)
        return;
      using (await this.asyncLock.LockAsync())
      {
        versionWarningDisabled = this.isShowMaxBadVersionWarningDisabled;
        bool flag2 = true;
        if (versionWarningDisabled.GetValueOrDefault() == flag2 & versionWarningDisabled.HasValue)
          return;
        this.isShowMaxBadVersionWarningDisabled = new bool?(true);
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.isShowMaxBadVersionWarningDisabledValueName, true.ToString());
      }
    }

    public async Task<bool> IsEmailConfirmedAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this.isEmailConfirmed.HasValue)
      {
        using (await this.asyncLock.LockAsync())
        {
          if (this.isEmailConfirmed.HasValue)
          {
            bool? isEmailConfirmed = this.isEmailConfirmed;
            return isEmailConfirmed.HasValue ? isEmailConfirmed.GetValueOrDefault() : this.isEmailConfirmedDefaultValue;
          }
          TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.isEmailConfirmedValueName);
          this.isEmailConfirmed = new bool?(tryGetResult.IsFound ? tryGetResult.Value.IsEqualsIgnoreCase("true") : this.isEmailConfirmedDefaultValue);
        }
      }
      bool? isEmailConfirmed1 = this.isEmailConfirmed;
      return isEmailConfirmed1.HasValue ? isEmailConfirmed1.GetValueOrDefault() : this.isEmailConfirmedDefaultValue;
    }

    public async Task SetEmailConfirmedAsync(
      bool confirmed,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      bool? isEmailConfirmed = this.isEmailConfirmed;
      bool flag1 = confirmed;
      if (isEmailConfirmed.GetValueOrDefault() == flag1 & isEmailConfirmed.HasValue)
        return;
      using (await this.asyncLock.LockAsync())
      {
        isEmailConfirmed = this.isEmailConfirmed;
        bool flag2 = confirmed;
        if (isEmailConfirmed.GetValueOrDefault() == flag2 & isEmailConfirmed.HasValue)
          return;
        this.isEmailConfirmed = new bool?(confirmed);
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.isEmailConfirmedValueName, confirmed.ToString());
      }
    }

    public async Task<bool> GetDontShowOnboardingDialogAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this.dontShowOnboardingDialog.HasValue)
      {
        using (await this.asyncLock.LockAsync())
        {
          if (this.dontShowOnboardingDialog.HasValue)
          {
            bool? onboardingDialog = this.dontShowOnboardingDialog;
            return onboardingDialog.HasValue ? onboardingDialog.GetValueOrDefault() : this.dontShowOnboardingDialogDefaultValue;
          }
          TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.dontShowOnboardingDialogValueName);
          this.dontShowOnboardingDialog = new bool?(tryGetResult.IsFound ? tryGetResult.Value.IsEqualsIgnoreCase("true") : this.dontShowOnboardingDialogDefaultValue);
        }
      }
      bool? onboardingDialog1 = this.dontShowOnboardingDialog;
      return onboardingDialog1.HasValue ? onboardingDialog1.GetValueOrDefault() : this.dontShowOnboardingDialogDefaultValue;
    }

    public async Task SetDontShowOnboardingDialogAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      bool? onboardingDialog = this.dontShowOnboardingDialog;
      bool flag1 = true;
      if (onboardingDialog.GetValueOrDefault() == flag1 & onboardingDialog.HasValue)
        return;
      using (await this.asyncLock.LockAsync())
      {
        onboardingDialog = this.dontShowOnboardingDialog;
        bool flag2 = true;
        if (onboardingDialog.GetValueOrDefault() == flag2 & onboardingDialog.HasValue)
          return;
        this.dontShowOnboardingDialog = new bool?(true);
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.dontShowOnboardingDialogValueName, true.ToString());
      }
    }

    public async Task<bool> GetDontShowThanksDialogAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this.dontShowThanksDialog.HasValue)
      {
        using (await this.asyncLock.LockAsync())
        {
          if (this.dontShowThanksDialog.HasValue)
          {
            bool? showThanksDialog = this.dontShowThanksDialog;
            return showThanksDialog.HasValue ? showThanksDialog.GetValueOrDefault() : this.dontShowThanksDialogDefaultValue;
          }
          TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.dontShowThanksDialogValueName);
          this.dontShowThanksDialog = new bool?(tryGetResult.IsFound ? tryGetResult.Value.IsEqualsIgnoreCase("true") : this.dontShowThanksDialogDefaultValue);
        }
      }
      bool? showThanksDialog1 = this.dontShowThanksDialog;
      return showThanksDialog1.HasValue ? showThanksDialog1.GetValueOrDefault() : this.dontShowThanksDialogDefaultValue;
    }

    public async Task SetDontShowThanksDialogAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      bool? showThanksDialog = this.dontShowThanksDialog;
      bool flag1 = true;
      if (showThanksDialog.GetValueOrDefault() == flag1 & showThanksDialog.HasValue)
        return;
      using (await this.asyncLock.LockAsync())
      {
        showThanksDialog = this.dontShowThanksDialog;
        bool flag2 = true;
        if (showThanksDialog.GetValueOrDefault() == flag2 & showThanksDialog.HasValue)
          return;
        this.dontShowThanksDialog = new bool?(true);
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.dontShowThanksDialogValueName, true.ToString());
      }
    }

    public async Task SetThanksDialogShowedCountAsync(
      uint count,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
      {
        uint? dialogShowedCount = this.thanksDialogShowedCount;
        uint num = count;
        if ((int) dialogShowedCount.GetValueOrDefault() == (int) num & dialogShowedCount.HasValue)
          return;
        this.thanksDialogShowedCount = new uint?(count);
        this.registryProvider.TrySetValue<uint>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.thanksDialogShowedCountValueName, count);
      }
    }

    public async Task<uint> GetThanksDialogShowedCountAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this.thanksDialogShowedCount.HasValue)
      {
        using (await this.asyncLock.LockAsync())
        {
          if (!this.thanksDialogShowedCount.HasValue)
          {
            TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.thanksDialogShowedCountValueName);
            uint result;
            this.thanksDialogShowedCount = !tryGetResult.IsFound || !uint.TryParse(tryGetResult.Value, out result) ? new uint?(0U) : new uint?(result);
          }
        }
      }
      return this.thanksDialogShowedCount.GetValueOrDefault();
    }

    public async Task<bool> IsLicenseAcceptedAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this.isLicenseAccepted.HasValue)
      {
        using (await this.asyncLock.LockAsync())
        {
          if (this.isEmailConfirmed.HasValue)
            return this.isLicenseAccepted.GetValueOrDefault();
          TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.isLicenseAcceptedValueName);
          this.isLicenseAccepted = new bool?(tryGetResult.IsFound && tryGetResult.Value.IsEqualsIgnoreCase("true"));
        }
      }
      return this.isLicenseAccepted.GetValueOrDefault();
    }

    public async Task SetLicenseAcceptedAsync(
      bool accepted,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      bool? isLicenseAccepted = this.isLicenseAccepted;
      bool flag1 = accepted;
      if (isLicenseAccepted.GetValueOrDefault() == flag1 & isLicenseAccepted.HasValue)
        return;
      using (await this.asyncLock.LockAsync())
      {
        isLicenseAccepted = this.isLicenseAccepted;
        bool flag2 = accepted;
        if (isLicenseAccepted.GetValueOrDefault() == flag2 & isLicenseAccepted.HasValue)
          return;
        this.isLicenseAccepted = new bool?(accepted);
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this.isLicenseAcceptedValueName, accepted.ToString());
      }
    }

    public async Task<string> GetSelectedTabAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this._selectedTab != null)
        return this._selectedTab;
      using (await this.asyncLock.LockAsync())
      {
        if (this._selectedTab != null)
          return this._selectedTab;
        TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this._selectedTabValueName);
        this._selectedTab = tryGetResult.IsFound ? tryGetResult.Value : this._selectedTabDefaultValue;
      }
      return this._selectedTab;
    }

    public async Task SetSelectedTabAsync(string tabId, CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
      {
        if (this._selectedTab == tabId)
          return;
        this._selectedTab = tabId;
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, this._selectedTabValueName, tabId);
      }
    }

    public async Task<string> GetTabLastCategoryAsync(
      string tabName,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
      {
        if (tabName.IsNullOrWhiteSpace())
          return string.Empty;
        TryGetResult<string> tryGetResult = this.registryProvider.TryGetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, tabName + "_last_category");
        return tryGetResult.IsFound ? tryGetResult.Value : string.Empty;
      }
    }

    public async Task SetTabLastCategoryAsync(
      string tabName,
      string categoryId,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      using (await this.asyncLock.LockAsync())
      {
        if (tabName.IsNullOrWhiteSpace() || categoryId.IsNullOrWhiteSpace())
          return;
        this.registryProvider.TrySetValue<string>(Logic.RegistrySettingsProvider.RegistrySettingsProvider.RegistrySettingsKey, tabName + "_last_category", categoryId);
      }
    }
  }
}
