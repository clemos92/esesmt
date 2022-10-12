using ESESMT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Interfaces
{
    public interface IChecklistTypeRepository : IBaseRepository<ChecklistType, int>
    {
        List<ChecklistType> GetAllActive();
    }
}