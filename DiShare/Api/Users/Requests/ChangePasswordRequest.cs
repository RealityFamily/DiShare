

using System.Runtime.Serialization;

namespace DiShare.Api.Users.Requests
{
  [DataContract]
  public class ChangePasswordRequest
  {
    [DataMember]
    public string Email { get; }

    [DataMember]
    public string OldPassword { get; }

    [DataMember]
    public string Password { get; }

    public ChangePasswordRequest(string email, string oldPassword, string password)
    {
      this.Email = email;
      this.OldPassword = oldPassword;
      this.Password = password;
    }
  }
}
