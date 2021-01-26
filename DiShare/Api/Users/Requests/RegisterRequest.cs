

using System.Runtime.Serialization;

namespace DiShare.Api.Users.Requests
{
  [DataContract]
  public class RegisterRequest
  {
    [DataMember]
    public string Name { get; }

    [DataMember]
    public string Email { get; }

    [DataMember]
    public string Password { get; }

    public RegisterRequest(string email, string name, string password)
    {
      this.Email = email;
      this.Name = name;
      this.Password = password;
    }
  }
}
