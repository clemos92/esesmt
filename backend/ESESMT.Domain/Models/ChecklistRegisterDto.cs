using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Models
{
    public class ChecklistRegisterDto : BaseDto<int>
    {
        public string Description { get; set; }
        public int? ChecklistTypeId { get; set; }
        public bool? IsActive { get; set; }
        public ICollection<ChecklistItemDto> ChecklistItems { get; set; }
    }
}
