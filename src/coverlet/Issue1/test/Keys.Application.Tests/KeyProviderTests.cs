using System;
using System.Threading;
using System.Threading.Tasks;
using Keys.Application.Services;
using Keys.Data.Enums;
using Keys.Data.Repositories.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;
using Key = Keys.Data.Entities.Key;

namespace Keys.Application.Tests
{
    public class KeyProviderTests
    {
        private readonly Mock<IKeyRepository> _keyRepositoryMock;
        private readonly MemoryCache _memoryCache;
        private readonly Guid _testGuid = Guid.NewGuid();
        private readonly KeyProvider _keyProvider;

        public KeyProviderTests()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());

            _keyRepositoryMock = new Mock<IKeyRepository>();

            _keyProvider = new KeyProvider(_keyRepositoryMock.Object, _memoryCache);
        }

        [Theory(DisplayName = "Should call repository method with correct values")]
        [InlineData(KeyType.Enum1)]
        [InlineData(KeyType.Enum3)]
        [InlineData(KeyType.Enum2)]
        public async Task GetByUuidAndTypeAsync01(KeyType keyType)
        {
            _keyRepositoryMock.Setup(kr =>
                    kr.FindOrCreateAsync(_testGuid, keyType, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Key());

            await _keyProvider.GetByUuidAndTypeAsync(_testGuid, keyType, new CancellationToken());

            _keyRepositoryMock.Verify(
                kr => kr.FindOrCreateAsync(_testGuid, keyType, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory(DisplayName = "Should get key from cache")]
        [InlineData(KeyType.Enum1)]
        [InlineData(KeyType.Enum3)]
        [InlineData(KeyType.Enum2)]
        public async Task GetByUuidAndTypeAsync02(KeyType keyType)
        {
            var cacheKey = $"uuid-{_testGuid.ToString()}-key-{keyType}";
            _memoryCache.Set(cacheKey, new Key());

            await _keyProvider.GetByUuidAndTypeAsync(_testGuid, keyType, new CancellationToken());

            _keyRepositoryMock.Verify(
                kr => kr.FindOrCreateAsync(_testGuid, keyType, It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}