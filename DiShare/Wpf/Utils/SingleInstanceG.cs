

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace DiShare.Wpf.Utils
{
  public static class SingleInstance<TApplication> where TApplication : Application, ISingleInstanceApp
  {
    private const string Delimiter = ":";
    private const string ChannelNameSuffix = "SingeInstanceIPCChannel";
    private const string RemoteServiceName = "SingleInstanceApplicationService";
    private const string IpcProtocol = "ipc://";
    private static Mutex singleInstanceMutex;
    private static IpcServerChannel channel;
    private static IList<string> commandLineArgs;

    public static IList<string> CommandLineArgs => SingleInstance<TApplication>.commandLineArgs;

    public static bool InitializeAsFirstInstance(string uniqueName)
    {
      SingleInstance<TApplication>.commandLineArgs = SingleInstance<TApplication>.GetCommandLineArgs(uniqueName);
      string name = uniqueName + Environment.UserName;
      string channelName = name + ":" + "SingeInstanceIPCChannel";
      bool createdNew;
      SingleInstance<TApplication>.singleInstanceMutex = new Mutex(true, name, out createdNew);
      if (createdNew)
        SingleInstance<TApplication>.CreateRemoteService(channelName);
      else
        SingleInstance<TApplication>.SignalFirstInstance(channelName, SingleInstance<TApplication>.commandLineArgs);
      return createdNew;
    }

    public static void Cleanup()
    {
      if (SingleInstance<TApplication>.singleInstanceMutex != null)
      {
        SingleInstance<TApplication>.singleInstanceMutex.Close();
        SingleInstance<TApplication>.singleInstanceMutex = (Mutex) null;
      }
      if (SingleInstance<TApplication>.channel == null)
        return;
      ChannelServices.UnregisterChannel((IChannel) SingleInstance<TApplication>.channel);
      SingleInstance<TApplication>.channel = (IpcServerChannel) null;
    }

    private static IList<string> GetCommandLineArgs(string uniqueApplicationName)
    {
      string[] strArray = (string[]) null;
      if (AppDomain.CurrentDomain.ActivationContext == null)
      {
        strArray = Environment.GetCommandLineArgs();
      }
      else
      {
        string path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), uniqueApplicationName), "cmdline.txt");
        if (File.Exists(path))
        {
          try
          {
            using (TextReader textReader = (TextReader) new StreamReader(path, Encoding.Unicode))
              strArray = NativeMethods.CommandLineToArgvW(textReader.ReadToEnd());
            File.Delete(path);
          }
          catch (IOException ex)
          {
          }
        }
      }
      if (strArray == null)
        strArray = new string[0];
      return (IList<string>) new List<string>((IEnumerable<string>) strArray);
    }

    private static void CreateRemoteService(string channelName)
    {
      SingleInstance<TApplication>.channel = new IpcServerChannel((IDictionary) new Dictionary<string, string>()
      {
        ["name"] = channelName,
        ["portName"] = channelName,
        ["exclusiveAddressUse"] = "false"
      }, (IServerChannelSinkProvider) new BinaryServerFormatterSinkProvider()
      {
        TypeFilterLevel = TypeFilterLevel.Full
      });
      ChannelServices.RegisterChannel((IChannel) SingleInstance<TApplication>.channel, true);
      RemotingServices.Marshal((MarshalByRefObject) new SingleInstance<TApplication>.IPCRemoteService(), "SingleInstanceApplicationService");
    }

    private static void SignalFirstInstance(string channelName, IList<string> args)
    {
      ChannelServices.RegisterChannel((IChannel) new IpcClientChannel(), true);
      ((SingleInstance<TApplication>.IPCRemoteService) RemotingServices.Connect(typeof (SingleInstance<TApplication>.IPCRemoteService), "ipc://" + channelName + "/SingleInstanceApplicationService"))?.InvokeFirstInstance(args);
    }

    private static object ActivateFirstInstanceCallback(object arg)
    {
      SingleInstance<TApplication>.ActivateFirstInstance(arg as IList<string>);
      return (object) null;
    }

    private static void ActivateFirstInstance(IList<string> args)
    {
      if (Application.Current == null)
        return;
      ((TApplication) Application.Current).SignalExternalCommandLineArgs(args);
    }

    private class IPCRemoteService : MarshalByRefObject
    {
      public void InvokeFirstInstance(IList<string> args)
      {
        if (Application.Current == null)
          return;
        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Delegate) new DispatcherOperationCallback(SingleInstance<TApplication>.ActivateFirstInstanceCallback), (object) args);
      }

      public override object InitializeLifetimeService() => (object) null;
    }
  }
}
