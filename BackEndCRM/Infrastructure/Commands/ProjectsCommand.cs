using Application.Interfaces.Command;
using Domain.Models;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class ProjectsCommand : IProjectsCommand
    {
        private readonly CRMDbContext _context;

        public ProjectsCommand(CRMDbContext context)
        {
            _context = context;
        }

        public async Task Insert(Projects project)
        {
            await _context.Projects.AddAsync(project);

            await _context.SaveChangesAsync();
        }
        public async Task Update(Projects project)
        {
            _context.Projects.Update(project);

            await _context.SaveChangesAsync();
        }
    }
}
