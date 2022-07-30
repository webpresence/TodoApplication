using AutoMapper;
using ToDoApplication.Data.Entities;
using ToDoApplication.Enums;
using ToDoApplication.Dto;
using Ardalis.SmartEnum.Core;

namespace ToDoApplication
{
    public class MappingProfile :  Profile
    {
        
     public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<ToDoItem, ToDoItemDto>().ForMember(dest => dest.Status, opts => opts.MapFrom(x => x.Status.Value));
            CreateMap<ToDoList, ToDoListDto>().ForMember(dest => dest.Status, opts => opts.MapFrom(x => x.Status.Value));
            CreateMap<ToDoItemDto, ToDoItem>().ForMember(dest => dest.Status, opts => opts.MapFrom(x => ToDoStatus.FromValue(x.Status)));
            CreateMap<ToDoListDto, ToDoList>().ForMember(dest => dest.Status, opts => opts.MapFrom(x => ToDoStatus.FromValue(x.Status)));
        }
    }
}
