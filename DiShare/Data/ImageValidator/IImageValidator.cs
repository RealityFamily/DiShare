// Decompiled with JetBrains decompiler
// Type: DiShare.Data.ImageValidator.IImageValidator
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using DiShare.Infrastructure;
using System.Threading.Tasks;

namespace DiShare.Data.ImageValidator
{
  public interface IImageValidator
  {
    Task<TryResult<bool>> CheckContentForImageAsync(string file);
  }
}
