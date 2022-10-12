using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Models
{
    public class ChecklistTypeDto : BaseDto<int>
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
