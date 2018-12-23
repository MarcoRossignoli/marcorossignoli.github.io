using System;
using Keys.Data.Entities;
using Keys.Data.Enums;

namespace Keys.Data.Factories.Contracts
{
    public interface IKeyFactory
    {
        Key Create(Guid uuid, KeyType type);
    }
}
