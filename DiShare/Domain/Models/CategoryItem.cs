

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DiShare.Domain.Models
{
  public class CategoryItem : BaseItem, INotifyPropertyChanged
  {
    private bool isSelected;
    private bool isExpanded;

    public ObservableCollection<CategoryItem> Categories { get; set; } = new ObservableCollection<CategoryItem>();

    public ObservableCollection<ModelItem> Models { get; set; } = new ObservableCollection<ModelItem>();

    public bool IsSelected
    {
      get => this.isSelected;
      set
      {
        if (value == this.isSelected)
          return;
        this.isSelected = value;
        this.NotifyPropertyChanged(nameof (IsSelected));
      }
    }

    public bool IsExpanded
    {
      get => this.isExpanded;
      set
      {
        if (value == this.isExpanded)
          return;
        this.isExpanded = value;
        this.NotifyPropertyChanged(nameof (IsExpanded));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void NotifyPropertyChanged(string propName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propName));
    }

    public CategoryItem CloneWithoutModels()
    {
      ObservableCollection<CategoryItem> observableCollection = new ObservableCollection<CategoryItem>(this.Categories.Select<CategoryItem, CategoryItem>((Func<CategoryItem, CategoryItem>) (w => w.CloneWithoutModels())));
      CategoryItem categoryItem = new CategoryItem();
      categoryItem.Categories = observableCollection;
      categoryItem.Models = this.Models;
      categoryItem.Id = this.Id;
      categoryItem.Name = this.Name;
      categoryItem.Path = this.Path;
      categoryItem.IsSelected = this.isSelected;
      categoryItem.IsExpanded = this.isExpanded;
      return categoryItem;
    }
  }
}
