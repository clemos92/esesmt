using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Entities
{
    public class ChecklistItem : BaseEntity<int>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int ChecklistId { get; set; }
        public Checklist Checklist { get; set; }
    }
}
