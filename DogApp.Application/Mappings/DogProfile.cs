using AutoMapper;
using DogApp.Application.Dtos.Dog;
using DogApp.Application.Requests.Dog;
using DogApp.Domain.DbEntities;
using DogApp.Domain.Entities;

namespace DogApp.Application.Mappings
{
    public class DogProfile : Profile
    {
        public DogProfile()
        {
            CreateMap<AddDogDto, AddDogRequest>();
            CreateMap<AddDogRequest, DbDog>();
            CreateMap<DbDog, DogEntity>();
            CreateMap<DogEntity, DogDto>();
        }
    }
}
