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
    public class InteractionsQuery : IInteractionsQuery
    {
        private readonly CRMDbContext _context;

        public InteractionsQuery(CRMDbContext context)
        {
            _context = context;
        }

        public async Task<Interactions> GetByIdInteractions(Guid id)
        {
            var interaction = await _context.Interactions
                .Include(i => i.InteractionTypes)
                .FirstOrDefaultAsync(i => i.InteractionID == id);

            return interaction;
        }
    }
}
