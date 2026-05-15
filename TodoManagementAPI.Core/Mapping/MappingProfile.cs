namespace TodoManagementAPI.Core.Mapping;

using AutoMapper;
using TodoManagementAPI.Core.DTOs;
using TodoManagementAPI.Core.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoDto>().ReverseMap();
        CreateMap<CreateTodoDto, Todo>();
    }
}
