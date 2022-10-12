using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Entities
{
    public class CompletedChecklist : BaseEntity<int>
    {
        public DateTime CreationDate { get; set; }
        public int ChecklistId { get; set; }

        public Checklist Checklist { get; set; }
        public ICollection<CompletedChecklistItem> CompletedChecklistItems { get; set; }
    }
}
