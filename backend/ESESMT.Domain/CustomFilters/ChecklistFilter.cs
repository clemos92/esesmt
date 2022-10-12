﻿using ESESMT.Infra.Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.CustomFilters
{
    public class ChecklistFilter : BasePaginationFilter
    {
        public DateTime? CreationDate { get; set; }
        public int? ChecklistTypeId { get; set; }
    }
}
