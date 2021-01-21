// Decompiled with JetBrains decompiler
// Type: Logic.DownloadQueue.IDownloadTaskQueue
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Domain.Models;
using DiShare.Logic.DownloadQueue.Enums;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DiShare.Logic.DownloadQueue
{
  public interface IDownloadTaskQueue
  {
    event EventHandler<DownloadQueueEventArgs> DownloadQueueChanged;

    event EventHandler<DownloadQueueStateEventArgs> DownloadQueueStateChanged;

    ObservableCollection<ModelItem> Queue { get; }

    DownloadQueueState State { get; }

    Task AddTaskAsync(ModelItem item);

    ModelItem GetQueuedItem(ModelItem item);

    Task Init();

    void Start();

    void StopAll();

    bool IsNeedToDownload();
  }
}
