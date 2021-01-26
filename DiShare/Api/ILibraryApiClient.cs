
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
