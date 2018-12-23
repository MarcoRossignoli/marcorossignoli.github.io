using System;
using Keys.Data.Enums;
using Keys.Data.Factories;
using Xunit;

namespace Keys.Data.Tests
{
    public class KeyFactoryTests
    {
        private readonly KeyFactory _keyFactory;

        public KeyFactoryTests()
        {
            _keyFactory = new KeyFactory();
        }

        [Fact(DisplayName = "Create should return ContentKeys with correct length")]
        public void Generate_Should_Return_ContentKeys_With_Correct_Length()
        {
            var testuuid = Guid.NewGuid();

            var type = KeyType.Enum1;

            var result = _keyFactory.Create(testuuid, type);

            Assert.Equal(24, result.ContentKey.Length);
        }
    }
}
