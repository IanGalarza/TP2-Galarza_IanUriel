using Application.DTOs.Response;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IServiceInteractionTypes
    {
        Task<List<GenericResponse>> GetAllInteractionTypes();
        Task<bool> InteractionTypeExist(int id);
    }
}
