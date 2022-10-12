using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Models
{
    public class ChecklistItemDto : BaseDto<int>
    {
        public string Name { get; set; }
        public int? ChecklistId { get; set; }
        public bool? IsActive { get; set; }
    }
}
