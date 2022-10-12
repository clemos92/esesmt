using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Entities
{
    public class CompletedChecklistItem : BaseEntity<int>
    {
        public bool Status { get; set; }
        public string Observation { get; set; }
        public int CompletedChecklistId { get; set; }
        public int ChecklistItemId { get; set; }

        public CompletedChecklist CompletedChecklist { get; set; }
        public ChecklistItem ChecklistItem { get; set; }
    }
}
