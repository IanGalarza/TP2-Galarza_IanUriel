using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request
{
    public class ClientsRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
