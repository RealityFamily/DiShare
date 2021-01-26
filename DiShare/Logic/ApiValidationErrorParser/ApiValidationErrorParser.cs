
using DiShare.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiShare.Logic.ApiValidationErrorParser
{
  public class ApiValidationErrorParser : IApiValidationErrorParser
  {
    public TryResult<string> ParseValidationResponse(string content)
    {
      try
      {
        JObject jobject = JObject.Parse(content);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
          stringBuilder.AppendLine(string.Format("{0}: {1}", (object) keyValuePair.Key, (object) keyValuePair.Value));
        return new TryResult<string>(stringBuilder.ToString());
      }
      catch (Exception ex)
      {
        return new TryResult<string>(ex);
      }
    }
  }
}
