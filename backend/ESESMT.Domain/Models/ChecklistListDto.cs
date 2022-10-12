using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Models
{
    public class ChecklistListDto : BaseDto<int>
    {
        public string Description { get; set; }
        public string ChecklistTypeName { get; set; }
        public bool? IsActive { get; set; }
    }
}
