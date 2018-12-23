using System;
using System.Threading;
using System.Threading.Tasks;
using Keys.Data.Entities;
using Keys.Data.Enums;

namespace Keys.Application.Services.Contracts
{
    public interface IKeyProvider
    {
        Task<Key> GetByUuidAndTypeAsync(Guid uuid, KeyType type, CancellationToken cancellationToken);
    }
}
