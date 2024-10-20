﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Interactions
    {
        public Guid InteractionID { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public int InteractionType { get; set; }
        public InteractionTypes InteractionTypes { get; set; }

        public Guid ProjectID { get; set; }
        public Projects Project { get; set; }
    }
}
