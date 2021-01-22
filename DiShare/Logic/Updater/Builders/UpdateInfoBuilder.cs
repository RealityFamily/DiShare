// Decompiled with JetBrains decompiler
// Type: Logic.Updater.Builders.UpdateInfoBuilder
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Api.Update.Responses;
using DiShare.Logic.Updater.Models;
using System;

namespace DiShare.Logic.Updater.Builders
{
  public class UpdateInfoBuilder : IUpdateInfoBuilder
  {
    private readonly IVersionInfoBuilder versionInfoBuilder;

    public UpdateInfoBuilder(IVersionInfoBuilder versionInfoBuilder) => this.versionInfoBuilder = versionInfoBuilder;

    public UpdateInfo Build(UpdateResponse update, string baseUrl)
    {
      if (update == null)
        return (UpdateInfo) null;
      if (baseUrl == null)
        baseUrl = "";
      Uri baseUri = new Uri(baseUrl);
      return new UpdateInfo(this.versionInfoBuilder.Build(update.Version), this.versionInfoBuilder.Build(update.LastCriticalVersion), update.Description?.Replace("<br>", "\n") ?? "", new Uri(baseUri, update.Image).ToString(), new Uri(baseUri, update.Path).ToString());
    }
  }
}
