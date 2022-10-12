using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Entities
{
    public class Checklist : BaseEntity<int>
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int ChecklistTypeId { get; set; }

        public ChecklistType ChecklistType { get; set; }

        public ICollection<ChecklistItem> ChecklistItems { get; set; }
    }
}
