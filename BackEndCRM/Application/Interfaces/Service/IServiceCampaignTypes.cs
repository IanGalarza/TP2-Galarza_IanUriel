using Application.DTOs.Response;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IServiceCampaignTypes
    {
        Task<List<GenericResponse>> GetAllCampaignTypes();
        Task<bool> CampaignTypeExist(int id);
    }
}
