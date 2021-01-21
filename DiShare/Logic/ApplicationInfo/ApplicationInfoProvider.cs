using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiShare.Logic.ApplicationInfo
{
    public class ApplicationInfoProvider :
        IApplicationInfoProvider,
        IApplicationNameProvider,
        IApplicationVersionProvider
    {
        private readonly IEntryAssemblyProvider _entryAssemblyProvider;
        private VersionInfo version;

        public ApplicationInfoProvider(IEntryAssemblyProvider entryAssemblyProvider) => this._entryAssemblyProvider = entryAssemblyProvider;

        public VersionInfo GetVersionInfo()
        {
            if (this.version == (VersionInfo)null)
            {
                Version version = this._entryAssemblyProvider.Get().GetName().Version;
                this.version = new VersionInfo(version.Major, version.Minor, version.Build, version.Revision);
            }
            return this.version;
        }

        public string GetLocation() => this._entryAssemblyProvider.Get().Location;

        public string GetName() => ConfigurationTokens.ApplicationName;

        public string GetVersion() => this.GetVersionInfo().ToString();
    }
}
