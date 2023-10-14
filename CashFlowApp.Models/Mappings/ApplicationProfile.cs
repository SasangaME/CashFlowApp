using AutoMapper;
using CashFlowApp.Models.DTOs;
using CashFlowApp.Models.Entities;

namespace CashFlowApp.Models.Mappings;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
    }
}