

using DiShare.Api.Update.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.Update
{
  public interface IUpdateApi
  {
    Task<UpdateResponse> GetUpdateInfoAsync(CancellationToken cancellationToken = default (CancellationToken));
  }
}
