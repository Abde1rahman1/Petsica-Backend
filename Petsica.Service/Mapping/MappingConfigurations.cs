using Petsica.Core.Entities.Pets;
using Petsica.Core.Entities.Services;
using Petsica.Shared.Contracts.Pets.Response;
using Petsica.Shared.Contracts.Users.Response;

namespace Petsica.Service.Mapping
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddSitterServiceResponse, SitterService>()
                .Map(dest => dest.ServiceID, src => src.ServiceID)
                .Map(des => des.SitterID, src => src.SitterID);

            config.NewConfig<PetsResponse, Pet>()
                .Map(des => des.PetID, src => src.PetID);
        }

    }
}