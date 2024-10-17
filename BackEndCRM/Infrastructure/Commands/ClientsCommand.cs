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
    public class ClientsCommand : IClientsCommand
    {
        private readonly CRMDbContext _context;

        public ClientsCommand(CRMDbContext context)
        {
            _context = context;
        }

        public async Task Insert(Clients client)
        {
            await _context.Clients.AddAsync(client);

            await _context.SaveChangesAsync();
        }
    }
}
