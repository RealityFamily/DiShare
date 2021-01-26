

using DiShare.Infrastructure;

namespace DiShare.Logic.ApiValidationErrorParser
{
  public interface IApiValidationErrorParser
  {
    TryResult<string> ParseValidationResponse(string content);
  }
}
