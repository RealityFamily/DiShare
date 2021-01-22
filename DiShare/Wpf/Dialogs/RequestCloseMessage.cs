// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Dialogs.RequestCloseMessage
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace DiShare.Wpf.Dialogs
{
  public class RequestCloseMessage : MessageBase
  {
    public ViewModelBase ViewModel { get; }

    public bool? DialogResult { get; }

    public RequestCloseMessage(ViewModelBase viewModel, bool? dialogResult = null)
    {
      this.ViewModel = viewModel;
      this.DialogResult = dialogResult;
    }
  }
}
