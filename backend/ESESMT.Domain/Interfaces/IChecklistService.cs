using ESESMT.Domain.CustomFilters;
using ESESMT.Domain.Models;
using ESESMT.Infra.Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Interfaces
{
    public interface IChecklistService
    {
        void Save(ChecklistRegisterDto obj);
        void Update(ChecklistRegisterDto obj);
        ChecklistRegisterDto GetDetails(int id);
        PagedResponse<List<ChecklistListDto>> GetPagedByFilter(ChecklistFilter filter);
    }
}
