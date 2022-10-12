using ESESMT.Infra.Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.CustomFilters
{
    public class CompletedChecklistFilter : BasePaginationFilter
    {
        public string Description { get; set; }
        public int? ChecklistTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
