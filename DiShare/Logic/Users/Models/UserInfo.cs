
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
