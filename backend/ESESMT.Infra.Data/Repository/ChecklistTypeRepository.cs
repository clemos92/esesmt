using ESESMT.Domain.Entities;
using ESESMT.Domain.Interfaces;
using ESESMT.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESESMT.Infra.Data.Repository
{
    public class ChecklistTypeRepository : BaseRepository<ChecklistType, int>, IChecklistTypeRepository
    {
        public ChecklistTypeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public List<ChecklistType> GetAllActive()
        {
            return _dbContext.ChecklistTypes.AsNoTracking().Where(e=>e.IsActive).ToList();
        }
    }
}
