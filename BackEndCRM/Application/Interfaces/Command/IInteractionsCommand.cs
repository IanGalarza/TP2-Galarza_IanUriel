using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Command
{
    public interface IInteractionsCommand
    {
        Task Insert(Interactions interaction);
    }
}
