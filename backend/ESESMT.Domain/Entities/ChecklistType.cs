using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Entities
{
    public class ChecklistType : BaseEntity<int>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Checklist> Checklists { get; set; }
    }
}
