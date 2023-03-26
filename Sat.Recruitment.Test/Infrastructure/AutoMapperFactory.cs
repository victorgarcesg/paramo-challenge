using AutoMapper;

namespace Sat.Recruitment.Test.Infrastructure
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps("Sat.Recruitment.Application");
            });

            return mappingConfig.CreateMapper();
        }
    }
}
