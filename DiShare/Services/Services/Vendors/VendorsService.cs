// Decompiled with JetBrains decompiler
// Type: DiShare.Services.Vendors.VendorsService
// Assembly: DiShare.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA4DCED3-3E6C-408B-8B3E-AE7715592923
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Services.dll

using DiShare.Api.Base;
using DiShare.Api.Vendors;
using DiShare.Api.Vendors.Responses;
using DiShare.Domain.DTO;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Network;
using DiShare.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Services.Vendors
{
  public class VendorsService : IVendorsService
  {
    private readonly IVendorsApi _vendorsApi;
    private readonly INetworkChecker _networkChecker;

    public VendorsService(IVendorsApi vendorsApi, INetworkChecker networkChecker)
    {
      this._vendorsApi = vendorsApi;
      this._networkChecker = networkChecker;
    }

    public async Task<TryResult<IReadOnlyCollection<VendorDto>>> GetAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this._networkChecker.CheckIsConnected())
        return new TryResult<IReadOnlyCollection<VendorDto>>((Exception) new VendorsServiceException("Network connection error."));
      try
      {
        Response<IReadOnlyCollection<VendorResponse>> response = await this._vendorsApi.GetVendorsAsync(cancellationToken).ConfigureAwait(false);
        return new TryResult<IReadOnlyCollection<VendorDto>>();
        /*return response.Status == ResponseStatus.Error || !response.Error.IsNullOrEmpty() ? new TryResult<IReadOnlyCollection<VendorDto>>((Exception) new VendorsServiceException(string.Format("Retrieve categories error: [{0}] {1}", (object) response.Status, (object) response.Error))) : (TryResult<IReadOnlyCollection<VendorDto>>) (IReadOnlyCollection<VendorDto>) response.Result.Select<VendorResponse, VendorDto>((Func<VendorResponse, VendorDto>) (i => new VendorDto()
        {
          Id = i.Id,
          Name = i.Name,
          Url = i.Url
        })).Distinct<VendorDto>().ToArray<VendorDto>();*/
      }
      catch (Exception ex)
      {
        return new TryResult<IReadOnlyCollection<VendorDto>>((Exception) new VendorsServiceException("Can't retrieve categories from server", ex));
      }
    }
  }
}
