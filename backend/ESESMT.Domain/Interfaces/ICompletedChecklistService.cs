using ESESMT.Domain.CustomFilters;
using ESESMT.Domain.Models;
using ESESMT.Infra.Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Interfaces
{
    public interface ICompletedChecklistService
    {
        void Save(CompletedChecklistRegisterDto obj);
        CompletedChecklistRegisterDto GetDetails(int id);
        PagedResponse<List<CompletedChecklistListDto>> GetPagedByFilter(CompletedChecklistFilter filter);
    }
}
