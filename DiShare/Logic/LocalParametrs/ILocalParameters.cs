

namespace DiShare.Logic.LocalParameters
{
  public interface ILocalParameters
  {
    bool IsNeedUpdateInternalPack { get; }

    void SetNeedUpdateInternalPack();
  }
}
