using Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Application.DTOs.Request;

namespace Application.Interfaces.Service
{
    public interface IServiceInteractions
    {
        Task<InteractionsResponse> CreateInteractions(Guid id, InteractionsRequest request);
    }
}
