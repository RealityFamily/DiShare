

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace DiShare.Wpf.Utils
{
  [SuppressUnmanagedCodeSecurity]
  internal static class NativeMethods
  {
    [DllImport("shell32.dll", EntryPoint = "CommandLineToArgvW", CharSet = CharSet.Unicode)]
    private static extern IntPtr _CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string cmdLine, out int numArgs);

    [DllImport("kernel32.dll", EntryPoint = "LocalFree", SetLastError = true)]
    private static extern IntPtr _LocalFree(IntPtr hMem);

    public static string[] CommandLineToArgvW(string cmdLine)
    {
      IntPtr num = IntPtr.Zero;
      try
      {
        int numArgs;
        num = NativeMethods._CommandLineToArgvW(cmdLine, out numArgs);
        if (num == IntPtr.Zero)
          throw new Win32Exception();
        string[] strArray = new string[numArgs];
        for (int index = 0; index < numArgs; ++index)
        {
          IntPtr ptr = Marshal.ReadIntPtr(num, index * Marshal.SizeOf(typeof (IntPtr)));
          strArray[index] = Marshal.PtrToStringUni(ptr);
        }
        return strArray;
      }
      finally
      {
        NativeMethods._LocalFree(num);
      }
    }

    public delegate IntPtr MessageHandler(
      WM uMsg,
      IntPtr wParam,
      IntPtr lParam,
      out bool handled);
  }
}
