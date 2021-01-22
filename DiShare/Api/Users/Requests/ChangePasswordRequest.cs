// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Users.Requests.ChangePasswordRequest
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

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
