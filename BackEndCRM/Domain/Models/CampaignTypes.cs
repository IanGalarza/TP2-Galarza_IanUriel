﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CampaignTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Projects> Projects { get; set; }
    }
}
