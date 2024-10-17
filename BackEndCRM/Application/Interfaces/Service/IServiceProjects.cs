using Application.DTOs.Request;
using Application.DTOs.Response;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IServiceProjects
    {
        Task<List<ProjectResponse>> GetAllProjects(string? name, int? campaign, int? client, int? offset, int? size);
        Task<ProjectDetails> CreateProject(ProjectRequest request);
        Task<ProjectDetails> GetByIdProject(Guid id);
        Task<InteractionsResponse> AddNewInteraction(Guid id, InteractionsRequest request);
        Task<TasksResponse> AddNewTask(Guid id, TasksRequest request);
        Task<TasksResponse> UpdateTasks(Guid id, TasksRequest request);
    }
}
