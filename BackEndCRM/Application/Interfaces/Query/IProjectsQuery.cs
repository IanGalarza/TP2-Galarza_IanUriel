using Application.DTOs.Response;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface IProjectsQuery
    {
        Task<List<Projects>> GetAllProjects(string? name, int? campaign, int? client, int? offset, int? size);
        Task<Projects> GetByIdProject(Guid id);
        Task<bool> NameExist(string name);
    }
}
