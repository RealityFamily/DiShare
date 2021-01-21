// Decompiled with JetBrains decompiler
// Type: Logic.Updater.Models.UpdateInfo
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

namespace DiShare.Logic.Updater.Models
{
  public class UpdateInfo
  {
    public VersionInfo Version { get; set; }

    public VersionInfo LastCriticalVersion { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public string Path { get; set; }

    public UpdateInfo(
      VersionInfo version,
      VersionInfo lastCriticalVersion,
      string description,
      string image,
      string path)
    {
      this.Version = version;
      this.LastCriticalVersion = lastCriticalVersion;
      this.Description = description;
      this.Image = image;
      this.Path = path;
    }
  }
}
