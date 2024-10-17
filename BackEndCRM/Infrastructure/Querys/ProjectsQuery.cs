using Application.Interfaces.Query;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class ProjectsQuery : IProjectsQuery
    {
        private readonly CRMDbContext _context;

        public ProjectsQuery(CRMDbContext context)
        {
            _context = context;
        }

        public async Task<List<Projects>> GetAllProjects(string? name, int? campaign, int? client, int? offset, int? size)
        {
            var query = _context.Projects.AsQueryable();

            if (!string.IsNullOrEmpty(name) )
            {
                query = query.Where(p => p.ProjectName.Contains(name));
            }

            if (campaign.HasValue)
            {
                query = query.Where(p => p.CampaignType == campaign);
            }

            if (client.HasValue)
            {
                query = query.Where(p => p.ClientID == client);
            }

            if (offset.HasValue)
            {
                query = query.Skip(offset.Value);
            }

            // Realizar el take unicamente si el valor es mayor a 0.

            if (size.HasValue)
            {
                if (size.Value > 0)
                {
                    query = query.Take(size.Value);
                }
                else
                {
                    return new List<Projects>();
                }
            }

            query = query.Include(p => p.CampaignTypes)
                .Include(p => p.Clients)
                .OrderBy(p => p.ProjectID);

            return await query.ToListAsync();
        }

        public async Task<Projects> GetByIdProject(Guid id)
        {
            var project = await _context.Projects
                .Include(p => p.Clients)
                .Include(p => p.CampaignTypes)
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.Users)
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.TaskStatus)
                .Include(p => p.Interactions)
                    .ThenInclude(i => i.InteractionTypes)
                .FirstOrDefaultAsync(p => p.ProjectID == id);

            return project;
        }

        public async Task<bool> NameExist(string name)
        {
            var resultado = await _context.Projects
                .AnyAsync(p => p.ProjectName.ToLower().Equals(name.ToLower()));

            return resultado;
        }
    }
}
