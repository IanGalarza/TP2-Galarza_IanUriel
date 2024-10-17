using Application.DTOs.Response;
using Application.Interfaces.Query;
using Application.Interfaces.Service;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class ServiceCampaignTypes : IServiceCampaignTypes
    {
        private readonly ICampaignTypesQuery _query;
        private readonly IMapper _mapper;

        public ServiceCampaignTypes(ICampaignTypesQuery query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }
        public async Task<List<GenericResponse>> GetAllCampaignTypes()
        {
            var list = await _query.GetAllCampaignTypes();

            return _mapper.Map<List<GenericResponse>>(list);
        }
        public async Task<bool> CampaignTypeExist(int id)
        {
            var result = await _query.CampaignTypeExist(id);

            return result;
        }
    }
}
