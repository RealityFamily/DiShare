

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
