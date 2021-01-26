

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
