using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ClientsResponse Client { get; set; }
        public GenericResponse CampaignType { get; set; }
    }
}
