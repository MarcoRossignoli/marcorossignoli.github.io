using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Keys.Data.Entities;
using Keys.Data.Enums;

namespace Keys.Data.Repositories.Contracts
{
    public interface IKeyRepository
    {
        Task<Maybe<Key>> FindAsync(Expression<Func<Key, bool>> predicate, CancellationToken cancellationToken);
        Task<Key> FindOrCreateAsync(Guid uuid, KeyType type, CancellationToken cancellationToken);
        Task<int> AddAsync(Key key, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}