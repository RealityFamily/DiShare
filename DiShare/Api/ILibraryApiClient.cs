// Decompiled with JetBrains decompiler
// Type: DiShare.Api.ILibraryApiClient
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using DiShare.Api.Categories;
using DiShare.Api.Events;
using DiShare.Api.Items;
using DiShare.Api.Models;
using DiShare.Api.Subscriptions;
using DiShare.Api.Update;
using DiShare.Api.Users;
using DiShare.Api.Vendors;

namespace DiShare.Api
{
  public interface ILibraryApiClient
  {
    IUpdateApi UpdateApi { get; }

    IEventsApi EventsApi { get; }

    IUsersApi UsersApi { get; }

    IModelsApi ModelsApi { get; }

    ICategoriesApi CategoriesApi { get; }

    IItemsApi ItemsApi { get; }

    ISubscriptionsApi SubscriptionsApi { get; }

    IVendorsApi VendorsApi { get; }

    string BaseAddress { get; }
  }
}
