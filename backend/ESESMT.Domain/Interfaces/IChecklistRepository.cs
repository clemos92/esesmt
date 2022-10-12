using ESESMT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Interfaces
{
    public interface IChecklistRepository : IBaseRepository<Checklist, int>
    {
        Checklist GetDetails(int id);
        List<Checklist> GetAllActive();
        Checklist GetByIdToDropdown(int id);
    }
}
