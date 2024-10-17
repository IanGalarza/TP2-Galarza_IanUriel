﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface IInteractionsQuery
    {
        Task<Interactions> GetByIdInteractions(Guid id);
    }
}
