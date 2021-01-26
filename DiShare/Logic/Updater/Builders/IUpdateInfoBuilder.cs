

using DiShare.Api.Update.Responses;
using DiShare.Logic.Updater.Models;

namespace DiShare.Logic.Updater.Builders
{
  public interface IUpdateInfoBuilder
  {
    UpdateInfo Build(UpdateResponse update, string baseUrl);
  }
}
