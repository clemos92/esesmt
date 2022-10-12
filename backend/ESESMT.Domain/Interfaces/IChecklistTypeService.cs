using ESESMT.Domain.CustomFilters;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Models;
using ESESMT.Infra.Shared.Common;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Interfaces
{
    public interface IChecklistTypeService
    {
        void Save(ChecklistTypeDto obj);
        void Update(ChecklistTypeDto obj);
        ChecklistTypeDto GetById(int id);
        PagedResponse<List<ChecklistTypeDto>> GetPagedByFilter(ChecklistTypeFilter filter);
        List<DropdownDefaultModel> GetAllActive();
    }
}
