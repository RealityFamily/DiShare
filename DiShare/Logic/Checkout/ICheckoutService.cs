// Decompiled with JetBrains decompiler
// Type: Logic.Checkout.ICheckoutService
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Logic.Checkout
{
  public interface ICheckoutService
  {
    Task<TryResult<bool>> CheckoutAsync(
      int tariffId,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
