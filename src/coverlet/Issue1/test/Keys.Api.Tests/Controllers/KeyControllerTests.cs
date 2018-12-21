using System;
using System.Threading;
using System.Threading.Tasks;
using Keys.Api.Controllers;
using Keys.Application.Services.Contracts;
using Keys.Data.Entities;
using Keys.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Keys.Api.Tests.Controllers
{
    public class KeyControllerTests
    {
        private readonly Mock<IKeyProvider> _keyProviderMock;
        private readonly KeyController _keyController;
        private readonly Guid testGuid = Guid.NewGuid();

        public KeyControllerTests()
        {
            _keyProviderMock = new Mock<IKeyProvider>();

            _keyController = new KeyController(_keyProviderMock.Object);
        }

        [Theory(DisplayName = "should return ok status when requesting key by uuid and type")]
        [InlineData(KeyType.Enum2)]
        [InlineData(KeyType.Enum1)]
        [InlineData(KeyType.Enum3)]
        public async Task GetByUuidAndTypeAsync(KeyType keyType)
        {
            _keyProviderMock.Setup(kp => kp.GetByUuidAndTypeAsync(testGuid, keyType, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Key());

            var get = await _keyController.Get(testGuid, keyType, new CancellationToken());

            Assert.IsType<OkObjectResult>(get);
        }
    }
}
