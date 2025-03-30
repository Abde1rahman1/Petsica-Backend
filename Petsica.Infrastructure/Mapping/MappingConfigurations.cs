using Mapster;
using Petsica.Shared.Contracts.Users.Response;

namespace Petsica.Infrastructure.Mapping
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddSitterServiceResponse, SitterService>()
                .Map(dest => dest.ServiceID, src => src.ServiceID)
                .Map(des => des.SitterID, src => src.SitterID);
        }

    }
}