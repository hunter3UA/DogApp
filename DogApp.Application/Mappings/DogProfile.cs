using AutoMapper;
using DogApp.Application.Commands.Dog;
using DogApp.Application.Dtos.Dog;
using DogApp.Domain.DbEntities;

namespace DogApp.Application.Mappings
{
    public class DogProfile : Profile
    {
        public DogProfile()
        {
            CreateMap<AddDogDto, AddDogCommand>();
            CreateMap<AddDogCommand, DbDog>();
        }
    }
}
