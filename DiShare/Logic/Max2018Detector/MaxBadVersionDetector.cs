using System.IO;
using DiShare.Infrastructure;
using DiShare.OS;
using Microsoft.Win32;

namespace DiShare.Logic.Max2018Detector
{
    class MaxBadVersionDetector
    {
        private readonly string MaxHKLMKeys = "SOFTWARE\\Autodesk\\3dsMax\\20.0";
        private readonly string MaxServicePacksHKLMKeys = "SOFTWARE\\Autodesk\\Autodesk 3ds Max 2018\\2018\\Service Packs";
        private readonly string MaxLocationKeyName = "Location";
        private readonly string MaxPatchTitleKeyName = "PatchTitle";
        private readonly IRegistryProvider registryProvider;

        public MaxBadVersionDetector(IRegistryProvider registryProvider) => this.registryProvider = registryProvider;

        public TryResult<bool> Detect()
        {
            bool flag = false;
            TryGetResult<string> tryGetResult1 = this.registryProvider.TryGetValue<string>(RegistryHive.LocalMachine, this.MaxHKLMKeys, this.MaxLocationKeyName, RegistryView.Registry64);
            if (tryGetResult1.IsFound && File.Exists(Path.Combine(tryGetResult1.Value, "3dsmax.exe")))
            {
                TryGetResult<string> tryGetResult2 = this.registryProvider.TryGetValue<string>(RegistryHive.LocalMachine, this.MaxServicePacksHKLMKeys, this.MaxPatchTitleKeyName, RegistryView.Registry64);
                flag = !tryGetResult2.IsFound || tryGetResult2.Value.EndsWith("Update1");
            }
            return new TryResult<bool>(flag);
        }
    }
}
