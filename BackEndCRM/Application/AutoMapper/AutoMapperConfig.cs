using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Application.DTOs.Response;
using Application.DTOs.Request;

namespace Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() //Configuracion
        {
            //Clients

            CreateMap<Clients, ClientsRequest>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();

            CreateMap<Clients, ClientsResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();

            //GenericResponse - Users

            CreateMap<CampaignTypes, GenericResponse>().ReverseMap();
            CreateMap<InteractionTypes, GenericResponse>().ReverseMap();
            CreateMap<Domain.Models.TaskStatus, GenericResponse>().ReverseMap();
            CreateMap<Users, UsersResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            //Project

            CreateMap<Projects, ProjectRequest>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.ClientID))
                .ForMember(dest => dest.CampaignType, opt => opt.MapFrom(src => src.CampaignType))
                .ReverseMap();

            CreateMap<Projects, ProjectResponse>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProjectID))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                 .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.StartDate))
                 .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.EndDate))
                 .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Clients))
                 .ForMember(dest => dest.CampaignType, opt => opt.MapFrom(src => src.CampaignTypes));

            CreateMap<Projects, ProjectDetails>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Interactions, opt => opt.MapFrom(src => src.Interactions))
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));

            //Interactions

            CreateMap<Interactions, InteractionsRequest>().ReverseMap();

            CreateMap<Interactions, InteractionsResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InteractionID))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(opt => opt.Date))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(opt => opt.ProjectID))
                .ForMember(dest => dest.InteractionType, opt => opt.MapFrom(opt => opt.InteractionTypes));

               
            //Tasks

            CreateMap<TasksRequest, Tasks>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();


            CreateMap<Tasks, TasksResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TaskID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TaskStatus))
                .ForMember(dest => dest.UserAssigned, opt => opt.MapFrom(src => src.Users))
                .ReverseMap();
        }
    }
}
