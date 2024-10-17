using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Clients
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Projects> Projects { get; set; }
    }
}
