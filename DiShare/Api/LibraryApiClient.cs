// Decompiled with JetBrains decompiler
// Type: DiShare.Api.LibraryApiClient
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
using DiShare.Common;
using System.Net;
using System.Net.Http;
using Tiny.RestClient;

namespace DiShare.Api
{
  public class LibraryApiClient : ILibraryApiClient
  {
    public IUpdateApi UpdateApi { get; }

    public IUsersApi UsersApi { get; }

    public IEventsApi EventsApi { get; }

    public IModelsApi ModelsApi { get; }

    public ICategoriesApi CategoriesApi { get; }

    public IItemsApi ItemsApi { get; }

    public ISubscriptionsApi SubscriptionsApi { get; }

    public IVendorsApi VendorsApi { get; }

    public string BaseAddress { get; private set; }

    public LibraryApiClient()
      : this(new HttpClient(), ConfigurationTokens.BaseUrl)
    {
    }

    public LibraryApiClient(HttpClient httpClient, string baseUrl)
    {
      ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
      this.BaseAddress = baseUrl;
      TinyRestClient client = new TinyRestClient(httpClient, this.BaseAddress);
      this.UpdateApi = (IUpdateApi) new Api.Update.UpdateApi(client);
      this.UsersApi = (IUsersApi) new Api.Users.UsersApi(client);
      this.EventsApi = (IEventsApi) new Api.Events.EventsApi(client);
      this.ModelsApi = (IModelsApi) new Api.Models.ModelsApi(client);
      this.ItemsApi = (IItemsApi) new Api.Items.ItemsApi(client);
      this.CategoriesApi = (ICategoriesApi) new Api.Categories.CategoriesApi(client);
      this.SubscriptionsApi = (ISubscriptionsApi) new Api.Subscriptions.SubscriptionsApi(client);
      this.VendorsApi = (IVendorsApi) new Api.Vendors.VendorsApi(client);
    }
  }
}
