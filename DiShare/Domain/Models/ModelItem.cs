

using DiShare.Domain.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DiShare.Domain.Models
{
  public class ModelItem : BaseItem, INotifyPropertyChanged
  {
    private ItemState _state;
    private int _progress;

    public string SmallImage { get; set; }

    public string Image { get; set; }

    public bool ForRegistered { get; set; }

    public ItemType Type { get; set; }

    public string Category { get; set; }

    public string Vendor { get; set; }

    public string Description { get; set; }

    public string Url { get; set; }

    public ItemState State
    {
      get => this._state;
      set
      {
        this._state = value;
        this.OnPropertyChanged(nameof (State));
      }
    }

    public int Progress
    {
      get => this._progress;
      set
      {
        this._progress = value;
        this.OnPropertyChanged(nameof (Progress));
      }
    }

    public string Hash { get; set; }

    public Decimal? Price { get; set; }

    public long Size { get; set; }

    public CancellationTokenSource TokenSource { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
