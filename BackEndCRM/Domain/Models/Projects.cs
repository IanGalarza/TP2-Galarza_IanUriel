using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Projects
    {
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int CampaignType { get; set; }
        public CampaignTypes CampaignTypes { get; set; }

        public int ClientID { get; set; }
        public Clients Clients { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
      
        public ICollection<Interactions> Interactions { get; set; }
    }
}
