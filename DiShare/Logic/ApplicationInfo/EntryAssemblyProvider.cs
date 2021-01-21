using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;


namespace DiShare.Logic.ApplicationInfo
{
    [ExcludeFromCodeCoverage]
    public class EntryAssemblyProvider : IEntryAssemblyProvider
    {
        private static readonly object AssemblyLock = new object();
        private _Assembly assembly;

        public _Assembly Get()
        {
            if (this.assembly == null)
            {
                lock (EntryAssemblyProvider.AssemblyLock)
                {
                    if (this.assembly == null)
                        this.assembly = (_Assembly)Assembly.GetEntryAssembly();
                }
            }
            return this.assembly;
        }
    }
}
