// Decompiled with JetBrains decompiler
// Type: Logic.Users.Models.UserInfo
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

namespace DiShare.Logic.Users.Models
{
  public class UserInfo
  {
    public string Email { get; }

    public string Name { get; }

    public string Token { get; }

    public bool? IsRegistered { get; }

    public bool? IsEmailConfirmed { get; }

    public bool? HasMarathonGroups { get; }

    public UserInfo(
      string email,
      string name,
      string token,
      bool? isRegistered,
      bool? isEmailConfirmed,
      bool? hasMarathonGroups)
    {
      this.Email = email;
      this.Name = name;
      this.Token = token;
      this.IsRegistered = isRegistered;
      this.IsEmailConfirmed = isEmailConfirmed;
      this.HasMarathonGroups = hasMarathonGroups;
    }

    public UserInfo(bool isRegistered) => this.IsRegistered = new bool?(isRegistered);
  }
}
