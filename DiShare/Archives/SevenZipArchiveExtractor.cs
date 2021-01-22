// Decompiled with JetBrains decompiler
// Type: DiShare.Archives.SevenZipArchiveExtractor
// Assembly: DiShare.Archives, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A03294E2-42F4-4728-80C5-334A63763BEF
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Archives.dll

using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using SevenZipExtractor;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Archives
{
  public class SevenZipArchiveExtractor : IArchiveExtractor
  {
    public Task<TryResult<bool>> ExtractAsync(
      string archive,
      string folder,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      TaskCompletionSource<TryResult<bool>> tcs = new TaskCompletionSource<TryResult<bool>>();
      try
      {
        Task.Run((Action) (() => this.ExtractArchive(archive, folder, tcs, progressChangedCallback)));
      }
      catch (Exception ex)
      {
        tcs.SetResult(new TryResult<bool>(ex));
      }
      return tcs.Task;
    }

    private void ExtractArchive(
      string archive,
      string path,
      TaskCompletionSource<TryResult<bool>> tcs,
      Action<ProgressChangedEventArgs> progressChangedCallback)
    {
      using (ArchiveFile archiveFile = new ArchiveFile(archive))
      {
        int count = archiveFile.Entries.Count;
        int index = 0;
        try
        {
          archiveFile.Extract((Func<Entry, string>) (entry =>
          {
            Action<ProgressChangedEventArgs> action = progressChangedCallback;
            if (action != null)
              action(new ProgressChangedEventArgs(++index * 100 / count, (object) null));
            string path1 = Path.Combine(path, entry.FileName);
            if (!entry.IsFolder && File.Exists(path1))
            {
              FileInfo fileInfo = new FileInfo(Path.Combine(path, entry.FileName));
              if ((long) entry.Size == fileInfo.Length)
              {
                path1 = (string) null;
              }
              else
              {
                try
                {
                  path1.RemoveFile();
                }
                catch
                {
                  return (string) null;
                }
              }
            }
            return path1;
          }));
        }
        catch (Exception ex)
        {
          tcs.SetResult(new TryResult<bool>(ex));
          return;
        }
      }
      tcs.SetResult(new TryResult<bool>(true));
    }
  }
}
