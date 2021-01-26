

namespace DiShare.Api.Users.Requests
{
  public class LoginRequest
  {
    public string Email { get; }

    public string Password { get; }

    public LoginRequest(string email, string password)
    {
      this.Email = email;
      this.Password = password;
    }
  }
}
