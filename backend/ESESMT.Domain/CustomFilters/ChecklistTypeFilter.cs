using ESESMT.Infra.Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.CustomFilters
{
    public class ChecklistTypeFilter : BasePaginationFilter
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
