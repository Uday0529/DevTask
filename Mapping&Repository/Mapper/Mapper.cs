using AutoMapper;
using DevTask2.DataAdapters.DBModels;
using DevTask2.Models.TaskModels;
using System.Runtime.InteropServices;
namespace DevTask2.Mapping_Repository.Mapper
{
    public class Mapper : Profile
    {

        public Mapper()
        {
            //Adding task
            CreateMap<Add_TaskModel, TblTask>()
                .ForMember(d => d.UserId, s => s.MapFrom(src => src.UserId))
                .ForMember(d => d.Title, s => s.MapFrom(src => src.Title))
                .ForMember(d => d.Description, s => s.MapFrom(src => src.Description))
                .ReverseMap();

            //Update task
            CreateMap<Update_TaskModel, TblTask>()
                .ForMember(d => d.Id, s => s.MapFrom(s => s.Id))
                .ForMember(d => d.Title, s => { s.Condition(s => s.Title != null); s.MapFrom(src => src.Title); })
                .ForMember(d => d.Description, s =>{ s.Condition(s => s.Description != null); s.MapFrom(src => src.Description); })
                .ForMember(d => d.IsCompleted, s => { s.Condition(s => s.IsCompleted != null); s.MapFrom(src => src.IsCompleted); })
                .ReverseMap();

            //View Task
            CreateMap<TblTask, ViewTaskModel>()
               .ForMember(d => d.Id, s=>s.MapFrom(s => s.Id))  
               .ForMember(d => d.Title, s => s.MapFrom(s => s.Title))
               .ForMember(d => d.Description, s => s.MapFrom(s => s.Description))
               .ForMember(d => d.IsCompleted, s => s.MapFrom(s => s.IsCompleted))
               .ForMember(d => d.CreatedAt, s => s.MapFrom(s => s.CreatedAt))
               .ForMember(d => d.UpdateAt, s => s.MapFrom(s => s.UpdateAt))
               .ForMember(d => d.CompletedAt, s => s.MapFrom(s => s.CompletedAt))
               .ReverseMap();
        }

    }
}
