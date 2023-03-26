using AutoMapper;

namespace Sat.Recruitment.Test.Infrastructure
{
    public class TestFixture
    {
        public IMapper Mapper { get; private set; }

        public TestFixture()
        {
            Mapper = AutoMapperFactory.Create();
        }
    }
}
