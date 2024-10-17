using Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Request;

namespace Application.Interfaces.Service
{
    public interface IServiceTasks
    {
        Task<TasksResponse> CreateTasks(Guid id, TasksRequest request);
        Task<TasksResponse> UpdateTasks(Guid id, TasksRequest request);
    }
}
