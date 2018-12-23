using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Keys.Data.Context;
using Keys.Data.Entities;
using Keys.Data.Enums;
using Keys.Data.Factories.Contracts;
using Keys.Data.Repositories;
using Keys.Data.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Keys.Data.Tests
{
    public class KeyRepositoryTests
    {
        private readonly Mock<DataContext> _contextMock;

        private readonly Mock<IKeyFactory> _keyFactoryMock;

        private readonly Guid _testUuid = Guid.NewGuid();

        private readonly Guid _testKid = Guid.NewGuid();

        private readonly KeyRepository _keyRepository;


        public KeyRepositoryTests()
        {
            var contextOptions = new DbContextOptions<DataContext>();

            _contextMock = new Mock<DataContext>(contextOptions);

            _keyFactoryMock = new Mock<IKeyFactory>();

            _keyRepository = new KeyRepository(_contextMock.Object, _keyFactoryMock.Object);
        }


        [Fact(DisplayName = "Should return key with matching properties when key already exists in db")]
        public async Task FindOrCreateAsync01()
        {
            SetupMockDbSet(GetTestKeys(false, KeyType.Enum1));

            var result = await _keyRepository.FindOrCreateAsync(_testUuid, KeyType.Enum1, new CancellationToken());

            Assert.Equal(KeyType.Enum1, result.KeyType);
            Assert.Equal(_testUuid, result.Uuid);
            Assert.Equal(_testKid, result.Kid);
            _keyFactoryMock.Verify(kf => kf.Create(It.IsAny<Guid>(), It.IsAny<KeyType>()), Times.Never);
        }

        [Fact(DisplayName = "Should create and return key with requested type and uuid when key doesn't exist in db")]
        public async Task FindOrCreateAsync02()
        {
            SetupMockDbSet(GetTestKeys(true));

            _keyFactoryMock.Setup(kf => kf.Create(_testUuid, KeyType.Enum1)).Returns(new Key
                {KeyType = KeyType.Enum1, Uuid = _testUuid, Kid = _testKid});

            var result = await _keyRepository.FindOrCreateAsync(_testUuid, KeyType.Enum1, new CancellationToken());

            Assert.Equal(KeyType.Enum1, result.KeyType);
            Assert.Equal(_testUuid, result.Uuid);
            Assert.Equal(_testKid, result.Kid);
            _keyFactoryMock.Verify(kf => kf.Create(_testUuid, KeyType.Enum1), Times.Once);
        }

        [Fact(DisplayName = "Should throw an exception if provided key is null")]
        public async Task AddAsync01()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => _keyRepository.AddAsync(null, new CancellationToken()));
        }

        private IQueryable<Key> GetTestKeys(bool empty, KeyType type = KeyType.Enum1)
        {
            if (empty)
            {
                return Enumerable.Empty<Key>().AsQueryable();
            }
            else
            {
                return new List<Key>()
                {
                    new Key {Kid = _testKid, Uuid = _testUuid, KeyType = type}
                }.AsQueryable();
            }
        }

        private void SetupMockDbSet(IQueryable<Key> testKeys)
        {
            var mockSet = new Mock<DbSet<Key>>();

            mockSet.As<IAsyncEnumerable<Key>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new DbMockingClasses.TestAsyncEnumerator<Key>(testKeys.GetEnumerator()));

            mockSet.As<IQueryable<Key>>()
                .Setup(m => m.Provider)
                .Returns(new DbMockingClasses.TestAsyncQueryProvider<Key>(testKeys.Provider));

            mockSet.As<IQueryable<Key>>().Setup(m => m.Expression).Returns(testKeys.Expression);
            mockSet.As<IQueryable<Key>>().Setup(m => m.ElementType).Returns(testKeys.ElementType);
            mockSet.As<IQueryable<Key>>().Setup(m => m.GetEnumerator()).Returns(() => testKeys.GetEnumerator());

            _contextMock.Setup(c => c.Set<Key>()).Returns(mockSet.Object);
            _contextMock.Setup(c => c.Keys).Returns(mockSet.Object);
        }
    }
}