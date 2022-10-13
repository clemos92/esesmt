using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Models
{
    public class CompletedChecklistItemDto : BaseDto<int>
    {
        public bool Status { get; set; }
        public string Observation { get; set; }
        public int CompletedChecklistId { get; set; }
        public int ChecklistItemId { get; set; }
        public string ChecklistItemName { get; set; }
    }
}
