using AutoMapper;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.Test.Infrastructure
{
    public class TestFixture
    {
        public IMapper Mapper { get; private set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public TestFixture()
        {
            Mapper = AutoMapperFactory.Create();
            UnitOfWork = new MockUnitOfWork();
        }
    }
}
