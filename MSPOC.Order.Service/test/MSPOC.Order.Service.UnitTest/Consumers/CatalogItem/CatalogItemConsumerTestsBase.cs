using AutoMapper;
using Bogus;
using MassTransit;
using MSPOC.CrossCutting;
using MSPOC.Order.Service.Entities;
using NSubstitute;

namespace MSPOC.Order.Service.UnitTest.Consumers
{
    public abstract class CatalogItemConsumerTestsBase<TEvent> where TEvent : class
    {
        protected readonly IRepository<CatalogItem> _repositoryMock;
        protected readonly IMapper _mapperMock;
        protected readonly ConsumeContext<TEvent> _consumeContextMock;

        public CatalogItemConsumerTestsBase()
        {
            _repositoryMock     = Substitute.For<IRepository<CatalogItem>>();
            _mapperMock         = Substitute.For<IMapper>();
            _consumeContextMock = Substitute.For<ConsumeContext<TEvent>>();
        }
    }
}