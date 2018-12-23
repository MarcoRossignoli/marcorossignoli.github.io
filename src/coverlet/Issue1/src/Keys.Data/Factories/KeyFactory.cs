using System;
using System.Security.Cryptography;
using Keys.Data.Entities;
using Keys.Data.Enums;
using Keys.Data.Factories.Contracts;

namespace Keys.Data.Factories
{
    public class KeyFactory : IKeyFactory
    {
        public Key Create(Guid uuid, KeyType type)
        {
            return new Key
            {
                Uuid = uuid,
                ContentKey = GetRandomString(),
                Kid = Guid.NewGuid(),
                KeyType = type
            };
        }

        private string GetRandomString()
        {
            var bytes = new byte[16];

            using (var rng =
                new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return Convert.ToBase64String(bytes);
        }
    }
}
