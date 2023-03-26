using AutoMapper;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.Test.Infrastructure
{
    /// <summary>
    /// Represents a test fixture for unit tests.
    /// </summary>
    public class TestFixture
    {
        /// <summary>
        /// Gets the mapper instance used by the test fixture.
        /// </summary>
        public IMapper Mapper { get; private set; }

        /// <summary>
        /// Gets or sets the unit of work instance used by the test fixture.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixture"/> class.
        /// </summary>
        public TestFixture()
        {
            Mapper = AutoMapperFactory.Create();
            UnitOfWork = new MockUnitOfWork();
        }
    }
}
