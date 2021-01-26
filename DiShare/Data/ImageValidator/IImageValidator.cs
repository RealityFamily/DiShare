
using DiShare.Infrastructure;
using System.Threading.Tasks;

namespace DiShare.Data.ImageValidator
{
  public interface IImageValidator
  {
    Task<TryResult<bool>> CheckContentForImageAsync(string file);
  }
}
