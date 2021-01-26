

namespace DiShare.Logic.LocalParameters
{
  public class LocalParameters : ILocalParameters
  {
    public bool IsNeedUpdateInternalPack { get; private set; }

    public void SetNeedUpdateInternalPack() => this.IsNeedUpdateInternalPack = true;
  }
}
