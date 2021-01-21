using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiShare.Logic.ApplicationInfo
{
    public interface IApplicationInfoProvider : IApplicationNameProvider, IApplicationVersionProvider
    {
        VersionInfo GetVersionInfo();

        string GetLocation();
    }
}
