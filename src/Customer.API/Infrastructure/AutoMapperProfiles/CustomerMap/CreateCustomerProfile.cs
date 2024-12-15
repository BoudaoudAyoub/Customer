using AutoMapper;
using CustomerMan.API.Application.Commands.CustomerCommands;
using CustomerMan.API.Application.Models.CustomerModels;
using CustomerMan.Domain.AggregateModels.CustomerAggregate;
namespace CustomerMan.API.Infrastructure.AutoMapperProfiles.CustomerMap;
public class CreateCustomerProfile : Profile
{
    public CreateCustomerProfile()
    {
        CreateMap<BaseCustomerModel, CreateCustomerCommand>()
            .ForMember(des => des.Name, ex => ex.MapFrom(src => src.Name))
            .ForMember(des => des.ContactEmail, ex => ex.MapFrom(src => src.ContactEmail))
            .ForMember(des => des.PhoneNumber, ex => ex.MapFrom(src => src.PhoneNumber))
            .ForMember(des => des.Description, ex => ex.MapFrom(src => src.Description))
            .ForMember(des => des.City, ex => ex.MapFrom(src => src.Adress.City))
            .ForMember(des => des.State, ex => ex.MapFrom(src => src.Adress.State))
            .ForMember(des => des.Street, ex => ex.MapFrom(src => src.Adress.Street))
            .ForMember(des => des.Coutry, ex => ex.MapFrom(src => src.Adress.Country))
            .ForMember(des => des.ZipCode, ex => ex.MapFrom(src => src.Adress.ZipCode));

        CreateMap<AdressModel, Adress>();

        CreateMap<Customer, CustomerModel>()
            .ForMember(des => des.Name, ex => ex.MapFrom(src => src.Name))
            .ForMember(des => des.ContactEmail, ex => ex.MapFrom(src => src.ContactEmail))
            .ForMember(des => des.PhoneNumber, ex => ex.MapFrom(src => src.Phone))
            .ForMember(des => des.Description, ex => ex.MapFrom(src => src.Description))
            .ForMember(des => des.Adress, ex => ex.MapFrom(src => new AdressModel()
            {
                City = src.Adress.City,
                Country = src.Adress.Country,
                State = src.Adress.State,
                Street = src.Adress.Street,
                ZipCode = src.Adress.ZipCode
            }));
    }
}