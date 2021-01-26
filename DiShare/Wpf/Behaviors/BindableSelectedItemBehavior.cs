﻿
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace DiShare.Wpf.Behaviors
{
  public class BindableSelectedItemBehavior : Behavior<TreeView>
  {
    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof (SelectedItem), typeof (object), typeof (BindableSelectedItemBehavior), (PropertyMetadata) new UIPropertyMetadata((object) null, new PropertyChangedCallback(BindableSelectedItemBehavior.OnSelectedItemChanged)));

    public object SelectedItem
    {
      get => this.GetValue(BindableSelectedItemBehavior.SelectedItemProperty);
      set => this.SetValue(BindableSelectedItemBehavior.SelectedItemProperty, value);
    }

    private static void OnSelectedItemChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(e.NewValue is TreeViewItem newValue))
        return;
      newValue.SetValue(TreeViewItem.IsSelectedProperty, (object) true);
    }

    protected override void OnAttached()
    {
      base.OnAttached();
      this.AssociatedObject.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(this.OnTreeViewSelectedItemChanged);
    }

    protected override void OnDetaching()
    {
      base.OnDetaching();
      if (this.AssociatedObject == null)
        return;
      this.AssociatedObject.SelectedItemChanged -= new RoutedPropertyChangedEventHandler<object>(this.OnTreeViewSelectedItemChanged);
    }

    private void OnTreeViewSelectedItemChanged(
      object sender,
      RoutedPropertyChangedEventArgs<object> e)
    {
      this.SelectedItem = e.NewValue;
    }
  }
}
