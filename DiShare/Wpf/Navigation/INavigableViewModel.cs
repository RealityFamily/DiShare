

namespace DiShare.Wpf.Navigation
{
  public interface INavigableViewModel
  {
    void OnNavigated(object extraData);

    void OnNavigatedBack(object extraData);
  }
}
