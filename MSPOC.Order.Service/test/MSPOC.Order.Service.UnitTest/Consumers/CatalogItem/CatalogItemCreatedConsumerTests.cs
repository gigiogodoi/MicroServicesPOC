using System.Threading.Tasks;
using Bogus;
using MSPOC.Events.Catalog;
using MSPOC.Order.Service.Consumers;
using MSPOC.Order.Service.Entities;
using MSPOC.Order.Service.UnitTest.Fixtures;
using NSubstitute;
using Xunit;

namespace MSPOC.Order.Service.UnitTest.Consumers
{
    public class CatalogItemCreatedConsumerTests 
        : CatalogItemConsumerTestsBase<CatalogItemCreated>, IClassFixture<CatalogItemConsumerFixture>
    {
        #pragma warning disable CS4014
        private readonly CatalogItemCreatedConsumer _sut;
        private readonly CatalogItemConsumerFixture _fixture;

        public CatalogItemCreatedConsumerTests(CatalogItemConsumerFixture fixture)
        {
            _sut = new CatalogItemCreatedConsumer(_repositoryMock, _mapperMock);
            _fixture = fixture;

            _consumeContextMock.Message.Returns(_fixture.NewCatalogItemCreated());
        }

        [Fact]
        public async Task Consome_CatalogItemNotExist_InsertDatabase()
        {
            // Arrange
            CatalogItem itemNotExist = null;
            _repositoryMock.GetAsync(default).ReturnsForAnyArgs(itemNotExist);
        
            // Act
            await _sut.Consume(_consumeContextMock);
        
            // Assert
            _repositoryMock.ReceivedWithAnyArgs(1).CreateAsync(default);
        }
        
        [Fact]
        public async Task Consome_CatalogItemExist_NotInsertDatabase()
        {
            // Arrange
            var itemExist = _fixture.NewCatalogItem();
            _repositoryMock.GetAsync(itemExist.Id).ReturnsForAnyArgs(itemExist);
        
            // Act
            await _sut.Consume(_consumeContextMock);
        
            // Assert
            _repositoryMock.ReceivedWithAnyArgs(0).CreateAsync(default);
        }

        #pragma warning restore CS4014
    }
}