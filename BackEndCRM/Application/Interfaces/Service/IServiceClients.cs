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
    public interface IServiceClients
    {
        Task<ClientsResponse> CreateClient(ClientsRequest request);
        Task<List<ClientsResponse>> GetAllClients();
        Task<bool> ClientExist(int id);
    }
}
