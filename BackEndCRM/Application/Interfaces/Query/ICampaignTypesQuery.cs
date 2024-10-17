using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface ICampaignTypesQuery
    {
        Task<List<CampaignTypes>> GetAllCampaignTypes();
        Task<bool> CampaignTypeExist(int id);
    }
}
