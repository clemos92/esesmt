using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Models
{
    public class CompletedChecklistRegisterDto : BaseDto<int>
    {
        public int ChecklistId { get; set; }
        public DateTime? CreationDate { get; set; }
        public ICollection<CompletedChecklistItemDto> CompletedChecklistItems { get; set; }
    }
}
