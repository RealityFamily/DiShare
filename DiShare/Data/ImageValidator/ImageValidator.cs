
using DiShare.Infrastructure;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DiShare.Data.ImageValidator
{
  public class ImageValidator : IImageValidator
  {
    public async Task<TryResult<bool>> CheckContentForImageAsync(string file)
    {
      if (!File.Exists(file))
        return new TryResult<bool>(false);
      try
      {
        if (new FileInfo(file).Length < 128L)
          return new TryResult<bool>(false);
        using (FileStream fs = File.OpenRead(file))
        {
          if (fs.Length > 4L)
          {
            byte[] buffer = new byte[4];
            if (await fs.ReadAsync(buffer, 0, 4) == 4 && this.IsEqualFirstBytesOfImage(buffer))
            {
              fs.Seek(-2L, SeekOrigin.End);
              return new TryResult<bool>(await fs.ReadAsync(buffer, 0, 2) == 2 && this.IsEqualLastBytesOfImage(buffer));
            }
            buffer = (byte[]) null;
          }
          return new TryResult<bool>(false);
        }
      }
      catch (Exception ex)
      {
        return new TryResult<bool>(ex);
      }
    }

    private bool IsEqualFirstBytesOfImage(byte[] bytes)
    {
      if (bytes[0] == byte.MaxValue && bytes[1] == (byte) 216)
        return true;
      return bytes[0] == (byte) 137 && bytes[1] == (byte) 80 && bytes[2] == (byte) 78 && bytes[3] == (byte) 71;
    }

    private bool IsEqualLastBytesOfImage(byte[] bytes)
    {
      if (bytes[0] == byte.MaxValue && bytes[1] == (byte) 217)
        return true;
      return bytes[0] == (byte) 96 && bytes[1] == (byte) 130;
    }
  }
}
