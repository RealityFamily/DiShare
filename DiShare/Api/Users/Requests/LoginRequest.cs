// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Users.Requests.LoginRequest
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

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
