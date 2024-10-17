using Application.Interfaces.Command;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class InteractionsCommand : IInteractionsCommand
    {
        private readonly CRMDbContext _context;

        public InteractionsCommand(CRMDbContext context)
        {
            _context = context;
        }

        public async Task Insert(Interactions interaction)
        {
            await _context.Interactions.AddAsync(interaction);

            await _context.SaveChangesAsync();
        }
    }
}
