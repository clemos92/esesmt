using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Models
{
    public class CompletedChecklistListDto : BaseDto<int>
    {
        public string ChecklistDescription { get; set; }
        public string ChecklistChecklistTypeName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
