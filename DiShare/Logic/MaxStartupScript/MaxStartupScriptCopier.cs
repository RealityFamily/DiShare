using DiShare.Infrastructure.Extensions;
using DiShare.Logic.ApplicationInfo;
using DiShare.Logic.MaxVersionChecker;
using System;
using System.IO;

namespace DiShare.Logic.MaxStartupScript
{
    class MaxStartupScriptCopier : IMaxStartupScriptCopier
    {
        private readonly IMaxVersionChecker _maxVersionChecker;
        private readonly string AutodeskFolderName = "Autodesk\\3dsMax";
        private readonly string ScriptsStartupFolderName = "ENU\\scripts\\startup";
        private readonly string ScriptFileName = "3dhamster-loader.ms";
        private readonly string _autodeskAppDataFolder;
        private readonly string _scriptPath;

        public MaxStartupScriptCopier(
            IMaxVersionChecker maxVersionChecker,
            IApplicationInfoProvider applicationInfoProvider)
        {
            this._maxVersionChecker = maxVersionChecker;
            this._autodeskAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.AutodeskFolderName);
            this._scriptPath = Path.Combine(Path.GetDirectoryName(applicationInfoProvider.GetLocation()), "Scripts\\3dhamster-loader.ms");
        }

        public void Execute()
        {
            if (!Directory.Exists(this._autodeskAppDataFolder))
                return;
            foreach (string directory in Directory.GetDirectories(this._autodeskAppDataFolder))
            {
                if (this._maxVersionChecker.IsSupportedVersion(Path.GetFileName(directory)))
                {
                    string str1 = Path.Combine(directory, this.ScriptsStartupFolderName);
                    try
                    {
                        str1.CreateDirectoryIfMissing();
                        string str2 = Path.Combine(str1, this.ScriptFileName);
                        if (!File.Exists(str2))
                            File.Copy(this._scriptPath, str2);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
