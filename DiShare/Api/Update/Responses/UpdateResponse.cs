// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Update.Responses.UpdateResponse
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

namespace DiShare.Api.Update.Responses
{
  public class UpdateResponse
  {
    public string Version { get; set; }

    public string LastCriticalVersion { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public string Path { get; set; }
  }
}
