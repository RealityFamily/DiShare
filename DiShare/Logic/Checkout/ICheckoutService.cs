

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
