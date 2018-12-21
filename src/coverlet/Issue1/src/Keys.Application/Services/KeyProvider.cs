using System;
using System.Threading;
using System.Threading.Tasks;
using Keys.Application.Services.Contracts;
using Keys.Data.Entities;
using Keys.Data.Enums;
using Keys.Data.Repositories.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace Keys.Application.Services
{
    public class KeyProvider : IKeyProvider
    {
        private readonly IKeyRepository _repository;

        public KeyProvider(IKeyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Key> GetByUuidAndTypeAsync(Guid uuid, KeyType type, CancellationToken cancellationToken)
        {
            return await _repository.FindOrCreateAsync(uuid, type, cancellationToken);
        }
    }
}