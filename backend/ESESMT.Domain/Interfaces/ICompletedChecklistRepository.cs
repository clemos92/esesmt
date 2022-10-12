using ESESMT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Interfaces
{
    public interface ICompletedChecklistRepository : IBaseRepository<CompletedChecklist, int>
    {
        CompletedChecklist GetDetails(int id);
    }
}
