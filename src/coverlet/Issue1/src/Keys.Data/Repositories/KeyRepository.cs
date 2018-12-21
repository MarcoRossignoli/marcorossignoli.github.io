using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Keys.Data.Context.Contracts;
using Keys.Data.Entities;
using Keys.Data.Enums;
using Keys.Data.Factories.Contracts;
using Keys.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Keys.Data.Repositories
{
    public class KeyRepository : IKeyRepository
    {
        private readonly IDataContext _context;
        private readonly IKeyFactory _keyFactory;

        public KeyRepository(IDataContext context, IKeyFactory keyFactory)
        {
            _context = context;
            _keyFactory = keyFactory;
        }

        public async Task<Maybe<Key>> FindAsync(Expression<Func<Key, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Keys.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<int> AddAsync(Key key,
            CancellationToken cancellationToken)
        {
            if (key == null)
            {
                throw new ArgumentNullException($"Unable to add a null entity {nameof(key)} to the repository.");
            }

            _context.Keys.Add(key);
            return await SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Key> FindOrCreateAsync(Guid uuid, KeyType type, CancellationToken cancellationToken)
        {
            var keyMaybe = await FindAsync(k => k.Uuid == uuid && k.KeyType == type, cancellationToken);

            if (keyMaybe.HasValue)
            {
                return keyMaybe.Value;
            }

            var key = _keyFactory.Create(uuid, type);

            await AddAsync(key, cancellationToken);

            return key;
        }
    }
}
